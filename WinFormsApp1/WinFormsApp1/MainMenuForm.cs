using System;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();
        }

        private void MainMenuForm_Load(object sender, EventArgs e)
        {
            // Title/Header bisa ditambah di sini nanti kalau perlu
            this.Text = "Anomaly Versus - Menu Utama";

            // Putar BGM menu saat form dibuka
            AudioManager.PlayBGM("bgm_menu.wav");
        }

        /// <summary>
        /// Event handler ketika klik tombol "Mulai Game"
        /// Membuka PlayerNameForm untuk input nama pemain
        /// </summary>
        private void btnStartGame_Click(object sender, EventArgs e)
        {
            // SFX klik tombol
            AudioManager.PlaySFX("sfx_click.wav");

            // Buka form input nama pemain
            PlayerNameForm playerNameForm = new PlayerNameForm();
            playerNameForm.ShowDialog();
        }

        /// <summary>
        /// Event handler ketika klik tombol "Keluar"
        /// </summary>
        private void btnExit_Click(object sender, EventArgs e)
        {
            AudioManager.PlaySFX("sfx_click.wav");
            AudioManager.StopBGM(); // Stop BGM sebelum exit
            Application.Exit();
        }
    }
}
