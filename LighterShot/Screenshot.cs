using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace LighterShot
{
    internal static class ScreenShot
    {
        public static bool SaveToClipboard = true;

        public static Bitmap GetScreenCapture(Bitmap bitmap)
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(new Point(0, 0), new Point(0, 0), Screen.PrimaryScreen.Bounds.Size);

                return bitmap;
            }
        }

        public static void CaptureImage(Point sourcePoint, Point destinationPoint, Rectangle selectionRectangle,
            string filePath, string extension)
        {
            using (var bitmap = new Bitmap(selectionRectangle.Width, selectionRectangle.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(sourcePoint, destinationPoint, selectionRectangle.Size);
                }

                if (SaveToClipboard)
                {
                    Clipboard.SetImage(bitmap);
                }
                else
                {
                    switch (extension)
                    {
                        case ".bmp":
                            bitmap.Save(filePath, ImageFormat.Bmp);
                            break;
                        case ".jpg":
                            bitmap.Save(filePath, ImageFormat.Jpeg);
                            break;
                        case ".gif":
                            bitmap.Save(filePath, ImageFormat.Gif);
                            break;
                        case ".tiff":
                            bitmap.Save(filePath, ImageFormat.Tiff);
                            break;
                        case ".png":
                            bitmap.Save(filePath, ImageFormat.Png);
                            break;
                        default:
                            bitmap.Save(filePath, ImageFormat.Jpeg);
                            break;
                    }
                }
            }
        }
    }
}