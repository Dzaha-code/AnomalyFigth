namespace WinFormsApp1.Models
{
    public class Skill
    {
        public int SkillID { get; set; }
        public int AnomalyID { get; set; }    // FK -> Anomaly.AnomalyID
        public string Name { get; set; }
        public string SkillType { get; set; } // Damage / Heal / Buff / Debuff
        public float Power { get; set; }
        public int Cooldown { get; set; }
        public string Description { get; set; }
    }
}
