namespace WinFormsApp1
{
    partial class FormBattle
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
            // Panel P1 - Kiri
            this.panelP1 = new System.Windows.Forms.Panel();
            this.picP1 = new System.Windows.Forms.PictureBox();
            this.lblP1Name = new System.Windows.Forms.Label();
            this.lblP1Anomaly = new System.Windows.Forms.Label();
            this.lblHpP1 = new System.Windows.Forms.Label();
            this.pbHpP1 = new System.Windows.Forms.ProgressBar();

            // Panel P2 - Kanan
            this.panelP2 = new System.Windows.Forms.Panel();
            this.picP2 = new System.Windows.Forms.PictureBox();
            this.lblP2Name = new System.Windows.Forms.Label();
            this.lblP2Anomaly = new System.Windows.Forms.Label();
            this.lblHpP2 = new System.Windows.Forms.Label();
            this.pbHpP2 = new System.Windows.Forms.ProgressBar();

            // Panel tengah (log & giliran)
            this.panelCenter = new System.Windows.Forms.Panel();
            this.lstBattleLog = new System.Windows.Forms.ListBox();
            this.lblGiliran = new System.Windows.Forms.Label();

            // Panel aksi bawah
            this.panelAction = new System.Windows.Forms.Panel();
            this.btnAttack = new System.Windows.Forms.Button();
            this.cmbSkill = new System.Windows.Forms.ComboBox();
            this.btnSkill = new System.Windows.Forms.Button();
            this.btnDefend = new System.Windows.Forms.Button();
            this.cmbItem = new System.Windows.Forms.ComboBox();
            this.btnItem = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.picP1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picP2)).BeginInit();
            this.SuspendLayout();

            // ===== PANEL P1 (KIRI) =====
            this.panelP1.Location = new System.Drawing.Point(10, 10);
            this.panelP1.Name = "panelP1";
            this.panelP1.Size = new System.Drawing.Size(200, 400);
            this.panelP1.TabIndex = 0;
            this.panelP1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // picP1
            this.picP1.Location = new System.Drawing.Point(10, 10);
            this.picP1.Name = "picP1";
            this.picP1.Size = new System.Drawing.Size(180, 150);
            this.picP1.TabIndex = 1;
            this.picP1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;

            // lblP1Name
            this.lblP1Name.AutoSize = true;
            this.lblP1Name.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.lblP1Name.Location = new System.Drawing.Point(10, 170);
            this.lblP1Name.Name = "lblP1Name";
            this.lblP1Name.Size = new System.Drawing.Size(100, 18);
            this.lblP1Name.TabIndex = 2;
            this.lblP1Name.Text = "Player 1";

            // lblP1Anomaly
            this.lblP1Anomaly.AutoSize = true;
            this.lblP1Anomaly.Font = new System.Drawing.Font("Arial", 10F);
            this.lblP1Anomaly.ForeColor = System.Drawing.Color.Blue;
            this.lblP1Anomaly.Location = new System.Drawing.Point(10, 195);
            this.lblP1Anomaly.Name = "lblP1Anomaly";
            this.lblP1Anomaly.Size = new System.Drawing.Size(100, 16);
            this.lblP1Anomaly.TabIndex = 3;
            this.lblP1Anomaly.Text = "Anomaly Name";

            // pbHpP1
            this.pbHpP1.Location = new System.Drawing.Point(10, 220);
            this.pbHpP1.Name = "pbHpP1";
            this.pbHpP1.Size = new System.Drawing.Size(180, 25);
            this.pbHpP1.TabIndex = 4;
            this.pbHpP1.Value = 100;

            // lblHpP1
            this.lblHpP1.AutoSize = true;
            this.lblHpP1.Font = new System.Drawing.Font("Arial", 9F);
            this.lblHpP1.Location = new System.Drawing.Point(10, 250);
            this.lblHpP1.Name = "lblHpP1";
            this.lblHpP1.Size = new System.Drawing.Size(100, 15);
            this.lblHpP1.TabIndex = 5;
            this.lblHpP1.Text = "HP: 100 / 100";

            this.panelP1.Controls.Add(this.picP1);
            this.panelP1.Controls.Add(this.lblP1Name);
            this.panelP1.Controls.Add(this.lblP1Anomaly);
            this.panelP1.Controls.Add(this.pbHpP1);
            this.panelP1.Controls.Add(this.lblHpP1);

            // ===== PANEL P2 (KANAN) =====
            this.panelP2.Location = new System.Drawing.Point(850, 10);
            this.panelP2.Name = "panelP2";
            this.panelP2.Size = new System.Drawing.Size(200, 400);
            this.panelP2.TabIndex = 6;
            this.panelP2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // picP2
            this.picP2.Location = new System.Drawing.Point(10, 10);
            this.picP2.Name = "picP2";
            this.picP2.Size = new System.Drawing.Size(180, 150);
            this.picP2.TabIndex = 7;
            this.picP2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;

            // lblP2Name
            this.lblP2Name = new System.Windows.Forms.Label();
            this.lblP2Name.AutoSize = true;
            this.lblP2Name.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.lblP2Name.Location = new System.Drawing.Point(10, 170);
            this.lblP2Name.Name = "lblP2Name";
            this.lblP2Name.Size = new System.Drawing.Size(100, 18);
            this.lblP2Name.TabIndex = 8;
            this.lblP2Name.Text = "Player 2";

            // lblP2Anomaly
            this.lblP2Anomaly = new System.Windows.Forms.Label();
            this.lblP2Anomaly.AutoSize = true;
            this.lblP2Anomaly.Font = new System.Drawing.Font("Arial", 10F);
            this.lblP2Anomaly.ForeColor = System.Drawing.Color.Purple;
            this.lblP2Anomaly.Location = new System.Drawing.Point(10, 195);
            this.lblP2Anomaly.Name = "lblP2Anomaly";
            this.lblP2Anomaly.Size = new System.Drawing.Size(100, 16);
            this.lblP2Anomaly.TabIndex = 9;
            this.lblP2Anomaly.Text = "Anomaly Name";

            // pbHpP2
            this.pbHpP2 = new System.Windows.Forms.ProgressBar();
            this.pbHpP2.Location = new System.Drawing.Point(10, 220);
            this.pbHpP2.Name = "pbHpP2";
            this.pbHpP2.Size = new System.Drawing.Size(180, 25);
            this.pbHpP2.TabIndex = 10;
            this.pbHpP2.Value = 100;

            // lblHpP2
            this.lblHpP2 = new System.Windows.Forms.Label();
            this.lblHpP2.AutoSize = true;
            this.lblHpP2.Font = new System.Drawing.Font("Arial", 9F);
            this.lblHpP2.Location = new System.Drawing.Point(10, 250);
            this.lblHpP2.Name = "lblHpP2";
            this.lblHpP2.Size = new System.Drawing.Size(100, 15);
            this.lblHpP2.TabIndex = 11;
            this.lblHpP2.Text = "HP: 100 / 100";

            this.panelP2.Controls.Add(this.picP2);
            this.panelP2.Controls.Add(this.lblP2Name);
            this.panelP2.Controls.Add(this.lblP2Anomaly);
            this.panelP2.Controls.Add(this.pbHpP2);
            this.panelP2.Controls.Add(this.lblHpP2);

            // ===== PANEL CENTER (LOG) =====
            this.panelCenter.Location = new System.Drawing.Point(220, 10);
            this.panelCenter.Name = "panelCenter";
            this.panelCenter.Size = new System.Drawing.Size(620, 350);
            this.panelCenter.TabIndex = 12;
            this.panelCenter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;

            // lstBattleLog
            this.lstBattleLog.Location = new System.Drawing.Point(10, 50);
            this.lstBattleLog.Name = "lstBattleLog";
            this.lstBattleLog.Size = new System.Drawing.Size(600, 290);
            this.lstBattleLog.TabIndex = 13;
            this.lstBattleLog.Font = new System.Drawing.Font("Courier New", 9F);

            // lblGiliran
            this.lblGiliran.AutoSize = true;
            this.lblGiliran.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblGiliran.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblGiliran.Location = new System.Drawing.Point(10, 10);
            this.lblGiliran.Name = "lblGiliran";
            this.lblGiliran.Size = new System.Drawing.Size(200, 18);
            this.lblGiliran.TabIndex = 14;
            this.lblGiliran.Text = "⏳ Giliran: Player 1";

            this.panelCenter.Controls.Add(this.lblGiliran);
            this.panelCenter.Controls.Add(this.lstBattleLog);

            // ===== PANEL ACTION (BAWAH) =====
            this.panelAction.Location = new System.Drawing.Point(10, 420);
            this.panelAction.Name = "panelAction";
            this.panelAction.Size = new System.Drawing.Size(1040, 80);
            this.panelAction.TabIndex = 15;
            this.panelAction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // btnAttack
            this.btnAttack.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.btnAttack.Location = new System.Drawing.Point(10, 10);
            this.btnAttack.Name = "btnAttack";
            this.btnAttack.Size = new System.Drawing.Size(100, 40);
            this.btnAttack.TabIndex = 16;
            this.btnAttack.Text = "⚔ Attack";
            this.btnAttack.UseVisualStyleBackColor = true;
            this.btnAttack.Click += new System.EventHandler(this.btnAttack_Click);

            // cmbSkill
            this.cmbSkill.Location = new System.Drawing.Point(120, 10);
            this.cmbSkill.Name = "cmbSkill";
            this.cmbSkill.Size = new System.Drawing.Size(200, 22);
            this.cmbSkill.TabIndex = 17;
            this.cmbSkill.Font = new System.Drawing.Font("Arial", 9F);

            // btnSkill
            this.btnSkill.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.btnSkill.Location = new System.Drawing.Point(330, 10);
            this.btnSkill.Name = "btnSkill";
            this.btnSkill.Size = new System.Drawing.Size(120, 40);
            this.btnSkill.TabIndex = 18;
            this.btnSkill.Text = "✨ Pakai Skill";
            this.btnSkill.UseVisualStyleBackColor = true;
            this.btnSkill.Click += new System.EventHandler(this.btnSkill_Click);

            // btnDefend
            this.btnDefend.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.btnDefend.Location = new System.Drawing.Point(460, 10);
            this.btnDefend.Name = "btnDefend";
            this.btnDefend.Size = new System.Drawing.Size(100, 40);
            this.btnDefend.TabIndex = 19;
            this.btnDefend.Text = "🛡 Defend";
            this.btnDefend.UseVisualStyleBackColor = true;
            this.btnDefend.Click += new System.EventHandler(this.btnDefend_Click);

            // cmbItem
            this.cmbItem.Location = new System.Drawing.Point(570, 10);
            this.cmbItem.Name = "cmbItem";
            this.cmbItem.Size = new System.Drawing.Size(200, 22);
            this.cmbItem.TabIndex = 20;
            this.cmbItem.Font = new System.Drawing.Font("Arial", 9F);

            // btnItem
            this.btnItem.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.btnItem.Location = new System.Drawing.Point(780, 10);
            this.btnItem.Name = "btnItem";
            this.btnItem.Size = new System.Drawing.Size(120, 40);
            this.btnItem.TabIndex = 21;
            this.btnItem.Text = "🎒 Pakai Item";
            this.btnItem.UseVisualStyleBackColor = true;
            this.btnItem.Click += new System.EventHandler(this.btnItem_Click);

            this.panelAction.Controls.Add(this.btnAttack);
            this.panelAction.Controls.Add(this.cmbSkill);
            this.panelAction.Controls.Add(this.btnSkill);
            this.panelAction.Controls.Add(this.btnDefend);
            this.panelAction.Controls.Add(this.cmbItem);
            this.panelAction.Controls.Add(this.btnItem);

            // ===== FORM BATTLE =====
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 510);
            this.Controls.Add(this.panelP1);
            this.Controls.Add(this.panelP2);
            this.Controls.Add(this.panelCenter);
            this.Controls.Add(this.panelAction);
            this.Name = "FormBattle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Anomaly Versus - Battle";
            this.Load += new System.EventHandler(this.FormBattle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picP1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picP2)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelP1;
        private System.Windows.Forms.PictureBox picP1;
        private System.Windows.Forms.Label lblP1Name;
        private System.Windows.Forms.Label lblP1Anomaly;
        private System.Windows.Forms.Label lblHpP1;
        private System.Windows.Forms.ProgressBar pbHpP1;

        private System.Windows.Forms.Panel panelP2;
        private System.Windows.Forms.PictureBox picP2;
        private System.Windows.Forms.Label lblP2Name;
        private System.Windows.Forms.Label lblP2Anomaly;
        private System.Windows.Forms.Label lblHpP2;
        private System.Windows.Forms.ProgressBar pbHpP2;

        private System.Windows.Forms.Panel panelCenter;
        private System.Windows.Forms.ListBox lstBattleLog;
        private System.Windows.Forms.Label lblGiliran;

        private System.Windows.Forms.Panel panelAction;
        private System.Windows.Forms.Button btnAttack;
        private System.Windows.Forms.ComboBox cmbSkill;
        private System.Windows.Forms.Button btnSkill;
        private System.Windows.Forms.Button btnDefend;
        private System.Windows.Forms.ComboBox cmbItem;
        private System.Windows.Forms.Button btnItem;
    }
}
