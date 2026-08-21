namespace WinFormsApp1.Models
{
    /// <summary>
    /// Model Skill — merepresentasikan satu skill milik Anomaly.
    /// Mapping ke tabel `Skill` di database anomaly_versus_db.
    /// Setiap Anomaly punya 2-3 skill.
    /// </summary>
    public class Skill
    {
        public int SkillID { get; set; }
        public int AnomalyID { get; set; }            // FK ke tabel Anomaly
        public string Name { get; set; } = "";
        public string SkillType { get; set; } = "Damage"; // "Damage", "Buff", "Heal", "Debuff", "Shield", "Poison", "Stun"
        public double Power { get; set; } = 1.0;          // Pengali power (e.g. 1.5 = 1.5x ATK)
        public int Cooldown { get; set; } = 2;            // Cooldown dalam turn
        public string Description { get; set; } = "";

        // Backward compatibility helper
        public int Damage => (int)Math.Round(Power * 10);
        public int MpCost => Cooldown;
    }
}
