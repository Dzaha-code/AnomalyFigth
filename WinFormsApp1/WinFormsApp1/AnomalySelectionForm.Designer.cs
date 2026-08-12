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
            this.labelTitle = new System.Windows.Forms.Label();
            this.listBoxAnomalies = new System.Windows.Forms.ListBox();
            this.btnSelectPlayer1 = new System.Windows.Forms.Button();
            this.btnSelectPlayer2 = new System.Windows.Forms.Button();
            this.lblPlayer1Selected = new System.Windows.Forms.Label();
            this.lblPlayer2Selected = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblAnomalyCount = new System.Windows.Forms.Label();
            this.pictureBoxAnomaly = new System.Windows.Forms.PictureBox();
            this.lblAnomalyDetail = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAnomaly)).BeginInit();
            this.SuspendLayout();

            // labelTitle
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.Navy;
            this.labelTitle.Location = new System.Drawing.Point(20, 10);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(300, 24);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Pilih Anomaly Untuk Tiap Pemain";

            // listBoxAnomalies
            this.listBoxAnomalies.Font = new System.Drawing.Font("Arial", 10F);
            this.listBoxAnomalies.FormattingEnabled = true;
            this.listBoxAnomalies.ItemHeight = 16;
            this.listBoxAnomalies.Location = new System.Drawing.Point(20, 50);
            this.listBoxAnomalies.Name = "listBoxAnomalies";
            this.listBoxAnomalies.Size = new System.Drawing.Size(400, 180);
            this.listBoxAnomalies.TabIndex = 1;
            this.listBoxAnomalies.SelectedIndexChanged += new System.EventHandler(this.listBoxAnomalies_SelectedIndexChanged);

            // pictureBoxAnomaly — Preview gambar anomaly yang dipilih
            this.pictureBoxAnomaly.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.pictureBoxAnomaly.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxAnomaly.Location = new System.Drawing.Point(440, 50);
            this.pictureBoxAnomaly.Name = "pictureBoxAnomaly";
            this.pictureBoxAnomaly.Size = new System.Drawing.Size(200, 200);
            this.pictureBoxAnomaly.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxAnomaly.TabIndex = 11;
            this.pictureBoxAnomaly.TabStop = false;

            // lblAnomalyDetail — Detail stats anomaly yang dipilih
            this.lblAnomalyDetail.Font = new System.Drawing.Font("Arial", 9F);
            this.lblAnomalyDetail.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblAnomalyDetail.Location = new System.Drawing.Point(440, 260);
            this.lblAnomalyDetail.Name = "lblAnomalyDetail";
            this.lblAnomalyDetail.Size = new System.Drawing.Size(200, 80);
            this.lblAnomalyDetail.TabIndex = 12;
            this.lblAnomalyDetail.Text = "Klik anomaly di list untuk melihat detail...";

            // lblAnomalyCount
            this.lblAnomalyCount.AutoSize = true;
            this.lblAnomalyCount.Font = new System.Drawing.Font("Arial", 9F);
            this.lblAnomalyCount.Location = new System.Drawing.Point(20, 235);
            this.lblAnomalyCount.Name = "lblAnomalyCount";
            this.lblAnomalyCount.Size = new System.Drawing.Size(100, 15);
            this.lblAnomalyCount.TabIndex = 10;
            this.lblAnomalyCount.Text = "Total Anomaly: 0";

            // btnSelectPlayer1
            this.btnSelectPlayer1.Font = new System.Drawing.Font("Arial", 10F);
            this.btnSelectPlayer1.Location = new System.Drawing.Point(20, 260);
            this.btnSelectPlayer1.Name = "btnSelectPlayer1";
            this.btnSelectPlayer1.Size = new System.Drawing.Size(130, 35);
            this.btnSelectPlayer1.TabIndex = 2;
            this.btnSelectPlayer1.Text = "Pilih Player 1";
            this.btnSelectPlayer1.UseVisualStyleBackColor = true;
            this.btnSelectPlayer1.Click += new System.EventHandler(this.btnSelectPlayer1_Click);

            // btnSelectPlayer2
            this.btnSelectPlayer2.Font = new System.Drawing.Font("Arial", 10F);
            this.btnSelectPlayer2.Location = new System.Drawing.Point(160, 260);
            this.btnSelectPlayer2.Name = "btnSelectPlayer2";
            this.btnSelectPlayer2.Size = new System.Drawing.Size(130, 35);
            this.btnSelectPlayer2.TabIndex = 3;
            this.btnSelectPlayer2.Text = "Pilih Player 2";
            this.btnSelectPlayer2.UseVisualStyleBackColor = true;
            this.btnSelectPlayer2.Click += new System.EventHandler(this.btnSelectPlayer2_Click);

            // lblPlayer1Selected
            this.lblPlayer1Selected.AutoSize = true;
            this.lblPlayer1Selected.Font = new System.Drawing.Font("Arial", 10F);
            this.lblPlayer1Selected.ForeColor = System.Drawing.Color.Gray;
            this.lblPlayer1Selected.Location = new System.Drawing.Point(20, 305);
            this.lblPlayer1Selected.Name = "lblPlayer1Selected";
            this.lblPlayer1Selected.Size = new System.Drawing.Size(130, 16);
            this.lblPlayer1Selected.TabIndex = 4;
            this.lblPlayer1Selected.Text = "Player 1: (belum dipilih)";

            // lblPlayer2Selected
            this.lblPlayer2Selected.AutoSize = true;
            this.lblPlayer2Selected.Font = new System.Drawing.Font("Arial", 10F);
            this.lblPlayer2Selected.ForeColor = System.Drawing.Color.Gray;
            this.lblPlayer2Selected.Location = new System.Drawing.Point(20, 325);
            this.lblPlayer2Selected.Name = "lblPlayer2Selected";
            this.lblPlayer2Selected.Size = new System.Drawing.Size(130, 16);
            this.lblPlayer2Selected.TabIndex = 5;
            this.lblPlayer2Selected.Text = "Player 2: (belum dipilih)";

            // btnNext
            this.btnNext.Font = new System.Drawing.Font("Arial", 10F);
            this.btnNext.Location = new System.Drawing.Point(300, 260);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(130, 35);
            this.btnNext.TabIndex = 6;
            this.btnNext.Text = "Lanjut";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);

            // btnBack
            this.btnBack.Font = new System.Drawing.Font("Arial", 10F);
            this.btnBack.Location = new System.Drawing.Point(300, 305);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(130, 35);
            this.btnBack.TabIndex = 7;
            this.btnBack.Text = "Kembali";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);

            // AnomalySelectionForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 360);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.listBoxAnomalies);
            this.Controls.Add(this.pictureBoxAnomaly);
            this.Controls.Add(this.lblAnomalyDetail);
            this.Controls.Add(this.lblAnomalyCount);
            this.Controls.Add(this.btnSelectPlayer1);
            this.Controls.Add(this.btnSelectPlayer2);
            this.Controls.Add(this.lblPlayer1Selected);
            this.Controls.Add(this.lblPlayer2Selected);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnBack);
            this.Name = "AnomalySelectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pemilihan Anomaly";
            this.Load += new System.EventHandler(this.AnomalySelectionForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAnomaly)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.ListBox listBoxAnomalies;
        private System.Windows.Forms.Button btnSelectPlayer1;
        private System.Windows.Forms.Button btnSelectPlayer2;
        private System.Windows.Forms.Label lblPlayer1Selected;
        private System.Windows.Forms.Label lblPlayer2Selected;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lblAnomalyCount;
        private System.Windows.Forms.PictureBox pictureBoxAnomaly;
        private System.Windows.Forms.Label lblAnomalyDetail;
    }
}
