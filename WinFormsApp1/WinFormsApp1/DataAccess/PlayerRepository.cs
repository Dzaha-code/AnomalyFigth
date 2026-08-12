using MySql.Data.MySqlClient;
using WinFormsApp1.Models;

namespace WinFormsApp1.DataAccess
{
    public class PlayerRepository
    {
        // Cari player berdasarkan nama, kalau belum ada akan dibuat baru
        public Player GetOrCreatePlayer(string name)
        {
            using (var conn = DBConnection.GetConnection())
            {
                conn.Open();

                string selectQuery = "SELECT * FROM Player WHERE PlayerName = @name";
                using (var cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Player
                            {
                                PlayerID = reader.GetInt32("PlayerID"),
                                PlayerName = reader.GetString("PlayerName"),
                                TotalWins = reader.GetInt32("TotalWins"),
                                TotalMatches = reader.GetInt32("TotalMatches")
                            };
                        }
                    }
                }

                // Kalau belum ada, insert baru
                string insertQuery = "INSERT INTO Player (PlayerName) VALUES (@name); SELECT LAST_INSERT_ID();";
                using (var cmd = new MySqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    int newId = System.Convert.ToInt32(cmd.ExecuteScalar());
                    return new Player { PlayerID = newId, PlayerName = name, TotalWins = 0, TotalMatches = 0 };
                }
            }
        }

        // Update statistik setelah match selesai
        public void UpdateStats(int playerId, bool isWinner)
        {
            using (var conn = DBConnection.GetConnection())
            {
                conn.Open();
                string query = isWinner
                    ? "UPDATE Player SET TotalWins = TotalWins + 1, TotalMatches = TotalMatches + 1 WHERE PlayerID = @id"
                    : "UPDATE Player SET TotalMatches = TotalMatches + 1 WHERE PlayerID = @id";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", playerId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
