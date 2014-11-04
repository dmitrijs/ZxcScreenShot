using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ZxcScreenShot.output;
using ZxcScreenShot.Properties;
using ZxcScreenShot.tools;
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
            None = 0,
            EditInPaint = 1,
            PutImageToClipboard = 2,
            PutImagePathToClipboard = 4,
            ShowInFolder = 8,
            PutImageUrlToClipboard = 16,
            ShowInBrowser = 32
        }

        private readonly ToolsPainter _toolsPainter = new ToolsPainter();
        private bool _isHoldingShift, _isHoldingControl;

        private ClickAction _currentAction;
        private Point _clickPoint;
        private Point _controlDownPoint;
        private Point _currentBottomRight, _currentTopLeft, _dragClickRelative;
        private bool _leftButtonDown, _rectangleDrawn;
        private int _rectangleHeight, _rectangleWidth;

        // tells that user has clicked any of Tool buttons
        private DrawingTool.DrawingToolType _goingToDrawTool = DrawingTool.DrawingToolType.NotDrawingTool;
        private bool _dragReady;
        private bool _dragStarted;

        private readonly LongPressAction _longPressAction;
        private Rectangle _lastUpdateBox, _currentlyUpdatedBox;

        private const int MinimumPixelsDrag = 3;
        private const double SensitivityModifier = 10.0d;

        public FormOverlay(bool startFullscreen = true)
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            pictureBox1.BackColor = Color.Black; // show transparent pixels as black

            if (!startFullscreen)
            {
                TopMost = false;
                WindowState = FormWindowState.Normal;
            }

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
            buttonPath.MouseMove += Panel_Mouse_Move;
            buttonUrl.MouseMove += Panel_Mouse_Move;
            buttonEditInPaint.MouseMove += Panel_Mouse_Move;
            buttonCancel.MouseMove += Panel_Mouse_Move;

            buttonColor1.Click += SwitchColor_Click;
            buttonColor2.Click += SwitchColor_Click;
            buttonColor3.Click += SwitchColor_Click;
            buttonColor4.Click += SwitchColor_Click;
            buttonColor5.Click += SwitchColor_Click;
            buttonColor6.Click += SwitchColor_Click;
            buttonColor7.Click += SwitchColor_Click;
            buttonColor8.Click += SwitchColor_Click;

            KeyDown += Key_Down;
            KeyUp += Key_Up;

            var screenBitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Size.Width, Screen.PrimaryScreen.Bounds.Size.Height);
            ScreenShot.GetScreenCapture(screenBitmap);

            pictureBox1.Image = screenBitmap;

            _toolsPainter.Clear();

            _longPressAction = new LongPressAction(longPressTimer);
        }
        
        public void SelectActiveWindow()
        {
            var bounds = User32.GetActiveWindowBounds();
            _currentTopLeft = bounds.Location;
            _currentBottomRight = new Point(bounds.Left + bounds.Width, bounds.Top + bounds.Height);
            _rectangleHeight = bounds.Height;
            _rectangleWidth = bounds.Width;

            MarkRectangleDrawn();
        }

        private void Key_Down(object sender, KeyEventArgs e)
        {
            if (e.Shift)
            {
                _isHoldingShift = true;
                buttonPath.ImageKey = @"folder.png";
                toolTip1.SetToolTip(buttonPath, "Show in Explorer");
                buttonUrl.ImageKey = @"browser.png";
                toolTip1.SetToolTip(buttonUrl, "Show in Browser");

                if (_currentAction == ClickAction.DrawingTool)
                {
                    _toolsPainter.DrawStraightLatest(true);
                }
            }
            if (e.Control)
            {
                if (!_isHoldingControl)
                {
                    _isHoldingControl = true;

                    var curPos = Cursor.Position;
                    curPos.X -= Left;
                    curPos.Y -= Top;

                    _controlDownPoint = curPos;
                }
            }
        }

        private void Key_Up(object sender, KeyEventArgs e)
        {
            if (!e.Shift)
            {
                _isHoldingShift = false;
                buttonPath.ImageKey = @"anchor.png";
                toolTip1.SetToolTip(buttonPath, "Copy file path");
                buttonUrl.ImageKey = @"link.png";
                toolTip1.SetToolTip(buttonUrl, "Copy URL");

                if (_currentAction == ClickAction.DrawingTool)
                {
                    _toolsPainter.DrawStraightLatest(false);
                }
            }
            if (!e.Control)
            {
                _isHoldingControl = false;
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
            var curPos = GetSensitivityAdjustedRelativeCursorPosition();

            if (_currentAction == ClickAction.LeftSizing)
            {
                if (curPos.X < _currentBottomRight.X - 10)
                {
                    //Erase the previous rectangle
                    _currentTopLeft.X = curPos.X;
                    _rectangleWidth = _currentBottomRight.X - _currentTopLeft.X;
                }
            }
            if (_currentAction == ClickAction.TopLeftSizing)
            {
                if (curPos.X < _currentBottomRight.X - 10 && curPos.Y < _currentBottomRight.Y - 10)
                {
                    //Erase the previous rectangle
                    _currentTopLeft.X = curPos.X;
                    _currentTopLeft.Y = curPos.Y;
                    _rectangleWidth = _currentBottomRight.X - _currentTopLeft.X;
                    _rectangleHeight = _currentBottomRight.Y - _currentTopLeft.Y;
                }
            }
            if (_currentAction == ClickAction.BottomLeftSizing)
            {
                if (curPos.X < _currentBottomRight.X - 10 && curPos.Y > _currentTopLeft.Y + 10)
                {
                    //Erase the previous rectangle
                    _currentTopLeft.X = curPos.X;
                    _currentBottomRight.Y = curPos.Y;
                    _rectangleWidth = _currentBottomRight.X - _currentTopLeft.X;
                    _rectangleHeight = _currentBottomRight.Y - _currentTopLeft.Y;
                }
            }
            if (_currentAction == ClickAction.RightSizing)
            {
                if (curPos.X > _currentTopLeft.X + 10)
                {
                    //Erase the previous rectangle
                    _currentBottomRight.X = curPos.X;
                    _rectangleWidth = _currentBottomRight.X - _currentTopLeft.X;
                }
            }
            if (_currentAction == ClickAction.TopRightSizing)
            {
                if (curPos.X > _currentTopLeft.X + 10 && curPos.Y < _currentBottomRight.Y - 10)
                {
                    //Erase the previous rectangle
                    _currentBottomRight.X = curPos.X;
                    _currentTopLeft.Y = curPos.Y;
                    _rectangleWidth = _currentBottomRight.X - _currentTopLeft.X;
                    _rectangleHeight = _currentBottomRight.Y - _currentTopLeft.Y;
                }
            }
            if (_currentAction == ClickAction.BottomRightSizing)
            {
                if (curPos.X > _currentTopLeft.X + 10 && curPos.Y > _currentTopLeft.Y + 10)
                {
                    //Erase the previous rectangle
                    _currentBottomRight.X = curPos.X;
                    _currentBottomRight.Y = curPos.Y;
                    _rectangleWidth = _currentBottomRight.X - _currentTopLeft.X;
                    _rectangleHeight = _currentBottomRight.Y - _currentTopLeft.Y;
                }
            }
            if (_currentAction == ClickAction.TopSizing)
            {
                if (curPos.Y < _currentBottomRight.Y - 10)
                {
                    //Erase the previous rectangle
                    _currentTopLeft.Y = curPos.Y;
                    _rectangleHeight = _currentBottomRight.Y - _currentTopLeft.Y;
                }
            }
            if (_currentAction == ClickAction.BottomSizing)
            {
                if (curPos.Y > _currentTopLeft.Y + 10)
                {
                    //Erase the previous rectangle
                    _currentBottomRight.Y = curPos.Y;
                    _rectangleHeight = _currentBottomRight.Y - _currentTopLeft.Y;
                }
            }
            UpdateUi();
        }

        private void MoveDrawingTool()
        {
            var curPos = GetSensitivityAdjustedRelativeCursorPosition();

            _toolsPainter.MoveLatestTo(new Point(curPos.X - _currentTopLeft.X, curPos.Y - _currentTopLeft.Y));

            UpdateUi();
        }

        private void DragSelection()
        {
            var curPos = GetSensitivityAdjustedRelativeCursorPosition();

            //Ensure that the rectangle stays within the bounds of the screen

            if (curPos.X - _dragClickRelative.X > 0 &&
                curPos.X - _dragClickRelative.X + _rectangleWidth < Width)
            {
                _currentTopLeft.X = curPos.X - _dragClickRelative.X;
                _currentBottomRight.X = _currentTopLeft.X + _rectangleWidth;
            }
            else
                //Selection area has reached the right side of the screen
                if (curPos.X - _dragClickRelative.X > 0)
                {
                    _currentTopLeft.X = Width - _rectangleWidth;
                    _currentBottomRight.X = _currentTopLeft.X + _rectangleWidth;
                }
                //Selection area has reached the left side of the screen
                else
                {
                    _currentTopLeft.X = 0;
                    _currentBottomRight.X = _currentTopLeft.X + _rectangleWidth;
                }

            if (curPos.Y - _dragClickRelative.Y > 0 &&
                curPos.Y - _dragClickRelative.Y + _rectangleHeight < Height)
            {
                _currentTopLeft.Y = curPos.Y - _dragClickRelative.Y;
                _currentBottomRight.Y = _currentTopLeft.Y + _rectangleHeight;
            }
            else
                //Selection area has reached the bottom of the screen
                if (curPos.Y - _dragClickRelative.Y > 0)
                {
                    _currentTopLeft.Y = Height - _rectangleHeight;
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

        private Point GetSensitivityAdjustedRelativeCursorPosition()
        {
            var curPos = Cursor.Position;
            curPos.X -= Left;
            curPos.Y -= Top;

            if (_isHoldingControl)
            {
                curPos.X = _controlDownPoint.X + (int)((curPos.X - _controlDownPoint.X) / SensitivityModifier);
                curPos.Y = _controlDownPoint.Y + (int)((curPos.Y - _controlDownPoint.Y) / SensitivityModifier);
            }

            return curPos;
        }

        private void DrawSelection()
        {
            var curPos = GetSensitivityAdjustedRelativeCursorPosition();

            Cursor = Cursors.Arrow;

            //Calculate X Coordinates
            if (curPos.X < _clickPoint.X)
            {
                _currentTopLeft.X = curPos.X;
                _currentBottomRight.X = _clickPoint.X;
            }
            else
            {
                _currentTopLeft.X = _clickPoint.X;
                _currentBottomRight.X = curPos.X;
            }

            //Calculate Y Coordinates
            if (curPos.Y < _clickPoint.Y)
            {
                _currentTopLeft.Y = curPos.Y;
                _currentBottomRight.Y = _clickPoint.Y;
            }
            else
            {
                _currentTopLeft.Y = _clickPoint.Y;
                _currentBottomRight.Y = curPos.Y;
            }

            UpdateUi();
        }

        private void Mouse_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var curPos = Cursor.Position;
                curPos.X -= Left;
                curPos.Y -= Top;

                _leftButtonDown = true;
                _clickPoint = curPos;

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
                    _dragClickRelative.X = curPos.X - _currentTopLeft.X;
                    _dragClickRelative.Y = curPos.Y - _currentTopLeft.Y;
                }
            }
        }

        private void Mouse_Up(object sender, MouseEventArgs e)
        {
            _leftButtonDown = false;
            MarkRectangleDrawn();
        }

        private void MarkRectangleDrawn()
        {
            _rectangleDrawn = true;
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
            _lastUpdateBox = _currentlyUpdatedBox;
            _currentlyUpdatedBox = GetExtendedBox(); // Box is too extended. no need for extra pixels on sides.

            UpdatePanelPosition();

            var combinedBox = Rectangle.Union(_lastUpdateBox, _currentlyUpdatedBox);
            // redraw rectangle
            pictureBox1.Invalidate(combinedBox);
        }

        private Rectangle GetExtendedBox()
        {
            var extended = new Rectangle(_currentTopLeft.X - 20, _currentTopLeft.Y - 20, _currentBottomRight.X - _currentTopLeft.X + 40,
                _currentBottomRight.Y - _currentTopLeft.Y + 40);
            
            // include text box with coordinates (roughly)
            extended.X -= 20;
            extended.Width += 80;
            return extended;
        }

        private void UpdatePanelPosition(Boolean force = false)
        {
            if (force || panelTools.Visible)
            {
                // move panel
                if (_currentBottomRight.X + 10 + panelTools.Width + 10 < Width)
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
                if (_currentBottomRight.Y + 10 + panelOutput.Height + 10 < Height)
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
            Do_Paint(e.Graphics, e.ClipRectangle);
        }

        private void Do_Paint(Graphics graphics, Rectangle clipRectangle)
        {
            var box = new Rectangle(_currentTopLeft.X, _currentTopLeft.Y, _currentBottomRight.X - _currentTopLeft.X,
                _currentBottomRight.Y - _currentTopLeft.Y); // new Rectangle(100, 50, 120, 70);

            DrawWorkarea(graphics, box, clipRectangle);
            DrawFrame(graphics, box);
            _toolsPainter.DrawAllTools(graphics, _currentTopLeft, _currentTopLeft, _currentBottomRight);

            graphics.DrawString(string.Format(@"{0}x{1} @ {2},{3}", box.Width, box.Height, box.Left, box.Top), DefaultFont, Brushes.White, box.Left, box.Top - 20);
        }

        private void DrawWorkarea(Graphics graphics, Rectangle box, Rectangle clipRectangle)
        {
            graphics.SetClip(box, CombineMode.Exclude);
            using (var b = new SolidBrush(Color.FromArgb(128, 0, 0, 0)))
            {
                graphics.FillRectangle(b, clipRectangle);
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
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            _goingToDrawTool = DrawingTool.DrawingToolType.NotDrawingTool;
            buttonDrawRect.Enabled = true;
            buttonDrawLine.Enabled = true;
            buttonDrawArrow.Enabled = true;
        }

        private void Do_Output(OutputActions outputActions)
        {
            if (!_rectangleDrawn)
            {
                return;
            }

            Hide();

            var doSaveImageToClipboard = (outputActions.HasFlag(OutputActions.PutImageToClipboard));
            var imageFullPath = CaptureImage(doSaveImageToClipboard);

            if (outputActions.HasFlag(OutputActions.PutImagePathToClipboard))
            {
                Clipboard.SetText(imageFullPath);
                AppContext.Instance().ShowNotifyMessage("Path was copied to clipboard", string.Format("Path: {0}", imageFullPath));
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

            var token = Uploader.GetToken();
            if (token != null)
            {
                var msg = Uploader.Upload(token, imageFullPath);
                if (msg == "ok")
                {
                    var url = string.Format("{0}{1}", Settings.Default.ShotsServiceBaseUrl, token);
                    if (outputActions.HasFlag(OutputActions.PutImageUrlToClipboard))
                    {
                        Clipboard.SetText(url);
                        AppContext.Instance().ShowNotifyMessage("Link was copied to clipboard", string.Format("URL: {0}", url));
                    }
                    if (outputActions.HasFlag(OutputActions.ShowInBrowser))
                    {
                        Process.Start(url);
                    }
                }
                else
                {
                    MessageBox.Show(string.Format("Upload error occured: {0}", msg));
                }
            }
        }

        private string CaptureImage(bool doSaveImageToClipboard)
        {
            var startPoint = new Point(_currentTopLeft.X, _currentTopLeft.Y);
            var bounds = new Rectangle(_currentTopLeft.X, _currentTopLeft.Y, _currentBottomRight.X - _currentTopLeft.X,
                _currentBottomRight.Y - _currentTopLeft.Y);

            return ScreenShot.CaptureImage(startPoint, Point.Empty, bounds, pictureBox1, _toolsPainter, doSaveImageToClipboard);
        }

        private void Do_Undo()
        {
            if (_toolsPainter.Undo())
            {
                pictureBox1.Invalidate();
            }
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

        private void buttonCopy_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _clickPoint = e.Location;
                _dragReady = true;
                _dragStarted = false;
            }
        }

        private void buttonCopy_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_dragReady) return;
            if (_dragStarted) return;
            if (Math.Abs(e.Location.X - _clickPoint.X) <= MinimumPixelsDrag &&
                Math.Abs(e.Location.Y - _clickPoint.Y) <= MinimumPixelsDrag)
            {
                return;
            }
            _dragStarted = true;

            var imageFullPath = CaptureImage(doSaveImageToClipboard: false);
            var filePath = new System.Collections.Specialized.StringCollection { imageFullPath };

            var dataObject = new DataObject();
            dataObject.SetFileDropList(filePath);

            Hide();

            buttonCopy.DoDragDrop(dataObject, DragDropEffects.Copy);

            _dragStarted = _dragReady = false;
        }

        private void buttonPath_Click(object sender, EventArgs e)
        {
        }

        private void buttonUrl_Click(object sender, EventArgs e)
        {
        }

        private void buttonPath_MouseDown(object sender, MouseEventArgs e)
        {
            _longPressAction.Start(buttonPath, contextMenuStripPath, e.Location);
        }

        private void buttonPath_MouseUp(object sender, MouseEventArgs e)
        {
            if (_longPressAction.CouldPreventMenuFromShowing())
            {
                Do_Output(_isHoldingShift ? OutputActions.ShowInFolder : OutputActions.PutImagePathToClipboard);
            }
        }

        private void buttonUrl_MouseDown(object sender, MouseEventArgs e)
        {
            _longPressAction.Start(buttonUrl, contextMenuStripUrl, e.Location);
        }

        private void buttonUrl_MouseUp(object sender, MouseEventArgs e)
        {
            if (_longPressAction.CouldPreventMenuFromShowing())
            {
                Do_Output(_isHoldingShift ? OutputActions.ShowInBrowser : OutputActions.PutImageUrlToClipboard);
            }
        }

        private void copyFilePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Do_Output(OutputActions.PutImagePathToClipboard);
        }

        private void viewInExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Do_Output(OutputActions.ShowInFolder);
        }

        private void copyURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Do_Output(OutputActions.PutImageUrlToClipboard);
        }

        private void viewInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Do_Output(OutputActions.ShowInBrowser);
        }

        private void SwitchColor_Click(object sender, EventArgs e)
        {
            buttonDrawColor.BackColor = ((Button)sender).BackColor;
        }

        private void buttonDrawColor_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    buttonDrawColor.BackColor = colorDialog1.Color;
                }
            }
            else if (e.Button == MouseButtons.Left)
            {
                var widthCollapsed = buttonColor1.Left;
                var widthExpanded = buttonColor1.Left + buttonColor1.Width + 3;
                panelTools.Width = panelTools.Width > buttonColor1.Left ? widthCollapsed : widthExpanded;
            }
        }

        private void buttonJustSave_Click(object sender, EventArgs e)
        {
            Do_Output(OutputActions.None);
        }
    }
}