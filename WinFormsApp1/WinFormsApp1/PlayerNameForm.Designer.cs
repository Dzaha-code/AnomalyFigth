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
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelPlayer1 = new System.Windows.Forms.Label();
            this.txtPlayer1Name = new System.Windows.Forms.TextBox();
            this.labelPlayer2 = new System.Windows.Forms.Label();
            this.txtPlayer2Name = new System.Windows.Forms.TextBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // labelTitle
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.Navy;
            this.labelTitle.Location = new System.Drawing.Point(50, 30);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(300, 24);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Masukkan Nama Pemain";

            // labelPlayer1
            this.labelPlayer1.AutoSize = true;
            this.labelPlayer1.Font = new System.Drawing.Font("Arial", 12F);
            this.labelPlayer1.Location = new System.Drawing.Point(50, 80);
            this.labelPlayer1.Name = "labelPlayer1";
            this.labelPlayer1.Size = new System.Drawing.Size(80, 18);
            this.labelPlayer1.TabIndex = 1;
            this.labelPlayer1.Text = "Player 1:";

            // txtPlayer1Name
            this.txtPlayer1Name.Font = new System.Drawing.Font("Arial", 12F);
            this.txtPlayer1Name.Location = new System.Drawing.Point(140, 75);
            this.txtPlayer1Name.Name = "txtPlayer1Name";
            this.txtPlayer1Name.Size = new System.Drawing.Size(250, 26);
            this.txtPlayer1Name.TabIndex = 2;

            // labelPlayer2
            this.labelPlayer2.AutoSize = true;
            this.labelPlayer2.Font = new System.Drawing.Font("Arial", 12F);
            this.labelPlayer2.Location = new System.Drawing.Point(50, 150);
            this.labelPlayer2.Name = "labelPlayer2";
            this.labelPlayer2.Size = new System.Drawing.Size(80, 18);
            this.labelPlayer2.TabIndex = 3;
            this.labelPlayer2.Text = "Player 2:";

            // txtPlayer2Name
            this.txtPlayer2Name.Font = new System.Drawing.Font("Arial", 12F);
            this.txtPlayer2Name.Location = new System.Drawing.Point(140, 145);
            this.txtPlayer2Name.Name = "txtPlayer2Name";
            this.txtPlayer2Name.Size = new System.Drawing.Size(250, 26);
            this.txtPlayer2Name.TabIndex = 4;

            // btnNext
            this.btnNext.Font = new System.Drawing.Font("Arial", 12F);
            this.btnNext.Location = new System.Drawing.Point(140, 220);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(100, 40);
            this.btnNext.TabIndex = 5;
            this.btnNext.Text = "Lanjut";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);

            // btnCancel
            this.btnCancel.Font = new System.Drawing.Font("Arial", 12F);
            this.btnCancel.Location = new System.Drawing.Point(290, 220);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 40);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Batal";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // PlayerNameForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 300);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.labelPlayer1);
            this.Controls.Add(this.txtPlayer1Name);
            this.Controls.Add(this.labelPlayer2);
            this.Controls.Add(this.txtPlayer2Name);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnCancel);
            this.Name = "PlayerNameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Input Nama Pemain";
            this.Load += new System.EventHandler(this.PlayerNameForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelPlayer1;
        private System.Windows.Forms.TextBox txtPlayer1Name;
        private System.Windows.Forms.Label labelPlayer2;
        private System.Windows.Forms.TextBox txtPlayer2Name;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnCancel;
    }
}
