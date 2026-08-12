using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WinFormsApp1.DataAccess;
using WinFormsApp1.Logic;
using WinFormsApp1.Models;

namespace WinFormsApp1
{
    /// <summary>
    /// FormBattle.cs - Form utama untuk gameplay battle.
    /// Menampilkan state dari BattleManager dan handle input pemain.
    /// Flow: BattleManager update state → RefreshUI() → user lihat perubahan
    /// </summary>
    public partial class FormBattle : Form
    {
        private BattleManager battle;
        private List<Item> itemsP1;
        private List<Item> itemsP2;

        public FormBattle()
        {
            InitializeComponent();
        }

        private void FormBattle_Load(object sender, EventArgs e)
        {
            // Inisialisasi BattleManager
            battle = new BattleManager();
            battle.InitBattle();

            // Ambil referensi items dari GameSession (untuk display info)
            itemsP1 = GameSession.Player1Items;
            itemsP2 = GameSession.Player2Items;

            // Load dan display gambar anomaly
            Image imgP1 = LoadAnomalyImage(GameSession.Player1Anomaly.Name);
            Image imgP2 = LoadAnomalyImage(GameSession.Player2Anomaly.Name);

            if (imgP1 != null) picP1.Image = imgP1;
            if (imgP2 != null) picP2.Image = imgP2;

            // Initial UI refresh
            RefreshUI();

            // Log pesan awal
            AddBattleLog("=== ANOMALY VERSUS BATTLE START ===");
            AddBattleLog(GameSession.Player1.PlayerName + " menggunakan " + GameSession.Player1Anomaly.Name);
            AddBattleLog(GameSession.Player2.PlayerName + " menggunakan " + GameSession.Player2Anomaly.Name);
            AddBattleLog("");
        }

        /// <summary>
        /// Refresh semua UI elements berdasarkan state BattleManager.
        /// Dipanggil setiap kali ada perubahan state (setelah aksi).
        /// </summary>
        private void RefreshUI()
        {
            // Update HP labels & progress bar P1
            lblHpP1.Text = "HP: " + battle.HpPlayer1 + " / " + (GameSession.Player1Anomaly.BaseHP);
            pbHpP1.Value = battle.GetHpPercentage(1);
            UpdateHpBarColor(pbHpP1, battle.GetHpPercentage(1));

            // Update HP labels & progress bar P2
            lblHpP2.Text = "HP: " + battle.HpPlayer2 + " / " + (GameSession.Player2Anomaly.BaseHP);
            pbHpP2.Value = battle.GetHpPercentage(2);
            UpdateHpBarColor(pbHpP2, battle.GetHpPercentage(2));

            // Update current turn label
            int currentPlayer = battle.CurrentTurn;
            string playerName = (currentPlayer == 1) ? GameSession.Player1.PlayerName : GameSession.Player2.PlayerName;
            lblGiliran.Text = "⏳ Giliran: " + playerName + " (Player " + currentPlayer + ")";

            // Update skill combo & dropdown
            RefreshSkillCombo(currentPlayer);

            // Update item combo & dropdown
            RefreshItemCombo(currentPlayer);

            // Enable/disable action buttons (hanya enable untuk pemain yang sekarang giliran)
            // Sebenarnya semua button bisa di-click, tapi hanya akan akurat untuk player yang giliran
            // Untuk simplicity, kita enable semua ja biar user bisa klik kapan saja

            // Cek pemenang
            int winner = battle.CheckWinner();
            if (winner != 0)
            {
                DisableAllActions();
                ShowResult(winner);
            }
        }

        /// <summary>
        /// Refresh ComboBox Skill dengan list skill pemain yang sekarang giliran.
        /// Tambahkan info cooldown di display text.
        /// </summary>
        private void RefreshSkillCombo(int playerNum)
        {
            List<Skill> skills = (playerNum == 1) ? battle.SkillsP1 : battle.SkillsP2;
            cmbSkill.Items.Clear();
            cmbSkill.DisplayMember = "DisplayText";
            cmbSkill.ValueMember = "Skill";

            for (int i = 0; i < skills.Count; i++)
            {
                Skill skill = skills[i];
                string displayText = skill.Name + " (DMG: " + skill.Damage + ")";

                if (battle.IsSkillOnCooldown(playerNum, i))
                {
                    int cdRemaining = battle.GetSkillCooldownRemaining(playerNum, i);
                    displayText += " [CD: " + cdRemaining + " turn]";
                }

                cmbSkill.Items.Add(new { DisplayText = displayText, Skill = skill, Index = i });
            }

            if (cmbSkill.Items.Count > 0)
                cmbSkill.SelectedIndex = 0;
        }

        /// <summary>
        /// Refresh ComboBox Item dengan list item pemain yang sekarang giliran.
        /// Tambahkan info cooldown di display text.
        /// </summary>
        private void RefreshItemCombo(int playerNum)
        {
            List<Item> items = (playerNum == 1) ? itemsP1 : itemsP2;
            cmbItem.Items.Clear();
            cmbItem.DisplayMember = "DisplayText";
            cmbItem.ValueMember = "Item";

            for (int i = 0; i < items.Count; i++)
            {
                Item item = items[i];
                string displayText = item.Name + " (" + item.EffectType + ")";

                if (battle.IsItemOnCooldown(playerNum, i))
                {
                    int cdRemaining = battle.GetItemCooldownRemaining(playerNum, i);
                    displayText += " [CD: " + cdRemaining + " turn]";
                }

                cmbItem.Items.Add(new { DisplayText = displayText, Item = item, Index = i });
            }

            if (cmbItem.Items.Count > 0)
                cmbItem.SelectedIndex = 0;
        }

        /// <summary>
        /// Update warna progress bar HP berdasarkan persentase.
        /// > 50% = hijau, 25-50% = kuning, < 25% = merah
        /// </summary>
        private void UpdateHpBarColor(ProgressBar bar, int percentage)
        {
            if (percentage > 50)
                bar.ForeColor = Color.LimeGreen;
            else if (percentage >= 25)
                bar.ForeColor = Color.Yellow;
            else
                bar.ForeColor = Color.Red;
        }

        /// <summary>
        /// Tambah text ke battle log ListBox dan auto-scroll ke bawah.
        /// </summary>
        private void AddBattleLog(string message)
        {
            lstBattleLog.Items.Add(message);
            lstBattleLog.TopIndex = lstBattleLog.Items.Count - 1; // Auto-scroll
        }

        /// <summary>
        /// Load gambar Anomaly dari folder Assets/Images.
        /// Nama file = Anomaly.Name (case-sensitive).
        /// Fallback ke placeholder.png jika tidak ditemukan.
        /// </summary>
        private Image LoadAnomalyImage(string anomalyName)
        {
            try
            {
                // Ganti spasi dengan underscore untuk nama file
                string safeName = anomalyName.Replace(" ", "_");
                string imagePath = Path.Combine(
                    Application.StartupPath,
                    "Assets", "Images", safeName + ".png"
                );

                if (File.Exists(imagePath))
                    return Image.FromFile(imagePath);
                else
                {
                    // Fallback ke placeholder
                    string placeholder = Path.Combine(
                        Application.StartupPath,
                        "Assets", "Images", "placeholder.png"
                    );
                    if (File.Exists(placeholder))
                        return Image.FromFile(placeholder);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading image: " + ex.Message);
            }

            return null;
        }

        /// <summary>
        /// Disable semua tombol aksi (saat battle selesai).
        /// </summary>
        private void DisableAllActions()
        {
            btnAttack.Enabled = false;
            btnSkill.Enabled = false;
            btnDefend.Enabled = false;
            btnItem.Enabled = false;
        }

        /// <summary>
        /// Tampilkan hasil battle dan simpan ke database.
        /// winner: 1 = Player 1 menang, 2 = Player 2 menang
        /// </summary>
        private void ShowResult(int winner)
        {
            string winnerName = (winner == 1) ? GameSession.Player1.PlayerName : GameSession.Player2.PlayerName;
            string loserName = (winner == 1) ? GameSession.Player2.PlayerName : GameSession.Player1.PlayerName;
            string winnerAnomalyName = (winner == 1) ? GameSession.Player1Anomaly.Name : GameSession.Player2Anomaly.Name;
            string loserAnomalyName = (winner == 1) ? GameSession.Player2Anomaly.Name : GameSession.Player1Anomaly.Name;

            int winnerId = (winner == 1) ? GameSession.Player1.PlayerID : GameSession.Player2.PlayerID;
            int loserId = (winner == 1) ? GameSession.Player2.PlayerID : GameSession.Player1.PlayerID;

            string resultMessage = "🏆 " + winnerName + " MENANG!\n\n";
            resultMessage += winnerAnomalyName + " mengalahkan " + loserAnomalyName;

            AddBattleLog("");
            AddBattleLog("=== BATTLE END ===");
            AddBattleLog(resultMessage);

            // Simpan ke database
            try
            {
                // Buat MatchHistory record
                MatchHistory match = new MatchHistory
                {
                    WinnerPlayerID = winnerId,
                    LoserPlayerID = loserId,
                    WinnerAnomalyName = winnerAnomalyName,
                    LoserAnomalyName = loserAnomalyName,
                    PlayedAt = DateTime.Now
                };

                // Simpan match ke database
                MatchHistoryRepository matchRepo = new MatchHistoryRepository();
                matchRepo.AddMatch(match);

                // Update stats pemain
                PlayerRepository playerRepo = new PlayerRepository();
                playerRepo.UpdateStats(winnerId, isWin: true);
                playerRepo.UpdateStats(loserId, isWin: false);

                AddBattleLog("✓ Match history saved to database");
            }
            catch (Exception ex)
            {
                AddBattleLog("✗ Error saving to database: " + ex.Message);
            }

            // Tampilkan dialog hasil
            MessageBox.Show(resultMessage, "Battle Result", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Reset session & kembali ke menu utama
            GameSession.ResetSession();
            this.Close();
        }

        // ===== EVENT HANDLERS =====

        private void btnAttack_Click(object sender, EventArgs e)
        {
            string result = battle.DoAttack();
            AddBattleLog(result);
            RefreshUI();
        }

        private void btnSkill_Click(object sender, EventArgs e)
        {
            if (cmbSkill.SelectedIndex < 0)
            {
                MessageBox.Show("Pilih skill terlebih dahulu!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ambil index skill dari combo box
            dynamic selectedItem = cmbSkill.SelectedItem;
            int skillIndex = selectedItem.Index;

            string result = battle.DoSkill(skillIndex);
            AddBattleLog(result);
            RefreshUI();
        }

        private void btnDefend_Click(object sender, EventArgs e)
        {
            string result = battle.DoDefend();
            AddBattleLog(result);
            RefreshUI();
        }

        private void btnItem_Click(object sender, EventArgs e)
        {
            if (cmbItem.SelectedIndex < 0)
            {
                MessageBox.Show("Pilih item terlebih dahulu!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ambil index item dari combo box
            dynamic selectedItem = cmbItem.SelectedItem;
            int itemIndex = selectedItem.Index;

            string result = battle.DoItem(itemIndex);
            AddBattleLog(result);
            RefreshUI();
        }
    }
}
