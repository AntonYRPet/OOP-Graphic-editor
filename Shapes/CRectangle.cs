using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Graphic_editor.Shapes
{
    internal class CRectangle: ColoredShape
    {
        public CRectangle(int x, int y, in Color color, float with = 100f, float height = 100f) : base(x, y, with, height, color)
        {

        }
        public CRectangle() { }
        public override bool BelongPoint(in int xChecked, in int yChecked)
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
        public override void Save(in string fileName, bool flag = false)
        {
            File.AppendAllText(fileName, "Rectangle ");
            base.Save(fileName, flag);
        }
    }
}
