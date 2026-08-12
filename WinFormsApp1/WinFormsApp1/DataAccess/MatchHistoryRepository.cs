using MySql.Data.MySqlClient;
using WinFormsApp1.Models;

namespace WinFormsApp1.DataAccess
{
    public class MatchHistoryRepository
    {
        // Simpan hasil pertandingan setelah battle selesai
        public void AddMatch(MatchHistory m)
        {
            using (var conn = DBConnection.GetConnection())
            {
                conn.Open();
                string query = @"INSERT INTO MatchHistory 
                                  (Player1ID, Player2ID, Anomaly1ID, Anomaly2ID, WinnerID, Notes)
                                  VALUES (@p1, @p2, @a1, @a2, @winner, @notes)";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@p1", m.Player1ID);
                    cmd.Parameters.AddWithValue("@p2", m.Player2ID);
                    cmd.Parameters.AddWithValue("@a1", m.Anomaly1ID);
                    cmd.Parameters.AddWithValue("@a2", m.Anomaly2ID);
                    cmd.Parameters.AddWithValue("@winner", m.WinnerID);
                    cmd.Parameters.AddWithValue("@notes", m.Notes ?? "");
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
