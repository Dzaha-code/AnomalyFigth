using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.DataAccess;
using WinFormsApp1.Models;

namespace WinFormsApp1
{
    /// <summary>
    /// ItemSelectionForm.cs
    /// Form untuk pemilihan 2 item support oleh tiap player
    /// Menggunakan ItemRepository.GetAllItem() untuk ambil daftar item dari database
    /// Pola standar: menggunakan CheckedListBox untuk multiple selection
    /// </summary>
    public partial class ItemSelectionForm : Form
    {
        private List<Item> itemList = new List<Item>(); // Simpan list item dari database
        private bool isSelectingForPlayer1 = true; // Flag untuk track player yang lagi memilih

        public ItemSelectionForm()
        {
            InitializeComponent();
        }

        private void ItemSelectionForm_Load(object sender, EventArgs e)
        {
            this.Text = "Pemilihan Item Support";
            LoadItems();
            UpdateUI();
        }

        /// <summary>
        /// Load semua Item dari database dan tampilkan di CheckedListBox
        /// Pola: ItemRepository repo = new ItemRepository();
        ///       List<Item> list = repo.GetAllItem();
        ///       foreach (Item i in list) { checkedListBox.Items.Add(...) }
        /// </summary>
        private void LoadItems()
        {
            try
            {
                // POLA STANDAR: Buat objek repository
                ItemRepository itemRepo = new ItemRepository();

                // POLA STANDAR: Panggil fungsinya
                itemList = itemRepo.GetAllItem();

                // POLA STANDAR: Iterasi hasilnya di CheckedListBox
                checkedListBoxItems.Items.Clear();
                foreach (Item item in itemList)
                {
                    checkedListBoxItems.Items.Add($"{item.Name} ({item.EffectType}: {item.EffectValue})", false);
                }

                lblItemCount.Text = $"Total Item: {itemList.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading items: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Update UI untuk menunjukkan player mana yang lagi memilih
        /// </summary>
        private void UpdateUI()
        {
            if (isSelectingForPlayer1)
            {
                lblInstruction.Text = $"Pilih 2 item untuk Player 1 ({GameSession.Player1Name}), lalu klik 'Pilih Untuk Player 1'";
                lblInstruction.ForeColor = System.Drawing.Color.Blue;
                btnSelect.Text = "Pilih Untuk Player 1";
                lblPlayer1Items.Text = $"Player 1: {GameSession.Player1Items.Count} item dipilih";
            }
            else
            {
                lblInstruction.Text = $"Pilih 2 item untuk Player 2 ({GameSession.Player2Name}), lalu klik 'Pilih Untuk Player 2'";
                lblInstruction.ForeColor = System.Drawing.Color.Purple;
                btnSelect.Text = "Pilih Untuk Player 2";
                lblPlayer2Items.Text = $"Player 2: {GameSession.Player2Items.Count} item dipilih";
            }

            // Uncheck semua items untuk player berikutnya
            for (int i = 0; i < checkedListBoxItems.Items.Count; i++)
            {
                checkedListBoxItems.SetItemChecked(i, false);
            }
        }

        /// <summary>
        /// Event handler ketika klik tombol "Pilih Untuk Player"
        /// Ambil item yang ter-check, validasi (harus 2 item), dan simpan ke GameSession
        /// </summary>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            AudioManager.PlaySFX("sfx_click.wav");

            // Cari items yang ter-check
            List<Item> selectedItems = new List<Item>();
            for (int i = 0; i < checkedListBoxItems.Items.Count; i++)
            {
                if (checkedListBoxItems.GetItemChecked(i))
                {
                    selectedItems.Add(itemList[i]);
                }
            }

            // VALIDASI: Harus pilih tepat 2 item
            if (selectedItems.Count != 2)
            {
                MessageBox.Show("Harus memilih tepat 2 item!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Simpan ke GameSession
            if (isSelectingForPlayer1)
            {
                GameSession.Player1Items = selectedItems;
                MessageBox.Show($"Player 1 memilih:\n1. {selectedItems[0].Name}\n2. {selectedItems[1].Name}", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Pindah ke Player 2
                isSelectingForPlayer1 = false;
                UpdateUI();
            }
            else
            {
                GameSession.Player2Items = selectedItems;
                MessageBox.Show($"Player 2 memilih:\n1. {selectedItems[0].Name}\n2. {selectedItems[1].Name}", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Cek apakah game session sudah lengkap
                if (GameSession.IsSessionComplete())
                {
                    MessageBox.Show("Semua data pemain siap! Silakan klik 'Mulai Battle'", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnStart.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Event handler ketika klik tombol "Mulai Battle"
        /// Navigasi ke FormBattle untuk mulai battle session
        /// </summary>
        private void btnStart_Click(object sender, EventArgs e)
        {
            AudioManager.PlaySFX("sfx_click.wav");

            if (!GameSession.IsSessionComplete())
            {
                MessageBox.Show("Data game belum lengkap!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Summary game session (untuk konfirmasi sebelum battle)
            string summary = $"=== BATTLE SUMMARY ===\n\n" +
                           $"Player 1: {GameSession.Player1Name}\n" +
                           $"  Anomaly: {GameSession.Player1Anomaly.Name}\n" +
                           $"  Items: {string.Join(", ", GameSession.Player1Items.Select(i => i.Name))}\n\n" +
                           $"Player 2: {GameSession.Player2Name}\n" +
                           $"  Anomaly: {GameSession.Player2Anomaly.Name}\n" +
                           $"  Items: {string.Join(", ", GameSession.Player2Items.Select(i => i.Name))}";

            DialogResult result = MessageBox.Show(summary + "\n\nSiap untuk mulai battle?", "Game Ready - Siap Battle!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Navigasi ke FormBattle
                FormBattle battleForm = new FormBattle();
                this.Hide();
                battleForm.ShowDialog();
                this.Close(); // Tutup ItemSelectionForm setelah battle selesai
            }
        }

        /// <summary>
        /// Event handler ketika klik tombol "Kembali"
        /// </summary>
        private void btnBack_Click(object sender, EventArgs e)
        {
            AudioManager.PlaySFX("sfx_click.wav");
            this.Close();
        }
    }
}
