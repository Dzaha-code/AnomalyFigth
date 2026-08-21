using System;
using MySql.Data.MySqlClient;
using WinFormsApp1.Models;

namespace WinFormsApp1.DataAccess
{
    /// <summary>
    /// PlayerRepository.cs — Repository untuk operasi CRUD tabel Player.
    /// Pola standar: PlayerRepository repo = new PlayerRepository();
    ///              Player p = repo.GetOrCreatePlayer("Budi");
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
                using (MySqlDataReader reader = selectCmd.ExecuteReader())
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

                // Tidak ditemukan → buat baru
                string insertQuery = "INSERT INTO Player (PlayerName, TotalWins, TotalMatches) VALUES (@name, 0, 0)";
                MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn);
                insertCmd.Parameters.AddWithValue("@name", playerName);
                insertCmd.ExecuteNonQuery();

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
        /// Update stats untuk player (TotalMatches + 1, TotalWins + 1 jika menang).
        /// </summary>
        public void UpdateStats(int playerId, bool isWin)
        {
            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    string query = isWin
                        ? "UPDATE Player SET TotalWins = TotalWins + 1, TotalMatches = TotalMatches + 1 WHERE PlayerID = @id"
                        : "UPDATE Player SET TotalMatches = TotalMatches + 1 WHERE PlayerID = @id";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", playerId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[PlayerRepository.UpdateStats Error] {ex.Message}");
            }
        }
    }
}
