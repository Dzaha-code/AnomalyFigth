using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using WinFormsApp1.DataAccess;
using WinFormsApp1.Models;

namespace WinFormsApp1
{
    /// <summary>
    /// AnomalySelectionForm.cs — Form Pemilihan Anomaly bergaya 3-Kolom Anime RPG.
    /// </summary>
    public partial class AnomalySelectionForm : Form
    {
        private List<Anomaly> anomalyList = new List<Anomaly>();
        private SkillRepository skillRepo = new SkillRepository();
        private System.Windows.Forms.Timer slideAvatarTimer = null!;
        private Point targetAvatarPos;

        public AnomalySelectionForm()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);
        }

        private void AnomalySelectionForm_Load(object sender, EventArgs e)
        {
            this.Text = "Anomaly Versus - Pemilihan Anomaly";
            targetAvatarPos = pictureBoxAnomaly.Location;

            LoadAnomalies();
            AudioManager.PlayBGM("bgm_selection.wav");
        }

        private void LoadAnomalies()
        {
            try
            {
                AnomalyRepository anomalyRepo = new AnomalyRepository();
                anomalyList = anomalyRepo.GetAllAnomaly();

                listBoxAnomalies.Items.Clear();
                foreach (Anomaly a in anomalyList)
                {
                    listBoxAnomalies.Items.Add(a);
                }

                if (listBoxAnomalies.Items.Count > 0)
                {
                    listBoxAnomalies.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading anomalies: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== CUSTOM DRAW LISTBOX ITEMS =====

        private void listBoxAnomalies_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= anomalyList.Count) return;

            Graphics g = e.Graphics;
            Anomaly anomaly = anomalyList[e.Index];
            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            // Background baris
            Color bgColor = isSelected ? Color.FromArgb(233, 69, 96) : Color.FromArgb(15, 52, 96);
            using (SolidBrush bgBrush = new SolidBrush(bgColor))
            {
                g.FillRectangle(bgBrush, e.Bounds);
            }

            // Role indicator color bar di sisi kiri
            Color roleColor = Color.FromArgb(245, 166, 35); // Gold default
            if (anomaly.Role.Equals("Attacker", StringComparison.OrdinalIgnoreCase))
                roleColor = Color.FromArgb(231, 76, 60); // Red
            else if (anomaly.Role.Equals("Defender", StringComparison.OrdinalIgnoreCase))
                roleColor = Color.FromArgb(52, 152, 219); // Blue
            else if (anomaly.Role.Equals("Support", StringComparison.OrdinalIgnoreCase))
                roleColor = Color.FromArgb(46, 204, 113); // Green

            using (SolidBrush roleBrush = new SolidBrush(roleColor))
            {
                g.FillRectangle(roleBrush, e.Bounds.X, e.Bounds.Y, 4, e.Bounds.Height);
            }

            // Teks Nama & Role
            string itemText = $"{anomaly.Name} ({anomaly.Role})";
            Color textColor = isSelected ? Color.White : Color.FromArgb(234, 234, 234);
            using (Font font = new Font("Segoe UI", 9.5F, isSelected ? FontStyle.Bold : FontStyle.Regular))
            {
                TextRenderer.DrawText(g, itemText, font, new Point(e.Bounds.X + 8, e.Bounds.Y + 4), textColor);
            }

            // Border pembatas bawah
            using (Pen borderPen = new Pen(Color.FromArgb(22, 33, 62), 1))
            {
                g.DrawLine(borderPen, e.Bounds.Left, e.Bounds.Bottom - 1, e.Bounds.Right, e.Bounds.Bottom - 1);
            }
        }

        // ===== SELECTION CHANGED: SLIDE-IN AVATAR & UPDATE DETAILS =====

        private void listBoxAnomalies_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxAnomalies.SelectedIndex < 0) return;

            Anomaly selected = anomalyList[listBoxAnomalies.SelectedIndex];
            AudioManager.PlaySFX("sfx_hover.wav");

            // Update Detail Stats
            lblAnomalyName.Text = selected.Name;
            lblAnomalyRole.Text = $"Role: {selected.Role}";
            lblStats.Text = $"HP: {selected.BaseHP,-4}  ATK: {selected.BaseATK,-3}\nDEF: {selected.BaseDEF,-4}  SPD: {selected.BaseSPD,-3}";
            lblDescription.Text = selected.Description;

            // Load Skills
            var skills = skillRepo.GetSkillsByAnomalyId(selected.AnomalyID, selected.Role);
            if (skills.Count > 0)
            {
                lblSkill1.Text = $"✨ {skills[0].Name} ({skills[0].SkillType})\nPower: {skills[0].Power:F1}x | Cooldown: {skills[0].Cooldown} turn\n{skills[0].Description}";
            }
            else
            {
                lblSkill1.Text = "✨ Skill 1: (Tidak ada skill)";
            }

            if (skills.Count > 1)
            {
                lblSkill2.Text = $"✨ {skills[1].Name} ({skills[1].SkillType})\nPower: {skills[1].Power:F1}x | Cooldown: {skills[1].Cooldown} turn\n{skills[1].Description}";
            }
            else
            {
                lblSkill2.Text = "✨ Skill 2: (Tidak ada skill)";
            }

            // Load Avatar Image dengan Animasi SlideIn dari kanan
            Image? img = AssetHelper.LoadAnomalyImage(selected.SpritePath);
            pictureBoxAnomaly.Image = img;

            AnimateAvatarSlideIn();
        }

        private void AnimateAvatarSlideIn()
        {
            if (slideAvatarTimer != null)
            {
                slideAvatarTimer.Stop();
                slideAvatarTimer.Dispose();
            }

            pictureBoxAnomaly.Location = new Point(targetAvatarPos.X + 30, targetAvatarPos.Y);

            slideAvatarTimer = new System.Windows.Forms.Timer { Interval = 16 };
            slideAvatarTimer.Tick += (s, ev) =>
            {
                int newX = pictureBoxAnomaly.Location.X + (int)((targetAvatarPos.X - pictureBoxAnomaly.Location.X) * 0.35f);
                pictureBoxAnomaly.Location = new Point(newX, targetAvatarPos.Y);

                if (Math.Abs(pictureBoxAnomaly.Location.X - targetAvatarPos.X) < 2)
                {
                    pictureBoxAnomaly.Location = targetAvatarPos;
                    slideAvatarTimer.Stop();
                    slideAvatarTimer.Dispose();
                    slideAvatarTimer = null!;
                }
            };
            slideAvatarTimer.Start();
        }

        // ===== CUSTOM PAINT: BACKGROUND & PANELS =====

        private void AnomalySelectionForm_Paint(object sender, PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(
                ClientRectangle,
                Color.FromArgb(26, 26, 46),   // #1A1A2E
                Color.FromArgb(15, 52, 96),   // #0F3460
                LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, ClientRectangle);
            }
        }

        private void panelList_Paint(object sender, PaintEventArgs e) => DrawCardPanel(e.Graphics, panelList);
        private void panelDetail_Paint(object sender, PaintEventArgs e) => DrawCardPanel(e.Graphics, panelDetail);
        private void panelPreview_Paint(object sender, PaintEventArgs e) => DrawCardPanel(e.Graphics, panelPreview);
        private void panelBottom_Paint(object sender, PaintEventArgs e) => DrawCardPanel(e.Graphics, panelBottom);

        private void panelSkillCard1_Paint(object sender, PaintEventArgs e) => DrawInnerCard(e.Graphics, panelSkillCard1);
        private void panelSkillCard2_Paint(object sender, PaintEventArgs e) => DrawInnerCard(e.Graphics, panelSkillCard2);

        private void DrawCardPanel(Graphics g, Panel panel)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);

            using (LinearGradientBrush brush = new LinearGradientBrush(
                rect,
                Color.FromArgb(22, 33, 62),   // #16213E
                Color.FromArgb(15, 52, 96),   // #0F3460
                LinearGradientMode.Vertical))
            {
                g.FillRectangle(brush, rect);
            }

            using (Pen borderPen = new Pen(Color.FromArgb(15, 52, 96), 2))
            {
                g.DrawRectangle(borderPen, rect);
            }

            // Accent Bar Kiri (#E94560)
            using (SolidBrush accentBrush = new SolidBrush(Color.FromArgb(233, 69, 96)))
            {
                g.FillRectangle(accentBrush, 0, 0, 4, panel.Height);
            }
        }

        private void DrawInnerCard(Graphics g, Panel panel)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);
            using (SolidBrush bg = new SolidBrush(Color.FromArgb(15, 52, 96)))
            {
                g.FillRectangle(bg, rect);
            }
            using (Pen borderPen = new Pen(Color.FromArgb(245, 166, 35), 1)) // Gold border
            {
                g.DrawRectangle(borderPen, rect);
            }
        }

        private void pictureBoxAnomaly_Paint(object sender, PaintEventArgs e)
        {
            // Gold border + glow di sekitar gambar Anomaly
            using (Pen goldPen = new Pen(Color.FromArgb(245, 166, 35), 2))
            {
                e.Graphics.DrawRectangle(goldPen, 0, 0, pictureBoxAnomaly.Width - 1, pictureBoxAnomaly.Height - 1);
            }
        }

        // ===== BUTTON HANDLERS =====

        private void btnSelectPlayer1_Click(object sender, EventArgs e)
        {
            AudioManager.PlaySFX("sfx_click.wav");

            if (listBoxAnomalies.SelectedIndex < 0)
            {
                MessageBox.Show("Pilih Anomaly terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Anomaly selected = anomalyList[listBoxAnomalies.SelectedIndex];
            GameSession.Player1Anomaly = selected;

            lblPlayer1Selected.Text = $"Player 1 ({GameSession.Player1Name}): {selected.Name}";
            lblPlayer1Selected.ForeColor = Color.FromArgb(245, 166, 35); // Gold
            btnSelectPlayer1.IsActive = true;
        }

        private void btnSelectPlayer2_Click(object sender, EventArgs e)
        {
            AudioManager.PlaySFX("sfx_click.wav");

            if (listBoxAnomalies.SelectedIndex < 0)
            {
                MessageBox.Show("Pilih Anomaly terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Anomaly selected = anomalyList[listBoxAnomalies.SelectedIndex];
            GameSession.Player2Anomaly = selected;

            lblPlayer2Selected.Text = $"Player 2 ({GameSession.Player2Name}): {selected.Name}";
            lblPlayer2Selected.ForeColor = Color.FromArgb(245, 166, 35); // Gold
            btnSelectPlayer2.IsActive = true;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            AudioManager.PlaySFX("sfx_click.wav");

            if (GameSession.Player1Anomaly == null)
            {
                MessageBox.Show("Player 1 belum memilih Anomaly!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (GameSession.Player2Anomaly == null)
            {
                MessageBox.Show("Player 2 belum memilih Anomaly!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ItemSelectionForm itemForm = new ItemSelectionForm();
            this.Hide();
            itemForm.ShowDialog();
            this.Show();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            AudioManager.PlaySFX("sfx_click.wav");
            this.Close();
        }
    }
}
