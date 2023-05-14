using OOP_Graphic_editor.Shapes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_Graphic_editor.Decorators
{
    internal class ShapeFrameDecorator : AbstractShape
    {
        public readonly AbstractShape realObject;
        private const int FRAME_OFFSET = 3;
        private Pen pen = new Pen(Color.Blue, 1);
        public ShapeFrameDecorator(in AbstractShape realObject)
        {
            this.realObject = realObject;
        }
        public override float HEIGHT
        {
            get { return realObject.HEIGHT; }
        }
        public override float WIDTH
        {
            get { return realObject.WIDTH; }
        }
        public override Color COLOR
        {
            get { return realObject.COLOR; }
            set { realObject.COLOR = value; }
        }
        public override int X
        {
            get { return realObject.X; }
        }
        public override int Y
        {
            get { return realObject.Y; }
        }
        public override bool BelongPoint(int xChecked, int yChecked)
        {
            return realObject.BelongPoint(xChecked, yChecked);
        }
        public override void Draw(in Graphics graphics)
        {
            realObject.Draw(graphics);
            graphics.DrawRectangle(pen, realObject.X - (realObject.WIDTH + FRAME_OFFSET) / 2, realObject.Y - (realObject.HEIGHT + FRAME_OFFSET) / 2, realObject.WIDTH + FRAME_OFFSET, realObject.HEIGHT + FRAME_OFFSET);
        }
        public override bool Move(in int canvasWidth, in int canvasHeight, in int newX, in int newY)
        {
            return realObject.Move(canvasWidth, canvasHeight, newX, newY);
        }
        public override bool SetSize(in int canvasWidth, in int canvasHeight, in float newWidth, in float newHeight)
        {
            return realObject.SetSize(canvasWidth, canvasHeight, newWidth, newHeight);
        }
        public override bool CheckSize(in int canvasWidth, in int canvasHeight, int? newX, int? newY, in float upOffset = 0, in float downOffset = 0, in float leftOffset = 0, in float rightOffset = 0)
        {
            return realObject.CheckSize(canvasWidth, canvasHeight, newX, newY, upOffset, downOffset, leftOffset, rightOffset);
        }
    }
}