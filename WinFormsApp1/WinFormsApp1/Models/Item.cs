namespace WinFormsApp1.Models
{
    /// <summary>
    /// Model Item — merepresentasikan satu item support.
    /// Mapping ke tabel `Item` di database anomaly_versus_db.
    /// Item diterapkan sebagai modifier pasif di awal battle dan/atau dapat digunakan saat battle.
    /// </summary>
    public class Item
    {
        public int ItemID { get; set; }
        public string Name { get; set; } = "";
        public string EffectType { get; set; } = "";   // "ATK", "DEF", "HP", "SPD", "Regen", "Heal", "Shield"
        public double EffectValue { get; set; }
        public bool IsPercentage { get; set; }         // 1 = percentage, 0 = flat value
        public string Description { get; set; } = "";
        public string IconPath { get; set; } = "";     // e.g. "iron_amulet.png"

        public string GetEffectSummary()
        {
            string unit = IsPercentage ? "%" : "";
            return $"{Name} (+{EffectValue}{unit} {EffectType})";
        }
    }
}
