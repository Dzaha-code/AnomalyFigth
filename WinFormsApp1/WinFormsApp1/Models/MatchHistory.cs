using System;

namespace WinFormsApp1.Models
{
    public class MatchHistory
    {
        public int MatchID { get; set; }
        public int Player1ID { get; set; }    // FK -> Player.PlayerID
        public int Player2ID { get; set; }    // FK -> Player.PlayerID
        public int Anomaly1ID { get; set; }   // FK -> Anomaly.AnomalyID
        public int Anomaly2ID { get; set; }   // FK -> Anomaly.AnomalyID
        public int WinnerID { get; set; }     // FK -> Player.PlayerID
        public DateTime MatchDate { get; set; }
        public string Notes { get; set; }
    }
}
