using System;
using System.IO;

namespace WinFormsApp1
{
    public static class SoundGenerator
    {
        public static void GenerateAllSounds(string basePath)
        {
            string sfxPath = Path.Combine(basePath, "Assets", "Audio", "SFX");
            string bgmPath = Path.Combine(basePath, "Assets", "Audio", "BGM");

            Directory.CreateDirectory(sfxPath);
            Directory.CreateDirectory(bgmPath);

            // 1. SFX Attack: Fast pitch sweep down + noise impact (heavy slash/punch)
            CreateAttackSfx(Path.Combine(sfxPath, "sfx_attack.wav"));

            // 2. SFX Defend: Resonant metallic bell/shield clank (bright harmonic tone)
            CreateDefendSfx(Path.Combine(sfxPath, "sfx_defend.wav"));

            // 3. SFX Skill: Magical rising arpeggio + shimmering harmonics
            CreateSkillSfx(Path.Combine(sfxPath, "sfx_skill.wav"));

            // 4. SFX Hit: Heavy sub-bass thud + crunch
            CreateHitSfx(Path.Combine(sfxPath, "sfx_hit.wav"));

            // 5. SFX Click: Crisp click pop
            CreateClickSfx(Path.Combine(sfxPath, "sfx_click.wav"));

            // 6. SFX Hover: Subtle light blip
            CreateHoverSfx(Path.Combine(sfxPath, "sfx_hover.wav"));

            // 7. SFX Item: Item pickup chime
            CreateItemSfx(Path.Combine(sfxPath, "sfx_item.wav"));

            // 8. SFX Win / Victory: Victorious fanfare chords
            CreateWinSfx(Path.Combine(sfxPath, "sfx_win.wav"));
            CreateWinSfx(Path.Combine(sfxPath, "sfx_victory.wav"));

            // 9. BGM Battle: Upbeat bassline & melodic pulse loop (~6 sec loop)
            CreateBattleBgm(Path.Combine(bgmPath, "bgm_battle.wav"));

            // 10. BGM Menu: Chill ambient melody (~4 sec loop)
            CreateMenuBgm(Path.Combine(bgmPath, "bgm_menu.wav"));

            // 11. BGM Selection: Mysterious synth loop (~4 sec loop)
            CreateSelectionBgm(Path.Combine(bgmPath, "bgm_selection.wav"));
            CreateSelectionBgm(Path.Combine(bgmPath, "bgm_select.wav"));
        }

        private static void WriteWav(string filePath, short[] samples, int sampleRate = 44100)
        {
            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            using (var bw = new BinaryWriter(fs))
            {
                int byteCount = samples.Length * 2;

                bw.Write(System.Text.Encoding.ASCII.GetBytes("RIFF"));
                bw.Write(36 + byteCount);
                bw.Write(System.Text.Encoding.ASCII.GetBytes("WAVE"));

                bw.Write(System.Text.Encoding.ASCII.GetBytes("fmt "));
                bw.Write(16);
                bw.Write((short)1);
                bw.Write((short)1);
                bw.Write(sampleRate);
                bw.Write(sampleRate * 2);
                bw.Write((short)2);
                bw.Write((short)16);

                bw.Write(System.Text.Encoding.ASCII.GetBytes("data"));
                bw.Write(byteCount);

                for (int i = 0; i < samples.Length; i++)
                {
                    bw.Write(samples[i]);
                }
            }
        }

        private static void CreateAttackSfx(string path)
        {
            int sampleRate = 44100;
            double duration = 0.35;
            int totalSamples = (int)(sampleRate * duration);
            short[] samples = new short[totalSamples];
            Random rnd = new Random(42);

            for (int i = 0; i < totalSamples; i++)
            {
                double t = (double)i / sampleRate;
                double env = Math.Exp(-t * 14.0);
                double freq = 600.0 * Math.Exp(-t * 20.0) + 60.0;
                double wave = Math.Sin(2.0 * Math.PI * freq * t);
                double noise = (rnd.NextDouble() * 2.0 - 1.0) * Math.Exp(-t * 30.0);

                double sample = (wave * 0.7 + noise * 0.6) * env;
                sample = Math.Max(-1.0, Math.Min(1.0, sample));
                samples[i] = (short)(sample * 30000);
            }

            WriteWav(path, samples, sampleRate);
        }

        private static void CreateDefendSfx(string path)
        {
            int sampleRate = 44100;
            double duration = 0.45;
            int totalSamples = (int)(sampleRate * duration);
            short[] samples = new short[totalSamples];

            for (int i = 0; i < totalSamples; i++)
            {
                double t = (double)i / sampleRate;
                double env = Math.Exp(-t * 7.0);

                double tone1 = Math.Sin(2.0 * Math.PI * 820.0 * t);
                double tone2 = Math.Sin(2.0 * Math.PI * 1230.0 * t) * 0.6;
                double tone3 = Math.Sin(2.0 * Math.PI * 1840.0 * t) * 0.4;
                double tone4 = Math.Sin(2.0 * Math.PI * 2600.0 * t) * 0.25;
                double click = (t < 0.01) ? Math.Sin(2.0 * Math.PI * 3000.0 * t) * 0.5 : 0;

                double sample = (tone1 + tone2 + tone3 + tone4 + click) * 0.35 * env;
                sample = Math.Max(-1.0, Math.Min(1.0, sample));
                samples[i] = (short)(sample * 28000);
            }

            WriteWav(path, samples, sampleRate);
        }

        private static void CreateSkillSfx(string path)
        {
            int sampleRate = 44100;
            double duration = 0.5;
            int totalSamples = (int)(sampleRate * duration);
            short[] samples = new short[totalSamples];

            for (int i = 0; i < totalSamples; i++)
            {
                double t = (double)i / sampleRate;
                double env = (t < 0.05) ? (t / 0.05) : Math.Exp(-(t - 0.05) * 6.0);
                double freq = 300.0 + 900.0 * (t / duration);
                double wave1 = Math.Sin(2.0 * Math.PI * freq * t);
                double wave2 = Math.Sin(2.0 * Math.PI * (freq * 1.5) * t) * 0.4;
                double wave3 = Math.Sin(2.0 * Math.PI * (freq * 2.0) * t) * 0.25;

                double sample = (wave1 + wave2 + wave3) * 0.45 * env;
                sample = Math.Max(-1.0, Math.Min(1.0, sample));
                samples[i] = (short)(sample * 28000);
            }

            WriteWav(path, samples, sampleRate);
        }

        private static void CreateHitSfx(string path)
        {
            int sampleRate = 44100;
            double duration = 0.3;
            int totalSamples = (int)(sampleRate * duration);
            short[] samples = new short[totalSamples];
            Random rnd = new Random(99);

            for (int i = 0; i < totalSamples; i++)
            {
                double t = (double)i / sampleRate;
                double env = Math.Exp(-t * 16.0);
                double thud = Math.Sin(2.0 * Math.PI * (120.0 * Math.Exp(-t * 15.0) + 40.0) * t);
                double noise = (rnd.NextDouble() * 2.0 - 1.0) * Math.Exp(-t * 25.0);

                double sample = (thud * 0.8 + noise * 0.4) * env;
                sample = Math.Max(-1.0, Math.Min(1.0, sample));
                samples[i] = (short)(sample * 30000);
            }

            WriteWav(path, samples, sampleRate);
        }

        private static void CreateClickSfx(string path)
        {
            int sampleRate = 44100;
            double duration = 0.08;
            int totalSamples = (int)(sampleRate * duration);
            short[] samples = new short[totalSamples];

            for (int i = 0; i < totalSamples; i++)
            {
                double t = (double)i / sampleRate;
                double env = Math.Exp(-t * 60.0);
                double wave = Math.Sin(2.0 * Math.PI * 1200.0 * t);
                double sample = wave * env * 0.6;
                samples[i] = (short)(sample * 25000);
            }

            WriteWav(path, samples, sampleRate);
        }

        private static void CreateHoverSfx(string path)
        {
            int sampleRate = 44100;
            double duration = 0.05;
            int totalSamples = (int)(sampleRate * duration);
            short[] samples = new short[totalSamples];

            for (int i = 0; i < totalSamples; i++)
            {
                double t = (double)i / sampleRate;
                double env = Math.Exp(-t * 80.0);
                double wave = Math.Sin(2.0 * Math.PI * 1800.0 * t);
                double sample = wave * env * 0.3;
                samples[i] = (short)(sample * 15000);
            }

            WriteWav(path, samples, sampleRate);
        }

        private static void CreateItemSfx(string path)
        {
            int sampleRate = 44100;
            double duration = 0.35;
            int totalSamples = (int)(sampleRate * duration);
            short[] samples = new short[totalSamples];

            double[] chimes = { 587.33, 880.00, 1174.66 }; // D5, A5, D6
            double stepLen = duration / chimes.Length;

            for (int i = 0; i < totalSamples; i++)
            {
                double t = (double)i / sampleRate;
                int step = Math.Min(chimes.Length - 1, (int)(t / stepLen));
                double stepT = t - (step * stepLen);
                double env = Math.Exp(-stepT * 12.0);

                double freq = chimes[step];
                double wave = Math.Sin(2.0 * Math.PI * freq * t);
                double sample = wave * env * 0.5;
                samples[i] = (short)(sample * 25000);
            }

            WriteWav(path, samples, sampleRate);
        }

        private static void CreateWinSfx(string path)
        {
            int sampleRate = 44100;
            double duration = 1.2;
            int totalSamples = (int)(sampleRate * duration);
            short[] samples = new short[totalSamples];

            double[] notes = { 523.25, 659.25, 783.99, 1046.50 };
            double noteLen = 0.22;

            for (int i = 0; i < totalSamples; i++)
            {
                double t = (double)i / sampleRate;
                int noteIdx = Math.Min(notes.Length - 1, (int)(t / noteLen));
                double noteT = t - (noteIdx * noteLen);
                double env = Math.Exp(-noteT * 5.0);

                double freq = notes[noteIdx];
                double wave1 = Math.Sin(2.0 * Math.PI * freq * t);
                double wave2 = Math.Sin(2.0 * Math.PI * (freq * 2.0) * t) * 0.3;

                double sample = (wave1 + wave2) * 0.45 * env;
                sample = Math.Max(-1.0, Math.Min(1.0, sample));
                samples[i] = (short)(sample * 27000);
            }

            WriteWav(path, samples, sampleRate);
        }

        private static void CreateBattleBgm(string path)
        {
            int sampleRate = 22050;
            double duration = 6.0;
            int totalSamples = (int)(sampleRate * duration);
            short[] samples = new short[totalSamples];

            double[] bassline = { 110.0, 110.0, 130.81, 146.83, 110.0, 164.81, 146.83, 130.81 };
            double noteTime = duration / bassline.Length;

            for (int i = 0; i < totalSamples; i++)
            {
                double t = (double)i / sampleRate;
                int step = (int)(t / noteTime) % bassline.Length;
                double stepT = t % noteTime;
                double bassEnv = Math.Exp(-stepT * 4.0);

                double bass = Math.Sin(2.0 * Math.PI * bassline[step] * t) * bassEnv;
                double drum = (stepT < 0.05) ? Math.Sin(2.0 * Math.PI * 60.0 * stepT) * 0.7 : 0;
                double lead = Math.Sin(2.0 * Math.PI * (bassline[step] * 4.0) * t) * 0.15;

                double sample = (bass * 0.5 + drum * 0.4 + lead) * 0.4;
                sample = Math.Max(-1.0, Math.Min(1.0, sample));
                samples[i] = (short)(sample * 20000);
            }

            WriteWav(path, samples, sampleRate);
        }

        private static void CreateMenuBgm(string path)
        {
            int sampleRate = 22050;
            double duration = 4.0;
            int totalSamples = (int)(sampleRate * duration);
            short[] samples = new short[totalSamples];

            double[] chords = { 261.63, 329.63, 392.00, 523.25 };
            double noteTime = duration / chords.Length;

            for (int i = 0; i < totalSamples; i++)
            {
                double t = (double)i / sampleRate;
                int step = (int)(t / noteTime) % chords.Length;
                double stepT = t % noteTime;
                double env = Math.Exp(-stepT * 2.0);

                double wave = Math.Sin(2.0 * Math.PI * chords[step] * t);
                double sub = Math.Sin(2.0 * Math.PI * (chords[step] / 2.0) * t) * 0.4;

                double sample = (wave + sub) * 0.35 * env;
                sample = Math.Max(-1.0, Math.Min(1.0, sample));
                samples[i] = (short)(sample * 18000);
            }

            WriteWav(path, samples, sampleRate);
        }

        private static void CreateSelectionBgm(string path)
        {
            int sampleRate = 22050;
            double duration = 4.0;
            int totalSamples = (int)(sampleRate * duration);
            short[] samples = new short[totalSamples];

            double[] chords = { 220.00, 277.18, 329.63, 440.00 };
            double noteTime = duration / chords.Length;

            for (int i = 0; i < totalSamples; i++)
            {
                double t = (double)i / sampleRate;
                int step = (int)(t / noteTime) % chords.Length;
                double stepT = t % noteTime;
                double env = Math.Exp(-stepT * 2.5);

                double wave = Math.Sin(2.0 * Math.PI * chords[step] * t);
                double sample = wave * 0.3 * env;
                sample = Math.Max(-1.0, Math.Min(1.0, sample));
                samples[i] = (short)(sample * 18000);
            }

            WriteWav(path, samples, sampleRate);
        }
    }
}
