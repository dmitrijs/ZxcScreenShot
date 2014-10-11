using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace LighterShot
{
    public partial class FormOverlay : Form
    {
        #region:::::::::::::::::::::::::::::::::::::::::::Form level declarations:::::::::::::::::::::::::::::::::::::::::::

        public enum ClickAction
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

        private Stack<DrawingTool> _drawings = new Stack<DrawingTool>();

        private readonly Pen EraserPen = new Pen(Color.FromArgb(255, 255, 192), 1);
        private readonly Pen MyPen = new Pen(Color.Black, 1);
        private readonly Graphics g;

        public Point ClickPoint = new Point();
        public ClickAction CurrentAction;
        public Point CurrentBottomRight = new Point();
        public Point CurrentTopLeft = new Point();
        public Point DragClickRelative = new Point();
        public bool LeftButtonDown = false;
        public bool ReadyToDrag = false;
        public bool RectangleDrawn = false;

        public int RectangleHeight = new int();
        public int RectangleWidth = new int();

        private SolidBrush TransparentBrush = new SolidBrush(Color.White);
        private SolidBrush eraserBrush = new SolidBrush(Color.FromArgb(255, 255, 192));

        public Form InstanceRef { get; set; }

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
            InstanceRef = null;

            InitializeComponent();

            Cursor = Cursors.Arrow;
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            //            this.Opacity = 0.1D;
            //            this.TransparencyKey = System.Drawing.Color.White;

            pictureBox1.MouseDown += mouse_Click;
            pictureBox1.MouseDoubleClick += mouse_DClick;
            pictureBox1.MouseUp += mouse_Up;
            pictureBox1.MouseMove += mouse_Move;
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
            g = CreateGraphics(); // pictureBox1.CreateGraphics();

//            pictureBox1.Dock = DockStyle.Fill;
            var bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Size.Width, Screen.PrimaryScreen.Bounds.Size.Height);
            ScreenShot.GetScreenCapture(bitmap);

            pictureBox1.Image = bitmap;

            _drawings.Clear();

            timer1.Enabled = true;

//            g.DrawImage((Image)bitmap, new Point(0, 0));
        }

        #endregion

        public void SaveSelection()
        {
            //Allow 250 milliseconds for the screen to repaint itself (we don't want to include this form in the capture)
            Thread.Sleep(250);

            var startPoint = new Point(CurrentTopLeft.X, CurrentTopLeft.Y);
            var bounds = new Rectangle(CurrentTopLeft.X, CurrentTopLeft.Y, CurrentBottomRight.X - CurrentTopLeft.X,
                CurrentBottomRight.Y - CurrentTopLeft.Y);

            ScreenShot.CaptureImage(startPoint, Point.Empty, bounds, pictureBox1);

            MessageBox.Show("Area saved to clipboard and file", "Lightershot", MessageBoxButtons.OK);

            if (InstanceRef != null)
            {
                InstanceRef.Show();
            }
            Close();
        }

        public void key_down(object sender, KeyEventArgs e)
        {
            if (e.Shift && CurrentAction == ClickAction.DrawingTool)
            {
                _drawings.Peek().DrawStraight = true;
            }
        }

        public void key_up(object sender, KeyEventArgs e)
        {
            if (!e.Shift && CurrentAction == ClickAction.DrawingTool)
            {
                _drawings.Peek().DrawStraight = false;
            }
            if (e.KeyCode.ToString() == "C" && e.Control && RectangleDrawn)
            {
                SaveSelection();
            }
            if (e.KeyCode.ToString() == "Z" && e.Control && CurrentAction != ClickAction.DrawingTool)
            {
                if (_drawings.Count > 0)
                {
                    _drawings.Pop();
                    pictureBox1.Invalidate();
                }                                             
            }
        }

        private CursPos CursorPosition()
        {
            if (((Cursor.Position.X > CurrentTopLeft.X - 10 && Cursor.Position.X < CurrentTopLeft.X + 10)) &&
                ((Cursor.Position.Y > CurrentTopLeft.Y + 10) && (Cursor.Position.Y < CurrentBottomRight.Y - 10)))
            {
                Cursor = Cursors.SizeWE;
                return CursPos.LeftLine;
            }
            if (((Cursor.Position.X >= CurrentTopLeft.X - 10 && Cursor.Position.X <= CurrentTopLeft.X + 10)) &&
                ((Cursor.Position.Y >= CurrentTopLeft.Y - 10) && (Cursor.Position.Y <= CurrentTopLeft.Y + 10)))
            {
                Cursor = Cursors.SizeNWSE;
                return CursPos.TopLeft;
            }
            if (((Cursor.Position.X >= CurrentTopLeft.X - 10 && Cursor.Position.X <= CurrentTopLeft.X + 10)) &&
                ((Cursor.Position.Y >= CurrentBottomRight.Y - 10) && (Cursor.Position.Y <= CurrentBottomRight.Y + 10)))
            {
                Cursor = Cursors.SizeNESW;
                return CursPos.BottomLeft;
            }
            if (((Cursor.Position.X > CurrentBottomRight.X - 10 && Cursor.Position.X < CurrentBottomRight.X + 10)) &&
                ((Cursor.Position.Y > CurrentTopLeft.Y + 10) && (Cursor.Position.Y < CurrentBottomRight.Y - 10)))
            {
                Cursor = Cursors.SizeWE;
                return CursPos.RightLine;
            }
            if (((Cursor.Position.X >= CurrentBottomRight.X - 10 && Cursor.Position.X <= CurrentBottomRight.X + 10)) &&
                ((Cursor.Position.Y >= CurrentTopLeft.Y - 10) && (Cursor.Position.Y <= CurrentTopLeft.Y + 10)))
            {
                Cursor = Cursors.SizeNESW;
                return CursPos.TopRight;
            }
            if (((Cursor.Position.X >= CurrentBottomRight.X - 10 && Cursor.Position.X <= CurrentBottomRight.X + 10)) &&
                ((Cursor.Position.Y >= CurrentBottomRight.Y - 10) && (Cursor.Position.Y <= CurrentBottomRight.Y + 10)))
            {
                Cursor = Cursors.SizeNWSE;
                return CursPos.BottomRight;
            }
            if (((Cursor.Position.Y > CurrentTopLeft.Y - 10) && (Cursor.Position.Y < CurrentTopLeft.Y + 10)) &&
                ((Cursor.Position.X > CurrentTopLeft.X + 10 && Cursor.Position.X < CurrentBottomRight.X - 10)))
            {
                Cursor = Cursors.SizeNS;
                return CursPos.TopLine;
            }
            if (((Cursor.Position.Y > CurrentBottomRight.Y - 10) && (Cursor.Position.Y < CurrentBottomRight.Y + 10)) &&
                ((Cursor.Position.X > CurrentTopLeft.X + 10 && Cursor.Position.X < CurrentBottomRight.X - 10)))
            {
                Cursor = Cursors.SizeNS;
                return CursPos.BottomLine;
            }
            if (
                (Cursor.Position.X >= CurrentTopLeft.X + 10 && Cursor.Position.X <= CurrentBottomRight.X - 10) &&
                (Cursor.Position.Y >= CurrentTopLeft.Y + 10 && Cursor.Position.Y <= CurrentBottomRight.Y - 10))
            {
                if (_goingToDrawTool != DrawingTool.DrawingToolType.NotDrawingTool ||
                    CurrentAction == ClickAction.DrawingTool)
                {
                    Cursor = Cursors.Hand;
                }
                else
                {
                    Cursor = Cursors.SizeAll;
                }
                return CursPos.WithinSelectionArea;
            }

            Cursor = Cursors.No;
            return CursPos.OutsideSelectionArea;
        }

        private void SetClickAction()
        {
            switch (CursorPosition())
            {
                case CursPos.BottomLine:
                    CurrentAction = ClickAction.BottomSizing;
                    break;
                case CursPos.TopLine:
                    CurrentAction = ClickAction.TopSizing;
                    break;
                case CursPos.LeftLine:
                    CurrentAction = ClickAction.LeftSizing;
                    break;
                case CursPos.TopLeft:
                    CurrentAction = ClickAction.TopLeftSizing;
                    break;
                case CursPos.BottomLeft:
                    CurrentAction = ClickAction.BottomLeftSizing;
                    break;
                case CursPos.RightLine:
                    CurrentAction = ClickAction.RightSizing;
                    break;
                case CursPos.TopRight:
                    CurrentAction = ClickAction.TopRightSizing;
                    break;
                case CursPos.BottomRight:
                    CurrentAction = ClickAction.BottomRightSizing;
                    break;
                case CursPos.WithinSelectionArea:
                    CurrentAction = ClickAction.Dragging;
                    break;
                case CursPos.OutsideSelectionArea:
                    CurrentAction = ClickAction.Outside;
                    break;
            }
        }

        private void ResizeSelection()
        {
            if (CurrentAction == ClickAction.LeftSizing)
            {
                if (Cursor.Position.X < CurrentBottomRight.X - 10)
                {
                    //Erase the previous rectangle
                    g.DrawRectangle(EraserPen, CurrentTopLeft.X, CurrentTopLeft.Y, RectangleWidth, RectangleHeight);
                    CurrentTopLeft.X = Cursor.Position.X;
                    RectangleWidth = CurrentBottomRight.X - CurrentTopLeft.X;
                    g.DrawRectangle(MyPen, CurrentTopLeft.X, CurrentTopLeft.Y, RectangleWidth, RectangleHeight);
                }
            }
            if (CurrentAction == ClickAction.TopLeftSizing)
            {
                if (Cursor.Position.X < CurrentBottomRight.X - 10 && Cursor.Position.Y < CurrentBottomRight.Y - 10)
                {
                    //Erase the previous rectangle
                    g.DrawRectangle(EraserPen, CurrentTopLeft.X, CurrentTopLeft.Y, RectangleWidth, RectangleHeight);
                    CurrentTopLeft.X = Cursor.Position.X;
                    CurrentTopLeft.Y = Cursor.Position.Y;
                    RectangleWidth = CurrentBottomRight.X - CurrentTopLeft.X;
                    RectangleHeight = CurrentBottomRight.Y - CurrentTopLeft.Y;
                    g.DrawRectangle(MyPen, CurrentTopLeft.X, CurrentTopLeft.Y, RectangleWidth, RectangleHeight);
                }
            }
            if (CurrentAction == ClickAction.BottomLeftSizing)
            {
                if (Cursor.Position.X < CurrentBottomRight.X - 10 && Cursor.Position.Y > CurrentTopLeft.Y + 10)
                {
                    //Erase the previous rectangle
                    g.DrawRectangle(EraserPen, CurrentTopLeft.X, CurrentTopLeft.Y, RectangleWidth, RectangleHeight);
                    CurrentTopLeft.X = Cursor.Position.X;
                    CurrentBottomRight.Y = Cursor.Position.Y;
                    RectangleWidth = CurrentBottomRight.X - CurrentTopLeft.X;
                    RectangleHeight = CurrentBottomRight.Y - CurrentTopLeft.Y;
                    g.DrawRectangle(MyPen, CurrentTopLeft.X, CurrentTopLeft.Y, RectangleWidth, RectangleHeight);
                }
            }
            if (CurrentAction == ClickAction.RightSizing)
            {
                if (Cursor.Position.X > CurrentTopLeft.X + 10)
                {
                    //Erase the previous rectangle
                    g.DrawRectangle(EraserPen, CurrentTopLeft.X, CurrentTopLeft.Y, RectangleWidth, RectangleHeight);
                    CurrentBottomRight.X = Cursor.Position.X;
                    RectangleWidth = CurrentBottomRight.X - CurrentTopLeft.X;
                    g.DrawRectangle(MyPen, CurrentTopLeft.X, CurrentTopLeft.Y, RectangleWidth, RectangleHeight);
                }
            }
            if (CurrentAction == ClickAction.TopRightSizing)
            {
                if (Cursor.Position.X > CurrentTopLeft.X + 10 && Cursor.Position.Y < CurrentBottomRight.Y - 10)
                {
                    //Erase the previous rectangle
                    g.DrawRectangle(EraserPen, CurrentTopLeft.X, CurrentTopLeft.Y, RectangleWidth, RectangleHeight);
                    CurrentBottomRight.X = Cursor.Position.X;
                    CurrentTopLeft.Y = Cursor.Position.Y;
                    RectangleWidth = CurrentBottomRight.X - CurrentTopLeft.X;
                    RectangleHeight = CurrentBottomRight.Y - CurrentTopLeft.Y;
                    g.DrawRectangle(MyPen, CurrentTopLeft.X, CurrentTopLeft.Y, RectangleWidth, RectangleHeight);
                }
            }
            if (CurrentAction == ClickAction.BottomRightSizing)
            {
                if (Cursor.Position.X > CurrentTopLeft.X + 10 && Cursor.Position.Y > CurrentTopLeft.Y + 10)
                {
                    //Erase the previous rectangle
                    g.DrawRectangle(EraserPen, CurrentTopLeft.X, CurrentTopLeft.Y, RectangleWidth, RectangleHeight);
                    CurrentBottomRight.X = Cursor.Position.X;
                    CurrentBottomRight.Y = Cursor.Position.Y;
                    RectangleWidth = CurrentBottomRight.X - CurrentTopLeft.X;
                    RectangleHeight = CurrentBottomRight.Y - CurrentTopLeft.Y;
                    g.DrawRectangle(MyPen, CurrentTopLeft.X, CurrentTopLeft.Y, RectangleWidth, RectangleHeight);
                }
            }
            if (CurrentAction == ClickAction.TopSizing)
            {
                if (Cursor.Position.Y < CurrentBottomRight.Y - 10)
                {
                    //Erase the previous rectangle
                    g.DrawRectangle(EraserPen, CurrentTopLeft.X, CurrentTopLeft.Y, RectangleWidth, RectangleHeight);
                    CurrentTopLeft.Y = Cursor.Position.Y;
                    RectangleHeight = CurrentBottomRight.Y - CurrentTopLeft.Y;
                    g.DrawRectangle(MyPen, CurrentTopLeft.X, CurrentTopLeft.Y, RectangleWidth, RectangleHeight);
                }
            }
            if (CurrentAction == ClickAction.BottomSizing)
            {
                if (Cursor.Position.Y > CurrentTopLeft.Y + 10)
                {
                    //Erase the previous rectangle
                    g.DrawRectangle(EraserPen, CurrentTopLeft.X, CurrentTopLeft.Y, RectangleWidth, RectangleHeight);
                    CurrentBottomRight.Y = Cursor.Position.Y;
                    RectangleHeight = CurrentBottomRight.Y - CurrentTopLeft.Y;
                    g.DrawRectangle(MyPen, CurrentTopLeft.X, CurrentTopLeft.Y, RectangleWidth, RectangleHeight);
                }
            }
            UpdateUI();
        }

        private void MoveDrawingTool()
        {
            _drawings.Peek().To = Cursor.Position;

            UpdateUI();
        }

        private void DragSelection()
        {
            //Ensure that the rectangle stays within the bounds of the screen

            //Erase the previous rectangle
            g.DrawRectangle(EraserPen, CurrentTopLeft.X, CurrentTopLeft.Y, RectangleWidth, RectangleHeight);

            if (Cursor.Position.X - DragClickRelative.X > 0 &&
                Cursor.Position.X - DragClickRelative.X + RectangleWidth < Screen.PrimaryScreen.Bounds.Width)
            {
                CurrentTopLeft.X = Cursor.Position.X - DragClickRelative.X;
                CurrentBottomRight.X = CurrentTopLeft.X + RectangleWidth;
            }
            else
                //Selection area has reached the right side of the screen
                if (Cursor.Position.X - DragClickRelative.X > 0)
                {
                    CurrentTopLeft.X = Screen.PrimaryScreen.Bounds.Width - RectangleWidth;
                    CurrentBottomRight.X = CurrentTopLeft.X + RectangleWidth;
                }
                    //Selection area has reached the left side of the screen
                else
                {
                    CurrentTopLeft.X = 0;
                    CurrentBottomRight.X = CurrentTopLeft.X + RectangleWidth;
                }

            if (Cursor.Position.Y - DragClickRelative.Y > 0 &&
                Cursor.Position.Y - DragClickRelative.Y + RectangleHeight < Screen.PrimaryScreen.Bounds.Height)
            {
                CurrentTopLeft.Y = Cursor.Position.Y - DragClickRelative.Y;
                CurrentBottomRight.Y = CurrentTopLeft.Y + RectangleHeight;
            }
            else
                //Selection area has reached the bottom of the screen
                if (Cursor.Position.Y - DragClickRelative.Y > 0)
                {
                    CurrentTopLeft.Y = Screen.PrimaryScreen.Bounds.Height - RectangleHeight;
                    CurrentBottomRight.Y = CurrentTopLeft.Y + RectangleHeight;
                }
                    //Selection area has reached the top of the screen
                else
                {
                    CurrentTopLeft.Y = 0;
                    CurrentBottomRight.Y = CurrentTopLeft.Y + RectangleHeight;
                }

            //Draw a new rectangle
            g.DrawRectangle(MyPen, CurrentTopLeft.X, CurrentTopLeft.Y, RectangleWidth, RectangleHeight);

            UpdateUI();
        }

        private void DrawSelection()
        {
            Cursor = Cursors.Arrow;

            //Erase the previous rectangle
            g.DrawRectangle(EraserPen, CurrentTopLeft.X, CurrentTopLeft.Y, CurrentBottomRight.X - CurrentTopLeft.X,
                CurrentBottomRight.Y - CurrentTopLeft.Y);

            //Calculate X Coordinates
            if (Cursor.Position.X < ClickPoint.X)
            {
                CurrentTopLeft.X = Cursor.Position.X;
                CurrentBottomRight.X = ClickPoint.X;
            }
            else
            {
                CurrentTopLeft.X = ClickPoint.X;
                CurrentBottomRight.X = Cursor.Position.X;
            }

            //Calculate Y Coordinates
            if (Cursor.Position.Y < ClickPoint.Y)
            {
                CurrentTopLeft.Y = Cursor.Position.Y;
                CurrentBottomRight.Y = ClickPoint.Y;
            }
            else
            {
                CurrentTopLeft.Y = ClickPoint.Y;
                CurrentBottomRight.Y = Cursor.Position.Y;
            }

            //Draw a new rectangle
            g.DrawRectangle(MyPen, CurrentTopLeft.X, CurrentTopLeft.Y, CurrentBottomRight.X - CurrentTopLeft.X,
                CurrentBottomRight.Y - CurrentTopLeft.Y);

            UpdateUI();
        }

        private void FormOverlay_Load(object sender, EventArgs e)
        {
        }

        private void FormOverlay_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (InstanceRef != null)
            {
                InstanceRef.Show();
            }
            else
            {
                Application.Exit();
            }
        }

        #region:::::::::::::::::::::::::::::::::::::::::::Mouse Buttons:::::::::::::::::::::::::::::::::::::::::::

        private void mouse_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LeftButtonDown = true;
                ClickPoint = new Point(MousePosition.X, MousePosition.Y);

                if (_goingToDrawTool == DrawingTool.DrawingToolType.NotDrawingTool)
                {
                    SetClickAction();
                }
                else
                {
                    CurrentAction = ClickAction.DrawingTool;
                    Cursor = Cursors.Hand;
                    _drawings.Push(new DrawingTool
                                   {
                                       Type = _goingToDrawTool,
                                       From = ClickPoint,
                                       To = ClickPoint,
                                       Color = buttonDrawColor.ForeColor,
                                       DrawStraight = false
                                   });
                }

                if (RectangleDrawn)
                {
                    RectangleHeight = CurrentBottomRight.Y - CurrentTopLeft.Y;
                    RectangleWidth = CurrentBottomRight.X - CurrentTopLeft.X;
                    DragClickRelative.X = Cursor.Position.X - CurrentTopLeft.X;
                    DragClickRelative.Y = Cursor.Position.Y - CurrentTopLeft.Y;
                }
            }
        }

        private void mouse_DClick(object sender, MouseEventArgs e)
        {
            if (RectangleDrawn)
            {
                SaveSelection();
            }
        }

        private void mouse_Up(object sender, MouseEventArgs e)
        {
            RectangleDrawn = true;
            LeftButtonDown = false;
            CurrentAction = ClickAction.NoClick;
            panelTools.Visible = true;
        }

        private void mouse_Move(object sender, MouseEventArgs e)
        {
            if (LeftButtonDown && !RectangleDrawn)
            {
                DrawSelection();
            }

            if (RectangleDrawn)
            {
                CursorPosition();

                if (CurrentAction == ClickAction.Dragging)
                {
                    DragSelection();
                }

                if (CurrentAction == ClickAction.DrawingTool)
                {
                    MoveDrawingTool();
                }

                if (CurrentAction != ClickAction.Dragging && CurrentAction != ClickAction.Outside &&
                    CurrentAction != ClickAction.DrawingTool)
                {
                    ResizeSelection();
                }
            }
        }

        #endregion

        private void UpdateUI()
        {
            // move panel
            panelTools.Left = CurrentBottomRight.X + 10;
            panelTools.Top = CurrentBottomRight.Y - panelTools.Height;

            // redraw rectangle
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            var box = new Rectangle(CurrentTopLeft.X, CurrentTopLeft.Y, CurrentBottomRight.X - CurrentTopLeft.X,
                CurrentBottomRight.Y - CurrentTopLeft.Y); // new Rectangle(100, 50, 120, 70);

            e.Graphics.SetClip(box, CombineMode.Exclude);
            using (var b = new SolidBrush(Color.FromArgb(128, 0, 0, 0)))
            {
                e.Graphics.FillRectangle(b, this.ClientRectangle);
            }

            e.Graphics.ResetClip();

            var tlCorner = new Rectangle(box.Left - 2, box.Top - 2, 5, 5);
            var trCorner = new Rectangle(box.Left + box.Width - 2, box.Top - 2, 5, 5);
            var blCorner = new Rectangle(box.Left - 2, box.Top + box.Height - 2, 5, 5);
            var brCorner = new Rectangle(box.Left + box.Width - 2, box.Top + box.Height - 2, 5, 5);

            using (var borderPen = new Pen(Brushes.White, 1))
            {
                e.Graphics.DrawRectangle(borderPen, tlCorner);
                e.Graphics.DrawRectangle(borderPen, trCorner);
                e.Graphics.DrawRectangle(borderPen, blCorner);
                e.Graphics.DrawRectangle(borderPen, brCorner);
            }

            float[] dashValues = {3, 3};
            using (var dashedPen = new Pen(Color.White, 1) {DashPattern = dashValues})
            {
                e.Graphics.DrawLine(dashedPen, new Point(box.Left + 4, box.Top),
                    new Point(box.Left + box.Width - 2, box.Top));
                e.Graphics.DrawLine(dashedPen, new Point(box.Left + box.Width, box.Top + 4),
                    new Point(box.Left + box.Width, box.Top + box.Height - 2));
                e.Graphics.DrawLine(dashedPen, new Point(box.Left + box.Width - 2, box.Top + box.Height),
                    new Point(box.Left + 2, box.Top + box.Height));
                e.Graphics.DrawLine(dashedPen, new Point(box.Left, box.Top + box.Height - 2),
                    new Point(box.Left, box.Top + 2));
            }

            DrawAllTools(e.Graphics);

//            DrawPanel(e.Graphics);
        }

        private void DrawPanel(Graphics picG)
        {
            var left = CurrentBottomRight.X + 10;
            var top = CurrentBottomRight.Y - panelTools.Height;
            var height = 200;

            picG.FillRectangle(Brushes.Wheat, left + 240, top, 40, height);

            var file = Assembly.GetExecutingAssembly().GetManifestResourceStream("LighterShot.images.rect.png");
            picG.DrawImage(Image.FromStream(file), new Rectangle(left + 240, top, 40, 30));
        }

        private void DrawAllTools(Graphics picG)
        {
            foreach (var drawing in _drawings.Reverse())
            {
                var src = drawing.From;
                var dest = drawing.To;

                switch (drawing.Type)
                {
                    case DrawingTool.DrawingToolType.Rectangle:
                        var topLeft = new Point {X = Math.Min(src.X, dest.X), Y = Math.Min(src.Y, dest.Y)};
                        var bottomRight = new Point {X = Math.Max(src.X, dest.X), Y = Math.Max(src.Y, dest.Y)};

                        if (topLeft.X < CurrentTopLeft.X) topLeft.X = CurrentTopLeft.X;
                        if (topLeft.Y < CurrentTopLeft.Y) topLeft.Y = CurrentTopLeft.Y;
                        if (bottomRight.X > CurrentBottomRight.X) bottomRight.X = CurrentBottomRight.X;
                        if (bottomRight.Y > CurrentBottomRight.Y) bottomRight.Y = CurrentBottomRight.Y;

                        using (var pen = new Pen(drawing.Color, 3))
                        {
                            picG.DrawRectangle(pen,
                                new Rectangle
                                {
                                    Location = topLeft,
                                    Width = bottomRight.X - topLeft.X,
                                    Height = bottomRight.Y - topLeft.Y
                                });
                        }
                        break;

                    case DrawingTool.DrawingToolType.Line:
                        if (dest.X < CurrentTopLeft.X) dest.X = CurrentTopLeft.X;
                        if (dest.Y < CurrentTopLeft.Y) dest.Y = CurrentTopLeft.Y;
                        if (dest.X > CurrentBottomRight.X) dest.X = CurrentBottomRight.X;
                        if (dest.Y > CurrentBottomRight.Y) dest.Y = CurrentBottomRight.Y;

                        if (drawing.DrawStraight)
                        {
                            makeLineStraight(src, ref dest);
                        }

                        using (var pen = new Pen(drawing.Color, 3))
                        {
                            picG.DrawLine(pen, src, dest);
                        }
                        break;

                    case DrawingTool.DrawingToolType.Arrow:
                        if (dest.X < CurrentTopLeft.X) dest.X = CurrentTopLeft.X;
                        if (dest.Y < CurrentTopLeft.Y) dest.Y = CurrentTopLeft.Y;
                        if (dest.X > CurrentBottomRight.X) dest.X = CurrentBottomRight.X;
                        if (dest.Y > CurrentBottomRight.Y) dest.Y = CurrentBottomRight.Y;

                        if (drawing.DrawStraight)
                        {
                            makeLineStraight(src, ref dest);
                        }

                        using (var bigArrow = new AdjustableArrowCap(3, 4, false))
                        {
                            using (var pen = new Pen(drawing.Color, 3) {CustomEndCap = bigArrow})
                            {
                                picG.DrawLine(pen, src, dest);
                            }
                        }
                        break;
                }
            }
        }

        private void makeLineStraight(Point src, ref Point dest)
        {
            // force straight vertical or horizontal line
            if (Math.Abs(dest.X - src.X) < 20) // vertical
            {
                dest.X = src.X;
            }
            else if (Math.Abs(dest.Y - src.Y) < 20) // horizontal
            {
                dest.Y = src.Y;
            }
            // diagonal?
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
            label1.Text = _goingToDrawTool.ToString() + @"/" + CurrentAction.ToString();
        }

        private void buttonDrawColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                buttonDrawColor.ForeColor = colorDialog1.Color;
            }
        }

        private void buttonDrawArrow_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void buttonDrawArrow_KeyUp(object sender, KeyEventArgs e)
        {
        }
    }
}