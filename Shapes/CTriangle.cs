using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Graphic_editor.Shapes
{
    internal class CTriangle : ColoredShape
    {
        Point[] vertexes = new Point[3];
        public CTriangle(in int x, in int y, in Color? color, float with = 100f, float height = 100f) : base(x, y, with, height, color)
        {
            vertexes[0] = new Point();
            vertexes[1] = new Point();
            vertexes[2] = new Point();
            UpdateVertexesCoord();
        }
        public CTriangle() { size.Width = 100; size.Height = 100; UpdateVertexesCoord(); }
        private void UpdateVertexesCoord()
        {
            vertexes[0].X = posCenter.X; vertexes[0].Y = (int)(posCenter.Y - size.Height /2);
            vertexes[1].X = (int)(posCenter.X - size.Width /2); vertexes[1].Y = (int)(posCenter.Y + size.Height /2);
            vertexes[2].X = (int)(posCenter.X + size.Width /2); vertexes[2].Y = (int)(posCenter.Y + size.Height /2);
        }
        public override void Init(in int x, in int y, in Color color)
        {
            base.Init(x, y, color);
            UpdateVertexesCoord();
        }
        public override bool BelongPoint(in int xChecked, in int yChecked)
        {
            int[] results = new int[3];
            results[0] = (vertexes[0].X - xChecked) * (vertexes[1].Y - vertexes[0].Y) - (vertexes[1].X - vertexes[0].X) * (vertexes[0].Y - yChecked);

            results[1] = (vertexes[1].X - xChecked) * (vertexes[2].Y - vertexes[1].Y) - (vertexes[2].X - vertexes[1].X) * (vertexes[1].Y - yChecked);

            results[2] = (vertexes[2].X - xChecked) * (vertexes[0].Y - vertexes[2].Y) - (vertexes[0].X - vertexes[2].X) * (vertexes[2].Y - yChecked);

            if (results[0] >= 0 && results[1] >= 0 && results[2] >= 0)
                return true;
            if (results[0] < 0 && results[1] < 0 && results[2] < 0)
                return true;
            return false;
        }
        public override bool SetSize(in float newWidth, in float newHeight)
        {
            if(base.SetSize(newWidth, newHeight))
            {
                UpdateVertexesCoord();
                return true;
            }
            return false;
        }
        public override void Move(in int dX, in int dY)
        {
            base.Move(dX, dY);
            UpdateVertexesCoord();
        }
        public override void Draw(in Graphics graphics)
        {
            graphics.DrawPolygon(pen, vertexes);
            graphics.FillPolygon(new SolidBrush(color), vertexes);
        }
        public override void Save(in string fileName)
        {
            File.AppendAllText(fileName, "Triangle ");
            base.Save(fileName);
        }
        public override void Load(in StreamReader reader, in CFactory factory = null)
        {
            base.Load(reader, factory);
            UpdateVertexesCoord();
        }
    }
}
