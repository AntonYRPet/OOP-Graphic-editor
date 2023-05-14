using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Graphic_editor.Shapes
{
    internal class CTriangle : BaseShape
    {
        Point[] vertexes = new Point[3];
        public CTriangle(int x, int y, in Color color, in Pen pen = null, float with = 100f, float height = 100f) : base(x, y, with, height, pen)
        {
            this.color = color;
            vertexes[0] = new Point(x, (int)(y - height / 2));
            vertexes[1] = new Point((int)(x - width / 2), (int)(y + height / 2));
            vertexes[2] = new Point((int)(x + width / 2), (int)(y + height / 2));
        }
        private void UpdateVertexesCoord()
        {
            vertexes[0].X = x; vertexes[0].Y = (int)(y - height/2);
            vertexes[1].X = (int)(x - width/2); vertexes[1].Y = (int)(y + height/2);
            vertexes[2].X = (int)(x + width/2); vertexes[2].Y = (int)(y + height/2);
        }
        public override bool BelongPoint(int xChecked, int yChecked)
        {
            UpdateVertexesCoord();
            int[] results = new int[3];
            results[0] = (vertexes[0].X - xChecked) * (vertexes[1].Y - vertexes[0].Y) - (vertexes[1].X - vertexes[0].X) * (vertexes[0].Y - yChecked);

            results[1] = (vertexes[1].X - xChecked) * (vertexes[2].Y - vertexes[1].Y) - (vertexes[2].X - vertexes[1].X) * (vertexes[1].Y - yChecked);

            results[2] = (vertexes[2].X - xChecked) * (vertexes[0].Y - vertexes[2].Y) - (vertexes[0].X - vertexes[2].X) * (vertexes[2].Y - yChecked);

            if (results[0] >= 0 && results[1] >= 0 && results[2] >= 0)
                return true;
            if (results[0] <= 0 && results[1] <= 0 && results[2] <= 0)
                return true;
            return false;
        }
        public override bool Move(in int canvasWidth, in int canvasHeight, in int newX, in int newY)
        {
            bool result = base.Move(canvasWidth, canvasHeight, newX, newY);
            if(result == false)
            {
                return result;
            }
            UpdateVertexesCoord();
            return result;
        }
        public override void Draw(in Graphics graphics)
        {
            UpdateVertexesCoord();
            graphics.DrawPolygon(pen, vertexes);
            graphics.FillPolygon(new SolidBrush(color), vertexes);
        }
    }
}
