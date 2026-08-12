using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.DataAccess;
using WinFormsApp1.Models;

namespace WinFormsApp1
{
    /// <summary>
    /// AnomalySelectionForm.cs
    /// Form untuk pemilihan Anomaly oleh Player 1 dan Player 2
    /// Menggunakan AnomalyRepository.GetAllAnomaly() untuk ambil daftar anomaly dari database
    /// Pola standar: buat objek repository → panggil fungsinya → iterasi hasilnya di ListBox
    /// </summary>
    public partial class AnomalySelectionForm : Form
    {
        private List<Anomaly> anomalyList = new List<Anomaly>(); // Simpan list anomaly dari database

        public AnomalySelectionForm()
        {
            InitializeComponent();
        }

        private void AnomalySelectionForm_Load(object sender, EventArgs e)
        {
            this.Text = "Pemilihan Anomaly";
            LoadAnomalies();

            // Ganti BGM ke selection music
            AudioManager.PlayBGM("bgm_selection.wav");
        }

        /// <summary>
        /// Load semua Anomaly dari database dan tampilkan di ListBox
        /// Pola: AnomalyRepository repo = new AnomalyRepository();
        ///       List<Anomaly> list = repo.GetAllAnomaly();
        ///       foreach (Anomaly a in list) { listBox.Items.Add(...) }
        /// </summary>
        private void LoadAnomalies()
        {
            try
            {
                // POLA STANDAR: Buat objek repository
                AnomalyRepository anomalyRepo = new AnomalyRepository();

                // POLA STANDAR: Panggil fungsinya
                anomalyList = anomalyRepo.GetAllAnomaly();

                // POLA STANDAR: Iterasi hasilnya di ListBox
                listBoxAnomalies.Items.Clear();
                foreach (Anomaly anomaly in anomalyList)
                {
                    listBoxAnomalies.Items.Add($"{anomaly.Name} ({anomaly.Role}) - HP:{anomaly.BaseHP} ATK:{anomaly.BaseATK} DEF:{anomaly.BaseDEF}");
                }

                lblAnomalyCount.Text = $"Total Anomaly: {anomalyList.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading anomalies: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Event ketika user klik anomaly di ListBox — tampilkan gambar & detail di panel kanan
        /// Menggunakan AssetHelper.LoadAnomalyImage() untuk load gambar dari folder Assets
        /// </summary>
        private void listBoxAnomalies_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxAnomalies.SelectedIndex < 0) return;

            Anomaly selected = anomalyList[listBoxAnomalies.SelectedIndex];

            // Load gambar anomaly dari folder Assets menggunakan SpritePath dari database
            Image? img = AssetHelper.LoadAnomalyImage(selected.SpritePath);
            if (img != null)
            {
                pictureBoxAnomaly.Image = img;
            }
            else
            {
                pictureBoxAnomaly.Image = null; // Gambar tidak ditemukan
            }

            // Tampilkan detail stats di label
            lblAnomalyDetail.Text = $"{selected.Name} ({selected.Role})\n" +
                                     $"HP: {selected.BaseHP}  ATK: {selected.BaseATK}\n" +
                                     $"DEF: {selected.BaseDEF}  SPD: {selected.BaseSPD}\n" +
                                     $"{selected.Description}";
        }

        /// <summary>
        /// Event handler ketika klik tombol "Pilih untuk Player 1"
        /// Simpan Anomaly yang dipilih ke GameSession
        /// </summary>
        private void btnSelectPlayer1_Click(object sender, EventArgs e)
        {
            AudioManager.PlaySFX("sfx_click.wav");

            if (listBoxAnomalies.SelectedIndex < 0)
            {
                MessageBox.Show("Pilih Anomaly terlebih dahulu!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ambil Anomaly yang dipilih (berdasarkan index di ListBox)
            Anomaly selectedAnomaly = anomalyList[listBoxAnomalies.SelectedIndex];

            // Simpan ke GameSession
            GameSession.Player1Anomaly = selectedAnomaly;

            // Tampilkan konfirmasi
            lblPlayer1Selected.Text = $"Player 1 ({GameSession.Player1Name}): {selectedAnomaly.Name}";
            lblPlayer1Selected.ForeColor = System.Drawing.Color.Green;

            MessageBox.Show($"Player 1 memilih: {selectedAnomaly.Name}", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Event handler ketika klik tombol "Pilih untuk Player 2"
        /// Simpan Anomaly yang dipilih ke GameSession
        /// </summary>
        private void btnSelectPlayer2_Click(object sender, EventArgs e)
        {
            AudioManager.PlaySFX("sfx_click.wav");

            if (listBoxAnomalies.SelectedIndex < 0)
            {
                MessageBox.Show("Pilih Anomaly terlebih dahulu!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ambil Anomaly yang dipilih
            Anomaly selectedAnomaly = anomalyList[listBoxAnomalies.SelectedIndex];

            // Simpan ke GameSession
            GameSession.Player2Anomaly = selectedAnomaly;

            // Tampilkan konfirmasi
            lblPlayer2Selected.Text = $"Player 2 ({GameSession.Player2Name}): {selectedAnomaly.Name}";
            lblPlayer2Selected.ForeColor = System.Drawing.Color.Green;

            MessageBox.Show($"Player 2 memilih: {selectedAnomaly.Name}", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Event handler ketika klik tombol "Lanjut ke Pemilihan Item"
        /// Cek apakah kedua player sudah memilih anomaly, lalu buka ItemSelectionForm
        /// </summary>
        private void btnNext_Click(object sender, EventArgs e)
        {
            AudioManager.PlaySFX("sfx_click.wav");

            // VALIDASI: Kedua player harus memilih anomaly
            if (GameSession.Player1Anomaly == null)
            {
                MessageBox.Show("Player 1 belum memilih Anomaly!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (GameSession.Player2Anomaly == null)
            {
                MessageBox.Show("Player 2 belum memilih Anomaly!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // NAVIGASI: Buka ItemSelectionForm
            ItemSelectionForm itemForm = new ItemSelectionForm();
            this.Hide();
            itemForm.ShowDialog();
            this.Show();
        }

        /// <summary>
        /// Event handler ketika klik tombol "Kembali"
        /// </summary>
        private void btnBack_Click(object sender, EventArgs e)
        {
            AudioManager.PlaySFX("sfx_click.wav");
            this.Close();
        }
    }
}
