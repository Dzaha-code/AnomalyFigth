namespace WinFormsApp1.Models
{
    /// <summary>
    /// Model Player — merepresentasikan satu pemain.
    /// Mapping ke tabel `Player` di database anomaly_versus_db.
    /// </summary>
    public class Player
    {
        public int PlayerID { get; set; }
        public string PlayerName { get; set; } = "";
        public int TotalWins { get; set; }
        public int TotalMatches { get; set; }
        public int TotalLosses => Math.Max(0, TotalMatches - TotalWins);
    }
}
