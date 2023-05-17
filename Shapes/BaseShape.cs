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
        public abstract float WIDTH { get; }
        public abstract float HEIGHT { get; }
        public abstract Color COLOR { get; set; }
        public abstract float MIN_WIDTH { get; }
        public abstract float MIN_HEIGHT { get; }
        public abstract void SetSize(in float newWidth, in float newHeight);
        public abstract void Move(in int dX, in int dY);
        public abstract bool CheckSize(in int canvasWidth, in int canvasHeight, in uint upOffset, in uint downOffset, in uint leftOffset, in uint rightOffset);
        public abstract void Draw(in Graphics graphics);
        public abstract bool BelongPoint(in int xChecked, in int yChecked);
        public abstract void Save(in string fileName, bool flag = false);
        //public abstract void Load(in StreamReader reader,in int beginIndex = 0, CShapeFactory factory = null);
        public abstract void Load(ref string fileInfo, CShapeFactory factory = null);
    }
    public abstract class BaseShape : AbstractShape
    {
        protected int x;
        protected int y;
        protected float width;
        protected float height;
        protected float minWidth = 5, minHeight = 5;
        public override float MIN_HEIGHT
        {
            get { return minHeight; }
        }
        public override float MIN_WIDTH
        {
            get { return minWidth; }
        }
        public override float WIDTH
        { get { return width; } }
        public override float HEIGHT
        { get { return height; } }
        public override int X
        {
            get { return x; }
        }
        public override int Y
        {
            get { return y; }
        }
        protected BaseShape(in int x, in int y, in float width, in float height)
        {
            this.x = x; this.y = y; this.width = width; this.height = height;
        }
        protected BaseShape() { }
        public override bool CheckSize(in int canvasWidth, in int canvasHeight, in uint upOffset, in uint downOffset, in uint leftOffset, in uint rightOffset)
        {
            if (y - height / 2 - upOffset <= 0)
                return false;
            if (y + height / 2 + downOffset >= canvasHeight)
                return false;
            if (x - width / 2 - leftOffset <= 0)
                return false;
            if (x + width / 2 + rightOffset >= canvasWidth)
                return false;
            return true;
        }
        public override void Move(in int dX, in int dY)
        {
            x += dX; y += dY;
        }
        public override void SetSize(in float newWidth, in float newHeight)
        {
            if (newWidth > minWidth && newHeight > minHeight)
            {
                width = newWidth; height = newHeight;
            }
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
        public override void Load(ref string fileInfo, CShapeFactory factory = null)
        {
            string[] words = fileInfo.Split(' ');
            height = float.Parse(words[3]);
            width = float.Parse(words[6]);
            x = int.Parse(words[9]);
            y = int.Parse(words[12]);
            color = Color.FromArgb(Convert.ToInt32(words[13]));
        }
        public override void Save(in string fileName, bool flag = false)
        {
            StringBuilder objInfo = new StringBuilder("height = " + height + " width = " + width + " x = " + x + " y = " + y + " " + color.ToArgb()+" ;");
            File.AppendAllText(fileName, objInfo.ToString());
            if(flag)
                File.AppendAllText(fileName, "\n");
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
            this.width += 2 * this.pen.Width;
            this.height += 2 * this.pen.Width;
        }
    }

}
