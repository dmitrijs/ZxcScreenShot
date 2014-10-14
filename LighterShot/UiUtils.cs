using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LighterShot
{
    static class UiUtils
    {
        static public FormOverlay.CursPos UpdateCursorAndGetCursorPosition(Form This, Point CurrentTopLeft, Point CurrentBottomRight)
        {
            if (((Cursor.Position.X > CurrentTopLeft.X - 10 && Cursor.Position.X < CurrentTopLeft.X + 10)) &&
                ((Cursor.Position.Y > CurrentTopLeft.Y + 10) && (Cursor.Position.Y < CurrentBottomRight.Y - 10)))
            {
                This.Cursor = Cursors.SizeWE;
                return FormOverlay.CursPos.LeftLine;
            }
            if (((Cursor.Position.X >= CurrentTopLeft.X - 10 && Cursor.Position.X <= CurrentTopLeft.X + 10)) &&
                ((Cursor.Position.Y >= CurrentTopLeft.Y - 10) && (Cursor.Position.Y <= CurrentTopLeft.Y + 10)))
            {
                This.Cursor = Cursors.SizeNWSE;
                return FormOverlay.CursPos.TopLeft;
            }
            if (((Cursor.Position.X >= CurrentTopLeft.X - 10 && Cursor.Position.X <= CurrentTopLeft.X + 10)) &&
                ((Cursor.Position.Y >= CurrentBottomRight.Y - 10) && (Cursor.Position.Y <= CurrentBottomRight.Y + 10)))
            {
                This.Cursor = Cursors.SizeNESW;
                return FormOverlay.CursPos.BottomLeft;
            }
            if (((Cursor.Position.X > CurrentBottomRight.X - 10 && Cursor.Position.X < CurrentBottomRight.X + 10)) &&
                ((Cursor.Position.Y > CurrentTopLeft.Y + 10) && (Cursor.Position.Y < CurrentBottomRight.Y - 10)))
            {
                This.Cursor = Cursors.SizeWE;
                return FormOverlay.CursPos.RightLine;
            }
            if (((Cursor.Position.X >= CurrentBottomRight.X - 10 && Cursor.Position.X <= CurrentBottomRight.X + 10)) &&
                ((Cursor.Position.Y >= CurrentTopLeft.Y - 10) && (Cursor.Position.Y <= CurrentTopLeft.Y + 10)))
            {
                This.Cursor = Cursors.SizeNESW;
                return FormOverlay.CursPos.TopRight;
            }
            if (((Cursor.Position.X >= CurrentBottomRight.X - 10 && Cursor.Position.X <= CurrentBottomRight.X + 10)) &&
                ((Cursor.Position.Y >= CurrentBottomRight.Y - 10) && (Cursor.Position.Y <= CurrentBottomRight.Y + 10)))
            {
                This.Cursor = Cursors.SizeNWSE;
                return FormOverlay.CursPos.BottomRight;
            }
            if (((Cursor.Position.Y > CurrentTopLeft.Y - 10) && (Cursor.Position.Y < CurrentTopLeft.Y + 10)) &&
                ((Cursor.Position.X > CurrentTopLeft.X + 10 && Cursor.Position.X < CurrentBottomRight.X - 10)))
            {
                This.Cursor = Cursors.SizeNS;
                return FormOverlay.CursPos.TopLine;
            }
            if (((Cursor.Position.Y > CurrentBottomRight.Y - 10) && (Cursor.Position.Y < CurrentBottomRight.Y + 10)) &&
                ((Cursor.Position.X > CurrentTopLeft.X + 10 && Cursor.Position.X < CurrentBottomRight.X - 10)))
            {
                This.Cursor = Cursors.SizeNS;
                return FormOverlay.CursPos.BottomLine;
            }
            if (
                (Cursor.Position.X >= CurrentTopLeft.X + 10 && Cursor.Position.X <= CurrentBottomRight.X - 10) &&
                (Cursor.Position.Y >= CurrentTopLeft.Y + 10 && Cursor.Position.Y <= CurrentBottomRight.Y - 10))
            {
                // This.Cursor = Cursors.SizeAll;
                return FormOverlay.CursPos.WithinSelectionArea;
            }

            This.Cursor = Cursors.No;
            return FormOverlay.CursPos.OutsideSelectionArea;
        }

    }
}
