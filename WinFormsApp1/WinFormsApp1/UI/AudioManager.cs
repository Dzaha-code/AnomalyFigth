using System;
using System.IO;
using System.Media;
using System.Threading.Tasks;

namespace WinFormsApp1.UI
{
    /// <summary>
    /// AudioManager.cs — Static audio controller untuk pemutaran BGM (looping) dan SFX (non-blocking).
    /// </summary>
    public static class AudioManager
    {
        private static readonly string BaseAudioPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Audio");
        private static SoundPlayer? bgmPlayer;
        private static string? currentBgmFile;

        /// <summary>
        /// Putar BGM secara looping dari folder Assets/Audio/BGM/
        /// </summary>
        public static void PlayBGM(string fileName)
        {
            try
            {
                if (currentBgmFile == fileName && bgmPlayer != null)
                {
                    return; // BGM yang sama sudah sedang berputar
                }

                StopBGM();

                // Cek kemungkinan path (Assets/Audio/BGM atau Assets/BGM)
                string fullPath = Path.Combine(BaseAudioPath, "BGM", fileName);
                if (!File.Exists(fullPath))
                {
                    fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "BGM", fileName);
                }

                if (File.Exists(fullPath))
                {
                    currentBgmFile = fileName;
                    bgmPlayer = new SoundPlayer(fullPath);
                    bgmPlayer.PlayLooping();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"[AudioManager] BGM file not found: {fullPath}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AudioManager] BGM Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Hentikan BGM yang sedang berputar
        /// </summary>
        public static void StopBGM()
        {
            try
            {
                if (bgmPlayer != null)
                {
                    bgmPlayer.Stop();
                    bgmPlayer.Dispose();
                    bgmPlayer = null;
                }
                currentBgmFile = null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AudioManager] StopBGM Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Putar SFX secara non-blocking di background thread dari folder Assets/Audio/SFX/
        /// </summary>
        public static void PlaySFX(string fileName)
        {
            try
            {
                string fullPath = Path.Combine(BaseAudioPath, "SFX", fileName);
                if (!File.Exists(fullPath))
                {
                    fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "SFX", fileName);
                }

                if (File.Exists(fullPath))
                {
                    Task.Run(() =>
                    {
                        try
                        {
                            using (var sfx = new SoundPlayer(fullPath))
                            {
                                sfx.PlaySync();
                            }
                        }
                        catch { }
                    });
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AudioManager] SFX Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Pengaturan volume (placeholder untuk kompatibilitas interface)
        /// </summary>
        public static void SetBGMVolume(double volume)
        {
            // SoundPlayer native Windows tidak menyediakan direct API volume,
            // method ini disediakan untuk menjaga signature interface tetap kompatibel.
        }
    }
}
