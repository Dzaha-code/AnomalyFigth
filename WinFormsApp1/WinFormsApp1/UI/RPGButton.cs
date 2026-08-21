using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinFormsApp1.UI
{
    /// <summary>
    /// RPGButton.cs — Custom Button bertema Anime RPG (DS/GBA style).
    /// Mendukung rounded corners (radius 8px), gradient fill, hover gold, press down effect, dan IsActive flag.
    /// </summary>
    public class RPGButton : Button
    {
        private bool isHovered = false;
        private bool isPressed = false;
        private bool isActive = false;

        // Palet warna Anime RPG
        private readonly Color NormalTop = Color.FromArgb(233, 69, 96);    // #E94560
        private readonly Color NormalBottom = Color.FromArgb(192, 57, 43); // #C0392B
        private readonly Color HoverTop = Color.FromArgb(245, 166, 35);    // #F5A623 (Gold)
        private readonly Color HoverBottom = Color.FromArgb(214, 137, 16);
        private readonly Color BorderNormal = Color.FromArgb(15, 52, 96);   // #0F3460
        private readonly Color BorderActive = Color.FromArgb(245, 166, 35); // Gold Accent

        /// <summary>
        /// Menandai apakah tombol dalam status terpilih / aktif (border emas tebal).
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool IsActive
        {
            get => isActive;
            set
            {
                isActive = value;
                Invalidate();
            }
        }

        public RPGButton()
        {
            SetStyle(ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);

            BackColor = Color.Transparent;
            ForeColor = Color.FromArgb(234, 234, 234); // #EAEAEA
            Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            Cursor = Cursors.Hand;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            isHovered = true;
            try
            {
                AudioManager.PlaySFX("sfx_hover.wav");
            }
            catch { }
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            isHovered = false;
            isPressed = false;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            if (mevent.Button == MouseButtons.Left)
            {
                isPressed = true;
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            isPressed = false;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // Offset +2px saat tombol ditekan (press down effect)
            int offset = isPressed ? 2 : 0;
            Rectangle rect = new Rectangle(0, offset, Width - 1, Height - 1 - offset);

            if (rect.Width <= 0 || rect.Height <= 0) return;

            int radius = 8;
            using (GraphicsPath path = CreateRoundedPath(rect, radius))
            {
                // 1. Gambar Drop Shadow tipis di bawah tombol
                if (!isPressed)
                {
                    Rectangle shadowRect = new Rectangle(1, 3, Width - 2, Height - 3);
                    using (GraphicsPath shadowPath = CreateRoundedPath(shadowRect, radius))
                    using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(90, 0, 0, 0)))
                    {
                        g.FillPath(shadowBrush, shadowPath);
                    }
                }

                // 2. Tentukan warna gradient background (Normal / Hover / Disabled)
                Color topColor = NormalTop;
                Color bottomColor = NormalBottom;
                Color textColor = Color.FromArgb(234, 234, 234);

                if (!Enabled)
                {
                    topColor = Color.FromArgb(80, 80, 100);
                    bottomColor = Color.FromArgb(50, 50, 70);
                    textColor = Color.FromArgb(140, 140, 160);
                }
                else if (isHovered)
                {
                    topColor = HoverTop;
                    bottomColor = HoverBottom;
                    textColor = Color.FromArgb(26, 26, 46); // #1A1A2E
                }

                using (LinearGradientBrush brush = new LinearGradientBrush(rect, topColor, bottomColor, LinearGradientMode.Vertical))
                {
                    g.FillPath(brush, path);
                }

                // 3. Highlight gloss tipis di bagian atas tombol
                if (Enabled && !isPressed)
                {
                    Rectangle glossRect = new Rectangle(rect.X + 2, rect.Y + 2, rect.Width - 4, rect.Height / 2);
                    using (GraphicsPath glossPath = CreateRoundedPath(glossRect, radius / 2))
                    using (SolidBrush glossBrush = new SolidBrush(Color.FromArgb(40, 255, 255, 255)))
                    {
                        g.FillPath(glossBrush, glossPath);
                    }
                }

                // 4. Gambar Border
                Color borderColor = isActive ? BorderActive : BorderNormal;
                float borderWidth = isActive ? 2.5f : 1.2f;

                using (Pen pen = new Pen(borderColor, borderWidth))
                {
                    g.DrawPath(pen, path);
                }

                // 5. Gambar Teks Tombol (Center aligned)
                TextRenderer.DrawText(
                    g,
                    Text,
                    Font,
                    new Rectangle(rect.X, rect.Y, rect.Width, rect.Height),
                    textColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak
                );
            }
        }

        private GraphicsPath CreateRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int d = radius * 2;

            if (rect.Width < d || rect.Height < d)
            {
                path.AddRectangle(rect);
                return path;
            }

            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
