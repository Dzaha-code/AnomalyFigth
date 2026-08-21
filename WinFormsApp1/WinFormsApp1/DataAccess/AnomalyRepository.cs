using System.Collections.Generic;
using MySql.Data.MySqlClient;
using WinFormsApp1.Models;

namespace WinFormsApp1.DataAccess
{
    /// <summary>
    /// AnomalyRepository.cs — Repository untuk operasi CRUD tabel Anomaly.
    /// Pola standar: AnomalyRepository repo = new AnomalyRepository();
    ///              List<Anomaly> list = repo.GetAllAnomaly();
    /// </summary>
    public class AnomalyRepository
    {
        /// <summary>
        /// Ambil semua Anomaly dari database.
        /// </summary>
        public List<Anomaly> GetAllAnomaly()
        {
            List<Anomaly> list = new List<Anomaly>();

            using (var conn = DBConnection.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Anomaly";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Anomaly anomaly = new Anomaly
                    {
                        AnomalyID = reader.GetInt32("AnomalyID"),
                        Name = reader.GetString("Name"),
                        Role = reader.GetString("Role"),
                        BaseHP = reader.GetInt32("BaseHP"),
                        BaseATK = reader.GetInt32("BaseATK"),
                        BaseDEF = reader.GetInt32("BaseDEF"),
                        BaseSPD = reader.GetInt32("BaseSPD"),
                        Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? "" : reader.GetString("Description"),
                        SpritePath = reader.IsDBNull(reader.GetOrdinal("SpritePath")) ? "" : reader.GetString("SpritePath")
                    };
                    list.Add(anomaly);
                }
            }

            return list;
        }

        /// <summary>
        /// Ambil satu Anomaly berdasarkan ID.
        /// Return null jika tidak ditemukan.
        /// </summary>
        public Anomaly? GetAnomalyById(int anomalyId)
        {
            using (var conn = DBConnection.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Anomaly WHERE AnomalyID = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", anomalyId);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Anomaly
                    {
                        AnomalyID = reader.GetInt32("AnomalyID"),
                        Name = reader.GetString("Name"),
                        Role = reader.GetString("Role"),
                        BaseHP = reader.GetInt32("BaseHP"),
                        BaseATK = reader.GetInt32("BaseATK"),
                        BaseDEF = reader.GetInt32("BaseDEF"),
                        BaseSPD = reader.GetInt32("BaseSPD"),
                        Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? "" : reader.GetString("Description"),
                        SpritePath = reader.IsDBNull(reader.GetOrdinal("SpritePath")) ? "" : reader.GetString("SpritePath")
                    };
                }
            }

            return null;
        }
    }
}
