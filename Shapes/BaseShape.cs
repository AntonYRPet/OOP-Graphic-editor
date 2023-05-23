using NHibernate.Id.Insert;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Graphic_editor.Shapes
{
    public abstract class AbstractShape
    {
        public abstract int X { get; }
        public abstract int Y { get; }
        public abstract Point POS_CENTER { get; }
        public abstract Size SIZE { get; }
        public abstract float WIDTH { get; }
        public abstract float HEIGHT { get; }
        public abstract Color COLOR { get; set; }
        public abstract float MIN_WIDTH { get; }
        public abstract float MIN_HEIGHT { get; }
        public abstract bool SetSize(in float newWidth, in float newHeight);
        public abstract void Move(in int dX, in int dY);
        public abstract bool CheckSize(in int canvasWidth, in int canvasHeight, in uint upOffset, in uint downOffset, in uint leftOffset, in uint rightOffset);
        public abstract void Draw(in Graphics graphics);
        public abstract bool BelongPoint(in int xChecked, in int yChecked);
        public abstract void Save(in string fileName);
        public abstract void Load(in StreamReader reader, in CFactory factory = null);
        public abstract void Init(in int x, in int y, in Color color);
        public abstract bool CheckMove(in int canvasWidth, in int canvasHeight, in int dX, in int dY);
    }
    public abstract class BaseShape : AbstractShape
    {
        protected Point posCenter = new Point();
        protected Size size = new Size();
        protected float minWidth = 5, minHeight = 5;
        public override Point POS_CENTER => posCenter;
        public override Size SIZE => size;
        public override float MIN_HEIGHT
        {
            get { return minHeight; }
        }
        public override float MIN_WIDTH
        {
            get { return minWidth; }
        }
        public override float WIDTH
        { get { return size.Width; } }
        public override float HEIGHT
        { get { return size.Height; } }
        public override int X
        {
            get { return posCenter.X; }
        }
        public override int Y
        {
            get { return posCenter.Y; }
        }
        protected BaseShape(in int x, in int y, in float width, in float height)
        {
            this.posCenter.X = x; this.posCenter.Y = y; size.Width = (int)width; size.Height = (int)height;
        }
        protected BaseShape() { }
        public override bool CheckSize(in int canvasWidth, in int canvasHeight, in uint upOffset, in uint downOffset, in uint leftOffset, in uint rightOffset)
        {
            if (posCenter.Y - size.Height / 2 - upOffset <= 0)
                return false;
            if (posCenter.Y + size.Height / 2 + downOffset >= canvasHeight)
                return false;
            if (posCenter.X - size.Width / 2 - leftOffset <= 0)
                return false;
            if (posCenter.X + size.Width / 2 + rightOffset >= canvasWidth)
                return false;
            return true;
        }
        public override bool CheckMove(in int canvasWidth, in int canvasHeight, in int dX, in int dY)
        {
            if (dX < 0 && CheckSize(canvasWidth, canvasHeight, 0, 0, (uint)-dX, 0) == false)
            { return false; }
            if (dX > 0 && CheckSize(canvasWidth, canvasHeight, 0, 0, 0, (uint)dX) == false)
            { return false; }
            if (dY < 0 && CheckSize(canvasWidth, canvasHeight, (uint)-dY, 0, 0, 0) == false)
            { return false; }
            if (dY > 0 && CheckSize(canvasWidth, canvasHeight, 0, (uint)dY, 0, 0) == false)
            { return false; }
            return true;
        }
        public override void Move(in int dX, in int dY)
        {
            posCenter.X += dX; posCenter.Y += dY;
        }
        public override bool SetSize(in float newWidth, in float newHeight)
        {
            if (newWidth > minWidth && newHeight > minHeight)
            {
                size.Width = (int)newWidth; size.Height = (int)newHeight; return true;
            }
            return false;
        }
    }
    public abstract class ColoredShape : BaseShape
    {
        protected Color color = Color.White;
        protected Pen pen;
        public override Color COLOR
        {
            get { return color; }
            set { color = value; }
        }
        public override void Load(in StreamReader reader, in CFactory factory = null)
        {
            string[] words = reader.ReadLine().Split(' ');
            size.Height = int.Parse(words[1]);
            size.Width = int.Parse(words[2]);
            posCenter.X = int.Parse(words[3]);
            posCenter.Y = int.Parse(words[4]);
            color = Color.FromArgb(Convert.ToInt32(words[5]));
        }
        public override void Save(in string fileName)
        {
            StringBuilder objInfo = new StringBuilder(size.Height + " " + size.Width + " " + posCenter.X + " " + posCenter.Y + " " + color.ToArgb() + "\n");
            File.AppendAllText(fileName, objInfo.ToString());
        }
        protected ColoredShape()
        {
            pen = new Pen(Color.Black, 2);
        }
        protected ColoredShape(in int x, in int y, in float width, in float height, in Color? color) : base(x, y, width, height)
        {
            if (color.HasValue)
                this.color = color.Value;
            else
                this.color = Color.White;
            pen = new Pen(Color.Black, 2);
            size.Width += (int)(2 * this.pen.Width);
            size.Height += (int)(2 * this.pen.Width);
        }
        public override void Init(in int x, in int y, in Color color)
        {
            this.posCenter.X = x; this.posCenter.Y = y; this.color = color;
        }
    }
}
