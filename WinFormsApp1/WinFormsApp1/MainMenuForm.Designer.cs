namespace WinFormsApp1
{
    partial class MainMenuForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnStartGame = new WinFormsApp1.UI.RPGButton();
            this.btnExit = new WinFormsApp1.UI.RPGButton();
            this.lblPressStart = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // lblPressStart (Animasi kedip)
            this.lblPressStart.AutoSize = false;
            this.lblPressStart.BackColor = System.Drawing.Color.Transparent;
            this.lblPressStart.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblPressStart.ForeColor = System.Drawing.Color.FromArgb(245, 166, 35); // Gold
            this.lblPressStart.Location = new System.Drawing.Point(150, 155);
            this.lblPressStart.Name = "lblPressStart";
            this.lblPressStart.Size = new System.Drawing.Size(340, 30);
            this.lblPressStart.TabIndex = 0;
            this.lblPressStart.Text = "★ PRESS START TO BATTLE ★";
            this.lblPressStart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // btnStartGame (RPGButton)
            this.btnStartGame.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.btnStartGame.Location = new System.Drawing.Point(170, 200);
            this.btnStartGame.Name = "btnStartGame";
            this.btnStartGame.Size = new System.Drawing.Size(300, 55);
            this.btnStartGame.TabIndex = 1;
            this.btnStartGame.Text = "⚔️ MULAI GAME";
            this.btnStartGame.Click += new System.EventHandler(this.btnStartGame_Click);

            // btnExit (RPGButton)
            this.btnExit.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnExit.Location = new System.Drawing.Point(170, 270);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(300, 50);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "🚪 KELUAR";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);

            // lblVersion
            this.lblVersion.AutoSize = true;
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(168, 178, 216); // #A8B2D8
            this.lblVersion.Location = new System.Drawing.Point(440, 385);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(180, 15);
            this.lblVersion.TabIndex = 3;
            this.lblVersion.Text = "v1.0 — SMKN 13 Bandung";

            // MainMenuForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 420);
            this.Controls.Add(this.lblPressStart);
            this.Controls.Add(this.btnStartGame);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblVersion);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainMenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Anomaly Versus - Menu Utama";
            this.Load += new System.EventHandler(this.MainMenuForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainMenuForm_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private WinFormsApp1.UI.RPGButton btnStartGame;
        private WinFormsApp1.UI.RPGButton btnExit;
        private System.Windows.Forms.Label lblPressStart;
        private System.Windows.Forms.Label lblVersion;
    }
}
