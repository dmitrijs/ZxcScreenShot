using System.Drawing;
using System.Windows.Forms;

namespace ZxcScreenShot.ui
{
    static class UiUtils
    {
        static public FormOverlay.CursPos UpdateCursorAndGetCursorPosition(Form This, Point CurrentTopLeft, Point CurrentBottomRight, bool isResizable)
        {
            var curPos = Cursor.Position;
            curPos.X -= This.Left;
            curPos.Y -= This.Top;

            if (isResizable)
            {
                if (((curPos.X > CurrentTopLeft.X - 5 && curPos.X < CurrentTopLeft.X + 5)) &&
                    ((curPos.Y > CurrentTopLeft.Y + 5) && (curPos.Y < CurrentBottomRight.Y - 5)))
                {
                    This.Cursor = Cursors.SizeWE;
                    return FormOverlay.CursPos.LeftLine;
                }
                if (((curPos.X >= CurrentTopLeft.X - 5 && curPos.X <= CurrentTopLeft.X + 5)) &&
                    ((curPos.Y >= CurrentTopLeft.Y - 5) && (curPos.Y <= CurrentTopLeft.Y + 5)))
                {
                    This.Cursor = Cursors.SizeNWSE;
                    return FormOverlay.CursPos.TopLeft;
                }
                if (((curPos.X >= CurrentTopLeft.X - 5 && curPos.X <= CurrentTopLeft.X + 5)) &&
                    ((curPos.Y >= CurrentBottomRight.Y - 5) &&
                     (curPos.Y <= CurrentBottomRight.Y + 5)))
                {
                    This.Cursor = Cursors.SizeNESW;
                    return FormOverlay.CursPos.BottomLeft;
                }
                if (((curPos.X > CurrentBottomRight.X - 5 && curPos.X < CurrentBottomRight.X + 5)) &&
                    ((curPos.Y > CurrentTopLeft.Y + 5) && (curPos.Y < CurrentBottomRight.Y - 5)))
                {
                    This.Cursor = Cursors.SizeWE;
                    return FormOverlay.CursPos.RightLine;
                }
                if (((curPos.X >= CurrentBottomRight.X - 5 && curPos.X <= CurrentBottomRight.X + 5)) &&
                    ((curPos.Y >= CurrentTopLeft.Y - 5) && (curPos.Y <= CurrentTopLeft.Y + 5)))
                {
                    This.Cursor = Cursors.SizeNESW;
                    return FormOverlay.CursPos.TopRight;
                }
                if (((curPos.X >= CurrentBottomRight.X - 5 && curPos.X <= CurrentBottomRight.X + 5)) &&
                    ((curPos.Y >= CurrentBottomRight.Y - 5) &&
                     (curPos.Y <= CurrentBottomRight.Y + 5)))
                {
                    This.Cursor = Cursors.SizeNWSE;
                    return FormOverlay.CursPos.BottomRight;
                }
                if (((curPos.Y > CurrentTopLeft.Y - 5) && (curPos.Y < CurrentTopLeft.Y + 5)) &&
                    ((curPos.X > CurrentTopLeft.X + 5 && curPos.X < CurrentBottomRight.X - 5)))
                {
                    This.Cursor = Cursors.SizeNS;
                    return FormOverlay.CursPos.TopLine;
                }
                if (((curPos.Y > CurrentBottomRight.Y - 5) && (curPos.Y < CurrentBottomRight.Y + 5)) &&
                    ((curPos.X > CurrentTopLeft.X + 5 && curPos.X < CurrentBottomRight.X - 5)))
                {
                    This.Cursor = Cursors.SizeNS;
                    return FormOverlay.CursPos.BottomLine;
                }

                if (
                    (curPos.X >= CurrentTopLeft.X + 5 && curPos.X <= CurrentBottomRight.X - 5) &&
                    (curPos.Y >= CurrentTopLeft.Y + 5 && curPos.Y <= CurrentBottomRight.Y - 5))
                {
                    // This.Cursor = Cursors.SizeAll;
                    return FormOverlay.CursPos.WithinSelectionArea;
                }

                This.Cursor = Cursors.No;
                return FormOverlay.CursPos.OutsideSelectionArea;
            }
            if (
                (curPos.X >= CurrentTopLeft.X && curPos.X <= CurrentBottomRight.X) &&
                (curPos.Y >= CurrentTopLeft.Y && curPos.Y <= CurrentBottomRight.Y))
            {
                // This.Cursor = Cursors.SizeAll;
                return FormOverlay.CursPos.WithinSelectionArea;
            }
            
            This.Cursor = Cursors.No;
            return FormOverlay.CursPos.OutsideSelectionArea;
        }
    }
}
