using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ZxcScreenShot.ui
{
    class PanelPainter
    {
        public delegate void InvalidatePanel(Rectangle invalidateRectangle);

        private Rectangle BOUNDS = new Rectangle(new Point(100, 100), new Size(300, 50));
        private readonly InvalidatePanel _panelInvalidates;

        private readonly List<Button> _buttons = new List<Button>();
        private readonly Dictionary<Button, EventHandler> _actions = new Dictionary<Button, EventHandler>();
        private Point _mouseLocation = Point.Empty;

        public PanelPainter(Rectangle bounds, InvalidatePanel panelInvalidates)
        {
            BOUNDS = bounds;
            _panelInvalidates = panelInvalidates;
            BOUNDS.Y -= 40;
        }

        public Rectangle Bounds
        {
            get { return BOUNDS; }
        }

        public void AddButton(Button btn, EventHandler clickHandler)
        {
            _buttons.Add(btn);
            _actions.Add(btn, clickHandler);
        }

        public void MouseMove(MouseEventArgs e)
        {
            _mouseLocation = e.Location;
            if (BOUNDS.Contains(_mouseLocation))
            {
                _panelInvalidates.Invoke(BOUNDS);
            }
        }

        public void DrawThePanel(Graphics g)
        {
            var panelMouseLocation = _mouseLocation;
            panelMouseLocation.X -= BOUNDS.Left;
            panelMouseLocation.Y -= BOUNDS.Top;

            g.FillRectangle(Brushes.White, BOUNDS);

            foreach (var button in _buttons)
            {
                var buttonImage = button.ImageList.Images[button.ImageKey];
                Debug.Assert(buttonImage != null);

                var fillBrush = button.Bounds.Contains(panelMouseLocation) ? Brushes.Azure : Brushes.White;
                var borderPen = button.Bounds.Contains(panelMouseLocation) ? Pens.LightBlue : Pens.Gray;

                g.FillRectangle(fillBrush, BOUNDS.X + button.Left, BOUNDS.Y + button.Top, button.Bounds.Width, button.Bounds.Height);
                g.DrawRectangle(borderPen, BOUNDS.X + button.Left + 1, BOUNDS.Y + button.Top + 1, button.Bounds.Width - 3, button.Bounds.Height - 3);

                var deltaToCenterImageX = (button.Width - buttonImage.Width)/2;
                var deltaToCenterImageY = (button.Height - buttonImage.Height) / 2;
                g.DrawImage(buttonImage, BOUNDS.X + button.Left + deltaToCenterImageX, BOUNDS.Y + button.Top + deltaToCenterImageY); // TODO: center
            }
        }

        public void MouseUp(MouseEventArgs e)
        {
            var panelMouseLocation = _mouseLocation;
            panelMouseLocation.X -= BOUNDS.Left;
            panelMouseLocation.Y -= BOUNDS.Top;

            foreach (var button in _buttons)
            {
                if (button.Bounds.Contains(panelMouseLocation))
                {
                    button.PerformClick();
                    // _actions[button].Invoke(null, e);
//                    button.PerformClick();
//                    Console.WriteLine("button " + button.ImageKey + " pressed");
                }
            }
        }
    }
}
