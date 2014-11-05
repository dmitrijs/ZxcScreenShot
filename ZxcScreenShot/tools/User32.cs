using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ZxcScreenShot.tools
{
    static class User32
    {
        #region SetProcessDPIAware

        [DllImport("user32.dll")]
        public static extern bool SetProcessDPIAware();

        #endregion

        #region SetProcessDPIAware

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        private static IntPtr GetActiveWindow()
        {
            return GetForegroundWindow();
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, out Rect lpRect);

        [StructLayout(LayoutKind.Sequential)]
        private struct Rect
        {
            public readonly int Left;
            public readonly int Top;
            public readonly int Right;
            public readonly int Bottom;
        }

        public static Rectangle GetActiveWindowBounds()
        {
            Rect windowRect;
            GetWindowRect(GetActiveWindow(), out windowRect);

            return new Rectangle(windowRect.Left, windowRect.Top, windowRect.Right - windowRect.Left, windowRect.Bottom - windowRect.Top);
        }

        #endregion

        #region GetPixelColor

        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("gdi32.dll")]
        static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

        static public Color GetPixelColor(int x, int y)
        {
            var hdc = GetDC(IntPtr.Zero);
            var pixel = GetPixel(hdc, x, y);
            ReleaseDC(IntPtr.Zero, hdc);
            var color = Color.FromArgb((int)(pixel & 0x000000FF),
                         (int)(pixel & 0x0000FF00) >> 8,
                         (int)(pixel & 0x00FF0000) >> 16);
            return color;
        }

        #endregion
    }
}
