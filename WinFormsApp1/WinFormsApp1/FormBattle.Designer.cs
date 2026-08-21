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
            this.lblP1TurnBadge = new System.Windows.Forms.Label();
            this.picP1 = new System.Windows.Forms.PictureBox();
            this.lblP1Name = new System.Windows.Forms.Label();
            this.lblP1Anomaly = new System.Windows.Forms.Label();
            this.lblP1Items = new System.Windows.Forms.Label();
            this.lblHpP1 = new System.Windows.Forms.Label();
            this.pbHpP1 = new System.Windows.Forms.ProgressBar();
            this.lblP1Status = new System.Windows.Forms.Label();

            // Panel P2 - Kanan
            this.panelP2 = new System.Windows.Forms.Panel();
            this.lblP2TurnBadge = new System.Windows.Forms.Label();
            this.picP2 = new System.Windows.Forms.PictureBox();
            this.lblP2Name = new System.Windows.Forms.Label();
            this.lblP2Anomaly = new System.Windows.Forms.Label();
            this.lblP2Items = new System.Windows.Forms.Label();
            this.lblHpP2 = new System.Windows.Forms.Label();
            this.pbHpP2 = new System.Windows.Forms.ProgressBar();
            this.lblP2Status = new System.Windows.Forms.Label();

            // Panel tengah (log & giliran)
            this.panelCenter = new System.Windows.Forms.Panel();
            this.lstBattleLog = new System.Windows.Forms.ListBox();
            this.lblGiliran = new System.Windows.Forms.Label();

            // Panel aksi bawah
            this.panelAction = new System.Windows.Forms.Panel();
            this.lblActionTitle = new System.Windows.Forms.Label();
            this.btnAttack = new WinFormsApp1.UI.RPGButton();
            this.cmbSkill = new System.Windows.Forms.ComboBox();
            this.btnSkill = new WinFormsApp1.UI.RPGButton();
            this.btnDefend = new WinFormsApp1.UI.RPGButton();
            this.cmbItem = new System.Windows.Forms.ComboBox();
            this.btnItem = new WinFormsApp1.UI.RPGButton();

            ((System.ComponentModel.ISupportInitialize)(this.picP1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picP2)).BeginInit();
            this.panelP1.SuspendLayout();
            this.panelP2.SuspendLayout();
            this.panelCenter.SuspendLayout();
            this.panelAction.SuspendLayout();
            this.SuspendLayout();

            // ===== PANEL P1 (KIRI) =====
            this.panelP1.BackColor = System.Drawing.Color.FromArgb(22, 33, 62); // #16213E
            this.panelP1.Location = new System.Drawing.Point(12, 12);
            this.panelP1.Name = "panelP1";
            this.panelP1.Size = new System.Drawing.Size(214, 430);
            this.panelP1.TabIndex = 0;
            this.panelP1.Paint += new System.Windows.Forms.PaintEventHandler(this.panelP1_Paint);

            // lblP1TurnBadge
            this.lblP1TurnBadge.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblP1TurnBadge.Location = new System.Drawing.Point(0, 0);
            this.lblP1TurnBadge.Name = "lblP1TurnBadge";
            this.lblP1TurnBadge.Size = new System.Drawing.Size(214, 28);
            this.lblP1TurnBadge.TabIndex = 0;
            this.lblP1TurnBadge.Text = "🔥 GILIRAN KAMU!";
            this.lblP1TurnBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // picP1
            this.picP1.BackColor = System.Drawing.Color.FromArgb(15, 52, 96);
            this.picP1.Location = new System.Drawing.Point(12, 36);
            this.picP1.Name = "picP1";
            this.picP1.Size = new System.Drawing.Size(188, 140);
            this.picP1.TabIndex = 1;
            this.picP1.TabStop = false;
            this.picP1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picP1.Paint += new System.Windows.Forms.PaintEventHandler(this.picP1_Paint);

            // lblP1Name
            this.lblP1Name.AutoSize = true;
            this.lblP1Name.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblP1Name.ForeColor = System.Drawing.Color.FromArgb(234, 234, 234);
            this.lblP1Name.Location = new System.Drawing.Point(10, 182);
            this.lblP1Name.Name = "lblP1Name";
            this.lblP1Name.Size = new System.Drawing.Size(65, 20);
            this.lblP1Name.TabIndex = 2;
            this.lblP1Name.Text = "Player 1";

            // lblP1Anomaly
            this.lblP1Anomaly.AutoSize = true;
            this.lblP1Anomaly.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblP1Anomaly.ForeColor = System.Drawing.Color.FromArgb(245, 166, 35); // Gold
            this.lblP1Anomaly.Location = new System.Drawing.Point(10, 204);
            this.lblP1Anomaly.Name = "lblP1Anomaly";
            this.lblP1Anomaly.Size = new System.Drawing.Size(104, 17);
            this.lblP1Anomaly.TabIndex = 3;
            this.lblP1Anomaly.Text = "Anomaly Name";

            // lblP1Items
            this.lblP1Items.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblP1Items.ForeColor = System.Drawing.Color.FromArgb(168, 178, 216);
            this.lblP1Items.Location = new System.Drawing.Point(10, 224);
            this.lblP1Items.Name = "lblP1Items";
            this.lblP1Items.Size = new System.Drawing.Size(190, 28);
            this.lblP1Items.TabIndex = 24;
            this.lblP1Items.Text = "🎒 Items: -";

            // pbHpP1
            this.pbHpP1.Location = new System.Drawing.Point(10, 255);
            this.pbHpP1.Name = "pbHpP1";
            this.pbHpP1.Size = new System.Drawing.Size(190, 22);
            this.pbHpP1.TabIndex = 4;
            this.pbHpP1.Value = 100;

            // lblHpP1
            this.lblHpP1.AutoSize = true;
            this.lblHpP1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold);
            this.lblHpP1.ForeColor = System.Drawing.Color.FromArgb(234, 234, 234);
            this.lblHpP1.Location = new System.Drawing.Point(10, 282);
            this.lblHpP1.Name = "lblHpP1";
            this.lblHpP1.Size = new System.Drawing.Size(91, 14);
            this.lblHpP1.TabIndex = 5;
            this.lblHpP1.Text = "HP: 100 / 100";

            // lblP1Status
            this.lblP1Status.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblP1Status.ForeColor = System.Drawing.Color.FromArgb(233, 69, 96);
            this.lblP1Status.Location = new System.Drawing.Point(10, 302);
            this.lblP1Status.Name = "lblP1Status";
            this.lblP1Status.Size = new System.Drawing.Size(190, 120);
            this.lblP1Status.TabIndex = 22;
            this.lblP1Status.Text = "";

            this.panelP1.Controls.Add(this.lblP1TurnBadge);
            this.panelP1.Controls.Add(this.picP1);
            this.panelP1.Controls.Add(this.lblP1Name);
            this.panelP1.Controls.Add(this.lblP1Anomaly);
            this.panelP1.Controls.Add(this.lblP1Items);
            this.panelP1.Controls.Add(this.pbHpP1);
            this.panelP1.Controls.Add(this.lblHpP1);
            this.panelP1.Controls.Add(this.lblP1Status);

            // ===== PANEL P2 (KANAN) =====
            this.panelP2.BackColor = System.Drawing.Color.FromArgb(22, 33, 62);
            this.panelP2.Location = new System.Drawing.Point(858, 12);
            this.panelP2.Name = "panelP2";
            this.panelP2.Size = new System.Drawing.Size(214, 430);
            this.panelP2.TabIndex = 6;
            this.panelP2.Paint += new System.Windows.Forms.PaintEventHandler(this.panelP2_Paint);

            // lblP2TurnBadge
            this.lblP2TurnBadge.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblP2TurnBadge.Location = new System.Drawing.Point(0, 0);
            this.lblP2TurnBadge.Name = "lblP2TurnBadge";
            this.lblP2TurnBadge.Size = new System.Drawing.Size(214, 28);
            this.lblP2TurnBadge.TabIndex = 0;
            this.lblP2TurnBadge.Text = "⏳ MENUNGGU";
            this.lblP2TurnBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // picP2
            this.picP2.BackColor = System.Drawing.Color.FromArgb(15, 52, 96);
            this.picP2.Location = new System.Drawing.Point(12, 36);
            this.picP2.Name = "picP2";
            this.picP2.Size = new System.Drawing.Size(188, 140);
            this.picP2.TabIndex = 7;
            this.picP2.TabStop = false;
            this.picP2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picP2.Paint += new System.Windows.Forms.PaintEventHandler(this.picP2_Paint);

            // lblP2Name
            this.lblP2Name.AutoSize = true;
            this.lblP2Name.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblP2Name.ForeColor = System.Drawing.Color.FromArgb(234, 234, 234);
            this.lblP2Name.Location = new System.Drawing.Point(10, 182);
            this.lblP2Name.Name = "lblP2Name";
            this.lblP2Name.Size = new System.Drawing.Size(65, 20);
            this.lblP2Name.TabIndex = 8;
            this.lblP2Name.Text = "Player 2";

            // lblP2Anomaly
            this.lblP2Anomaly.AutoSize = true;
            this.lblP2Anomaly.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblP2Anomaly.ForeColor = System.Drawing.Color.FromArgb(245, 166, 35); // Gold
            this.lblP2Anomaly.Location = new System.Drawing.Point(10, 204);
            this.lblP2Anomaly.Name = "lblP2Anomaly";
            this.lblP2Anomaly.Size = new System.Drawing.Size(104, 17);
            this.lblP2Anomaly.TabIndex = 9;
            this.lblP2Anomaly.Text = "Anomaly Name";

            // lblP2Items
            this.lblP2Items.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblP2Items.ForeColor = System.Drawing.Color.FromArgb(168, 178, 216);
            this.lblP2Items.Location = new System.Drawing.Point(10, 224);
            this.lblP2Items.Name = "lblP2Items";
            this.lblP2Items.Size = new System.Drawing.Size(190, 28);
            this.lblP2Items.TabIndex = 25;
            this.lblP2Items.Text = "🎒 Items: -";

            // pbHpP2
            this.pbHpP2.Location = new System.Drawing.Point(10, 255);
            this.pbHpP2.Name = "pbHpP2";
            this.pbHpP2.Size = new System.Drawing.Size(190, 22);
            this.pbHpP2.TabIndex = 10;
            this.pbHpP2.Value = 100;

            // lblHpP2
            this.lblHpP2.AutoSize = true;
            this.lblHpP2.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold);
            this.lblHpP2.ForeColor = System.Drawing.Color.FromArgb(234, 234, 234);
            this.lblHpP2.Location = new System.Drawing.Point(10, 282);
            this.lblHpP2.Name = "lblHpP2";
            this.lblHpP2.Size = new System.Drawing.Size(91, 14);
            this.lblHpP2.TabIndex = 11;
            this.lblHpP2.Text = "HP: 100 / 100";

            // lblP2Status
            this.lblP2Status.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblP2Status.ForeColor = System.Drawing.Color.FromArgb(233, 69, 96);
            this.lblP2Status.Location = new System.Drawing.Point(10, 302);
            this.lblP2Status.Name = "lblP2Status";
            this.lblP2Status.Size = new System.Drawing.Size(190, 120);
            this.lblP2Status.TabIndex = 23;
            this.lblP2Status.Text = "";

            this.panelP2.Controls.Add(this.lblP2TurnBadge);
            this.panelP2.Controls.Add(this.picP2);
            this.panelP2.Controls.Add(this.lblP2Name);
            this.panelP2.Controls.Add(this.lblP2Anomaly);
            this.panelP2.Controls.Add(this.lblP2Items);
            this.panelP2.Controls.Add(this.pbHpP2);
            this.panelP2.Controls.Add(this.lblHpP2);
            this.panelP2.Controls.Add(this.lblP2Status);

            // ===== PANEL CENTER (LOG & GILIRAN) =====
            this.panelCenter.BackColor = System.Drawing.Color.FromArgb(10, 10, 26); // #0A0A1A
            this.panelCenter.Location = new System.Drawing.Point(234, 12);
            this.panelCenter.Name = "panelCenter";
            this.panelCenter.Size = new System.Drawing.Size(616, 430);
            this.panelCenter.TabIndex = 12;
            this.panelCenter.Paint += new System.Windows.Forms.PaintEventHandler(this.panelCenter_Paint);

            // lblGiliran - High contrast central banner
            this.lblGiliran.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblGiliran.ForeColor = System.Drawing.Color.White;
            this.lblGiliran.BackColor = System.Drawing.Color.FromArgb(233, 69, 96); // #E94560
            this.lblGiliran.Location = new System.Drawing.Point(0, 0);
            this.lblGiliran.Name = "lblGiliran";
            this.lblGiliran.Size = new System.Drawing.Size(616, 36);
            this.lblGiliran.TabIndex = 14;
            this.lblGiliran.Text = "⚔️ GILIRAN: PLAYER 1";
            this.lblGiliran.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lstBattleLog (Custom Drawn Color-Coded ListBox)
            this.lstBattleLog.BackColor = System.Drawing.Color.FromArgb(10, 10, 26); // #0A0A1A
            this.lstBattleLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstBattleLog.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstBattleLog.Font = new System.Drawing.Font("Consolas", 9F);
            this.lstBattleLog.ForeColor = System.Drawing.Color.FromArgb(234, 234, 234);
            this.lstBattleLog.FormattingEnabled = true;
            this.lstBattleLog.ItemHeight = 20;
            this.lstBattleLog.Location = new System.Drawing.Point(10, 44);
            this.lstBattleLog.Name = "lstBattleLog";
            this.lstBattleLog.Size = new System.Drawing.Size(596, 370);
            this.lstBattleLog.TabIndex = 13;
            this.lstBattleLog.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstBattleLog_DrawItem);

            this.panelCenter.Controls.Add(this.lblGiliran);
            this.panelCenter.Controls.Add(this.lstBattleLog);

            // ===== PANEL ACTION (BAWAH) =====
            this.panelAction.BackColor = System.Drawing.Color.FromArgb(22, 33, 62);
            this.panelAction.Location = new System.Drawing.Point(12, 450);
            this.panelAction.Name = "panelAction";
            this.panelAction.Size = new System.Drawing.Size(1060, 95);
            this.panelAction.TabIndex = 15;
            this.panelAction.Paint += new System.Windows.Forms.PaintEventHandler(this.panelAction_Paint);

            // lblActionTitle
            this.lblActionTitle.AutoSize = true;
            this.lblActionTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblActionTitle.ForeColor = System.Drawing.Color.FromArgb(245, 166, 35); // Gold
            this.lblActionTitle.Location = new System.Drawing.Point(10, 4);
            this.lblActionTitle.Name = "lblActionTitle";
            this.lblActionTitle.Size = new System.Drawing.Size(100, 17);
            this.lblActionTitle.TabIndex = 22;
            this.lblActionTitle.Text = "🎮 PILIH AKSI:";

            // btnAttack
            this.btnAttack.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnAttack.Location = new System.Drawing.Point(10, 24);
            this.btnAttack.Name = "btnAttack";
            this.btnAttack.Size = new System.Drawing.Size(115, 60);
            this.btnAttack.TabIndex = 16;
            this.btnAttack.Text = "⚔️ Attack";
            this.btnAttack.Click += new System.EventHandler(this.btnAttack_Click);

            // cmbSkill
            this.cmbSkill.BackColor = System.Drawing.Color.FromArgb(15, 52, 96);
            this.cmbSkill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSkill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSkill.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.cmbSkill.ForeColor = System.Drawing.Color.FromArgb(234, 234, 234);
            this.cmbSkill.Location = new System.Drawing.Point(135, 26);
            this.cmbSkill.Name = "cmbSkill";
            this.cmbSkill.Size = new System.Drawing.Size(255, 23);
            this.cmbSkill.TabIndex = 17;

            // btnSkill
            this.btnSkill.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSkill.Location = new System.Drawing.Point(135, 54);
            this.btnSkill.Name = "btnSkill";
            this.btnSkill.Size = new System.Drawing.Size(255, 30);
            this.btnSkill.TabIndex = 18;
            this.btnSkill.Text = "✨ Pakai Skill";
            this.btnSkill.Click += new System.EventHandler(this.btnSkill_Click);

            // btnDefend
            this.btnDefend.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnDefend.Location = new System.Drawing.Point(400, 24);
            this.btnDefend.Name = "btnDefend";
            this.btnDefend.Size = new System.Drawing.Size(115, 60);
            this.btnDefend.TabIndex = 19;
            this.btnDefend.Text = "🛡️ Defend";
            this.btnDefend.Click += new System.EventHandler(this.btnDefend_Click);

            // cmbItem
            this.cmbItem.BackColor = System.Drawing.Color.FromArgb(15, 52, 96);
            this.cmbItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.cmbItem.ForeColor = System.Drawing.Color.FromArgb(234, 234, 234);
            this.cmbItem.Location = new System.Drawing.Point(525, 26);
            this.cmbItem.Name = "cmbItem";
            this.cmbItem.Size = new System.Drawing.Size(255, 23);
            this.cmbItem.TabIndex = 20;

            // btnItem
            this.btnItem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnItem.Location = new System.Drawing.Point(525, 54);
            this.btnItem.Name = "btnItem";
            this.btnItem.Size = new System.Drawing.Size(255, 30);
            this.btnItem.TabIndex = 21;
            this.btnItem.Text = "🎒 Pakai Item";
            this.btnItem.Click += new System.EventHandler(this.btnItem_Click);

            this.panelAction.Controls.Add(this.lblActionTitle);
            this.panelAction.Controls.Add(this.btnAttack);
            this.panelAction.Controls.Add(this.cmbSkill);
            this.panelAction.Controls.Add(this.btnSkill);
            this.panelAction.Controls.Add(this.btnDefend);
            this.panelAction.Controls.Add(this.cmbItem);
            this.panelAction.Controls.Add(this.btnItem);

            // ===== FORM BATTLE =====
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(26, 26, 46); // #1A1A2E
            this.ClientSize = new System.Drawing.Size(1084, 555);
            this.Controls.Add(this.panelP1);
            this.Controls.Add(this.panelP2);
            this.Controls.Add(this.panelCenter);
            this.Controls.Add(this.panelAction);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormBattle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Anomaly Versus - Battle Arena";
            this.Load += new System.EventHandler(this.FormBattle_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormBattle_Paint);
            this.panelP1.ResumeLayout(false);
            this.panelP1.PerformLayout();
            this.panelP2.ResumeLayout(false);
            this.panelP2.PerformLayout();
            this.panelCenter.ResumeLayout(false);
            this.panelAction.ResumeLayout(false);
            this.panelAction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picP1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picP2)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelP1;
        private System.Windows.Forms.Label lblP1TurnBadge;
        private System.Windows.Forms.PictureBox picP1;
        private System.Windows.Forms.Label lblP1Name;
        private System.Windows.Forms.Label lblP1Anomaly;
        private System.Windows.Forms.Label lblP1Items;
        private System.Windows.Forms.Label lblHpP1;
        private System.Windows.Forms.ProgressBar pbHpP1;
        private System.Windows.Forms.Label lblP1Status;

        private System.Windows.Forms.Panel panelP2;
        private System.Windows.Forms.Label lblP2TurnBadge;
        private System.Windows.Forms.PictureBox picP2;
        private System.Windows.Forms.Label lblP2Name;
        private System.Windows.Forms.Label lblP2Anomaly;
        private System.Windows.Forms.Label lblP2Items;
        private System.Windows.Forms.Label lblHpP2;
        private System.Windows.Forms.ProgressBar pbHpP2;
        private System.Windows.Forms.Label lblP2Status;

        private System.Windows.Forms.Panel panelCenter;
        private System.Windows.Forms.ListBox lstBattleLog;
        private System.Windows.Forms.Label lblGiliran;

        private System.Windows.Forms.Panel panelAction;
        private System.Windows.Forms.Label lblActionTitle;
        private WinFormsApp1.UI.RPGButton btnAttack;
        private System.Windows.Forms.ComboBox cmbSkill;
        private WinFormsApp1.UI.RPGButton btnSkill;
        private WinFormsApp1.UI.RPGButton btnDefend;
        private System.Windows.Forms.ComboBox cmbItem;
        private WinFormsApp1.UI.RPGButton btnItem;
    }
}
