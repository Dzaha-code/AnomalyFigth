using System;
using System.IO;
using System.Windows.Forms;

namespace WinFormsApp1
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Generate sound assets if not already generated
            try
            {
                SoundGenerator.GenerateAllSounds(AppDomain.CurrentDomain.BaseDirectory);

                // Also try generating in the project source tree if found
                string sourceProjectAssets = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));
                if (Directory.Exists(Path.Combine(sourceProjectAssets, "Assets")))
                {
                    SoundGenerator.GenerateAllSounds(sourceProjectAssets);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Sound Generator Init: {ex.Message}");
            }

            Application.Run(new MainMenuForm());
        }
    }
}