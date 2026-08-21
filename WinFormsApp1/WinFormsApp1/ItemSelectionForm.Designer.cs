namespace WinFormsApp1
{
    partial class ItemSelectionForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblInstruction = new System.Windows.Forms.Label();
            this.lblCounter = new System.Windows.Forms.Label();
            this.flowPanelItems = new System.Windows.Forms.FlowLayoutPanel();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnConfirm = new WinFormsApp1.UI.RPGButton();
            this.btnStartBattle = new WinFormsApp1.UI.RPGButton();
            this.btnBack = new WinFormsApp1.UI.RPGButton();
            this.lblPlayer1Status = new System.Windows.Forms.Label();
            this.lblPlayer2Status = new System.Windows.Forms.Label();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();

            // ===== TITLE =====
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(245, 166, 35); // Gold
            this.lblTitle.Location = new System.Drawing.Point(12, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(860, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🎒 PEMILIHAN ITEM SUPPORT 🎒";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // ===== INSTRUCTION =====
            this.lblInstruction.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblInstruction.ForeColor = System.Drawing.Color.FromArgb(233, 69, 96); // Merah-pink
            this.lblInstruction.Location = new System.Drawing.Point(15, 45);
            this.lblInstruction.Name = "lblInstruction";
            this.lblInstruction.Size = new System.Drawing.Size(550, 25);
            this.lblInstruction.TabIndex = 1;
            this.lblInstruction.Text = "👉 Giliran Player 1: Pilih 2 Item Support";

            // ===== COUNTER BESAR (ANIMATED) =====
            this.lblCounter.Font = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Bold);
            this.lblCounter.ForeColor = System.Drawing.Color.FromArgb(245, 166, 35); // Gold
            this.lblCounter.Location = new System.Drawing.Point(670, 42);
            this.lblCounter.Name = "lblCounter";
            this.lblCounter.Size = new System.Drawing.Size(200, 28);
            this.lblCounter.TabIndex = 2;
            this.lblCounter.Text = "TERPILIH: [ 0 / 2 ]";
            this.lblCounter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // ===== FLOW PANEL ITEM CARDS (GRID) =====
            this.flowPanelItems.AutoScroll = true;
            this.flowPanelItems.BackColor = System.Drawing.Color.FromArgb(22, 33, 62); // #16213E
            this.flowPanelItems.Location = new System.Drawing.Point(15, 75);
            this.flowPanelItems.Name = "flowPanelItems";
            this.flowPanelItems.Padding = new System.Windows.Forms.Padding(10);
            this.flowPanelItems.Size = new System.Drawing.Size(855, 340);
            this.flowPanelItems.TabIndex = 3;

            // ===== PANEL BOTTOM =====
            this.panelBottom.BackColor = System.Drawing.Color.FromArgb(22, 33, 62);
            this.panelBottom.Location = new System.Drawing.Point(15, 425);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(855, 110);
            this.panelBottom.TabIndex = 4;
            this.panelBottom.Paint += new System.Windows.Forms.PaintEventHandler(this.panelBottom_Paint);

            // lblPlayer1Status
            this.lblPlayer1Status.AutoSize = true;
            this.lblPlayer1Status.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblPlayer1Status.ForeColor = System.Drawing.Color.FromArgb(168, 178, 216);
            this.lblPlayer1Status.Location = new System.Drawing.Point(20, 15);
            this.lblPlayer1Status.Name = "lblPlayer1Status";
            this.lblPlayer1Status.Size = new System.Drawing.Size(185, 17);
            this.lblPlayer1Status.TabIndex = 0;
            this.lblPlayer1Status.Text = "Player 1: (Belum konfirmasi)";

            // lblPlayer2Status
            this.lblPlayer2Status.AutoSize = true;
            this.lblPlayer2Status.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblPlayer2Status.ForeColor = System.Drawing.Color.FromArgb(168, 178, 216);
            this.lblPlayer2Status.Location = new System.Drawing.Point(20, 45);
            this.lblPlayer2Status.Name = "lblPlayer2Status";
            this.lblPlayer2Status.Size = new System.Drawing.Size(185, 17);
            this.lblPlayer2Status.TabIndex = 1;
            this.lblPlayer2Status.Text = "Player 2: (Belum konfirmasi)";

            // btnConfirm
            this.btnConfirm.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnConfirm.Location = new System.Drawing.Point(400, 20);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(210, 65);
            this.btnConfirm.TabIndex = 2;
            this.btnConfirm.Text = "✓ KONFIRMASI (P1)";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);

            // btnStartBattle
            this.btnStartBattle.Enabled = false;
            this.btnStartBattle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnStartBattle.Location = new System.Drawing.Point(625, 20);
            this.btnStartBattle.Name = "btnStartBattle";
            this.btnStartBattle.Size = new System.Drawing.Size(215, 65);
            this.btnStartBattle.TabIndex = 3;
            this.btnStartBattle.Text = "⚔️ MULAI BATTLE!";
            this.btnStartBattle.Click += new System.EventHandler(this.btnStartBattle_Click);

            // btnBack
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBack.Location = new System.Drawing.Point(20, 75);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(110, 28);
            this.btnBack.TabIndex = 4;
            this.btnBack.Text = "↩️ KEMBALI";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);

            this.panelBottom.Controls.Add(this.lblPlayer1Status);
            this.panelBottom.Controls.Add(this.lblPlayer2Status);
            this.panelBottom.Controls.Add(this.btnConfirm);
            this.panelBottom.Controls.Add(this.btnStartBattle);
            this.panelBottom.Controls.Add(this.btnBack);

            // ===== FORM SETTINGS =====
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(26, 26, 46); // #1A1A2E
            this.ClientSize = new System.Drawing.Size(884, 545);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblInstruction);
            this.Controls.Add(this.lblCounter);
            this.Controls.Add(this.flowPanelItems);
            this.Controls.Add(this.panelBottom);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ItemSelectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Anomaly Versus - Pemilihan Item";
            this.Load += new System.EventHandler(this.ItemSelectionForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ItemSelectionForm_Paint);
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblInstruction;
        private System.Windows.Forms.Label lblCounter;
        private System.Windows.Forms.FlowLayoutPanel flowPanelItems;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Label lblPlayer1Status;
        private System.Windows.Forms.Label lblPlayer2Status;
        private WinFormsApp1.UI.RPGButton btnConfirm;
        private WinFormsApp1.UI.RPGButton btnStartBattle;
        private WinFormsApp1.UI.RPGButton btnBack;
    }
}
