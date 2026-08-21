using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using WinFormsApp1.UI;

namespace WinFormsApp1
{
    /// <summary>
    /// FormResult.cs — Popup Hasil Battle bergaya Anime RPG dengan animasi Scale/Zoom dari tengah layar.
    /// </summary>
    public partial class FormResult : Form
    {
        private string winnerName;
        private string winnerAnomaly;
        private string statsSummary;
        private System.Windows.Forms.Timer zoomTimer = null!;
        private Size targetSize = new Size(460, 360);

        public bool PlayAgain { get; private set; } = false;

        public FormResult(string winner, string anomaly, string stats)
        {
            winnerName = winner;
            winnerAnomaly = anomaly;
            statsSummary = stats;

            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblWinner = new System.Windows.Forms.Label();
            this.lblAnomaly = new System.Windows.Forms.Label();
            this.lblStats = new System.Windows.Forms.Label();
            this.btnPlayAgain = new WinFormsApp1.UI.RPGButton();
            this.btnMenu = new WinFormsApp1.UI.RPGButton();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 20F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(245, 166, 35); // Gold
            this.lblTitle.Location = new Point(12, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(436, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🏆 VICTORY! 🏆";
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            // lblWinner
            this.lblWinner.Font = new System.Drawing.Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblWinner.ForeColor = Color.FromArgb(234, 234, 234);
            this.lblWinner.Location = new Point(12, 65);
            this.lblWinner.Name = "lblWinner";
            this.lblWinner.Size = new Size(436, 30);
            this.lblWinner.TabIndex = 1;
            this.lblWinner.Text = $"PEMENANG: {winnerName.ToUpper()}";
            this.lblWinner.TextAlign = ContentAlignment.MiddleCenter;

            // lblAnomaly
            this.lblAnomaly.Font = new System.Drawing.Font("Segoe UI", 10.5F, FontStyle.Bold);
            this.lblAnomaly.ForeColor = Color.FromArgb(233, 69, 96); // Merah-pink
            this.lblAnomaly.Location = new Point(12, 95);
            this.lblAnomaly.Name = "lblAnomaly";
            this.lblAnomaly.Size = new Size(436, 25);
            this.lblAnomaly.TabIndex = 2;
            this.lblAnomaly.Text = $"Anomaly: {winnerAnomaly}";
            this.lblAnomaly.TextAlign = ContentAlignment.MiddleCenter;

            // lblStats
            this.lblStats.Font = new System.Drawing.Font("Consolas", 9.5F);
            this.lblStats.ForeColor = Color.FromArgb(168, 178, 216);
            this.lblStats.Location = new Point(25, 125);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new Size(410, 140);
            this.lblStats.TabIndex = 3;
            this.lblStats.Text = statsSummary;

            // btnPlayAgain
            this.btnPlayAgain.Font = new System.Drawing.Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnPlayAgain.Location = new Point(35, 280);
            this.btnPlayAgain.Name = "btnPlayAgain";
            this.btnPlayAgain.Size = new Size(185, 50);
            this.btnPlayAgain.TabIndex = 4;
            this.btnPlayAgain.Text = "🔄 MAIN LAGI";
            this.btnPlayAgain.Click += (s, e) =>
            {
                PlayAgain = true;
                this.Close();
            };

            // btnMenu
            this.btnMenu.Font = new System.Drawing.Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnMenu.Location = new Point(240, 280);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new Size(185, 50);
            this.btnMenu.TabIndex = 5;
            this.btnMenu.Text = "🏠 MENU UTAMA";
            this.btnMenu.Click += (s, e) =>
            {
                PlayAgain = false;
                this.Close();
            };

            // Form Settings
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(22, 33, 62); // #16213E
            this.ClientSize = targetSize;
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblWinner);
            this.Controls.Add(this.lblAnomaly);
            this.Controls.Add(this.lblStats);
            this.Controls.Add(this.btnPlayAgain);
            this.Controls.Add(this.btnMenu);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Victory";
            this.Load += new EventHandler(this.FormResult_Load!);
            this.Paint += new PaintEventHandler(this.FormResult_Paint!);
            this.ResumeLayout(false);
        }

        private void FormResult_Load(object? sender, EventArgs e)
        {
            AnimatePopupZoom();
        }

        private void AnimatePopupZoom()
        {
            var bounds = Screen.PrimaryScreen?.Bounds ?? new Rectangle(0, 0, 1024, 768);
            Point screenCenter = new Point(bounds.Width / 2, bounds.Height / 2);

            this.Size = new Size(50, 50);
            this.Location = new Point(screenCenter.X - 25, screenCenter.Y - 25);

            zoomTimer = new System.Windows.Forms.Timer { Interval = 16 };
            zoomTimer.Tick += (s, e) =>
            {
                int newW = this.Width + (int)((targetSize.Width - this.Width) * 0.35f);
                int newH = this.Height + (int)((targetSize.Height - this.Height) * 0.35f);

                this.Size = new Size(newW, newH);
                this.Location = new Point(screenCenter.X - newW / 2, screenCenter.Y - newH / 2);

                if (Math.Abs(newW - targetSize.Width) < 4)
                {
                    this.Size = targetSize;
                    this.Location = new Point(screenCenter.X - targetSize.Width / 2, screenCenter.Y - targetSize.Height / 2);
                    zoomTimer.Stop();
                    zoomTimer.Dispose();
                }
            };
            zoomTimer.Start();
        }

        private void FormResult_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);

            using (LinearGradientBrush brush = new LinearGradientBrush(
                rect,
                Color.FromArgb(22, 33, 62),
                Color.FromArgb(15, 52, 96),
                LinearGradientMode.Vertical))
            {
                g.FillRectangle(brush, rect);
            }

            using (Pen goldPen = new Pen(Color.FromArgb(245, 166, 35), 3))
            {
                g.DrawRectangle(goldPen, rect);
            }

            using (SolidBrush redBrush = new SolidBrush(Color.FromArgb(233, 69, 96)))
            {
                g.FillRectangle(redBrush, 0, 0, Width, 6);
            }
        }

        private System.Windows.Forms.Label lblTitle = null!;
        private System.Windows.Forms.Label lblWinner = null!;
        private System.Windows.Forms.Label lblAnomaly = null!;
        private System.Windows.Forms.Label lblStats = null!;
        private WinFormsApp1.UI.RPGButton btnPlayAgain = null!;
        private WinFormsApp1.UI.RPGButton btnMenu = null!;
    }
}
