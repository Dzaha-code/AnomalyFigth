using MySql.Data.MySqlClient;
using WinFormsApp1.Models;

namespace WinFormsApp1.DataAccess
{
    /// <summary>
    /// PlayerRepository.cs — Repository untuk operasi CRUD tabel Player.
    /// Note: Database schema uses TotalMatches instead of TotalLosses.
    /// </summary>
    public class PlayerRepository
    {
        /// <summary>
        /// Cari player berdasarkan nama. Jika tidak ada, buat baru.
        /// Return objek Player (sudah ada atau baru dibuat).
        /// </summary>
        public Player GetOrCreatePlayer(string playerName)
        {
            using (var conn = DBConnection.GetConnection())
            {
                conn.Open();

                // Cari dulu
                string selectQuery = "SELECT * FROM Player WHERE PlayerName = @name";
                MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn);
                selectCmd.Parameters.AddWithValue("@name", playerName);
                MySqlDataReader reader = selectCmd.ExecuteReader();

                if (reader.Read())
                {
                    Player existing = new Player
                    {
                        PlayerID = reader.GetInt32("PlayerID"),
                        PlayerName = reader.GetString("PlayerName"),
                        TotalWins = reader.GetInt32("TotalWins"),
                        TotalMatches = reader.GetInt32("TotalMatches")
                    };
                    reader.Close();
                    return existing;
                }

                reader.Close();

                // Tidak ditemukan → buat baru (TotalWins=0, TotalMatches=0)
                string insertQuery = "INSERT INTO Player (PlayerName, TotalWins, TotalMatches) VALUES (@name, 0, 0)";
                MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn);
                insertCmd.Parameters.AddWithValue("@name", playerName);
                insertCmd.ExecuteNonQuery();

                // Return player baru
                return new Player
                {
                    PlayerID = (int)insertCmd.LastInsertedId,
                    PlayerName = playerName,
                    TotalWins = 0,
                    TotalMatches = 0
                };
            }
        }

        /// <summary>
        /// Update stats untuk player.
        /// If isWin == true: increment TotalWins and TotalMatches.
        /// If isWin == false: increment TotalMatches only.
        /// This matches database schema (TotalMatches column exists).
        /// </summary>
        public void UpdateStats(int playerId, bool isWin)
        {
            using (var conn = DBConnection.GetConnection())
            {
                conn.Open();
                string query;

                if (isWin)
                {
                    // Winner: increment wins AND total matches
                    query = "UPDATE Player SET TotalWins = TotalWins + 1, TotalMatches = TotalMatches + 1 WHERE PlayerID = @id";
                }
                else
                {
                    // Loser: increment total matches only
                    query = "UPDATE Player SET TotalMatches = TotalMatches + 1 WHERE PlayerID = @id";
                }

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", playerId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
