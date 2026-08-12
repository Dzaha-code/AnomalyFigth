namespace WinFormsApp1.Models
{
    public class Item
    {
        public int ItemID { get; set; }
        public string Name { get; set; }
        public string EffectType { get; set; }   // ATK / DEF / HP / Regen, dst
        public float EffectValue { get; set; }
        public bool IsPercentage { get; set; }    // true = persentase, false = nilai tetap
        public string Description { get; set; }
        public string IconPath { get; set; }
    }
}
