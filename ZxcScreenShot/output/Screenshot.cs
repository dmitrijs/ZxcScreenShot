using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using ZxcScreenShot.Properties;
using ZxcScreenShot.ui;

namespace ZxcScreenShot.output
{
    internal static class ScreenShot
    {
        public static Bitmap GetScreenCapture(Bitmap bitmap)
        {
            using (var g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(new Point(0, 0), new Point(0, 0), Screen.PrimaryScreen.Bounds.Size);

                return bitmap;
            }
        }

        public static string CaptureImage(Point sourcePoint, Point destinationPoint, Rectangle selectionRectangle, PictureBox pictureBox1, ToolsPainter toolsPainter, bool updateClipboard)
        {
            if (selectionRectangle.Width == 0 || selectionRectangle.Height == 0)
            {
                return null;
            }

            using (var bitmap = new Bitmap(selectionRectangle.Width, selectionRectangle.Height))
            {
                using (var g = Graphics.FromImage(bitmap))
                {
                    g.Clear(Color.Black); // transparent pixels will be black

                    var destRect = new Rectangle {Location = destinationPoint, Size = selectionRectangle.Size};
                    var srcRect = new Rectangle {Location = sourcePoint, Size = selectionRectangle.Size};
                    g.DrawImage(pictureBox1.Image, destRect, srcRect, GraphicsUnit.Pixel);

                    toolsPainter.DrawAllTools(g, Point.Empty, Point.Empty, new Point(selectionRectangle.Width, selectionRectangle.Height));
                }

                // save to file
                if (!Directory.Exists(Settings.Default.SaveFileFolder))
                {
                    Directory.CreateDirectory(Settings.Default.SaveFileFolder);
                }
                var imagePath = Path.Combine(Settings.Default.SaveFileFolder, "Screen shot " + DateTime.Now.ToString("yyyy-M-dd HH.mm.ss") + ".png");
                bitmap.Save(imagePath, ImageFormat.Png);

                // update clipboard
                if (updateClipboard)
                {
                    Clipboard.SetImage(bitmap);
                }

                return Path.GetFullPath(imagePath);
            }
        }
    }
}