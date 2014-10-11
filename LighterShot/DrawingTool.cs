using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LighterShot
{
    class DrawingTool
    {
        public enum DrawingToolType
        {
            NotDrawingTool = 0,
            Rectangle,
            Line,
            Arrow
        }

        public DrawingToolType Type { get; set; }

        public Point From { get; set; }
        public Point To { get; set; }
        public Color Color { get; set; }

    }
}
