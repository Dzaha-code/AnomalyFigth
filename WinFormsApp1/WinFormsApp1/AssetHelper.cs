using System;
using System.Drawing;
using System.IO;

namespace WinFormsApp1
{
    /// <summary>
    /// AssetHelper - Helper untuk load gambar dari folder Assets
    /// Semua path relatif terhadap folder Assets di output directory
    /// 
    /// Contoh pemakaian:
    ///   pictureBox1.Image = AssetHelper.LoadAnomalyImage("ferrox.png");
    ///   pictureBox1.Image = AssetHelper.LoadItemIcon("iron_amulet.png");
    ///   this.BackgroundImage = AssetHelper.LoadUIImage("bg_menu.png");
    /// </summary>
    public static class AssetHelper
    {
        // Base path ke folder Assets (relatif dari .exe)
        private static readonly string AssetsPath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, "Assets");

        /// <summary>
        /// Load gambar Anomaly berdasarkan nama file (SpritePath dari database)
        /// Contoh: AssetHelper.LoadAnomalyImage("ferrox.png")
        /// </summary>
        public static Image? LoadAnomalyImage(string spritePath)
        {
            if (string.IsNullOrEmpty(spritePath)) return null;

            string fullPath = Path.Combine(AssetsPath, "Images", "Anomaly", spritePath);

            if (File.Exists(fullPath))
            {
                // Pakai MemoryStream agar file tidak ter-lock
                byte[] bytes = File.ReadAllBytes(fullPath);
                using (var ms = new MemoryStream(bytes))
                {
                    return Image.FromStream(ms);
                }
            }

            return null; // File tidak ditemukan
        }

        /// <summary>
        /// Load icon Item berdasarkan nama file (IconPath dari database)
        /// Contoh: AssetHelper.LoadItemIcon("iron_amulet.png")
        /// </summary>
        public static Image? LoadItemIcon(string iconPath)
        {
            if (string.IsNullOrEmpty(iconPath)) return null;

            string fullPath = Path.Combine(AssetsPath, "Images", "Item", iconPath);

            if (File.Exists(fullPath))
            {
                byte[] bytes = File.ReadAllBytes(fullPath);
                using (var ms = new MemoryStream(bytes))
                {
                    return Image.FromStream(ms);
                }
            }

            return null;
        }

        /// <summary>
        /// Load gambar UI (background, logo, dll)
        /// Contoh: AssetHelper.LoadUIImage("bg_menu.png")
        /// </summary>
        public static Image? LoadUIImage(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return null;

            string fullPath = Path.Combine(AssetsPath, "Images", "UI", fileName);

            if (File.Exists(fullPath))
            {
                byte[] bytes = File.ReadAllBytes(fullPath);
                using (var ms = new MemoryStream(bytes))
                {
                    return Image.FromStream(ms);
                }
            }

            return null;
        }
    }
}
