using WinFormsApp1.Models;

namespace WinFormsApp1
{
    /// <summary>
    /// GameSession.cs - Kelas static untuk menyimpan data pemain & pilihan mereka selama sesi bermain
    /// Diakses dari berbagai form (PlayerNameForm, AnomalySelectionForm, ItemSelectionForm, BattleForm)
    /// 
    /// Contoh pemakaian:
    /// GameSession.Player1Name = "Budi";
    /// GameSession.Player1Anomaly = anomalyDipilih;
    /// MessageBox.Show(GameSession.Player1Name); // Output: "Budi"
    /// </summary>
    public static class GameSession
    {
        // ===== DATA PEMAIN =====
        public static string Player1Name { get; set; }
        public static string Player2Name { get; set; }

        // ===== ANOMALY YANG DIPILIH =====
        public static Anomaly Player1Anomaly { get; set; }
        public static Anomaly Player2Anomaly { get; set; }

        // ===== ITEM YANG DIPILIH (2 per pemain) =====
        public static List<Item> Player1Items { get; set; } = new List<Item>();
        public static List<Item> Player2Items { get; set; } = new List<Item>();

        // ===== METHOD HELPER =====

        /// <summary>
        /// Reset semua data saat mulai game baru
        /// Dipanggil saat klik tombol "Mulai Game" di MainMenuForm
        /// </summary>
        public static void ResetSession()
        {
            Player1Name = null;
            Player2Name = null;
            Player1Anomaly = null;
            Player2Anomaly = null;
            Player1Items.Clear();
            Player2Items.Clear();
        }

        /// <summary>
        /// Cek apakah data sudah lengkap untuk mulai battle
        /// Return true jika semua data pemain & pilihan sudah ada
        /// </summary>
        public static bool IsSessionComplete()
        {
            return !string.IsNullOrEmpty(Player1Name) &&
                   !string.IsNullOrEmpty(Player2Name) &&
                   Player1Anomaly != null &&
                   Player2Anomaly != null &&
                   Player1Items.Count == 2 &&
                   Player2Items.Count == 2;
        }
    }
}
