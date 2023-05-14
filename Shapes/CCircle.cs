using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Graphic_editor.Shapes
{
    internal class CCircle : BaseShape
    {
        public CCircle(int x, int y, in Color color, in Pen pen = null, float with = 100f, float height = 100f) : base(x, y, with, height, pen)
        {
            this.color = color;
        }
        public override void Draw(in Graphics graphics)
        {
            graphics.DrawEllipse(pen, x - width / 2, y - height / 2, width, height);
            graphics.FillEllipse(new SolidBrush(color), x - width / 2, y - height / 2, width, height);
        }
        public override bool BelongPoint(int xChecked, int yChecked)
        {
            if (Math.Sqrt(Math.Pow(xChecked - x, 2) + Math.Pow(yChecked - y, 2)) > (height / 2))
            {
                return false;
            }
            return true;
        }
    }
}
