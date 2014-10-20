using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using LighterShot.Properties;

namespace LighterShot
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

        public static void CaptureImage(Point sourcePoint, Point destinationPoint, Rectangle selectionRectangle, PictureBox pictureBox1, ToolsPainter toolsPainter)
        {
            using (var bitmap = new Bitmap(selectionRectangle.Width, selectionRectangle.Height))
            {
                using (var g = Graphics.FromImage(bitmap))
                {
                    var destRect = new Rectangle {Location = destinationPoint, Size = selectionRectangle.Size};
                    var srcRect = new Rectangle {Location = sourcePoint, Size = selectionRectangle.Size};
                    g.DrawImage(pictureBox1.Image, destRect, srcRect, GraphicsUnit.Pixel);

                    toolsPainter.DrawAllTools(g, Point.Empty, Point.Empty, new Point(selectionRectangle.Width, selectionRectangle.Height));
                }

                // update clipboard
                Clipboard.SetImage(bitmap);

                // save to file
                if (!Directory.Exists(Settings.Default.SaveFileFolder))
                {
                    Directory.CreateDirectory(Settings.Default.SaveFileFolder);
                }

                var imagePath = Settings.Default.SaveFileFolder + "Screen shot " + DateTime.Now.ToString("yyyy-dd-M HH.mm.ss") + ".png";
                bitmap.Save(imagePath, ImageFormat.Png);

                var key = Uploader.GetKey();
                
                if (key == null) return;

                var msg = Uploader.Upload(key, imagePath);
                if (msg == "ok")
                {
                    MessageBox.Show(string.Format("{0}/{1}/{2}", Settings.Default.ShotsServiceBaseUrl, key.Item1, key.Item2));
                }
                else
                {
                    MessageBox.Show(string.Format("Upload error occured: {0}", msg));
                }
            }
        }
    }
}