using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.DataAccess;
using WinFormsApp1.Models;

namespace WinFormsApp1
{
    /// <summary>
    /// ItemSelectionForm.cs — Form Pemilihan Item Support dengan Grid Kartu RPG Interaktif & Staggered Animation.
    /// </summary>
    public partial class ItemSelectionForm : Form
    {
        private List<Item> itemList = new List<Item>();
        private List<Item> selectedItems = new List<Item>();
        private bool isSelectingForPlayer1 = true;
        private List<Panel> cardPanels = new List<Panel>();
        private System.Windows.Forms.Timer entranceTimer = null!;
        private int currentCardIndex = 0;

        public ItemSelectionForm()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);
        }

        private void ItemSelectionForm_Load(object sender, EventArgs e)
        {
            this.Text = "Anomaly Versus - Pemilihan Item Support";
            LoadItemsFromDB();
            UpdateUI();
        }

        private void LoadItemsFromDB()
        {
            try
            {
                ItemRepository repo = new ItemRepository();
                itemList = repo.GetAllItem();
                BuildItemCardGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading items: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== MEMBUAT GRID KARTU ITEM RPG =====

        private void BuildItemCardGrid()
        {
            flowPanelItems.Controls.Clear();
            cardPanels.Clear();

            foreach (var item in itemList)
            {
                Panel card = CreateItemCard(item);
                card.Visible = false; // Disembunyikan dulu untuk animasi staggered entrance
                cardPanels.Add(card);
                flowPanelItems.Controls.Add(card);
            }

            // Staggered entrance animation (muncul satu per satu tiap 40ms)
            currentCardIndex = 0;
            entranceTimer = new System.Windows.Forms.Timer { Interval = 40 };
            entranceTimer.Tick += (s, e) =>
            {
                if (currentCardIndex < cardPanels.Count)
                {
                    cardPanels[currentCardIndex].Visible = true;
                    currentCardIndex++;
                }
                else
                {
                    entranceTimer.Stop();
                    entranceTimer.Dispose();
                }
            };
            entranceTimer.Start();
        }

        private Panel CreateItemCard(Item item)
        {
            Panel card = new Panel
            {
                Size = new Size(195, 140),
                Margin = new Padding(6),
                BackColor = Color.FromArgb(15, 52, 96), // #0F3460
                Cursor = Cursors.Hand,
                Tag = item
            };

            // Dapatkan emoji icon berdasarkan EffectType
            string emoji = "✨";
            string effectType = item.EffectType.ToUpperInvariant();
            if (effectType.Contains("ATK")) emoji = "⚔️";
            else if (effectType.Contains("DEF")) emoji = "🛡️";
            else if (effectType.Contains("HP") || effectType.Contains("HEAL")) emoji = "❤️";
            else if (effectType.Contains("SPD")) emoji = "⚡";
            else if (effectType.Contains("REGEN")) emoji = "🧪";

            string unit = item.IsPercentage ? "%" : "";
            string effectText = $"{emoji} +{item.EffectValue}{unit} {item.EffectType}";

            Label lblName = new Label
            {
                Text = item.Name,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(245, 166, 35), // Gold
                Location = new Point(8, 8),
                Size = new Size(178, 20),
                AutoEllipsis = true
            };

            Label lblEffect = new Label
            {
                Text = effectText,
                Font = new Font("Consolas", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(233, 69, 96), // Merah-pink
                Location = new Point(8, 30),
                Size = new Size(178, 20)
            };

            Label lblDesc = new Label
            {
                Text = item.Description,
                Font = new Font("Segoe UI", 8F),
                ForeColor = Color.FromArgb(168, 178, 216),
                Location = new Point(8, 52),
                Size = new Size(178, 80)
            };

            // Forward click event dari label ke panel card
            Action<object?, EventArgs> clickHandler = (s, e) => ToggleSelectItem(item, card);
            card.Click += (s, e) => clickHandler(s, e);
            lblName.Click += (s, e) => clickHandler(s, e);
            lblEffect.Click += (s, e) => clickHandler(s, e);
            lblDesc.Click += (s, e) => clickHandler(s, e);

            card.Paint += (s, e) => DrawItemCardBorder(e.Graphics, card, selectedItems.Contains(item));

            card.Controls.Add(lblName);
            card.Controls.Add(lblEffect);
            card.Controls.Add(lblDesc);

            return card;
        }

        private void DrawItemCardBorder(Graphics g, Panel card, bool isSelected)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, card.Width - 1, card.Height - 1);

            // Border emas jika terpilih, border navy jika tidak
            Color borderColor = isSelected ? Color.FromArgb(245, 166, 35) : Color.FromArgb(22, 33, 62);
            float borderWidth = isSelected ? 3f : 1.5f;

            using (Pen pen = new Pen(borderColor, borderWidth))
            {
                g.DrawRectangle(pen, rect);
            }

            // Badge checkmark di pojok kanan atas jika terpilih
            if (isSelected)
            {
                using (SolidBrush badgeBrush = new SolidBrush(Color.FromArgb(245, 166, 35)))
                {
                    g.FillEllipse(badgeBrush, card.Width - 26, 4, 20, 20);
                }
                using (Font checkFont = new Font("Segoe UI", 8.5F, FontStyle.Bold))
                {
                    TextRenderer.DrawText(g, "✓", checkFont, new Point(card.Width - 24, 5), Color.FromArgb(26, 26, 46));
                }
            }
        }

        // ===== SELECTION LOGIC =====

        private void ToggleSelectItem(Item item, Panel card)
        {
            if (selectedItems.Contains(item))
            {
                selectedItems.Remove(item);
                AudioManager.PlaySFX("sfx_click.wav");
            }
            else
            {
                if (selectedItems.Count >= 2)
                {
                    MessageBox.Show("Maksimal memilih 2 item per pemain!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                selectedItems.Add(item);
                AudioManager.PlaySFX("sfx_item.wav");
            }

            lblCounter.Text = $"TERPILIH: [ {selectedItems.Count} / 2 ]";
            card.Invalidate();
        }

        private void UpdateUI()
        {
            selectedItems.Clear();
            lblCounter.Text = "TERPILIH: [ 0 / 2 ]";

            if (isSelectingForPlayer1)
            {
                lblInstruction.Text = $"👉 Giliran Player 1 ({GameSession.Player1Name}): Pilih 2 Item Support";
                lblInstruction.ForeColor = Color.FromArgb(52, 152, 219); // Biru
                btnConfirm.Text = "✓ KONFIRMASI (P1)";
            }
            else
            {
                lblInstruction.Text = $"👉 Giliran Player 2 ({GameSession.Player2Name}): Pilih 2 Item Support";
                lblInstruction.ForeColor = Color.FromArgb(155, 89, 182); // Ungu
                btnConfirm.Text = "✓ KONFIRMASI (P2)";
            }

            foreach (var card in cardPanels)
            {
                card.Invalidate();
            }
        }

        // ===== CUSTOM PAINT BACKGROUND & BOTTOM PANEL =====

        private void ItemSelectionForm_Paint(object sender, PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(
                ClientRectangle,
                Color.FromArgb(26, 26, 46),   // #1A1A2E
                Color.FromArgb(15, 52, 96),   // #0F3460
                LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, ClientRectangle);
            }
        }

        private void panelBottom_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, panelBottom.Width - 1, panelBottom.Height - 1);

            using (LinearGradientBrush brush = new LinearGradientBrush(
                rect,
                Color.FromArgb(22, 33, 62),   // #16213E
                Color.FromArgb(15, 52, 96),   // #0F3460
                LinearGradientMode.Vertical))
            {
                g.FillRectangle(brush, rect);
            }

            using (Pen borderPen = new Pen(Color.FromArgb(15, 52, 96), 2))
            {
                g.DrawRectangle(borderPen, rect);
            }

            using (SolidBrush accentBrush = new SolidBrush(Color.FromArgb(233, 69, 96)))
            {
                g.FillRectangle(accentBrush, 0, 0, 4, panelBottom.Height);
            }
        }

        // ===== BUTTON HANDLERS =====

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (selectedItems.Count != 2)
            {
                MessageBox.Show("Harus memilih tepat 2 item sebelum konfirmasi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                AudioManager.PlaySFX("sfx_hit.wav");
                return;
            }

            AudioManager.PlaySFX("sfx_click.wav");

            if (isSelectingForPlayer1)
            {
                GameSession.Player1Items = new List<Item>(selectedItems);
                lblPlayer1Status.Text = $"Player 1 ({GameSession.Player1Name}): {string.Join(", ", GameSession.Player1Items.Select(i => i.Name))}";
                lblPlayer1Status.ForeColor = Color.FromArgb(245, 166, 35); // Gold

                isSelectingForPlayer1 = false;
                UpdateUI();
            }
            else
            {
                GameSession.Player2Items = new List<Item>(selectedItems);
                lblPlayer2Status.Text = $"Player 2 ({GameSession.Player2Name}): {string.Join(", ", GameSession.Player2Items.Select(i => i.Name))}";
                lblPlayer2Status.ForeColor = Color.FromArgb(245, 166, 35); // Gold

                btnConfirm.Enabled = false;
                btnStartBattle.Enabled = true;
                btnStartBattle.IsActive = true;

                MessageBox.Show("Semua pemain siap bertarung! Klik 'MULAI BATTLE' untuk memasuki arena!", "Siap Battle!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnStartBattle_Click(object sender, EventArgs e)
        {
            AudioManager.PlaySFX("sfx_click.wav");

            if (!GameSession.IsSessionComplete())
            {
                MessageBox.Show("Data pemain belum lengkap!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FormBattle battleForm = new FormBattle();
            this.Hide();
            battleForm.ShowDialog();
            this.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            AudioManager.PlaySFX("sfx_click.wav");
            this.Close();
        }
    }
}
