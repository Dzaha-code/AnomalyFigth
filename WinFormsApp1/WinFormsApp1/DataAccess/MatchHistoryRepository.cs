using System;
using MySql.Data.MySqlClient;
using WinFormsApp1.Models;

namespace WinFormsApp1.DataAccess
{
    /// <summary>
    /// MatchHistoryRepository.cs — Repository untuk operasi CRUD tabel MatchHistory.
    /// Pola standar: MatchHistoryRepository repo = new MatchHistoryRepository();
    ///              repo.AddMatch(matchHistory);
    /// </summary>
    public class MatchHistoryRepository
    {
        /// <summary>
        /// Simpan record pertandingan baru ke database.
        /// MatchHistory columns: Player1ID, Player2ID, Anomaly1ID, Anomaly2ID, WinnerID, MatchDate, Notes
        /// </summary>
        public void AddMatch(MatchHistory match)
        {
            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    string query = @"INSERT INTO MatchHistory 
                        (Player1ID, Player2ID, Anomaly1ID, Anomaly2ID, WinnerID, MatchDate, Notes) 
                        VALUES (@p1, @p2, @a1, @a2, @winner, @date, @notes)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@p1", match.Player1ID);
                    cmd.Parameters.AddWithValue("@p2", match.Player2ID);
                    cmd.Parameters.AddWithValue("@a1", match.Anomaly1ID);
                    cmd.Parameters.AddWithValue("@a2", match.Anomaly2ID);
                    cmd.Parameters.AddWithValue("@winner", match.WinnerID);
                    cmd.Parameters.AddWithValue("@date", match.MatchDate);
                    cmd.Parameters.AddWithValue("@notes", match.Notes ?? "");
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[MatchHistoryRepository.AddMatch Error] {ex.Message}");
            }
        }
    }
}
