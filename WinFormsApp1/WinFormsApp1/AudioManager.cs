using System;
using System.IO;
using System.Media;

namespace WinFormsApp1
{
    /// <summary>
    /// AudioManager - Helper untuk memutar BGM dan SFX
    /// Menggunakan SoundPlayer bawaan .NET (support format .wav saja)
    /// 
    /// Contoh pemakaian:
    ///   AudioManager.PlayBGM("bgm_menu.wav");     // Loop terus
    ///   AudioManager.PlaySFX("sfx_click.wav");     // Sekali putar
    ///   AudioManager.StopBGM();                     // Stop BGM
    /// </summary>
    public static class AudioManager
    {
        // Base path ke folder Audio
        private static readonly string AudioPath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, "Assets", "Audio");

        // SoundPlayer untuk BGM (disimpan supaya bisa di-stop)
        private static SoundPlayer? bgmPlayer;

        /// <summary>
        /// Putar BGM (background music) secara loop
        /// Contoh: AudioManager.PlayBGM("bgm_menu.wav");
        /// </summary>
        public static void PlayBGM(string fileName)
        {
            try
            {
                StopBGM(); // Stop BGM sebelumnya dulu

                string fullPath = Path.Combine(AudioPath, "BGM", fileName);
                if (File.Exists(fullPath))
                {
                    bgmPlayer = new SoundPlayer(fullPath);
                    bgmPlayer.PlayLooping(); // Loop terus sampai di-stop
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"BGM Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Stop BGM yang sedang diputar
        /// </summary>
        public static void StopBGM()
        {
            if (bgmPlayer != null)
            {
                bgmPlayer.Stop();
                bgmPlayer.Dispose();
                bgmPlayer = null;
            }
        }

        /// <summary>
        /// Putar SFX (sound effect) sekali saja
        /// Dijalankan di thread terpisah agar tidak blocking UI dan tidak mengganggu BGM
        /// 
        /// Contoh: AudioManager.PlaySFX("sfx_click.wav");
        /// </summary>
        public static void PlaySFX(string fileName)
        {
            try
            {
                string fullPath = Path.Combine(AudioPath, "SFX", fileName);
                if (File.Exists(fullPath))
                {
                    // Pakai thread terpisah agar tidak blocking UI
                    System.Threading.Tasks.Task.Run(() =>
                    {
                        try
                        {
                            using (var sfxPlayer = new SoundPlayer(fullPath))
                            {
                                sfxPlayer.PlaySync(); // Play sampai selesai di thread terpisah
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"SFX Play Error: {ex.Message}");
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SFX Error: {ex.Message}");
            }
        }
    }
}
