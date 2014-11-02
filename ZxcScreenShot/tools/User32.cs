using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ZxcScreenShot.tools
{
    class User32
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        private static IntPtr GetActiveWindow()
        {
            // IntPtr handle = IntPtr.Zero;
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
            var r = new Rect();
            var activeWindow = GetActiveWindow();
            GetWindowRect(activeWindow, out r);

            return new Rectangle(r.Left, r.Top, r.Right - r.Left, r.Bottom - r.Top);
        }
    }
}
