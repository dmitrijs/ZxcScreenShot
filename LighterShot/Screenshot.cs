using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace LighterShot
{
    internal static class ScreenShot
    {
        public static Bitmap GetScreenCapture(Bitmap bitmap)
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(new Point(0, 0), new Point(0, 0), Screen.PrimaryScreen.Bounds.Size);

                return bitmap;
            }
        }

        public static void CaptureImage(Point sourcePoint, Point destinationPoint, Rectangle selectionRectangle)
        {
            using (var bitmap = new Bitmap(selectionRectangle.Width, selectionRectangle.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(sourcePoint, destinationPoint, selectionRectangle.Size);
                }

                // update clipboard
                Clipboard.SetImage(bitmap);

                // save to file
                var path = @"c:\Users\dmitr_000\Desktop\Screen shots\lightershot\";
                bitmap.Save(path + "Screen shot " + DateTime.Now.ToString("yyyy-dd-M HH.mm.ss") + ".png", ImageFormat.Png);
            }
        }
    }
}