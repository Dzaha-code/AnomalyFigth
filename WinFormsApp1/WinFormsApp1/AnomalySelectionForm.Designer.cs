namespace WinFormsApp1
{
    partial class AnomalySelectionForm
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
            this.panelList = new System.Windows.Forms.Panel();
            this.lblListHeader = new System.Windows.Forms.Label();
            this.listBoxAnomalies = new System.Windows.Forms.ListBox();
            this.panelDetail = new System.Windows.Forms.Panel();
            this.lblDetailHeader = new System.Windows.Forms.Label();
            this.lblAnomalyName = new System.Windows.Forms.Label();
            this.lblAnomalyRole = new System.Windows.Forms.Label();
            this.lblStats = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.panelSkillCard1 = new System.Windows.Forms.Panel();
            this.lblSkill1 = new System.Windows.Forms.Label();
            this.panelSkillCard2 = new System.Windows.Forms.Panel();
            this.lblSkill2 = new System.Windows.Forms.Label();
            this.panelPreview = new System.Windows.Forms.Panel();
            this.pictureBoxAnomaly = new System.Windows.Forms.PictureBox();
            this.lblPreviewHeader = new System.Windows.Forms.Label();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnSelectPlayer1 = new WinFormsApp1.UI.RPGButton();
            this.btnSelectPlayer2 = new WinFormsApp1.UI.RPGButton();
            this.lblPlayer1Selected = new System.Windows.Forms.Label();
            this.lblPlayer2Selected = new System.Windows.Forms.Label();
            this.btnNext = new WinFormsApp1.UI.RPGButton();
            this.btnBack = new WinFormsApp1.UI.RPGButton();
            this.panelList.SuspendLayout();
            this.panelDetail.SuspendLayout();
            this.panelSkillCard1.SuspendLayout();
            this.panelSkillCard2.SuspendLayout();
            this.panelPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAnomaly)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();

            // ===== HEADER TITLE =====
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(245, 166, 35); // Gold
            this.lblTitle.Location = new System.Drawing.Point(12, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(896, 35);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🔮 PILIH ANOMALY UNTUK TIAP PEMAIN 🔮";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // ===== 1. PANEL LIST KIRI =====
            this.panelList.BackColor = System.Drawing.Color.FromArgb(22, 33, 62);
            this.panelList.Location = new System.Drawing.Point(15, 55);
            this.panelList.Name = "panelList";
            this.panelList.Size = new System.Drawing.Size(270, 370);
            this.panelList.TabIndex = 1;
            this.panelList.Paint += new System.Windows.Forms.PaintEventHandler(this.panelList_Paint);

            this.lblListHeader.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblListHeader.ForeColor = System.Drawing.Color.FromArgb(234, 234, 234);
            this.lblListHeader.Location = new System.Drawing.Point(10, 8);
            this.lblListHeader.Name = "lblListHeader";
            this.lblListHeader.Size = new System.Drawing.Size(250, 22);
            this.lblListHeader.TabIndex = 0;
            this.lblListHeader.Text = "📜 DAFTAR ANOMALY";

            this.listBoxAnomalies.BackColor = System.Drawing.Color.FromArgb(15, 52, 96);
            this.listBoxAnomalies.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxAnomalies.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBoxAnomalies.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.listBoxAnomalies.ForeColor = System.Drawing.Color.FromArgb(234, 234, 234);
            this.listBoxAnomalies.FormattingEnabled = true;
            this.listBoxAnomalies.ItemHeight = 28;
            this.listBoxAnomalies.Location = new System.Drawing.Point(10, 35);
            this.listBoxAnomalies.Name = "listBoxAnomalies";
            this.listBoxAnomalies.Size = new System.Drawing.Size(250, 320);
            this.listBoxAnomalies.TabIndex = 1;
            this.listBoxAnomalies.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBoxAnomalies_DrawItem);
            this.listBoxAnomalies.SelectedIndexChanged += new System.EventHandler(this.listBoxAnomalies_SelectedIndexChanged);

            this.panelList.Controls.Add(this.lblListHeader);
            this.panelList.Controls.Add(this.listBoxAnomalies);

            // ===== 2. PANEL DETAIL TENGAH =====
            this.panelDetail.BackColor = System.Drawing.Color.FromArgb(22, 33, 62);
            this.panelDetail.Location = new System.Drawing.Point(300, 55);
            this.panelDetail.Name = "panelDetail";
            this.panelDetail.Size = new System.Drawing.Size(340, 370);
            this.panelDetail.TabIndex = 2;
            this.panelDetail.Paint += new System.Windows.Forms.PaintEventHandler(this.panelDetail_Paint);

            this.lblDetailHeader.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDetailHeader.ForeColor = System.Drawing.Color.FromArgb(234, 234, 234);
            this.lblDetailHeader.Location = new System.Drawing.Point(12, 8);
            this.lblDetailHeader.Name = "lblDetailHeader";
            this.lblDetailHeader.Size = new System.Drawing.Size(315, 22);
            this.lblDetailHeader.TabIndex = 0;
            this.lblDetailHeader.Text = "📊 INFORMASI & STATISTIK";

            this.lblAnomalyName.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblAnomalyName.ForeColor = System.Drawing.Color.FromArgb(245, 166, 35);
            this.lblAnomalyName.Location = new System.Drawing.Point(12, 33);
            this.lblAnomalyName.Name = "lblAnomalyName";
            this.lblAnomalyName.Size = new System.Drawing.Size(315, 26);
            this.lblAnomalyName.TabIndex = 1;
            this.lblAnomalyName.Text = "Nama Anomaly";

            this.lblAnomalyRole.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblAnomalyRole.ForeColor = System.Drawing.Color.FromArgb(233, 69, 96);
            this.lblAnomalyRole.Location = new System.Drawing.Point(12, 59);
            this.lblAnomalyRole.Name = "lblAnomalyRole";
            this.lblAnomalyRole.Size = new System.Drawing.Size(315, 20);
            this.lblAnomalyRole.TabIndex = 2;
            this.lblAnomalyRole.Text = "Role: Attacker";

            this.lblStats.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
            this.lblStats.ForeColor = System.Drawing.Color.FromArgb(168, 178, 216);
            this.lblStats.Location = new System.Drawing.Point(12, 82);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(315, 42);
            this.lblStats.TabIndex = 3;
            this.lblStats.Text = "HP: 100  ATK: 25\nDEF: 10  SPD: 15";

            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblDescription.ForeColor = System.Drawing.Color.FromArgb(220, 220, 230);
            this.lblDescription.Location = new System.Drawing.Point(12, 128);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(315, 65);
            this.lblDescription.TabIndex = 4;
            this.lblDescription.Text = "Deskripsi Anomaly...";

            // Skill Card 1
            this.panelSkillCard1.BackColor = System.Drawing.Color.FromArgb(15, 52, 96);
            this.panelSkillCard1.Location = new System.Drawing.Point(12, 205);
            this.panelSkillCard1.Name = "panelSkillCard1";
            this.panelSkillCard1.Size = new System.Drawing.Size(315, 70);
            this.panelSkillCard1.TabIndex = 5;
            this.panelSkillCard1.Paint += new System.Windows.Forms.PaintEventHandler(this.panelSkillCard1_Paint);

            this.lblSkill1.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSkill1.ForeColor = System.Drawing.Color.FromArgb(234, 234, 234);
            this.lblSkill1.Location = new System.Drawing.Point(8, 6);
            this.lblSkill1.Name = "lblSkill1";
            this.lblSkill1.Size = new System.Drawing.Size(300, 58);
            this.lblSkill1.TabIndex = 0;
            this.lblSkill1.Text = "✨ Skill 1: -\nPower: 1.0x | Cooldown: 2 turn";

            this.panelSkillCard1.Controls.Add(this.lblSkill1);

            // Skill Card 2
            this.panelSkillCard2.BackColor = System.Drawing.Color.FromArgb(15, 52, 96);
            this.panelSkillCard2.Location = new System.Drawing.Point(12, 285);
            this.panelSkillCard2.Name = "panelSkillCard2";
            this.panelSkillCard2.Size = new System.Drawing.Size(315, 70);
            this.panelSkillCard2.TabIndex = 6;
            this.panelSkillCard2.Paint += new System.Windows.Forms.PaintEventHandler(this.panelSkillCard2_Paint);

            this.lblSkill2.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSkill2.ForeColor = System.Drawing.Color.FromArgb(234, 234, 234);
            this.lblSkill2.Location = new System.Drawing.Point(8, 6);
            this.lblSkill2.Name = "lblSkill2";
            this.lblSkill2.Size = new System.Drawing.Size(300, 58);
            this.lblSkill2.TabIndex = 0;
            this.lblSkill2.Text = "✨ Skill 2: -\nPower: 1.0x | Cooldown: 3 turn";

            this.panelSkillCard2.Controls.Add(this.lblSkill2);

            this.panelDetail.Controls.Add(this.lblDetailHeader);
            this.panelDetail.Controls.Add(this.lblAnomalyName);
            this.panelDetail.Controls.Add(this.lblAnomalyRole);
            this.panelDetail.Controls.Add(this.lblStats);
            this.panelDetail.Controls.Add(this.lblDescription);
            this.panelDetail.Controls.Add(this.panelSkillCard1);
            this.panelDetail.Controls.Add(this.panelSkillCard2);

            // ===== 3. PANEL PREVIEW KANAN =====
            this.panelPreview.BackColor = System.Drawing.Color.FromArgb(22, 33, 62);
            this.panelPreview.Location = new System.Drawing.Point(655, 55);
            this.panelPreview.Name = "panelPreview";
            this.panelPreview.Size = new System.Drawing.Size(250, 370);
            this.panelPreview.TabIndex = 3;
            this.panelPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.panelPreview_Paint);

            this.lblPreviewHeader.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPreviewHeader.ForeColor = System.Drawing.Color.FromArgb(234, 234, 234);
            this.lblPreviewHeader.Location = new System.Drawing.Point(10, 8);
            this.lblPreviewHeader.Name = "lblPreviewHeader";
            this.lblPreviewHeader.Size = new System.Drawing.Size(230, 22);
            this.lblPreviewHeader.TabIndex = 0;
            this.lblPreviewHeader.Text = "🖼️ AVATAR ANOMALY";
            this.lblPreviewHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.pictureBoxAnomaly.BackColor = System.Drawing.Color.FromArgb(15, 52, 96);
            this.pictureBoxAnomaly.Location = new System.Drawing.Point(25, 45);
            this.pictureBoxAnomaly.Name = "pictureBoxAnomaly";
            this.pictureBoxAnomaly.Size = new System.Drawing.Size(200, 200);
            this.pictureBoxAnomaly.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxAnomaly.TabIndex = 1;
            this.pictureBoxAnomaly.TabStop = false;
            this.pictureBoxAnomaly.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxAnomaly_Paint);

            this.panelPreview.Controls.Add(this.lblPreviewHeader);
            this.panelPreview.Controls.Add(this.pictureBoxAnomaly);

            // ===== 4. PANEL BOTTOM (ACTIONS & SELECTIONS) =====
            this.panelBottom.BackColor = System.Drawing.Color.FromArgb(22, 33, 62);
            this.panelBottom.Location = new System.Drawing.Point(15, 440);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(890, 115);
            this.panelBottom.TabIndex = 4;
            this.panelBottom.Paint += new System.Windows.Forms.PaintEventHandler(this.panelBottom_Paint);

            // btnSelectPlayer1
            this.btnSelectPlayer1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSelectPlayer1.Location = new System.Drawing.Point(15, 12);
            this.btnSelectPlayer1.Name = "btnSelectPlayer1";
            this.btnSelectPlayer1.Size = new System.Drawing.Size(220, 42);
            this.btnSelectPlayer1.TabIndex = 0;
            this.btnSelectPlayer1.Text = "👤 PILIH UNTUK PLAYER 1";
            this.btnSelectPlayer1.Click += new System.EventHandler(this.btnSelectPlayer1_Click);

            // btnSelectPlayer2
            this.btnSelectPlayer2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSelectPlayer2.Location = new System.Drawing.Point(250, 12);
            this.btnSelectPlayer2.Name = "btnSelectPlayer2";
            this.btnSelectPlayer2.Size = new System.Drawing.Size(220, 42);
            this.btnSelectPlayer2.TabIndex = 1;
            this.btnSelectPlayer2.Text = "👤 PILIH UNTUK PLAYER 2";
            this.btnSelectPlayer2.Click += new System.EventHandler(this.btnSelectPlayer2_Click);

            // lblPlayer1Selected
            this.lblPlayer1Selected.AutoSize = true;
            this.lblPlayer1Selected.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblPlayer1Selected.ForeColor = System.Drawing.Color.FromArgb(168, 178, 216);
            this.lblPlayer1Selected.Location = new System.Drawing.Point(15, 62);
            this.lblPlayer1Selected.Name = "lblPlayer1Selected";
            this.lblPlayer1Selected.Size = new System.Drawing.Size(180, 17);
            this.lblPlayer1Selected.TabIndex = 2;
            this.lblPlayer1Selected.Text = "Player 1: (Belum memilih)";

            // lblPlayer2Selected
            this.lblPlayer2Selected.AutoSize = true;
            this.lblPlayer2Selected.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblPlayer2Selected.ForeColor = System.Drawing.Color.FromArgb(168, 178, 216);
            this.lblPlayer2Selected.Location = new System.Drawing.Point(250, 62);
            this.lblPlayer2Selected.Name = "lblPlayer2Selected";
            this.lblPlayer2Selected.Size = new System.Drawing.Size(180, 17);
            this.lblPlayer2Selected.TabIndex = 3;
            this.lblPlayer2Selected.Text = "Player 2: (Belum memilih)";

            // btnNext
            this.btnNext.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnNext.Location = new System.Drawing.Point(540, 20);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(160, 65);
            this.btnNext.TabIndex = 4;
            this.btnNext.Text = "➡️ LANJUT KE ITEM";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);

            // btnBack
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBack.Location = new System.Drawing.Point(715, 20);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(155, 65);
            this.btnBack.TabIndex = 5;
            this.btnBack.Text = "↩️ KEMBALI";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);

            this.panelBottom.Controls.Add(this.btnSelectPlayer1);
            this.panelBottom.Controls.Add(this.btnSelectPlayer2);
            this.panelBottom.Controls.Add(this.lblPlayer1Selected);
            this.panelBottom.Controls.Add(this.lblPlayer2Selected);
            this.panelBottom.Controls.Add(this.btnNext);
            this.panelBottom.Controls.Add(this.btnBack);

            // ===== ANOMALY SELECTION FORM =====
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(26, 26, 46); // #1A1A2E
            this.ClientSize = new System.Drawing.Size(920, 565);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.panelList);
            this.Controls.Add(this.panelDetail);
            this.Controls.Add(this.panelPreview);
            this.Controls.Add(this.panelBottom);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AnomalySelectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Anomaly Versus - Pemilihan Anomaly";
            this.Load += new System.EventHandler(this.AnomalySelectionForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.AnomalySelectionForm_Paint);
            this.panelList.ResumeLayout(false);
            this.panelDetail.ResumeLayout(false);
            this.panelSkillCard1.ResumeLayout(false);
            this.panelSkillCard2.ResumeLayout(false);
            this.panelPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAnomaly)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelList;
        private System.Windows.Forms.Label lblListHeader;
        private System.Windows.Forms.ListBox listBoxAnomalies;
        private System.Windows.Forms.Panel panelDetail;
        private System.Windows.Forms.Label lblDetailHeader;
        private System.Windows.Forms.Label lblAnomalyName;
        private System.Windows.Forms.Label lblAnomalyRole;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Panel panelSkillCard1;
        private System.Windows.Forms.Label lblSkill1;
        private System.Windows.Forms.Panel panelSkillCard2;
        private System.Windows.Forms.Label lblSkill2;
        private System.Windows.Forms.Panel panelPreview;
        private System.Windows.Forms.Label lblPreviewHeader;
        private System.Windows.Forms.PictureBox pictureBoxAnomaly;
        private System.Windows.Forms.Panel panelBottom;
        private WinFormsApp1.UI.RPGButton btnSelectPlayer1;
        private WinFormsApp1.UI.RPGButton btnSelectPlayer2;
        private System.Windows.Forms.Label lblPlayer1Selected;
        private System.Windows.Forms.Label lblPlayer2Selected;
        private WinFormsApp1.UI.RPGButton btnNext;
        private WinFormsApp1.UI.RPGButton btnBack;
    }
}
