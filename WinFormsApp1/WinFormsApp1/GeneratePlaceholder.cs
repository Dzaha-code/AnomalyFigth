// This is a helper script to generate placeholder.png
// Run ini sekali untuk generate image, kemudian bisa di-delete

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

{
    // Create a 150x150 placeholder image with gray background
    Bitmap placeholder = new Bitmap(150, 150);
    using (Graphics g = Graphics.FromImage(placeholder))
    {
        // Fill dengan warna light gray
        g.FillRectangle(Brushes.LightGray, 0, 0, 150, 150);

        // Draw border
        g.DrawRectangle(Pens.DarkGray, 0, 0, 149, 149);

        // Draw text
        Font font = new Font("Arial", 12, FontStyle.Bold);
        StringFormat format = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };
        g.DrawString("NO IMAGE", font, Brushes.DarkGray, new RectangleF(0, 0, 150, 150), format);
    }

    // Save to Assets/Images/placeholder.png
    string outputPath = Path.Combine("Assets", "Images", "placeholder.png");
    placeholder.Save(outputPath, ImageFormat.Png);
    Console.WriteLine("Placeholder image created: " + outputPath);
}
