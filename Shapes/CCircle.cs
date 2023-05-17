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
        public CCircle() { }
        public override void Draw(in Graphics graphics)
        {
            graphics.DrawEllipse(pen, x - width / 2, y - height / 2, width, height);
            graphics.FillEllipse(new SolidBrush(color), x - width / 2, y - height / 2, width, height);
        }
        public override bool BelongPoint(in int xChecked, in int yChecked)
        {
            if (Math.Sqrt((xChecked - x) * (xChecked - x) + (yChecked - y) * (yChecked - y)) > (height / 2))
            {
                return false;
            }
            return true;
        }
        public override void Save(in string fileName, bool flag = false)
        {
            File.AppendAllText(fileName, "Circle ");
            base.Save(fileName, flag);
        }
    }
}
