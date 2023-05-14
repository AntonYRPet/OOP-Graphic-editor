using System;
using System.Collections.Generic;
using System.Drawing;
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
        public abstract bool SetSize(in int canvasWidth, in int canvasHeight, in float newWidth, in float newHeight);
        public abstract bool Move(in int canvasWidth, in int canvasHeight, in int newX, in int newY);
        public abstract bool CheckSize(in int canvasWidth, in int canvasHeight, int? newX, int? newY, in float upOffset = 0, in float downOffset = 0, in float leftOffset = 0, in float rightOffset = 0);
        public abstract void Draw(in Graphics graphics);
        public abstract bool BelongPoint(int xChecked, int yChecked);
    }
    internal abstract class BaseShape : AbstractShape
    {
        protected int x;
        protected int y;
        protected float width;
        protected float height;
        protected Color color = Color.White;
        protected Pen pen;
        public override Color COLOR
        {
            get { return color; }
            set { color = value; }
        }
        public override int X
        {
            get { return x; }
        }
        public override int Y
        {
            get { return y; }
        }
        public override float WIDTH
        { get { return width; } }
        public override float HEIGHT
        { get { return height; } }
        protected BaseShape(int x, int y, float width, float height, in Pen pen)
        {
            this.x = x; this.y = y; this.width = width; this.height = height;
            if (pen != null)
            {
                this.pen = pen;
            }
            else
            {
                this.pen = new Pen(Color.Black, 2);
            }
            this.width += 2 * this.pen.Width;
            this.height += 2 * this.pen.Width;
        }
        public override bool CheckSize(in int canvasWidth, in int canvasHeight, int? newX, int? newY, in float upOffset = 0, in float downOffset = 0, in float leftOffset = 0, in float rightOffset = 0)
        {
            if (newX.HasValue == false)
                newX = x;
            if (newY.HasValue == false)
                newY = y;
            if ((newY - height / 2 - upOffset) < 0 || (newY + height / 2 + downOffset) >= canvasHeight)
                return false;
            if ((newX - width / 2 - leftOffset) < 0 || (newX + width / 2 + rightOffset) >= canvasWidth)
                return false;
            return true;
        }
        public override bool Move(in int canvasWidth, in int canvasHeight, in int newX, in int newY)
        {
            bool result = false;
            if (CheckSize(canvasWidth, canvasHeight, newX, y) == true)
            {
                x = newX;
                result = true;
            }
            else
            {
                if (newX < x)
                {
                    for (int i = 0; CheckSize(canvasWidth, canvasHeight, x - 1, y) && i < x - newX; ++i)
                    {
                        --x;
                        result = true;
                    }
                }
                else
                {
                    for (int i = 0; CheckSize(canvasWidth, canvasHeight, x + 1, y) && i < newX - x; ++i)
                    {
                        ++x;
                        result = true;
                    }
                }
            }
            if (CheckSize(canvasWidth, canvasHeight, x, newY) == true)
            {
                y = newY;
                result = true;
            }
            else
            {
                if (newY < y)
                {
                    for (int i = 0; CheckSize(canvasWidth, canvasHeight, x, y - 1) && i < y - newY; ++i)
                    {
                        --y;
                        result = true;
                    }
                }
                else
                {
                    for (int i = 0; CheckSize(canvasWidth, canvasHeight, x, y + 1) && i < newY - y; ++i)
                    {
                        ++y;
                        result = true;
                    }
                }
            }
            return result;
        }
        public override bool SetSize(in int canvasWidth, in int canvasHeight, in float newWidth, in float newHeight)
        {
            if (newWidth >= width && newHeight >= height)
            {
                float widthOffset = Math.Abs(width - newWidth) / 2;
                float heightOffset = Math.Abs(height - newHeight) / 2;
                if (CheckSize(canvasWidth, canvasHeight, null, null, heightOffset, heightOffset, widthOffset, widthOffset) == false)
                {
                    return false;
                }
            }
            if (newHeight < 1 || newWidth < 1)
                return false;
            width = newWidth;
            height = newHeight;
            return true;
        }

    }

}
