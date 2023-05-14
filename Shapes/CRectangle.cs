using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Graphic_editor.Shapes
{
    internal class CRectangle: BaseShape
    {
        public CRectangle(int x, int y, in Color color, in Pen pen = null, float with = 100f, float height = 100f) : base(x, y, with, height, pen)
        {
            this.color = color;
        }
        public override bool BelongPoint(int xChecked, int yChecked)
        {
            if (Math.Abs(xChecked - x) > width / 2)
                return false;
            if (Math.Abs(yChecked - y) > height / 2)
                return false;
            return true;
        }
        public override void Draw(in Graphics graphics)
        {
            graphics.DrawRectangle(pen, x - width / 2, y - height / 2, width, height);
            graphics.FillRectangle(new SolidBrush(color), x - width / 2, y - height / 2, width, height);
        }
    }
}
