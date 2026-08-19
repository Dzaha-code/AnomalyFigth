namespace WinFormsApp1.Models
{
    /// <summary>
    /// Model Player — merepresentasikan satu pemain.
    /// Mapping ke tabel `Player` di database anomaly_versus_db.
    /// Note: Database schema uses TotalMatches (not TotalLosses). Use TotalMatches to track matches.
    /// </summary>
    public class Player
    {
        public int PlayerID { get; set; }
        public string PlayerName { get; set; } = "";
        public int TotalWins { get; set; }
        // TotalMatches disimpan di database. TotalLosses can be derived = TotalMatches - TotalWins
        public int TotalMatches { get; set; }
    }
}
