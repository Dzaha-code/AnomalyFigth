using MySql.Data.MySqlClient;
using System.Collections.Generic;
using WinFormsApp1.Models;

namespace WinFormsApp1.DataAccess
{
    public class ItemRepository
    {
        // Ambil semua item (dipakai di form pemilihan item)
        public List<Item> GetAllItem()
        {
            var list = new List<Item>();

            using (var conn = DBConnection.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Item";
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Item
                        {
                            ItemID = reader.GetInt32("ItemID"),
                            Name = reader.GetString("Name"),
                            EffectType = reader.GetString("EffectType"),
                            EffectValue = reader.GetFloat("EffectValue"),
                            IsPercentage = reader.GetBoolean("IsPercentage"),
                            Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? "" : reader.GetString("Description"),
                            IconPath = reader.IsDBNull(reader.GetOrdinal("IconPath")) ? "" : reader.GetString("IconPath")
                        });
                    }
                }
            }
            return list;
        }

        // Tambah item baru
        public void AddItem(Item i)
        {
            using (var conn = DBConnection.GetConnection())
            {
                conn.Open();
                string query = @"INSERT INTO Item (Name, EffectType, EffectValue, IsPercentage, Description, IconPath)
                                  VALUES (@name, @type, @value, @isPercent, @desc, @icon)";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", i.Name);
                    cmd.Parameters.AddWithValue("@type", i.EffectType);
                    cmd.Parameters.AddWithValue("@value", i.EffectValue);
                    cmd.Parameters.AddWithValue("@isPercent", i.IsPercentage);
                    cmd.Parameters.AddWithValue("@desc", i.Description ?? "");
                    cmd.Parameters.AddWithValue("@icon", i.IconPath ?? "");
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
