using System.Drawing;
using System.Windows.Forms;

namespace ZxcScreenShot.ui
{
    static class UiUtils
    {
        static public FormOverlay.CursPos UpdateCursorAndGetCursorPosition(Form overlayForm, Point currentTopLeft, Point currentBottomRight, bool isResizable)
        {
            var curPos = Cursor.Position;
            curPos.X -= overlayForm.Left;
            curPos.Y -= overlayForm.Top;

            if (isResizable)
            {
                if (((curPos.X > currentTopLeft.X - 5 && curPos.X < currentTopLeft.X + 5)) &&
                    ((curPos.Y > currentTopLeft.Y + 5) && (curPos.Y < currentBottomRight.Y - 5)))
                {
                    overlayForm.Cursor = Cursors.SizeWE;
                    return FormOverlay.CursPos.LeftLine;
                }
                if (((curPos.X >= currentTopLeft.X - 5 && curPos.X <= currentTopLeft.X + 5)) &&
                    ((curPos.Y >= currentTopLeft.Y - 5) && (curPos.Y <= currentTopLeft.Y + 5)))
                {
                    overlayForm.Cursor = Cursors.SizeNWSE;
                    return FormOverlay.CursPos.TopLeft;
                }
                if (((curPos.X >= currentTopLeft.X - 5 && curPos.X <= currentTopLeft.X + 5)) &&
                    ((curPos.Y >= currentBottomRight.Y - 5) &&
                     (curPos.Y <= currentBottomRight.Y + 5)))
                {
                    overlayForm.Cursor = Cursors.SizeNESW;
                    return FormOverlay.CursPos.BottomLeft;
                }
                if (((curPos.X > currentBottomRight.X - 5 && curPos.X < currentBottomRight.X + 5)) &&
                    ((curPos.Y > currentTopLeft.Y + 5) && (curPos.Y < currentBottomRight.Y - 5)))
                {
                    overlayForm.Cursor = Cursors.SizeWE;
                    return FormOverlay.CursPos.RightLine;
                }
                if (((curPos.X >= currentBottomRight.X - 5 && curPos.X <= currentBottomRight.X + 5)) &&
                    ((curPos.Y >= currentTopLeft.Y - 5) && (curPos.Y <= currentTopLeft.Y + 5)))
                {
                    overlayForm.Cursor = Cursors.SizeNESW;
                    return FormOverlay.CursPos.TopRight;
                }
                if (((curPos.X >= currentBottomRight.X - 5 && curPos.X <= currentBottomRight.X + 5)) &&
                    ((curPos.Y >= currentBottomRight.Y - 5) &&
                     (curPos.Y <= currentBottomRight.Y + 5)))
                {
                    overlayForm.Cursor = Cursors.SizeNWSE;
                    return FormOverlay.CursPos.BottomRight;
                }
                if (((curPos.Y > currentTopLeft.Y - 5) && (curPos.Y < currentTopLeft.Y + 5)) &&
                    ((curPos.X > currentTopLeft.X + 5 && curPos.X < currentBottomRight.X - 5)))
                {
                    overlayForm.Cursor = Cursors.SizeNS;
                    return FormOverlay.CursPos.TopLine;
                }
                if (((curPos.Y > currentBottomRight.Y - 5) && (curPos.Y < currentBottomRight.Y + 5)) &&
                    ((curPos.X > currentTopLeft.X + 5 && curPos.X < currentBottomRight.X - 5)))
                {
                    overlayForm.Cursor = Cursors.SizeNS;
                    return FormOverlay.CursPos.BottomLine;
                }

                if (
                    (curPos.X >= currentTopLeft.X + 5 && curPos.X <= currentBottomRight.X - 5) &&
                    (curPos.Y >= currentTopLeft.Y + 5 && curPos.Y <= currentBottomRight.Y - 5))
                {
                    // This.Cursor = Cursors.SizeAll;
                    return FormOverlay.CursPos.WithinSelectionArea;
                }

                overlayForm.Cursor = Cursors.No;
                return FormOverlay.CursPos.OutsideSelectionArea;
            }
            if (
                (curPos.X >= currentTopLeft.X && curPos.X <= currentBottomRight.X) &&
                (curPos.Y >= currentTopLeft.Y && curPos.Y <= currentBottomRight.Y))
            {
                // This.Cursor = Cursors.SizeAll;
                return FormOverlay.CursPos.WithinSelectionArea;
            }
            
            overlayForm.Cursor = Cursors.No;
            return FormOverlay.CursPos.OutsideSelectionArea;
        }
    }
}
