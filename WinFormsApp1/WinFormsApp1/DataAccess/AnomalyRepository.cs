using MySql.Data.MySqlClient;
using System.Collections.Generic;
using WinFormsApp1.Models;

namespace WinFormsApp1.DataAccess
{
    public class AnomalyRepository
    {
        // Ambil semua data Anomaly
        public List<Anomaly> GetAllAnomaly()
        {
            var list = new List<Anomaly>();

            using (var conn = DBConnection.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Anomaly";
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Anomaly
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
                        });
                    }
                }
            }
            return list;
        }

        // Ambil 1 Anomaly berdasarkan ID
        public Anomaly GetAnomalyById(int id)
        {
            Anomaly result = null;

            using (var conn = DBConnection.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Anomaly WHERE AnomalyID = @id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result = new Anomaly
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
                }
            }
            return result;
        }

        // Tambah Anomaly baru
        public void AddAnomaly(Anomaly a)
        {
            using (var conn = DBConnection.GetConnection())
            {
                conn.Open();
                string query = @"INSERT INTO Anomaly (Name, Role, BaseHP, BaseATK, BaseDEF, BaseSPD, Description, SpritePath)
                                  VALUES (@name, @role, @hp, @atk, @def, @spd, @desc, @sprite)";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", a.Name);
                    cmd.Parameters.AddWithValue("@role", a.Role);
                    cmd.Parameters.AddWithValue("@hp", a.BaseHP);
                    cmd.Parameters.AddWithValue("@atk", a.BaseATK);
                    cmd.Parameters.AddWithValue("@def", a.BaseDEF);
                    cmd.Parameters.AddWithValue("@spd", a.BaseSPD);
                    cmd.Parameters.AddWithValue("@desc", a.Description ?? "");
                    cmd.Parameters.AddWithValue("@sprite", a.SpritePath ?? "");
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Update Anomaly
        public void UpdateAnomaly(Anomaly a)
        {
            using (var conn = DBConnection.GetConnection())
            {
                conn.Open();
                string query = @"UPDATE Anomaly SET Name=@name, Role=@role, BaseHP=@hp, BaseATK=@atk,
                                  BaseDEF=@def, BaseSPD=@spd, Description=@desc, SpritePath=@sprite
                                  WHERE AnomalyID=@id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", a.AnomalyID);
                    cmd.Parameters.AddWithValue("@name", a.Name);
                    cmd.Parameters.AddWithValue("@role", a.Role);
                    cmd.Parameters.AddWithValue("@hp", a.BaseHP);
                    cmd.Parameters.AddWithValue("@atk", a.BaseATK);
                    cmd.Parameters.AddWithValue("@def", a.BaseDEF);
                    cmd.Parameters.AddWithValue("@spd", a.BaseSPD);
                    cmd.Parameters.AddWithValue("@desc", a.Description ?? "");
                    cmd.Parameters.AddWithValue("@sprite", a.SpritePath ?? "");
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Hapus Anomaly
        public void DeleteAnomaly(int id)
        {
            using (var conn = DBConnection.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM Anomaly WHERE AnomalyID = @id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
