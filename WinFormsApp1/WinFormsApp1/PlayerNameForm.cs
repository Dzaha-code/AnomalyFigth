using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using WinFormsApp1.DataAccess;

namespace WinFormsApp1
{
    /// <summary>
    /// PlayerNameForm.cs — Form Input Nama Pemain dengan gaya RPG Dialog Box & Animasi Slide-In.
    /// </summary>
    public partial class PlayerNameForm : Form
    {
        private Point targetDialogPos;
        private System.Windows.Forms.Timer slideTimer = null!;

        public PlayerNameForm()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);
        }

        private void PlayerNameForm_Load(object sender, EventArgs e)
        {
            GameSession.ResetSession();

            // Animasi Slide-In dari bawah layar
            targetDialogPos = panelDialog.Location;
            panelDialog.Location = new Point(targetDialogPos.X, this.Height + 50);

            slideTimer = new System.Windows.Forms.Timer { Interval = 16 };
            slideTimer.Tick += (s, ev) =>
            {
                // Easing formula: posisi += (target - posisi) * 0.2
                int newY = panelDialog.Location.Y + (int)((targetDialogPos.Y - panelDialog.Location.Y) * 0.25f);
                panelDialog.Location = new Point(targetDialogPos.X, newY);

                if (Math.Abs(panelDialog.Location.Y - targetDialogPos.Y) < 3)
                {
                    panelDialog.Location = targetDialogPos;
                    slideTimer.Stop();
                    slideTimer.Dispose();
                }
            };
            slideTimer.Start();
        }

        // ===== CUSTOM PAINT: BACKGROUND GRADIENT =====

        private void PlayerNameForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Color topBg = Color.FromArgb(26, 26, 46);    // #1A1A2E
            Color bottomBg = Color.FromArgb(15, 52, 96); // #0F3460
            using (LinearGradientBrush brush = new LinearGradientBrush(ClientRectangle, topBg, bottomBg, LinearGradientMode.Vertical))
            {
                g.FillRectangle(brush, ClientRectangle);
            }
        }

        // ===== CUSTOM PAINT: DIALOG BOX RPG (PANEL) =====

        private void panelDialog_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, panelDialog.Width - 1, panelDialog.Height - 1);

            // 1. Background Gradient Card (#16213E -> #0F3460)
            using (LinearGradientBrush brush = new LinearGradientBrush(
                rect,
                Color.FromArgb(22, 33, 62),   // #16213E
                Color.FromArgb(15, 52, 96),   // #0F3460
                LinearGradientMode.Vertical))
            {
                g.FillRectangle(brush, rect);
            }

            // 2. Border Card
            using (Pen borderPen = new Pen(Color.FromArgb(15, 52, 96), 2))
            {
                g.DrawRectangle(borderPen, rect);
            }

            // 3. Accent Bar Kiri (#E94560, lebar 5px)
            using (SolidBrush accentBrush = new SolidBrush(Color.FromArgb(233, 69, 96)))
            {
                g.FillRectangle(accentBrush, 0, 0, 5, panelDialog.Height);
            }
        }

        // ===== EFEK SHAKE SAAT VALIDASI GAGAL =====

        private void ShakeError(Control control, int durationMs = 300)
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

        // ===== BUTTON HANDLERS =====

        private void btnNext_Click(object sender, EventArgs e)
        {
            string player1Name = txtPlayer1Name.Text.Trim();
            string player2Name = txtPlayer2Name.Text.Trim();

            // Validasi Input
            if (string.IsNullOrEmpty(player1Name))
            {
                lblError.Text = "⚠️ Nama Player 1 tidak boleh kosong!";
                AudioManager.PlaySFX("sfx_hit.wav");
                ShakeError(txtPlayer1Name);
                return;
            }

            if (string.IsNullOrEmpty(player2Name))
            {
                lblError.Text = "⚠️ Nama Player 2 tidak boleh kosong!";
                AudioManager.PlaySFX("sfx_hit.wav");
                ShakeError(txtPlayer2Name);
                return;
            }

            if (player1Name.Equals(player2Name, StringComparison.OrdinalIgnoreCase))
            {
                lblError.Text = "⚠️ Nama Player 1 dan Player 2 tidak boleh sama!";
                AudioManager.PlaySFX("sfx_hit.wav");
                ShakeError(panelDialog);
                return;
            }

            lblError.Text = "";
            AudioManager.PlaySFX("sfx_click.wav");

            try
            {
                PlayerRepository playerRepo = new PlayerRepository();
                var player1 = playerRepo.GetOrCreatePlayer(player1Name);
                var player2 = playerRepo.GetOrCreatePlayer(player2Name);

                GameSession.Player1Name = player1Name;
                GameSession.Player2Name = player2Name;
                GameSession.Player1 = player1;
                GameSession.Player2 = player2;

                // Navigasi ke FormSelectAnomaly (AnomalySelectionForm)
                AnomalySelectionForm anomalyForm = new AnomalySelectionForm();
                this.Hide();
                anomalyForm.ShowDialog();
                this.Show();
            }
            catch (Exception ex)
            {
                lblError.Text = "❌ Error: " + ex.Message;
                AudioManager.PlaySFX("sfx_hit.wav");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AudioManager.PlaySFX("sfx_click.wav");
            this.Close();
        }
    }
}
