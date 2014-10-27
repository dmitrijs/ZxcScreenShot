using System.Drawing;
using System.Windows.Forms;

namespace ZxcScreenShot.ui
{
    static class UiUtils
    {
        static public FormOverlay.CursPos UpdateCursorAndGetCursorPosition(Form This, Point CurrentTopLeft, Point CurrentBottomRight, bool isResizable)
        {
            var cursorPosition = Cursor.Position;
            cursorPosition.X -= This.Left;
            cursorPosition.Y -= This.Top;

            if (isResizable)
            {
                if (((cursorPosition.X > CurrentTopLeft.X - 5 && cursorPosition.X < CurrentTopLeft.X + 5)) &&
                    ((cursorPosition.Y > CurrentTopLeft.Y + 5) && (cursorPosition.Y < CurrentBottomRight.Y - 5)))
                {
                    This.Cursor = Cursors.SizeWE;
                    return FormOverlay.CursPos.LeftLine;
                }
                if (((cursorPosition.X >= CurrentTopLeft.X - 5 && cursorPosition.X <= CurrentTopLeft.X + 5)) &&
                    ((cursorPosition.Y >= CurrentTopLeft.Y - 5) && (cursorPosition.Y <= CurrentTopLeft.Y + 5)))
                {
                    This.Cursor = Cursors.SizeNWSE;
                    return FormOverlay.CursPos.TopLeft;
                }
                if (((cursorPosition.X >= CurrentTopLeft.X - 5 && cursorPosition.X <= CurrentTopLeft.X + 5)) &&
                    ((cursorPosition.Y >= CurrentBottomRight.Y - 5) &&
                     (cursorPosition.Y <= CurrentBottomRight.Y + 5)))
                {
                    This.Cursor = Cursors.SizeNESW;
                    return FormOverlay.CursPos.BottomLeft;
                }
                if (((cursorPosition.X > CurrentBottomRight.X - 5 && cursorPosition.X < CurrentBottomRight.X + 5)) &&
                    ((cursorPosition.Y > CurrentTopLeft.Y + 5) && (cursorPosition.Y < CurrentBottomRight.Y - 5)))
                {
                    This.Cursor = Cursors.SizeWE;
                    return FormOverlay.CursPos.RightLine;
                }
                if (((cursorPosition.X >= CurrentBottomRight.X - 5 && cursorPosition.X <= CurrentBottomRight.X + 5)) &&
                    ((cursorPosition.Y >= CurrentTopLeft.Y - 5) && (cursorPosition.Y <= CurrentTopLeft.Y + 5)))
                {
                    This.Cursor = Cursors.SizeNESW;
                    return FormOverlay.CursPos.TopRight;
                }
                if (((cursorPosition.X >= CurrentBottomRight.X - 5 && cursorPosition.X <= CurrentBottomRight.X + 5)) &&
                    ((cursorPosition.Y >= CurrentBottomRight.Y - 5) &&
                     (cursorPosition.Y <= CurrentBottomRight.Y + 5)))
                {
                    This.Cursor = Cursors.SizeNWSE;
                    return FormOverlay.CursPos.BottomRight;
                }
                if (((cursorPosition.Y > CurrentTopLeft.Y - 5) && (cursorPosition.Y < CurrentTopLeft.Y + 5)) &&
                    ((cursorPosition.X > CurrentTopLeft.X + 5 && cursorPosition.X < CurrentBottomRight.X - 5)))
                {
                    This.Cursor = Cursors.SizeNS;
                    return FormOverlay.CursPos.TopLine;
                }
                if (((cursorPosition.Y > CurrentBottomRight.Y - 5) && (cursorPosition.Y < CurrentBottomRight.Y + 5)) &&
                    ((cursorPosition.X > CurrentTopLeft.X + 5 && cursorPosition.X < CurrentBottomRight.X - 5)))
                {
                    This.Cursor = Cursors.SizeNS;
                    return FormOverlay.CursPos.BottomLine;
                }

                if (
                    (cursorPosition.X >= CurrentTopLeft.X + 5 && cursorPosition.X <= CurrentBottomRight.X - 5) &&
                    (cursorPosition.Y >= CurrentTopLeft.Y + 5 && cursorPosition.Y <= CurrentBottomRight.Y - 5))
                {
                    // This.Cursor = Cursors.SizeAll;
                    return FormOverlay.CursPos.WithinSelectionArea;
                }

                This.Cursor = Cursors.No;
                return FormOverlay.CursPos.OutsideSelectionArea;
            }
            if (
                (cursorPosition.X >= CurrentTopLeft.X && cursorPosition.X <= CurrentBottomRight.X) &&
                (cursorPosition.Y >= CurrentTopLeft.Y && cursorPosition.Y <= CurrentBottomRight.Y))
            {
                // This.Cursor = Cursors.SizeAll;
                return FormOverlay.CursPos.WithinSelectionArea;
            }
            
            This.Cursor = Cursors.No;
            return FormOverlay.CursPos.OutsideSelectionArea;
        }
    }
}
