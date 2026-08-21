using System;
using System.Collections.Generic;

namespace WinFormsApp1.Utilities
{
    /// <summary>
    /// Helper untuk debug skill loading
    /// </summary>
    public class SkillDebugger
    {
        public static void LogSkillsLoaded(List<Models.Skill> skills, string playerName)
        {
            System.Diagnostics.Debug.WriteLine("");
            System.Diagnostics.Debug.WriteLine("═══════════════════════════════════════");
            System.Diagnostics.Debug.WriteLine($"[SkillDebugger] {playerName} Skills Loaded:");
            System.Diagnostics.Debug.WriteLine("═══════════════════════════════════════");

            if (skills == null || skills.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("⚠️ WARNING: NO SKILLS LOADED!");
                return;
            }

            foreach (var skill in skills)
            {
                System.Diagnostics.Debug.WriteLine($"  • {skill.Name}");
                System.Diagnostics.Debug.WriteLine($"    - SkillID: {skill.SkillID}");
                System.Diagnostics.Debug.WriteLine($"    - Type: {skill.SkillType}");
                System.Diagnostics.Debug.WriteLine($"    - Power: {skill.Power}");
                System.Diagnostics.Debug.WriteLine($"    - Cooldown: {skill.Cooldown}");
                System.Diagnostics.Debug.WriteLine($"    - Description: {skill.Description}");
            }

            System.Diagnostics.Debug.WriteLine("═══════════════════════════════════════");
            System.Diagnostics.Debug.WriteLine($"Total Skills: {skills.Count}");
            System.Diagnostics.Debug.WriteLine("");
        }

        public static void LogDatabaseSchema()
        {
            System.Diagnostics.Debug.WriteLine("");
            System.Diagnostics.Debug.WriteLine("🔍 Database Skill Table Schema:");

            try
            {
                using (var conn = DataAccess.DBConnection.GetConnection())
                {
                    conn.Open();
                    var cmd = new MySql.Data.MySqlClient.MySqlCommand("DESC Skill;", conn);
                    using (var reader = cmd.ExecuteReader())
                    {
                        System.Diagnostics.Debug.WriteLine("┌─ Field ─────────────┬─ Type ──────────┬─ Null ─┐");
                        while (reader.Read())
                        {
                            string field = (reader["Field"]?.ToString() ?? "").PadRight(20);
                            string type = (reader["Type"]?.ToString() ?? "").PadRight(15);
                            string isNull = (reader["Null"]?.ToString() ?? "").PadRight(6);
                            System.Diagnostics.Debug.WriteLine($"│ {field} │ {type} │ {isNull} │");
                        }
                        System.Diagnostics.Debug.WriteLine("└──────────────────────┴─────────────────┴────────┘");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Error checking schema: {ex.Message}");
            }
        }
    }
}
