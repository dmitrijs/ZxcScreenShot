using System;
using System.Drawing;
using System.Windows.Forms;

namespace ZxcScreenShot.ui
{
    class LongPressAction
    {
        private readonly Timer _timer;
        private Button _longPressSourceButton;
        private ContextMenuStrip _longPressTargetMenu;
        private Point _clickLocation;

        public LongPressAction(Timer longPressTimer)
        {
            _timer = longPressTimer;
            _timer.Tick += Timer_Tick;
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            _timer.Stop();
            _longPressTargetMenu.Show(_longPressSourceButton, _clickLocation.X, _clickLocation.Y);
            _longPressTargetMenu.Focus();
        }

        public void Start(Button longPressSourceButton, ContextMenuStrip longPressTargetMenu, Point clickLocation)
        {
            _longPressSourceButton = longPressSourceButton;
            _longPressTargetMenu = longPressTargetMenu;
            _clickLocation = clickLocation;
            _timer.Start();
        }

        public bool CouldPreventMenuFromShowing()
        {
            var success = _timer.Enabled;
            _timer.Stop();

            return success;
        }
    }
}
