using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace LighterShot
{
    public partial class FormOverlay : Form
    {
        #region:::::::::::::::::::::::::::::::::::::::::::Form level declarations:::::::::::::::::::::::::::::::::::::::::::

        private enum ClickAction
        {
            NoClick = 0,
            Dragging,
            Outside,
            TopSizing,
            BottomSizing,
            LeftSizing,
            TopLeftSizing,
            BottomLeftSizing,
            RightSizing,
            TopRightSizing,
            BottomRightSizing,
            DrawingTool
        }

        public enum CursPos
        {
            WithinSelectionArea = 0,
            OutsideSelectionArea,
            TopLine,
            BottomLine,
            LeftLine,
            RightLine,
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight
        }

        private readonly ToolsPainter _toolsPainter = new ToolsPainter();

        private ClickAction _currentAction;
        private Point _clickPoint;
        private Point _currentBottomRight, _currentTopLeft, _dragClickRelative;
        private bool _leftButtonDown, _rectangleDrawn;
        private int _rectangleHeight, _rectangleWidth;

        // tells that user has clicked any of Tool buttons
        private DrawingTool.DrawingToolType _goingToDrawTool = DrawingTool.DrawingToolType.NotDrawingTool;

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                e = null;
            }
            base.OnMouseClick(e);
        }

        #endregion

        #region:::::::::::::::::::::::::::::::::::::::::::Mouse Event Handlers & Drawing Initialization:::::::::::::::::::::::::::::::::::::::::::

        public FormOverlay()
        {
            InitializeComponent();

            Cursor = Cursors.Arrow;
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            pictureBox1.MouseDown += mouse_Click;
            pictureBox1.MouseDoubleClick += mouse_DClick;
            pictureBox1.MouseUp += mouse_Up;
            pictureBox1.MouseMove += mouse_Move;

            panelTools.MouseMove += panel_mouse_move;
            buttonDrawRect.MouseMove += panel_mouse_move;
            buttonDrawLine.MouseMove += panel_mouse_move;
            buttonDrawArrow.MouseMove += panel_mouse_move;
            buttonDrawColor.MouseMove += panel_mouse_move;
            buttonDone.MouseMove += panel_mouse_move;

            KeyDown += key_down;
            KeyUp += key_up;
            buttonCancel.KeyDown += key_down;
            buttonCancel.KeyUp += key_up;
            buttonDrawRect.KeyDown += key_down;
            buttonDrawRect.KeyUp += key_up;
            buttonDrawLine.KeyDown += key_down;
            buttonDrawLine.KeyUp += key_up;
            buttonDrawArrow.KeyDown += key_down;
            buttonDrawArrow.KeyUp += key_up;
            buttonDrawColor.KeyDown += key_down;
            buttonDrawColor.KeyUp += key_up;
            panelTools.KeyDown += key_down;
            panelTools.KeyUp += key_up;

            Bitmap screenBitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Size.Width, Screen.PrimaryScreen.Bounds.Size.Height);
            ScreenShot.GetScreenCapture(screenBitmap);

            pictureBox1.Image = screenBitmap;

            _toolsPainter.Clear();

            timer1.Enabled = true;
        }

        #endregion

        public void SaveSelection()
        {
            //Allow 250 milliseconds for the screen to repaint itself (we don't want to include this form in the capture)
            Thread.Sleep(250);

            var startPoint = new Point(_currentTopLeft.X, _currentTopLeft.Y);
            var bounds = new Rectangle(_currentTopLeft.X, _currentTopLeft.Y, _currentBottomRight.X - _currentTopLeft.X,
                _currentBottomRight.Y - _currentTopLeft.Y);

            ScreenShot.CaptureImage(startPoint, Point.Empty, bounds, pictureBox1, _toolsPainter);

            Close();
        }

        public void key_down(object sender, KeyEventArgs e)
        {
            if (e.Shift && _currentAction == ClickAction.DrawingTool)
            {
                _toolsPainter.DrawStraightLatest(true);
            }
        }

        public void key_up(object sender, KeyEventArgs e)
        {
            if (!e.Shift && _currentAction == ClickAction.DrawingTool)
            {
                _toolsPainter.DrawStraightLatest(false);
            }
            if (e.Control && e.KeyCode == Keys.C)
            {
                if (_rectangleDrawn) SaveSelection();
            }
            if (e.Control && e.KeyCode == Keys.Z)
            {
                if (_currentAction != ClickAction.DrawingTool && _toolsPainter.Undo())
                {
                    pictureBox1.Invalidate();
                }
            }
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void SetClickAction()
        {
            switch (UiUtils.UpdateCursorAndGetCursorPosition(this, _currentTopLeft, _currentBottomRight, _goingToDrawTool == DrawingTool.DrawingToolType.NotDrawingTool))
            {
                case CursPos.BottomLine:
                    _currentAction = ClickAction.BottomSizing;
                    break;
                case CursPos.TopLine:
                    _currentAction = ClickAction.TopSizing;
                    break;
                case CursPos.LeftLine:
                    _currentAction = ClickAction.LeftSizing;
                    break;
                case CursPos.TopLeft:
                    _currentAction = ClickAction.TopLeftSizing;
                    break;
                case CursPos.BottomLeft:
                    _currentAction = ClickAction.BottomLeftSizing;
                    break;
                case CursPos.RightLine:
                    _currentAction = ClickAction.RightSizing;
                    break;
                case CursPos.TopRight:
                    _currentAction = ClickAction.TopRightSizing;
                    break;
                case CursPos.BottomRight:
                    _currentAction = ClickAction.BottomRightSizing;
                    break;
                case CursPos.WithinSelectionArea:
                    _currentAction = ClickAction.Dragging;
                    break;
                case CursPos.OutsideSelectionArea:
                    _currentAction = ClickAction.Outside;
                    break;
            }
        }

        private void ResizeSelection()
        {
            if (_currentAction == ClickAction.LeftSizing)
            {
                if (Cursor.Position.X < _currentBottomRight.X - 10)
                {
                    //Erase the previous rectangle
                    _currentTopLeft.X = Cursor.Position.X;
                    _rectangleWidth = _currentBottomRight.X - _currentTopLeft.X;
                }
            }
            if (_currentAction == ClickAction.TopLeftSizing)
            {
                if (Cursor.Position.X < _currentBottomRight.X - 10 && Cursor.Position.Y < _currentBottomRight.Y - 10)
                {
                    //Erase the previous rectangle
                    _currentTopLeft.X = Cursor.Position.X;
                    _currentTopLeft.Y = Cursor.Position.Y;
                    _rectangleWidth = _currentBottomRight.X - _currentTopLeft.X;
                    _rectangleHeight = _currentBottomRight.Y - _currentTopLeft.Y;
                }
            }
            if (_currentAction == ClickAction.BottomLeftSizing)
            {
                if (Cursor.Position.X < _currentBottomRight.X - 10 && Cursor.Position.Y > _currentTopLeft.Y + 10)
                {
                    //Erase the previous rectangle
                    _currentTopLeft.X = Cursor.Position.X;
                    _currentBottomRight.Y = Cursor.Position.Y;
                    _rectangleWidth = _currentBottomRight.X - _currentTopLeft.X;
                    _rectangleHeight = _currentBottomRight.Y - _currentTopLeft.Y;
                }
            }
            if (_currentAction == ClickAction.RightSizing)
            {
                if (Cursor.Position.X > _currentTopLeft.X + 10)
                {
                    //Erase the previous rectangle
                    _currentBottomRight.X = Cursor.Position.X;
                    _rectangleWidth = _currentBottomRight.X - _currentTopLeft.X;
                }
            }
            if (_currentAction == ClickAction.TopRightSizing)
            {
                if (Cursor.Position.X > _currentTopLeft.X + 10 && Cursor.Position.Y < _currentBottomRight.Y - 10)
                {
                    //Erase the previous rectangle
                    _currentBottomRight.X = Cursor.Position.X;
                    _currentTopLeft.Y = Cursor.Position.Y;
                    _rectangleWidth = _currentBottomRight.X - _currentTopLeft.X;
                    _rectangleHeight = _currentBottomRight.Y - _currentTopLeft.Y;
                }
            }
            if (_currentAction == ClickAction.BottomRightSizing)
            {
                if (Cursor.Position.X > _currentTopLeft.X + 10 && Cursor.Position.Y > _currentTopLeft.Y + 10)
                {
                    //Erase the previous rectangle
                    _currentBottomRight.X = Cursor.Position.X;
                    _currentBottomRight.Y = Cursor.Position.Y;
                    _rectangleWidth = _currentBottomRight.X - _currentTopLeft.X;
                    _rectangleHeight = _currentBottomRight.Y - _currentTopLeft.Y;
                }
            }
            if (_currentAction == ClickAction.TopSizing)
            {
                if (Cursor.Position.Y < _currentBottomRight.Y - 10)
                {
                    //Erase the previous rectangle
                    _currentTopLeft.Y = Cursor.Position.Y;
                    _rectangleHeight = _currentBottomRight.Y - _currentTopLeft.Y;
                }
            }
            if (_currentAction == ClickAction.BottomSizing)
            {
                if (Cursor.Position.Y > _currentTopLeft.Y + 10)
                {
                    //Erase the previous rectangle
                    _currentBottomRight.Y = Cursor.Position.Y;
                    _rectangleHeight = _currentBottomRight.Y - _currentTopLeft.Y;
                }
            }
            UpdateUi();
        }

        private void MoveDrawingTool()
        {
            _toolsPainter.MoveLatestTo(new Point(Cursor.Position.X - _currentTopLeft.X, Cursor.Position.Y - _currentTopLeft.Y));

            UpdateUi();
        }

        private void DragSelection()
        {
            //Ensure that the rectangle stays within the bounds of the screen

            if (Cursor.Position.X - _dragClickRelative.X > 0 &&
                Cursor.Position.X - _dragClickRelative.X + _rectangleWidth < Screen.PrimaryScreen.Bounds.Width)
            {
                _currentTopLeft.X = Cursor.Position.X - _dragClickRelative.X;
                _currentBottomRight.X = _currentTopLeft.X + _rectangleWidth;
            }
            else
                //Selection area has reached the right side of the screen
                if (Cursor.Position.X - _dragClickRelative.X > 0)
                {
                    _currentTopLeft.X = Screen.PrimaryScreen.Bounds.Width - _rectangleWidth;
                    _currentBottomRight.X = _currentTopLeft.X + _rectangleWidth;
                }
                    //Selection area has reached the left side of the screen
                else
                {
                    _currentTopLeft.X = 0;
                    _currentBottomRight.X = _currentTopLeft.X + _rectangleWidth;
                }

            if (Cursor.Position.Y - _dragClickRelative.Y > 0 &&
                Cursor.Position.Y - _dragClickRelative.Y + _rectangleHeight < Screen.PrimaryScreen.Bounds.Height)
            {
                _currentTopLeft.Y = Cursor.Position.Y - _dragClickRelative.Y;
                _currentBottomRight.Y = _currentTopLeft.Y + _rectangleHeight;
            }
            else
                //Selection area has reached the bottom of the screen
                if (Cursor.Position.Y - _dragClickRelative.Y > 0)
                {
                    _currentTopLeft.Y = Screen.PrimaryScreen.Bounds.Height - _rectangleHeight;
                    _currentBottomRight.Y = _currentTopLeft.Y + _rectangleHeight;
                }
                    //Selection area has reached the top of the screen
                else
                {
                    _currentTopLeft.Y = 0;
                    _currentBottomRight.Y = _currentTopLeft.Y + _rectangleHeight;
                }

            UpdateUi();
        }

        private void DrawSelection()
        {
            Cursor = Cursors.Arrow;

            //Calculate X Coordinates
            if (Cursor.Position.X < _clickPoint.X)
            {
                _currentTopLeft.X = Cursor.Position.X;
                _currentBottomRight.X = _clickPoint.X;
            }
            else
            {
                _currentTopLeft.X = _clickPoint.X;
                _currentBottomRight.X = Cursor.Position.X;
            }

            //Calculate Y Coordinates
            if (Cursor.Position.Y < _clickPoint.Y)
            {
                _currentTopLeft.Y = Cursor.Position.Y;
                _currentBottomRight.Y = _clickPoint.Y;
            }
            else
            {
                _currentTopLeft.Y = _clickPoint.Y;
                _currentBottomRight.Y = Cursor.Position.Y;
            }

            UpdateUi();
        }

        private void FormOverlay_Load(object sender, EventArgs e)
        {
        }

        private void FormOverlay_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        #region:::::::::::::::::::::::::::::::::::::::::::Mouse Buttons:::::::::::::::::::::::::::::::::::::::::::

        private void mouse_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _leftButtonDown = true;
                _clickPoint = new Point(MousePosition.X, MousePosition.Y);

                if (_goingToDrawTool == DrawingTool.DrawingToolType.NotDrawingTool)
                {
                    panelTools.Visible = false;
                    panelOutput.Visible = false;
                    SetClickAction();
                }
                else
                {
                    _currentAction = ClickAction.DrawingTool;
                    Cursor = Cursors.Hand;
                    _toolsPainter.Push(new DrawingTool
                    {
                        Type = _goingToDrawTool,
                        From = new Point(_clickPoint.X - _currentTopLeft.X, _clickPoint.Y - _currentTopLeft.Y),
                        To = new Point(_clickPoint.X - _currentTopLeft.X, _clickPoint.Y - _currentTopLeft.Y),
                        Color = buttonDrawColor.BackColor,
                        DrawStraight = false
                    });
                }

                if (_rectangleDrawn)
                {
                    _rectangleHeight = _currentBottomRight.Y - _currentTopLeft.Y;
                    _rectangleWidth = _currentBottomRight.X - _currentTopLeft.X;
                    _dragClickRelative.X = Cursor.Position.X - _currentTopLeft.X;
                    _dragClickRelative.Y = Cursor.Position.Y - _currentTopLeft.Y;
                }
            }
        }

        private void mouse_DClick(object sender, MouseEventArgs e)
        {
            if (_rectangleDrawn)
            {
                SaveSelection();
            }
        }

        private void mouse_Up(object sender, MouseEventArgs e)
        {
            _rectangleDrawn = true;
            _leftButtonDown = false;
            _currentAction = ClickAction.NoClick;
            UpdatePanelPosition(force: true);
            panelOutput.Visible = panelTools.Visible = true;
        }

        private void panel_mouse_move(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void mouse_Move(object sender, MouseEventArgs e)
        {
            if (_leftButtonDown && !_rectangleDrawn)
            {
                DrawSelection();
            }

            if (_rectangleDrawn)
            {
                var pos = UiUtils.UpdateCursorAndGetCursorPosition(this, _currentTopLeft, _currentBottomRight, _goingToDrawTool == DrawingTool.DrawingToolType.NotDrawingTool);
                if (pos == CursPos.WithinSelectionArea)
                {
                    if (_goingToDrawTool != DrawingTool.DrawingToolType.NotDrawingTool ||
                        _currentAction == ClickAction.DrawingTool)
                    {
                        Cursor = Cursors.Hand;
                    }
                    else
                    {
                        Cursor = Cursors.SizeAll;
                    }
                }

                if (_currentAction == ClickAction.Dragging)
                {
                    DragSelection();
                }

                if (_currentAction == ClickAction.DrawingTool)
                {
                    MoveDrawingTool();
                }

                if (_currentAction != ClickAction.Dragging && _currentAction != ClickAction.Outside &&
                    _currentAction != ClickAction.DrawingTool)
                {
                    ResizeSelection();
                }
            }
        }

        #endregion

        private void UpdateUi()
        {
            UpdatePanelPosition();

            // redraw rectangle
            pictureBox1.Invalidate();
        }

        private void UpdatePanelPosition(Boolean force = false)
        {
            if (force || panelTools.Visible)
            {
                // move panel
                if (_currentBottomRight.X + 10 + panelTools.Width + 10 < Screen.PrimaryScreen.Bounds.Width)
                {
                    // panel fits on the right
                    panelTools.Left = _currentBottomRight.X + 10;
                }
                else
                {
                    // place panel on the left
                    panelTools.Left = _currentTopLeft.X - panelTools.Width - 10;
                }
                panelTools.Top = Math.Max(10, _currentBottomRight.Y - panelTools.Height);

                // move panel
                if (_currentBottomRight.Y + 10 + panelOutput.Height + 10 < Screen.PrimaryScreen.Bounds.Height)
                {
                    // panel fits in the bottom
                    panelOutput.Top = _currentBottomRight.Y + 10;
                }
                else
                {
                    // place panel on top
                    panelOutput.Top = _currentTopLeft.Y - panelOutput.Height - 10;
                }
                panelOutput.Left = Math.Max(10, _currentBottomRight.X - panelOutput.Width - 10);
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            do_paint(e.Graphics);
        }

        private void do_paint(Graphics graphics) {
            var box = new Rectangle(_currentTopLeft.X, _currentTopLeft.Y, _currentBottomRight.X - _currentTopLeft.X,
                _currentBottomRight.Y - _currentTopLeft.Y); // new Rectangle(100, 50, 120, 70);

            var extendedBox = new Rectangle(_currentTopLeft.X - 20, _currentTopLeft.Y + 20, _currentBottomRight.X - _currentTopLeft.X + 40,
                _currentBottomRight.Y - _currentTopLeft.Y + 40);

            DrawWorkarea(graphics, box);
            DrawFrame(graphics, box);
            _toolsPainter.DrawAllTools(graphics, _currentTopLeft, _currentTopLeft, _currentBottomRight);

            graphics.DrawString(string.Format(@"{0}x{1} @ {2},{3}", box.Width, box.Height, box.Left, box.Top), DefaultFont, Brushes.White, box.Left, box.Top - 20);
        }

        private void DrawWorkarea(Graphics graphics, Rectangle box)
        {
            graphics.SetClip(box, CombineMode.Exclude);
            using (var b = new SolidBrush(Color.FromArgb(128, 0, 0, 0)))
            {
                graphics.FillRectangle(b, ClientRectangle);
            }
            graphics.ResetClip();
        }

        private void DrawFrame(Graphics graphics, Rectangle box)
        {
            var tlCorner = new Rectangle(box.Left - 2, box.Top - 2, 5, 5);
            var trCorner = new Rectangle(box.Left + box.Width - 2, box.Top - 2, 5, 5);
            var blCorner = new Rectangle(box.Left - 2, box.Top + box.Height - 2, 5, 5);
            var brCorner = new Rectangle(box.Left + box.Width - 2, box.Top + box.Height - 2, 5, 5);

            if (_goingToDrawTool == DrawingTool.DrawingToolType.NotDrawingTool)
            {
                using (var borderPen = new Pen(Brushes.White, 1))
                {
                    graphics.DrawRectangle(borderPen, tlCorner);
                    graphics.DrawRectangle(borderPen, trCorner);
                    graphics.DrawRectangle(borderPen, blCorner);
                    graphics.DrawRectangle(borderPen, brCorner);
                }
            }

            float[] dashValues = { 3, 3 };
            using (var dashedPen = new Pen(Color.White, 1) { DashPattern = dashValues })
            {
                graphics.DrawLine(dashedPen, new Point(box.Left + 4, box.Top),
                    new Point(box.Left + box.Width - 2, box.Top));
                graphics.DrawLine(dashedPen, new Point(box.Left + box.Width, box.Top + 4),
                    new Point(box.Left + box.Width, box.Top + box.Height - 2));
                graphics.DrawLine(dashedPen, new Point(box.Left + box.Width - 2, box.Top + box.Height),
                    new Point(box.Left + 2, box.Top + box.Height));
                graphics.DrawLine(dashedPen, new Point(box.Left, box.Top + box.Height - 2),
                    new Point(box.Left, box.Top + 2));
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonDrawRect_Click(object sender, EventArgs e)
        {
            _goingToDrawTool = DrawingTool.DrawingToolType.Rectangle;
            buttonDrawRect.Enabled = false;
            buttonDrawLine.Enabled = true;
            buttonDrawArrow.Enabled = true;
        }

        private void buttonDrawLine_Click(object sender, EventArgs e)
        {
            _goingToDrawTool = DrawingTool.DrawingToolType.Line;
            buttonDrawRect.Enabled = true;
            buttonDrawLine.Enabled = false;
            buttonDrawArrow.Enabled = true;
        }

        private void buttonDrawArrow_Click(object sender, EventArgs e)
        {
            _goingToDrawTool = DrawingTool.DrawingToolType.Arrow;
            buttonDrawRect.Enabled = true;
            buttonDrawLine.Enabled = true;
            buttonDrawArrow.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // labelInfo.Text = _goingToDrawTool.ToString() + @"/" + CurrentAction.ToString();
        }

        private void buttonDrawColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                buttonDrawColor.BackColor = colorDialog1.Color;
            }
        }

        private void buttonDrawArrow_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void buttonDrawArrow_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            _goingToDrawTool = DrawingTool.DrawingToolType.NotDrawingTool;
            buttonDrawRect.Enabled = true;
            buttonDrawLine.Enabled = true;
            buttonDrawArrow.Enabled = true;
        }
    }
}