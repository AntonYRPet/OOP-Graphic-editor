using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace OOP_Graphic_editor.Shapes
{
    internal class CCircle : ColoredShape
    {
        public CCircle(in int x, in int y, in Color? color, float with = 100f, float height = 100f) : base(x, y, with, height, color)
        {

        }
        public CCircle() { size.Width = 100; size.Height = 100; }
        public override void Draw(in Graphics graphics)
        {
            graphics.DrawEllipse(pen, posCenter.X - size.Width / 2, posCenter.Y - size.Height / 2, size.Width, size.Height);
            graphics.FillEllipse(new SolidBrush(color), posCenter.X - size.Width / 2, posCenter.Y - size.Height / 2, size.Width, size.Height);
        }
        public override bool BelongPoint(in int xChecked, in int yChecked)
        {
            if (Math.Sqrt((xChecked - posCenter.X) * (xChecked - posCenter.X) + (yChecked - posCenter.Y) * (yChecked - posCenter.Y)) > (size.Height / 2))
            {
                return false;
            }
            return true;
        }
        public override void Save(in string fileName)
        {
            File.AppendAllText(fileName, "Circle ");
            base.Save(fileName);
        }
    }
}
