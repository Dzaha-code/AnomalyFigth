namespace WinFormsApp1.Models
{
    public class Anomaly
    {
        public int AnomalyID { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }      // Attacker / Defender / Support
        public int BaseHP { get; set; }
        public int BaseATK { get; set; }
        public int BaseDEF { get; set; }
        public int BaseSPD { get; set; }
        public string Description { get; set; }
        public string SpritePath { get; set; }
    }
}
