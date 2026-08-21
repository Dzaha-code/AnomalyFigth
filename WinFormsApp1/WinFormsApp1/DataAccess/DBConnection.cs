using MySql.Data.MySqlClient;

namespace WinFormsApp1.DataAccess
{
    /// <summary>
    /// DBConnection.cs — Kelas helper untuk membuat koneksi ke database MySQL.
    /// Menggunakan Laragon + phpMyAdmin, database: anomaly_versus_db
    /// 
    /// Contoh pemakaian:
    /// using (var conn = DBConnection.GetConnection()) {
    ///     conn.Open();
    ///     // query...
    /// }
    /// </summary>
    public static class DBConnection
    {
        // Connection string ke MySQL lokal via Laragon
        // Sesuaikan port/password jika berbeda di setup kamu
        private static string connectionString =
            "Server=localhost;Port=3306;Database=anomaly_versus_db;Uid=root;Pwd=;";

        /// <summary>
        /// Buat dan return koneksi MySQL baru (belum di-Open).
        /// Caller harus Open() sendiri dan wrap dalam using.
        /// </summary>
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
