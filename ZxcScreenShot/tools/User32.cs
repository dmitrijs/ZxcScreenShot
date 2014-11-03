using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ZxcScreenShot.tools
{
    static class User32
    {
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

        [DllImport("user32.dll")]
        public static extern bool SetProcessDPIAware();

        public static Rectangle GetActiveWindowBounds()
        {
            Rect windowRect;
            GetWindowRect(GetActiveWindow(), out windowRect);

            return new Rectangle(windowRect.Left, windowRect.Top, windowRect.Right - windowRect.Left, windowRect.Bottom - windowRect.Top);
        }
    }
}
