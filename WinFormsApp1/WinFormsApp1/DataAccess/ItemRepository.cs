using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using WinFormsApp1.Models;

namespace WinFormsApp1.DataAccess
{
    /// <summary>
    /// ItemRepository.cs — Repository untuk operasi CRUD tabel Item.
    /// Pola standar: ItemRepository repo = new ItemRepository();
    ///              List<Item> list = repo.GetAllItem();
    /// </summary>
    public class ItemRepository
    {
        /// <summary>
        /// Ambil semua Item dari database.
        /// </summary>
        public List<Item> GetAllItem()
        {
            List<Item> list = new List<Item>();

            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM Item";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                double effectVal = 0;
                                int ordinalVal = reader.GetOrdinal("EffectValue");
                                if (!reader.IsDBNull(ordinalVal))
                                {
                                    effectVal = Convert.ToDouble(reader.GetValue(ordinalVal));
                                }

                                bool isPercent = false;
                                int ordinalPercent = reader.GetOrdinal("IsPercentage");
                                if (!reader.IsDBNull(ordinalPercent))
                                {
                                    isPercent = Convert.ToInt32(reader.GetValue(ordinalPercent)) == 1;
                                }

                                Item item = new Item
                                {
                                    ItemID = reader.GetInt32("ItemID"),
                                    Name = reader.GetString("Name"),
                                    EffectType = reader.GetString("EffectType"),
                                    EffectValue = effectVal,
                                    IsPercentage = isPercent,
                                    Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? "" : reader.GetString("Description"),
                                    IconPath = reader.IsDBNull(reader.GetOrdinal("IconPath")) ? "" : reader.GetString("IconPath")
                                };
                                list.Add(item);
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"[ItemRepository Row Error] {ex.Message}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ItemRepository Error] {ex.Message}");
            }

            return list;
        }
    }
}
