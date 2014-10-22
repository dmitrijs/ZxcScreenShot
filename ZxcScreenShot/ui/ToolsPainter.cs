using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace ZxcScreenShot.ui
{
    class ToolsPainter
    {
        private readonly Stack<DrawingTool> _drawings = new Stack<DrawingTool>();

        public void Clear()
        {
            _drawings.Clear();
        }

        public void DrawStraightLatest(bool b)
        {
            _drawings.Peek().DrawStraight = b;
        }

        public bool Undo()
        {
            if (_drawings.Count > 0)
            {
                _drawings.Pop();

                return true;
            }
            return false;
        }

        public void MoveLatestTo(Point point)
        {
            _drawings.Peek().To = point;
        }

        public void Push(DrawingTool drawingTool)
        {
            _drawings.Push(drawingTool);
        }
        
        public void DrawAllTools(Graphics picG, Point origin, Point boundsTopLeft, Point boundsBottomRight)
        {
            picG.SmoothingMode = SmoothingMode.AntiAlias;
            
            foreach (var drawing in _drawings.Reverse())
            {
                var src = drawing.From;
                var dest = drawing.To;
                src.X += origin.X;
                src.Y += origin.Y;
                dest.X += origin.X;
                dest.Y += origin.Y;

                switch (drawing.Type)
                {
                    case DrawingTool.DrawingToolType.Rectangle:
                        var topLeft = new Point { X = Math.Min(src.X, dest.X), Y = Math.Min(src.Y, dest.Y) };
                        var bottomRight = new Point { X = Math.Max(src.X, dest.X), Y = Math.Max(src.Y, dest.Y) };

                        if (topLeft.X < boundsTopLeft.X) topLeft.X = boundsTopLeft.X;
                        if (topLeft.Y < boundsTopLeft.Y) topLeft.Y = boundsTopLeft.Y;
                        if (bottomRight.X > boundsBottomRight.X) bottomRight.X = boundsBottomRight.X;
                        if (bottomRight.Y > boundsBottomRight.Y) bottomRight.Y = boundsBottomRight.Y;

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
                        if (dest.X < boundsTopLeft.X) dest.X = boundsTopLeft.X;
                        if (dest.Y < boundsTopLeft.Y) dest.Y = boundsTopLeft.Y;
                        if (dest.X > boundsBottomRight.X) dest.X = boundsBottomRight.X;
                        if (dest.Y > boundsBottomRight.Y) dest.Y = boundsBottomRight.Y;

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
                        if (dest.X < boundsTopLeft.X) dest.X = boundsTopLeft.X;
                        if (dest.Y < boundsTopLeft.Y) dest.Y = boundsTopLeft.Y;
                        if (dest.X > boundsBottomRight.X) dest.X = boundsBottomRight.X;
                        if (dest.Y > boundsBottomRight.Y) dest.Y = boundsBottomRight.Y;

                        if (drawing.DrawStraight)
                        {
                            makeLineStraight(src, ref dest);
                        }

                        using (var bigArrow = new AdjustableArrowCap(2.3f, 3.6f, false))
                        {
                            using (var pen = new Pen(drawing.Color, 3) { CustomEndCap = bigArrow })
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
    }
}
