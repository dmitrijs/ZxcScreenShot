using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ZxcScreenShot.output;
using ZxcScreenShot.Properties;
using ZxcScreenShot.ui;

namespace ZxcScreenShot
{
    public partial class FormOverlay : Form
    {
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

        [Flags]
        private enum OutputActions
        {
            PutImageToClipboard = 1,
            PutImagePathToClipboard = 2,
            PutImageUrlToClipboard = 4,
            ShowInFolder = 8,
            EditInPaint = 16
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

        public FormOverlay()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;

            pictureBox1.MouseDown += Mouse_Click;
            pictureBox1.MouseUp += Mouse_Up;
            pictureBox1.MouseMove += Mouse_Move;

            panelTools.MouseMove += Panel_Mouse_Move;
            buttonDrawRect.MouseMove += Panel_Mouse_Move;
            buttonDrawLine.MouseMove += Panel_Mouse_Move;
            buttonDrawArrow.MouseMove += Panel_Mouse_Move;
            buttonDrawColor.MouseMove += Panel_Mouse_Move;
            buttonDrawUndo.MouseMove += Panel_Mouse_Move;
            buttonDrawResize.MouseMove += Panel_Mouse_Move;

            panelOutput.MouseMove += Panel_Mouse_Move;
            buttonFolder.MouseMove += Panel_Mouse_Move;
            buttonPath.MouseMove += Panel_Mouse_Move;
            buttonUrl.MouseMove += Panel_Mouse_Move;
            buttonEditInPaint.MouseMove += Panel_Mouse_Move;
            buttonCancel.MouseMove += Panel_Mouse_Move;

            KeyDown += Key_Down;
            KeyUp += Key_Up;

            Bitmap screenBitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Size.Width, Screen.PrimaryScreen.Bounds.Size.Height);
            ScreenShot.GetScreenCapture(screenBitmap);

            pictureBox1.Image = screenBitmap;

            _toolsPainter.Clear();
        }
        
        private void Key_Down(object sender, KeyEventArgs e)
        {
            if (e.Shift && _currentAction == ClickAction.DrawingTool)
            {
                _toolsPainter.DrawStraightLatest(true);
            }
        }

        private void Key_Up(object sender, KeyEventArgs e)
        {
            if (!e.Shift && _currentAction == ClickAction.DrawingTool)
            {
                _toolsPainter.DrawStraightLatest(false);
            }
            if (e.Control && e.KeyCode == Keys.C)
            {
                Do_Output(OutputActions.PutImageToClipboard);
            }
            if (e.Control && e.KeyCode == Keys.Z)
            {
                if (_currentAction != ClickAction.DrawingTool)
                {
                    Do_Undo();
                }
            }
            if (e.KeyCode == Keys.Escape)
            {
                Hide();
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
        
        private void Mouse_Click(object sender, MouseEventArgs e)
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
        
        private void Mouse_Up(object sender, MouseEventArgs e)
        {
            _rectangleDrawn = true;
            _leftButtonDown = false;
            _currentAction = ClickAction.NoClick;
            UpdatePanelPosition(force: true);
            panelOutput.Visible = panelTools.Visible = true;
        }

        private void Panel_Mouse_Move(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void Mouse_Move(object sender, MouseEventArgs e)
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
            Do_Paint(e.Graphics);
        }

        private void Do_Paint(Graphics graphics) {
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
            Hide();
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
        
        private void buttonDrawColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                buttonDrawColor.BackColor = colorDialog1.Color;
            }
        }
        
        private void buttonDone_Click(object sender, EventArgs e)
        {
            _goingToDrawTool = DrawingTool.DrawingToolType.NotDrawingTool;
            buttonDrawRect.Enabled = true;
            buttonDrawLine.Enabled = true;
            buttonDrawArrow.Enabled = true;
        }

        private void buttonFolder_Click(object sender, EventArgs e)
        {
            Do_Output(OutputActions.ShowInFolder);
        }

        private void Do_Output(OutputActions outputActions)
        {
            var doSaveImageToClipboard = (outputActions.HasFlag(OutputActions.PutImageToClipboard));

            if (!_rectangleDrawn)
            {
                return;
            }

            var startPoint = new Point(_currentTopLeft.X, _currentTopLeft.Y);
            var bounds = new Rectangle(_currentTopLeft.X, _currentTopLeft.Y, _currentBottomRight.X - _currentTopLeft.X,
                _currentBottomRight.Y - _currentTopLeft.Y);

            var imageFullPath = ScreenShot.CaptureImage(startPoint, Point.Empty, bounds, pictureBox1, _toolsPainter, doSaveImageToClipboard);
            
            if (outputActions.HasFlag(OutputActions.PutImagePathToClipboard))
            {
                Clipboard.SetText(imageFullPath);
            }

            if (outputActions.HasFlag(OutputActions.ShowInFolder))
            {
                Process.Start("explorer.exe", string.Format("/select, \"{0}\"", imageFullPath));
            }

            if (outputActions.HasFlag(OutputActions.EditInPaint))
            {
                var startInfo = new ProcessStartInfo(imageFullPath) { Verb = "edit" };
                Process.Start(startInfo);
            }

            var key = Uploader.GetKey();
            if (key != null)
            {
                var msg = Uploader.Upload(key, imageFullPath);
                if (msg == "ok")
                {
                    var url = string.Format("{0}{1}/{2}", Settings.Default.ShotsServiceBaseUrl, key.Item1, key.Item2);
                    if (outputActions.HasFlag(OutputActions.PutImageUrlToClipboard))
                    {
                        Clipboard.SetText(url);
                    }
                }
                else
                {
                    MessageBox.Show(string.Format("Upload error occured: {0}", msg));
                }
            }

            Hide();
        }

        private void Do_Undo()
        {
            if (_toolsPainter.Undo())
            {
                pictureBox1.Invalidate();
            }
        }

        private void buttonPath_Click(object sender, EventArgs e)
        {
            Do_Output(OutputActions.PutImagePathToClipboard);
        }

        private void buttonUrl_Click(object sender, EventArgs e)
        {
            Do_Output(OutputActions.PutImageUrlToClipboard);
        }

        private void buttonEditInPaint_Click(object sender, EventArgs e)
        {
            Do_Output(OutputActions.EditInPaint);
        }

        private void buttonDrawUndo_Click(object sender, EventArgs e)
        {
            Do_Undo();
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            Do_Output(OutputActions.PutImageToClipboard);
        }
    }
}