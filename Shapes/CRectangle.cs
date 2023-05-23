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
        public CRectangle() { size.Width = 100; size.Height = 100; }
        public override bool BelongPoint(in int xChecked, in int yChecked)
        {
            if (Math.Abs(xChecked - posCenter.X) > size.Width / 2)
                return false;
            if (Math.Abs(yChecked - posCenter.Y) > size.Height / 2)
                return false;
            return true;
        }
        public override void Draw(in Graphics graphics)
        {
            graphics.DrawRectangle(pen, posCenter.X - size.Width / 2, posCenter.Y - size.Height / 2, size.Width, size.Height);
            graphics.FillRectangle(new SolidBrush(color), posCenter.X - size.Width / 2, posCenter.Y - size.Height / 2, size.Width, size.Height);
        }
        public override void Save(in string fileName)
        {
            File.AppendAllText(fileName, "Rectangle ");
            base.Save(fileName);
        }
    }
}
