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
            this.labelTitle = new System.Windows.Forms.Label();
            this.lblInstruction = new System.Windows.Forms.Label();
            this.checkedListBoxItems = new System.Windows.Forms.CheckedListBox();
            this.lblItemCount = new System.Windows.Forms.Label();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblPlayer1Items = new System.Windows.Forms.Label();
            this.lblPlayer2Items = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // labelTitle
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.Navy;
            this.labelTitle.Location = new System.Drawing.Point(20, 10);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(350, 24);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Pilih 2 Item Support Untuk Tiap Player";

            // lblInstruction
            this.lblInstruction.AutoSize = true;
            this.lblInstruction.Font = new System.Drawing.Font("Arial", 10F);
            this.lblInstruction.ForeColor = System.Drawing.Color.Blue;
            this.lblInstruction.Location = new System.Drawing.Point(20, 40);
            this.lblInstruction.Name = "lblInstruction";
            this.lblInstruction.Size = new System.Drawing.Size(200, 16);
            this.lblInstruction.TabIndex = 1;
            this.lblInstruction.Text = "Pilih 2 item untuk Player 1...";

            // checkedListBoxItems
            this.checkedListBoxItems.Font = new System.Drawing.Font("Arial", 10F);
            this.checkedListBoxItems.FormattingEnabled = true;
            this.checkedListBoxItems.Location = new System.Drawing.Point(20, 65);
            this.checkedListBoxItems.Name = "checkedListBoxItems";
            this.checkedListBoxItems.Size = new System.Drawing.Size(540, 180);
            this.checkedListBoxItems.TabIndex = 2;

            // lblItemCount
            this.lblItemCount.AutoSize = true;
            this.lblItemCount.Font = new System.Drawing.Font("Arial", 9F);
            this.lblItemCount.Location = new System.Drawing.Point(20, 250);
            this.lblItemCount.Name = "lblItemCount";
            this.lblItemCount.Size = new System.Drawing.Size(80, 15);
            this.lblItemCount.TabIndex = 10;
            this.lblItemCount.Text = "Total Item: 0";

            // btnSelect
            this.btnSelect.Font = new System.Drawing.Font("Arial", 10F);
            this.btnSelect.Location = new System.Drawing.Point(20, 275);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(150, 35);
            this.btnSelect.TabIndex = 3;
            this.btnSelect.Text = "Pilih Untuk Player 1";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);

            // lblPlayer1Items
            this.lblPlayer1Items.AutoSize = true;
            this.lblPlayer1Items.Font = new System.Drawing.Font("Arial", 9F);
            this.lblPlayer1Items.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblPlayer1Items.Location = new System.Drawing.Point(20, 315);
            this.lblPlayer1Items.Name = "lblPlayer1Items";
            this.lblPlayer1Items.Size = new System.Drawing.Size(130, 15);
            this.lblPlayer1Items.TabIndex = 4;
            this.lblPlayer1Items.Text = "Player 1: 0 item dipilih";

            // lblPlayer2Items
            this.lblPlayer2Items.AutoSize = true;
            this.lblPlayer2Items.Font = new System.Drawing.Font("Arial", 9F);
            this.lblPlayer2Items.ForeColor = System.Drawing.Color.Purple;
            this.lblPlayer2Items.Location = new System.Drawing.Point(20, 335);
            this.lblPlayer2Items.Name = "lblPlayer2Items";
            this.lblPlayer2Items.Size = new System.Drawing.Size(130, 15);
            this.lblPlayer2Items.TabIndex = 5;
            this.lblPlayer2Items.Text = "Player 2: 0 item dipilih";

            // btnStart
            this.btnStart.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.btnStart.ForeColor = System.Drawing.Color.Green;
            this.btnStart.Location = new System.Drawing.Point(180, 275);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(150, 35);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "Mulai Battle";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Enabled = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);

            // btnBack
            this.btnBack.Font = new System.Drawing.Font("Arial", 10F);
            this.btnBack.Location = new System.Drawing.Point(440, 275);
            this.btnBack.Size = new System.Drawing.Size(120, 35);
            this.btnBack.TabIndex = 7;
            this.btnBack.Text = "Kembali";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);

            // ItemSelectionForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 360);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.lblInstruction);
            this.Controls.Add(this.checkedListBoxItems);
            this.Controls.Add(this.lblItemCount);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.lblPlayer1Items);
            this.Controls.Add(this.lblPlayer2Items);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnBack);
            this.Name = "ItemSelectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pemilihan Item Support";
            this.Load += new System.EventHandler(this.ItemSelectionForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label lblInstruction;
        private System.Windows.Forms.CheckedListBox checkedListBoxItems;
        private System.Windows.Forms.Label lblItemCount;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lblPlayer1Items;
        private System.Windows.Forms.Label lblPlayer2Items;
    }
}
