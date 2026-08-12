using MySql.Data.MySqlClient;
using System.Collections.Generic;
using WinFormsApp1.Models;

namespace WinFormsApp1.DataAccess
{
    public class SkillRepository
    {
        // Ambil semua skill milik 1 Anomaly (dipakai saat battle)
        public List<Skill> GetSkillsByAnomalyId(int anomalyId)
        {
            var list = new List<Skill>();

            using (var conn = DBConnection.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Skill WHERE AnomalyID = @anomalyId";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@anomalyId", anomalyId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Skill
                            {
                                SkillID = reader.GetInt32("SkillID"),
                                AnomalyID = reader.GetInt32("AnomalyID"),
                                Name = reader.GetString("Name"),
                                SkillType = reader.GetString("SkillType"),
                                Power = reader.GetFloat("Power"),
                                Cooldown = reader.GetInt32("Cooldown"),
                                Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? "" : reader.GetString("Description")
                            });
                        }
                    }
                }
            }
            return list;
        }

        // Tambah skill baru
        public void AddSkill(Skill s)
        {
            using (var conn = DBConnection.GetConnection())
            {
                conn.Open();
                string query = @"INSERT INTO Skill (AnomalyID, Name, SkillType, Power, Cooldown, Description)
                                  VALUES (@anomalyId, @name, @type, @power, @cooldown, @desc)";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@anomalyId", s.AnomalyID);
                    cmd.Parameters.AddWithValue("@name", s.Name);
                    cmd.Parameters.AddWithValue("@type", s.SkillType);
                    cmd.Parameters.AddWithValue("@power", s.Power);
                    cmd.Parameters.AddWithValue("@cooldown", s.Cooldown);
                    cmd.Parameters.AddWithValue("@desc", s.Description ?? "");
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
