using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using WinFormsApp1.Models;

namespace WinFormsApp1.DataAccess
{
    /// <summary>
    /// SkillRepository.cs — Repository untuk operasi CRUD tabel Skill.
    /// Pola standar: SkillRepository repo = new SkillRepository();
    ///              List<Skill> list = repo.GetSkillsByAnomalyId(anomalyId);
    /// </summary>
    public class SkillRepository
    {
        /// <summary>
        /// Ambil semua Skill milik Anomaly tertentu berdasarkan AnomalyID.
        /// Jika belum ada di database, sediakan skill default sesuai role.
        /// </summary>
        public List<Skill> GetSkillsByAnomalyId(int anomalyId, string role = "Attacker")
        {
            List<Skill> list = new List<Skill>();

            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM Skill WHERE AnomalyID = @anomalyId";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@anomalyId", anomalyId);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                double power = 1.0;
                                int ordinalPower = reader.GetOrdinal("Power");
                                if (!reader.IsDBNull(ordinalPower))
                                {
                                    power = Convert.ToDouble(reader.GetValue(ordinalPower));
                                }

                                int cooldown = 2;
                                int ordinalCd = reader.GetOrdinal("Cooldown");
                                if (!reader.IsDBNull(ordinalCd))
                                {
                                    cooldown = Convert.ToInt32(reader.GetValue(ordinalCd));
                                }

                                Skill skill = new Skill
                                {
                                    SkillID = reader.GetInt32("SkillID"),
                                    AnomalyID = reader.GetInt32("AnomalyID"),
                                    Name = reader.GetString("Name"),
                                    SkillType = reader.IsDBNull(reader.GetOrdinal("SkillType")) ? "Damage" : reader.GetString("SkillType"),
                                    Power = power,
                                    Cooldown = cooldown,
                                    Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? "" : reader.GetString("Description")
                                };
                                list.Add(skill);
                            }
                            catch
                            {
                                // Skip broken row
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[SkillRepository Error] {ex.Message}");
            }

            // Fallback jika database belum memiliki skill untuk anomaly ini
            if (list.Count == 0)
            {
                list = GenerateFallbackSkills(anomalyId, role);
            }

            return list;
        }

        private List<Skill> GenerateFallbackSkills(int anomalyId, string role)
        {
            List<Skill> fallbacks = new List<Skill>();

            if (role.Equals("Defender", StringComparison.OrdinalIgnoreCase))
            {
                fallbacks.Add(new Skill { AnomalyID = anomalyId, Name = "Shield Barrier", SkillType = "Buff", Power = 1.4, Cooldown = 3, Description = "Meningkatkan DEF sebesar 40%." });
                fallbacks.Add(new Skill { AnomalyID = anomalyId, Name = "Heavy Impact", SkillType = "Damage", Power = 1.3, Cooldown = 2, Description = "Serangan benturan kuat." });
            }
            else if (role.Equals("Support", StringComparison.OrdinalIgnoreCase))
            {
                fallbacks.Add(new Skill { AnomalyID = anomalyId, Name = "Vitalize", SkillType = "Heal", Power = 1.5, Cooldown = 3, Description = "Memulihkan HP." });
                fallbacks.Add(new Skill { AnomalyID = anomalyId, Name = "Aura Boost", SkillType = "Buff", Power = 1.3, Cooldown = 2, Description = "Meningkatkan ATK." });
            }
            else // Attacker
            {
                fallbacks.Add(new Skill { AnomalyID = anomalyId, Name = "Fatal Strike", SkillType = "Damage", Power = 1.7, Cooldown = 2, Description = "Serangan mematikan dengan damage besar." });
                fallbacks.Add(new Skill { AnomalyID = anomalyId, Name = "Berserk Rage", SkillType = "Buff", Power = 1.4, Cooldown = 3, Description = "Meningkatkan ATK secara drastis." });
            }

            return fallbacks;
        }
    }
}
