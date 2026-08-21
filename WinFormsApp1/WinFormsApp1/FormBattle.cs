using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.DataAccess;
using WinFormsApp1.Logic;
using WinFormsApp1.Models;

namespace WinFormsApp1
{
    /// <summary>
    /// FormBattle.cs — Gameplay Battle Arena bergaya Anime RPG (DS/GBA Style).
    /// Menggunakan System.Windows.Forms.Timer untuk semua animasi visual & efek audio.
    /// </summary>
    public partial class FormBattle : Form
    {
        private BattleEngine engine = null!;
        private List<string> battleLogEntries = new List<string>();
        private System.Windows.Forms.Timer pulseTurnTimer = null!;
        private bool pulseState = false;

        public FormBattle()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);

            InitPulseTimer();
        }

        private void InitPulseTimer()
        {
            // Timer pulse untuk banner giliran (tiap 500ms)
            pulseTurnTimer = new System.Windows.Forms.Timer { Interval = 500 };
            pulseTurnTimer.Tick += (s, e) =>
            {
                pulseState = !pulseState;
                lblGiliran.Font = new Font("Segoe UI", pulseState ? 12.5F : 12F, FontStyle.Bold);
            };
        }

        private void FormBattle_Load(object sender, EventArgs e)
        {
            if (GameSession.Player1Anomaly == null || GameSession.Player2Anomaly == null)
            {
                MessageBox.Show("Data sesi tidak lengkap!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            // Setup UI labels
            lblP1Name.Text = GameSession.Player1Name;
            lblP1Anomaly.Text = $"{GameSession.Player1Anomaly.Name} ({GameSession.Player1Anomaly.Role})";
            lblP2Name.Text = GameSession.Player2Name;
            lblP2Anomaly.Text = $"{GameSession.Player2Anomaly.Name} ({GameSession.Player2Anomaly.Role})";

            // Tampilkan Item terpasang
            lblP1Items.Text = GameSession.Player1Items.Count > 0
                ? "🎒 " + string.Join(", ", GameSession.Player1Items.Select(i => i.Name))
                : "🎒 Tanpa Item";

            lblP2Items.Text = GameSession.Player2Items.Count > 0
                ? "🎒 " + string.Join(", ", GameSession.Player2Items.Select(i => i.Name))
                : "🎒 Tanpa Item";

            // Load gambar avatar
            Image? imgP1 = AssetHelper.LoadAnomalyImage(GameSession.Player1Anomaly.SpritePath);
            Image? imgP2 = AssetHelper.LoadAnomalyImage(GameSession.Player2Anomaly.SpritePath);
            if (imgP1 != null) picP1.Image = imgP1;
            if (imgP2 != null) picP2.Image = imgP2;

            // Putar BGM Battle
            AudioManager.PlayBGM("bgm_battle.wav");

            // Inisialisasi BattleEngine
            engine = new BattleEngine();
            engine.OnBattleLog += Engine_OnBattleLog;
            engine.OnHPChanged += Engine_OnHPChanged;
            engine.OnTurnChanged += Engine_OnTurnChanged;
            engine.OnBattleEnd += Engine_OnBattleEnd;
            engine.OnPhaseChanged += Engine_OnPhaseChanged;
            engine.OnStunSkipTurn += Engine_OnStunSkipTurn;

            // Load Skills dari Database
            SkillRepository skillRepo = new SkillRepository();
            List<Skill> skills1 = skillRepo.GetSkillsByAnomalyId(GameSession.Player1Anomaly.AnomalyID, GameSession.Player1Anomaly.Role);
            List<Skill> skills2 = skillRepo.GetSkillsByAnomalyId(GameSession.Player2Anomaly.AnomalyID, GameSession.Player2Anomaly.Role);

            // Start Battle
            engine.InitBattle(
                GameSession.Player1Anomaly, skills1, GameSession.Player1Items, GameSession.Player1Name,
                GameSession.Player2Anomaly, skills2, GameSession.Player2Items, GameSession.Player2Name
            );

            // Initial UI
            RefreshHPDisplay(1);
            RefreshHPDisplay(2);
            pulseTurnTimer.Start();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            pulseTurnTimer.Stop();
            pulseTurnTimer.Dispose();
            base.OnFormClosing(e);
        }

        // ===== EVENT HANDLERS DARI BATTLEENGINE =====

        private void Engine_OnBattleLog(string message)
        {
            battleLogEntries.Add(message);
            lstBattleLog.Items.Add(message);
            lstBattleLog.TopIndex = lstBattleLog.Items.Count - 1; // Auto-scroll
        }

        private void Engine_OnHPChanged(int playerNum, int newHP, int maxHP)
        {
            ProgressBar bar = (playerNum == 1) ? pbHpP1 : pbHpP2;
            Label lbl = (playerNum == 1) ? lblHpP1 : lblHpP2;

            lbl.Text = $"HP: {newHP} / {maxHP}";

            int targetPct = (maxHP > 0) ? Math.Max(0, Math.Min(100, (int)Math.Round((double)newHP * 100 / maxHP))) : 0;

            // Animasi HP bar turun perlahan
            AnimateHpBar(bar, targetPct);
        }

        private void Engine_OnTurnChanged(int playerNum)
        {
            UpdateTurnVisuals(playerNum);
            RefreshSkillCombo(playerNum);
            RefreshItemCombo(playerNum);
            RefreshStatusLabels();
            EnableActions(true);
        }

        private void Engine_OnPhaseChanged()
        {
            if (engine.CurrentPhase == BattlePhase.ResolvingAction || engine.CurrentPhase == BattlePhase.BattleOver)
            {
                EnableActions(false);
            }
        }

        private void Engine_OnStunSkipTurn(int playerNum)
        {
            AudioManager.PlaySFX("sfx_hit.wav");
            ShakeControl((playerNum == 1) ? picP1 : picP2);
            RefreshStatusLabels();
        }

        private void Engine_OnBattleEnd(int winner, string summary)
        {
            EnableActions(false);
            pulseTurnTimer.Stop();

            AudioManager.PlaySFX("sfx_victory.wav");
            AudioManager.PlayBGM("bgm_selection.wav");

            SaveMatchResult(winner);

            // Buka Popup FormResult dengan animasi Zoom RPG
            string winnerName = (winner == 1) ? GameSession.Player1Name : GameSession.Player2Name;
            string winnerAnomaly = (winner == 1) ? GameSession.Player1Anomaly!.Name : GameSession.Player2Anomaly!.Name;

            FormResult resultDialog = new FormResult(winnerName, winnerAnomaly, summary);
            resultDialog.ShowDialog();

            AudioManager.StopBGM();
            GameSession.ResetSession();
            this.Close();
        }

        // ===== SISTEM ANIMASI BERBASIS TIMER =====

        /// <summary>
        /// 1. Animasi HP Bar turun/naik perlahan.
        /// </summary>
        private void AnimateHpBar(ProgressBar bar, int targetValue)
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer { Interval = 16 };
            timer.Tick += (s, e) =>
            {
                if (bar.Value > targetValue)
                {
                    bar.Value = Math.Max(targetValue, bar.Value - 2);
                }
                else if (bar.Value < targetValue)
                {
                    bar.Value = Math.Min(targetValue, bar.Value + 2);
                }
                else
                {
                    timer.Stop();
                    timer.Dispose();
                }
            };
            timer.Start();
        }

        /// <summary>
        /// 2. Animasi Damage Floating Text di atas karakter.
        /// </summary>
        private void ShowDamageNumber(string text, Point startPos, Color color)
        {
            Label dmgLabel = new Label
            {
                Text = text,
                ForeColor = color,
                Font = new Font("Consolas", 16F, FontStyle.Bold),
                BackColor = Color.Transparent,
                AutoSize = true,
                Location = startPos
            };

            this.Controls.Add(dmgLabel);
            dmgLabel.BringToFront();

            int elapsed = 0;
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer { Interval = 16 };
            timer.Tick += (s, e) =>
            {
                elapsed += 16;
                dmgLabel.Top -= 2; // Bergerak ke atas

                if (elapsed >= 500) // 500ms durasi
                {
                    timer.Stop();
                    timer.Dispose();
                    this.Controls.Remove(dmgLabel);
                    dmgLabel.Dispose();
                }
            };
            timer.Start();
        }

        /// <summary>
        /// 3. Animasi Shake (Getar) pada PictureBox atau Panel saat terkena damage.
        /// </summary>
        private void ShakeControl(Control control, int durationMs = 300)
        {
            Point originalPos = control.Location;
            int elapsed = 0;
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer { Interval = 16 };
            timer.Tick += (s, e) =>
            {
                elapsed += 16;
                int offset = (elapsed % 60 < 30) ? 6 : -6;
                control.Location = new Point(originalPos.X + offset, originalPos.Y);

                if (elapsed >= durationMs)
                {
                    control.Location = originalPos;
                    timer.Stop();
                    timer.Dispose();
                }
            };
            timer.Start();
        }

        // ===== VISUAL TURN INDICATOR =====

        private void UpdateTurnVisuals(int activePlayerNum)
        {
            if (activePlayerNum == 1)
            {
                lblP1TurnBadge.Text = "🔥 GILIRAN KAMU!";
                lblP1TurnBadge.BackColor = Color.FromArgb(245, 166, 35); // Gold
                lblP1TurnBadge.ForeColor = Color.FromArgb(26, 26, 46);

                lblP2TurnBadge.Text = "⏳ MENUNGGU";
                lblP2TurnBadge.BackColor = Color.FromArgb(15, 52, 96);
                lblP2TurnBadge.ForeColor = Color.FromArgb(168, 178, 216);

                lblGiliran.Text = $"⚔️ GILIRAN: {GameSession.Player1Name.ToUpper()} (P1) ⚔️";
                lblGiliran.BackColor = Color.FromArgb(233, 69, 96); // #E94560

                lblActionTitle.Text = $"🎮 Aksi untuk: {GameSession.Player1Name} (Player 1)";
            }
            else
            {
                lblP2TurnBadge.Text = "🔥 GILIRAN KAMU!";
                lblP2TurnBadge.BackColor = Color.FromArgb(245, 166, 35); // Gold
                lblP2TurnBadge.ForeColor = Color.FromArgb(26, 26, 46);

                lblP1TurnBadge.Text = "⏳ MENUNGGU";
                lblP1TurnBadge.BackColor = Color.FromArgb(15, 52, 96);
                lblP1TurnBadge.ForeColor = Color.FromArgb(168, 178, 216);

                lblGiliran.Text = $"⚔️ GILIRAN: {GameSession.Player2Name.ToUpper()} (P2) ⚔️";
                lblGiliran.BackColor = Color.FromArgb(155, 89, 182); // Purple

                lblActionTitle.Text = $"🎮 Aksi untuk: {GameSession.Player2Name} (Player 2)";
            }

            panelP1.Invalidate();
            panelP2.Invalidate();
            picP1.Invalidate();
            picP2.Invalidate();
        }

        // ===== CUSTOM DRAWING BATTLE LOG (COLOR-CODED) =====

        private void lstBattleLog_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= battleLogEntries.Count) return;

            Graphics g = e.Graphics;
            string text = battleLogEntries[e.Index];

            // Background item log
            using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(10, 10, 26))) // #0A0A1A
            {
                g.FillRectangle(bgBrush, e.Bounds);
            }

            // Tentukan warna teks berdasarkan aksi
            Color textColor = Color.FromArgb(234, 234, 234); // Default putih

            if (text.Contains("⚔") || text.Contains("Damage") || text.Contains("menyerang"))
            {
                textColor = Color.FromArgb(231, 76, 60); // Merah (#E74C3C)
            }
            else if (text.Contains("✨") || text.Contains("skill") || text.Contains("POISON") || text.Contains("STUN"))
            {
                textColor = Color.FromArgb(155, 89, 182); // Ungu (#9B59B6)
            }
            else if (text.Contains("🛡") || text.Contains("Defend") || text.Contains("Shield"))
            {
                textColor = Color.FromArgb(52, 152, 219); // Biru (#3498DB)
            }
            else if (text.Contains("🎒") || text.Contains("Item") || text.Contains("Memulihkan") || text.Contains("Regen") || text.Contains("💚"))
            {
                textColor = Color.FromArgb(46, 204, 113); // Hijau (#2ECC71)
            }
            else if (text.Contains("===") || text.Contains("Turn") || text.Contains("Giliran") || text.Contains("🏆"))
            {
                textColor = Color.FromArgb(245, 166, 35); // Gold (#F5A623)
            }

            using (Font font = new Font("Consolas", 9F, text.Contains("===") ? FontStyle.Bold : FontStyle.Regular))
            {
                TextRenderer.DrawText(g, text, font, new Point(e.Bounds.X + 4, e.Bounds.Y + 2), textColor);
            }
        }

        // ===== CUSTOM PAINT: BACKGROUND & PANELS =====

        private void FormBattle_Paint(object sender, PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(
                ClientRectangle,
                Color.FromArgb(26, 26, 46),
                Color.FromArgb(15, 52, 96),
                LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, ClientRectangle);
            }
        }

        private void panelP1_Paint(object sender, PaintEventArgs e) => DrawFighterPanel(e.Graphics, panelP1, engine?.CurrentTurn == 1);
        private void panelP2_Paint(object sender, PaintEventArgs e) => DrawFighterPanel(e.Graphics, panelP2, engine?.CurrentTurn == 2);
        private void panelCenter_Paint(object sender, PaintEventArgs e) => DrawCenterPanel(e.Graphics, panelCenter);
        private void panelAction_Paint(object sender, PaintEventArgs e) => DrawActionPanel(e.Graphics, panelAction);

        private void DrawFighterPanel(Graphics g, Panel panel, bool isActive)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);

            using (LinearGradientBrush brush = new LinearGradientBrush(
                rect,
                Color.FromArgb(22, 33, 62),
                Color.FromArgb(15, 52, 96),
                LinearGradientMode.Vertical))
            {
                g.FillRectangle(brush, rect);
            }

            // Border emas jika giliran aktif
            Color borderColor = isActive ? Color.FromArgb(245, 166, 35) : Color.FromArgb(15, 52, 96);
            float borderWidth = isActive ? 3f : 1.5f;

            using (Pen pen = new Pen(borderColor, borderWidth))
            {
                g.DrawRectangle(pen, rect);
            }

            // Accent Bar di sisi kiri
            using (SolidBrush accent = new SolidBrush(isActive ? Color.FromArgb(245, 166, 35) : Color.FromArgb(233, 69, 96)))
            {
                g.FillRectangle(accent, 0, 0, 4, panel.Height);
            }
        }

        private void picP1_Paint(object sender, PaintEventArgs e) => DrawAvatarBorder(e.Graphics, picP1, engine?.CurrentTurn == 1);
        private void picP2_Paint(object sender, PaintEventArgs e) => DrawAvatarBorder(e.Graphics, picP2, engine?.CurrentTurn == 2);

        private void DrawAvatarBorder(Graphics g, PictureBox pic, bool isActive)
        {
            Color color = isActive ? Color.FromArgb(245, 166, 35) : Color.FromArgb(15, 52, 96);
            using (Pen pen = new Pen(color, isActive ? 3 : 1))
            {
                g.DrawRectangle(pen, 0, 0, pic.Width - 1, pic.Height - 1);
            }
        }

        private void DrawCenterPanel(Graphics g, Panel panel)
        {
            Rectangle rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);
            using (Pen pen = new Pen(Color.FromArgb(15, 52, 96), 2))
            {
                g.DrawRectangle(pen, rect);
            }
        }

        private void DrawActionPanel(Graphics g, Panel panel)
        {
            Rectangle rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);
            using (LinearGradientBrush brush = new LinearGradientBrush(
                rect,
                Color.FromArgb(22, 33, 62),
                Color.FromArgb(15, 52, 96),
                LinearGradientMode.Vertical))
            {
                g.FillRectangle(brush, rect);
            }
            using (Pen pen = new Pen(Color.FromArgb(15, 52, 96), 2))
            {
                g.DrawRectangle(pen, rect);
            }
        }

        // ===== UI HELPERS =====

        private void RefreshHPDisplay(int playerNum)
        {
            FighterState fighter = (playerNum == 1) ? engine.Fighter1 : engine.Fighter2;
            Label lblHp = (playerNum == 1) ? lblHpP1 : lblHpP2;
            ProgressBar pbHp = (playerNum == 1) ? pbHpP1 : pbHpP2;

            lblHp.Text = $"HP: {fighter.CurrentHP} / {fighter.MaxHP}";
            int pct = engine.GetHpPercentage(playerNum);
            AnimateHpBar(pbHp, pct);
        }

        private void RefreshSkillCombo(int playerNum)
        {
            FighterState fighter = (playerNum == 1) ? engine.Fighter1 : engine.Fighter2;
            cmbSkill.Items.Clear();

            for (int i = 0; i < fighter.Skills.Count; i++)
            {
                Skill skill = fighter.Skills[i];
                string display = $"{skill.Name} ({skill.SkillType}, {skill.Power:F1}x)";

                if (engine.IsSkillOnCooldown(playerNum, i))
                {
                    int cd = engine.GetSkillCooldownRemaining(playerNum, i);
                    display += $" [CD: {cd}t]";
                }

                cmbSkill.Items.Add(display);
            }

            if (cmbSkill.Items.Count > 0)
                cmbSkill.SelectedIndex = 0;
        }

        private void RefreshItemCombo(int playerNum)
        {
            FighterState fighter = (playerNum == 1) ? engine.Fighter1 : engine.Fighter2;
            cmbItem.Items.Clear();

            for (int i = 0; i < fighter.Items.Count; i++)
            {
                Item item = fighter.Items[i];
                string unit = item.IsPercentage ? "%" : "";
                string display = $"{item.Name} (+{item.EffectValue}{unit} {item.EffectType})";

                if (engine.IsItemOnCooldown(playerNum, i))
                {
                    int cd = engine.GetItemCooldownRemaining(playerNum, i);
                    display += $" [CD: {cd}t]";
                }

                cmbItem.Items.Add(display);
            }

            if (cmbItem.Items.Count > 0)
                cmbItem.SelectedIndex = 0;
        }

        private void RefreshStatusLabels()
        {
            lblP1Status.Text = BuildStatusText(engine.Fighter1);
            lblP2Status.Text = BuildStatusText(engine.Fighter2);
        }

        private string BuildStatusText(FighterState fighter)
        {
            List<string> statuses = new List<string>();

            if (fighter.IsDefending) statuses.Add("🛡 Defending (50% Dmg Red.)");
            if (fighter.HasPoison) statuses.Add($"☠ Poison ({fighter.PoisonTurns} turn)");
            if (fighter.HasStun) statuses.Add("⚡ Stunned (Skip turn)");
            if (fighter.ShieldHP > 0) statuses.Add($"🔰 Shield: {fighter.ShieldHP} HP");
            if (fighter.ATKBuffTurns > 0)
            {
                string prefix = fighter.ATKBuffValue >= 0 ? "💪 ATK+" : "🔻 ATK";
                statuses.Add($"{prefix}{fighter.ATKBuffValue} ({fighter.ATKBuffTurns} turn)");
            }
            if (fighter.DEFBuffTurns > 0)
            {
                string prefix = fighter.DEFBuffValue >= 0 ? "💪 DEF+" : "🔻 DEF";
                statuses.Add($"{prefix}{fighter.DEFBuffValue} ({fighter.DEFBuffTurns} turn)");
            }

            return string.Join("\n", statuses);
        }

        private void EnableActions(bool enabled)
        {
            btnAttack.Enabled = enabled;
            btnSkill.Enabled = enabled && cmbSkill.Items.Count > 0;
            btnDefend.Enabled = enabled;
            cmbSkill.Enabled = enabled && cmbSkill.Items.Count > 0;
            btnItem.Enabled = enabled && cmbItem.Items.Count > 0;
            cmbItem.Enabled = enabled && cmbItem.Items.Count > 0;
        }

        private void SaveMatchResult(int winner)
        {
            try
            {
                int p1Id = GameSession.Player1?.PlayerID ?? 1;
                int p2Id = GameSession.Player2?.PlayerID ?? 2;
                int winnerId = (winner == 1) ? p1Id : p2Id;

                int anom1Id = GameSession.Player1Anomaly?.AnomalyID ?? 1;
                int anom2Id = GameSession.Player2Anomaly?.AnomalyID ?? 2;

                MatchHistory match = new MatchHistory
                {
                    Player1ID = p1Id,
                    Player2ID = p2Id,
                    Anomaly1ID = anom1Id,
                    Anomaly2ID = anom2Id,
                    WinnerID = winnerId,
                    MatchDate = DateTime.Now,
                    Notes = $"Total Turn: {engine.TurnNumber}"
                };

                MatchHistoryRepository matchRepo = new MatchHistoryRepository();
                matchRepo.AddMatch(match);

                PlayerRepository playerRepo = new PlayerRepository();
                playerRepo.UpdateStats(winnerId, isWin: true);
                playerRepo.UpdateStats((winner == 1) ? p2Id : p1Id, isWin: false);
            }
            catch { }
        }

        // ===== BUTTON HANDLERS DENGAN ANIMASI DAN SFX =====

        private void btnAttack_Click(object sender, EventArgs e)
        {
            int targetPlayerNum = (engine.CurrentTurn == 1) ? 2 : 1;
            PictureBox targetPic = (targetPlayerNum == 1) ? picP1 : picP2;

            AudioManager.PlaySFX("sfx_attack.wav");
            ShakeControl(targetPic);

            // Tampilkan floating damage
            Point startPos = new Point((targetPic.Parent?.Left ?? 0) + targetPic.Left + 20, (targetPic.Parent?.Top ?? 0) + targetPic.Top + 20);
            ShowDamageNumber("-DMG", startPos, Color.FromArgb(231, 76, 60));

            engine.DoAttack();
        }

        private void btnSkill_Click(object sender, EventArgs e)
        {
            if (cmbSkill.SelectedIndex < 0)
            {
                MessageBox.Show("Pilih skill terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int skillIndex = cmbSkill.SelectedIndex;
            if (engine.IsSkillOnCooldown(engine.CurrentTurn, skillIndex))
            {
                MessageBox.Show("Skill masih cooldown!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int targetPlayerNum = (engine.CurrentTurn == 1) ? 2 : 1;
            PictureBox targetPic = (targetPlayerNum == 1) ? picP1 : picP2;

            AudioManager.PlaySFX("sfx_skill.wav");

            // Efek getar sihir
            ShakeControl(targetPic);

            Point startPos = new Point((targetPic.Parent?.Left ?? 0) + targetPic.Left + 20, (targetPic.Parent?.Top ?? 0) + targetPic.Top + 20);
            ShowDamageNumber("✨SKILL", startPos, Color.FromArgb(155, 89, 182));

            engine.DoSkill(skillIndex);
        }

        private void btnDefend_Click(object sender, EventArgs e)
        {
            int currentPlayerNum = engine.CurrentTurn;
            PictureBox currentPic = (currentPlayerNum == 1) ? picP1 : picP2;

            AudioManager.PlaySFX("sfx_defend.wav");
            ShakeControl(currentPic, 200);

            Point startPos = new Point((currentPic.Parent?.Left ?? 0) + currentPic.Left + 20, (currentPic.Parent?.Top ?? 0) + currentPic.Top + 20);
            ShowDamageNumber("🛡️DEFEND", startPos, Color.FromArgb(52, 152, 219));

            engine.DoDefend();
        }

        private void btnItem_Click(object sender, EventArgs e)
        {
            if (cmbItem.SelectedIndex < 0)
            {
                MessageBox.Show("Pilih item terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int itemIndex = cmbItem.SelectedIndex;
            if (engine.IsItemOnCooldown(engine.CurrentTurn, itemIndex))
            {
                MessageBox.Show("Item masih cooldown!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PictureBox currentPic = (engine.CurrentTurn == 1) ? picP1 : picP2;

            AudioManager.PlaySFX("sfx_item.wav");

            Point startPos = new Point((currentPic.Parent?.Left ?? 0) + currentPic.Left + 20, (currentPic.Parent?.Top ?? 0) + currentPic.Top + 20);
            ShowDamageNumber("💚ITEM", startPos, Color.FromArgb(46, 204, 113));

            engine.DoItem(itemIndex);
        }
    }
}
