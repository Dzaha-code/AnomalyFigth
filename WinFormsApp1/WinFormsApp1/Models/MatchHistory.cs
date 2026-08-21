using System;

namespace WinFormsApp1.Models
{
    /// <summary>
    /// Model MatchHistory — merepresentasikan satu record riwayat pertandingan.
    /// Mapping ke tabel `MatchHistory` di database anomaly_versus_db.
    /// </summary>
    public class MatchHistory
    {
        public int MatchID { get; set; }
        public int Player1ID { get; set; }
        public int Player2ID { get; set; }
        public int Anomaly1ID { get; set; }
        public int Anomaly2ID { get; set; }
        public int WinnerID { get; set; }
        public DateTime MatchDate { get; set; } = DateTime.Now;
        public string Notes { get; set; } = "";
    }
}
