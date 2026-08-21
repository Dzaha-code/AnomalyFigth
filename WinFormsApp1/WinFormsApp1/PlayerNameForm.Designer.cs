namespace WinFormsApp1
{
    partial class PlayerNameForm
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
            this.panelDialog = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblPlayer1 = new System.Windows.Forms.Label();
            this.txtPlayer1Name = new System.Windows.Forms.TextBox();
            this.lblPlayer2 = new System.Windows.Forms.Label();
            this.txtPlayer2Name = new System.Windows.Forms.TextBox();
            this.lblError = new System.Windows.Forms.Label();
            this.btnNext = new WinFormsApp1.UI.RPGButton();
            this.btnCancel = new WinFormsApp1.UI.RPGButton();
            this.panelDialog.SuspendLayout();
            this.SuspendLayout();

            // ===== PANEL DIALOG RPG (TENGAH) =====
            this.panelDialog.BackColor = System.Drawing.Color.FromArgb(22, 33, 62); // #16213E
            this.panelDialog.Location = new System.Drawing.Point(40, 30);
            this.panelDialog.Name = "panelDialog";
            this.panelDialog.Size = new System.Drawing.Size(460, 330);
            this.panelDialog.TabIndex = 0;
            this.panelDialog.Paint += new System.Windows.Forms.PaintEventHandler(this.panelDialog_Paint);

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(233, 69, 96); // #E94560
            this.lblTitle.Location = new System.Drawing.Point(30, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(250, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "⚔️ REGISTRASI PENANTANG";

            // lblPlayer1
            this.lblPlayer1.AutoSize = true;
            this.lblPlayer1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPlayer1.ForeColor = System.Drawing.Color.FromArgb(245, 166, 35); // Gold #F5A623
            this.lblPlayer1.Location = new System.Drawing.Point(30, 65);
            this.lblPlayer1.Name = "lblPlayer1";
            this.lblPlayer1.Size = new System.Drawing.Size(125, 19);
            this.lblPlayer1.TabIndex = 1;
            this.lblPlayer1.Text = "👤 Nama Player 1:";

            // txtPlayer1Name
            this.txtPlayer1Name.BackColor = System.Drawing.Color.FromArgb(15, 52, 96); // #0F3460
            this.txtPlayer1Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPlayer1Name.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtPlayer1Name.ForeColor = System.Drawing.Color.FromArgb(234, 234, 234);
            this.txtPlayer1Name.Location = new System.Drawing.Point(30, 90);
            this.txtPlayer1Name.Name = "txtPlayer1Name";
            this.txtPlayer1Name.Size = new System.Drawing.Size(400, 27);
            this.txtPlayer1Name.TabIndex = 2;

            // lblPlayer2
            this.lblPlayer2.AutoSize = true;
            this.lblPlayer2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPlayer2.ForeColor = System.Drawing.Color.FromArgb(245, 166, 35); // Gold
            this.lblPlayer2.Location = new System.Drawing.Point(30, 135);
            this.lblPlayer2.Name = "lblPlayer2";
            this.lblPlayer2.Size = new System.Drawing.Size(125, 19);
            this.lblPlayer2.TabIndex = 3;
            this.lblPlayer2.Text = "👤 Nama Player 2:";

            // txtPlayer2Name
            this.txtPlayer2Name.BackColor = System.Drawing.Color.FromArgb(15, 52, 96); // #0F3460
            this.txtPlayer2Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPlayer2Name.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtPlayer2Name.ForeColor = System.Drawing.Color.FromArgb(234, 234, 234);
            this.txtPlayer2Name.Location = new System.Drawing.Point(30, 160);
            this.txtPlayer2Name.Name = "txtPlayer2Name";
            this.txtPlayer2Name.Size = new System.Drawing.Size(400, 27);
            this.txtPlayer2Name.TabIndex = 4;

            // lblError
            this.lblError.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(233, 69, 96); // Merah
            this.lblError.Location = new System.Drawing.Point(30, 195);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(400, 22);
            this.lblError.TabIndex = 5;
            this.lblError.Text = "";
            this.lblError.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // btnNext
            this.btnNext.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnNext.Location = new System.Drawing.Point(30, 230);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(190, 48);
            this.btnNext.TabIndex = 6;
            this.btnNext.Text = "➡️ LANJUT";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);

            // btnCancel
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Location = new System.Drawing.Point(240, 230);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(190, 48);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "↩️ KEMBALI";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.panelDialog.Controls.Add(this.lblTitle);
            this.panelDialog.Controls.Add(this.lblPlayer1);
            this.panelDialog.Controls.Add(this.txtPlayer1Name);
            this.panelDialog.Controls.Add(this.lblPlayer2);
            this.panelDialog.Controls.Add(this.txtPlayer2Name);
            this.panelDialog.Controls.Add(this.lblError);
            this.panelDialog.Controls.Add(this.btnNext);
            this.panelDialog.Controls.Add(this.btnCancel);

            // ===== FORM SETTINGS =====
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(26, 26, 46); // #1A1A2E
            this.ClientSize = new System.Drawing.Size(540, 390);
            this.Controls.Add(this.panelDialog);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "PlayerNameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Anomaly Versus - Registrasi Pemain";
            this.Load += new System.EventHandler(this.PlayerNameForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.PlayerNameForm_Paint);
            this.panelDialog.ResumeLayout(false);
            this.panelDialog.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelDialog;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblPlayer1;
        private System.Windows.Forms.TextBox txtPlayer1Name;
        private System.Windows.Forms.Label lblPlayer2;
        private System.Windows.Forms.TextBox txtPlayer2Name;
        private System.Windows.Forms.Label lblError;
        private WinFormsApp1.UI.RPGButton btnNext;
        private WinFormsApp1.UI.RPGButton btnCancel;
    }
}
