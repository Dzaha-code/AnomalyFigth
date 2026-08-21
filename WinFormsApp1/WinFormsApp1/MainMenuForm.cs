using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using WinFormsApp1.UI;

namespace WinFormsApp1
{
    /// <summary>
    /// MainMenuForm.cs — Tampilan Menu Utama bergaya Anime RPG (Handheld DS/GBA Style).
    /// </summary>
    public partial class MainMenuForm : Form
    {
        // Sistem partikel melayang di background
        private class Particle
        {
            public float X, Y, Speed, Size;
            public int Alpha;
        }

        private List<Particle> particles = new List<Particle>();
        private System.Windows.Forms.Timer particleTimer = null!;
        private System.Windows.Forms.Timer blinkTimer = null!;
        private Random rnd = new Random();

        public MainMenuForm()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);

            InitParticles();
            InitTimers();
        }

        private void InitParticles()
        {
            particles.Clear();
            for (int i = 0; i < 15; i++)
            {
                particles.Add(new Particle
                {
                    X = rnd.Next(0, 640),
                    Y = rnd.Next(0, 420),
                    Speed = 0.5f + (float)rnd.NextDouble() * 1.2f,
                    Size = rnd.Next(2, 6),
                    Alpha = rnd.Next(80, 200)
                });
            }
        }

        private void InitTimers()
        {
            // Timer partikel background (30ms ~ 33fps)
            particleTimer = new System.Windows.Forms.Timer { Interval = 30 };
            particleTimer.Tick += (s, e) =>
            {
                foreach (var p in particles)
                {
                    p.Y -= p.Speed;
                    if (p.Y < 0)
                    {
                        p.Y = 420;
                        p.X = rnd.Next(0, 640);
                    }
                }
                Invalidate();
            };

            // Timer kedip subtitle (600ms)
            blinkTimer = new System.Windows.Forms.Timer { Interval = 600 };
            blinkTimer.Tick += (s, e) =>
            {
                lblPressStart.Visible = !lblPressStart.Visible;
            };
        }

        private void MainMenuForm_Load(object sender, EventArgs e)
        {
            FadeIn(this, 300);
            particleTimer.Start();
            blinkTimer.Start();

            // Putar BGM Menu
            AudioManager.PlayBGM("bgm_menu.wav");
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            particleTimer.Stop();
            particleTimer.Dispose();
            blinkTimer.Stop();
            blinkTimer.Dispose();
            base.OnFormClosing(e);
        }

        // ===== CUSTOM PAINTING: GRADIENT + GLOW TITLE + PARTICLES =====

        private void MainMenuForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // 1. Background Gradient Vertikal (#1A1A2E -> #0F3460)
            Color topBg = Color.FromArgb(26, 26, 46);    // #1A1A2E
            Color bottomBg = Color.FromArgb(15, 52, 96); // #0F3460
            using (LinearGradientBrush bgBrush = new LinearGradientBrush(ClientRectangle, topBg, bottomBg, LinearGradientMode.Vertical))
            {
                g.FillRectangle(bgBrush, ClientRectangle);
            }

            // 2. Partikel Melayang di Background
            foreach (var p in particles)
            {
                using (SolidBrush pBrush = new SolidBrush(Color.FromArgb(p.Alpha, 233, 69, 96))) // Aksen merah-pink
                {
                    g.FillEllipse(pBrush, p.X, p.Y, p.Size, p.Size);
                }
            }

            // 3. Judul "ANOMALY VERSUS" dengan Efek Glow Ganda
            string title = "ANOMALY VERSUS";
            using (Font titleFont = new Font("Segoe UI Black", 28, FontStyle.Bold))
            {
                // Shadow / Red Glow Offset
                using (SolidBrush glowBrush = new SolidBrush(Color.FromArgb(180, 233, 69, 96))) // #E94560
                {
                    g.DrawString(title, titleFont, glowBrush, 122, 62);
                    g.DrawString(title, titleFont, glowBrush, 118, 58);
                }

                // Teks Utama Putih Hangat
                using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(234, 234, 234))) // #EAEAEA
                {
                    g.DrawString(title, titleFont, textBrush, 120, 60);
                }
            }

            // Garis Aksen Emas di Bawah Judul
            using (Pen goldPen = new Pen(Color.FromArgb(245, 166, 35), 3))
            {
                g.DrawLine(goldPen, 180, 125, 460, 125);
            }
        }

        // ===== TRANSISI FADE IN / FADE OUT =====

        private void FadeIn(Form form, int durationMs = 300)
        {
            form.Opacity = 0;
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer { Interval = 16 };
            timer.Tick += (s, e) =>
            {
                form.Opacity += (16.0 / durationMs);
                if (form.Opacity >= 1)
                {
                    form.Opacity = 1;
                    timer.Stop();
                    timer.Dispose();
                }
            };
            timer.Start();
        }

        private void FadeOut(Form form, Action onComplete, int durationMs = 200)
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer { Interval = 16 };
            timer.Tick += (s, e) =>
            {
                form.Opacity -= (16.0 / durationMs);
                if (form.Opacity <= 0)
                {
                    form.Opacity = 0;
                    timer.Stop();
                    timer.Dispose();
                    onComplete?.Invoke();
                }
            };
            timer.Start();
        }

        // ===== BUTTON CLICKS =====

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            AudioManager.PlaySFX("sfx_click.wav");

            FadeOut(this, () =>
            {
                PlayerNameForm playerForm = new PlayerNameForm();
                this.Hide();
                this.Opacity = 1;
                playerForm.ShowDialog();
                this.Show();
                FadeIn(this, 300);
            });
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            AudioManager.PlaySFX("sfx_click.wav");
            FadeOut(this, () =>
            {
                AudioManager.StopBGM();
                Application.Exit();
            });
        }
    }
}
