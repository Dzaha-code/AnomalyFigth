using System;
using System.Windows.Forms;
using WinFormsApp1.DataAccess;

namespace WinFormsApp1
{
    /// <summary>
    /// PlayerNameForm.cs
    /// Form untuk input nama Player 1 dan Player 2
    /// Menggunakan PlayerRepository.GetOrCreatePlayer() untuk simpan data ke database
    /// </summary>
    public partial class PlayerNameForm : Form
    {
        public PlayerNameForm()
        {
            InitializeComponent();
        }

        private void PlayerNameForm_Load(object sender, EventArgs e)
        {
            this.Text = "Input Nama Pemain";
            GameSession.ResetSession(); // Reset session saat form dimulai
        }

        /// <summary>
        /// Event handler ketika klik tombol "Lanjut ke Pemilihan Anomaly"
        /// Validasi input, simpan ke GameSession, dan buka AnomalySelectionForm
        /// </summary>
        private void btnNext_Click(object sender, EventArgs e)
        {
            AudioManager.PlaySFX("sfx_click.wav");

            string player1Name = txtPlayer1Name.Text.Trim();
            string player2Name = txtPlayer2Name.Text.Trim();

            // VALIDASI: Nama tidak boleh kosong
            if (string.IsNullOrEmpty(player1Name))
            {
                MessageBox.Show("Nama Player 1 tidak boleh kosong!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(player2Name))
            {
                MessageBox.Show("Nama Player 2 tidak boleh kosong!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // VALIDASI: Nama tidak boleh sama
            if (player1Name.Equals(player2Name, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Nama Player 1 dan Player 2 tidak boleh sama!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ===== PROSES: Gunakan PlayerRepository.GetOrCreatePlayer() =====
            try
            {
                PlayerRepository playerRepo = new PlayerRepository();

                // GetOrCreatePlayer() akan cari player di database, kalau tidak ada akan dibuat baru
                var player1 = playerRepo.GetOrCreatePlayer(player1Name);
                var player2 = playerRepo.GetOrCreatePlayer(player2Name);

                // SIMPAN ke GameSession (digunakan form selanjutnya)
                GameSession.Player1Name = player1Name;
                GameSession.Player2Name = player2Name;

                MessageBox.Show($"Pemain terdaftar!\nPlayer 1: {player1Name}\nPlayer 2: {player2Name}", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // NAVIGASI: Buka AnomalySelectionForm
                AnomalySelectionForm anomalyForm = new AnomalySelectionForm();
                this.Hide(); // Sembunyikan form ini (tidak ditutup, bisa kembali nanti)
                anomalyForm.ShowDialog();
                this.Show(); // Tampilkan kembali jika user batalkan anomalyForm
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Event handler ketika klik tombol "Batal"
        /// Kembali ke MainMenuForm
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            AudioManager.PlaySFX("sfx_click.wav");
            this.Close();
        }
    }
}
