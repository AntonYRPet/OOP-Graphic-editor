﻿using OOP_Graphic_editor.Shapes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_Graphic_editor.Decorators
{
    internal class CShapeFrameDecorator : AbstractShape
    {
        public readonly AbstractShape realObject;
        private const int FRAME_OFFSET = 10;
        private Pen pen = new Pen(Color.Blue, 1);
        public CShapeFrameDecorator(in AbstractShape realObject)
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

        public override float MIN_WIDTH
        {
            get { return realObject.MIN_WIDTH; }
        }
        public override float MIN_HEIGHT
        {
            get { return realObject.MIN_HEIGHT; }
        }
        public override Point POS_CENTER => realObject.POS_CENTER;

        public override Size SIZE => realObject.SIZE;

        public override bool BelongPoint(in int xChecked, in int yChecked)
        {
            return realObject.BelongPoint(xChecked, yChecked);
        }
        public override void Draw(in Graphics graphics)
        {
            realObject.Draw(graphics);
            graphics.DrawRectangle(pen, realObject.X - (realObject.WIDTH + FRAME_OFFSET) / 2, realObject.Y - (realObject.HEIGHT + FRAME_OFFSET) / 2, realObject.WIDTH + FRAME_OFFSET, realObject.HEIGHT + FRAME_OFFSET);
        }
        public override void Move(in int dX, in int dY)
        {
            realObject.Move(dX, dY);
        }
        public override bool SetSize(in float newWidth, in float newHeight)
        {
            return realObject.SetSize(newWidth, newHeight);
        }
        public override bool CheckSize(in int canvasWidth, in int canvasHeight, in uint upOffset, in uint downOffset, in uint leftOffset, in uint rightOffset)
        {
            return realObject.CheckSize(canvasWidth, canvasHeight, upOffset, downOffset, leftOffset, rightOffset);
        }

        public override void Save(in string fileName)
        {
            realObject.Save(fileName);
        }

        //public override void Load(in StreamReader reader,in int beginIndex = 0, CShapeFactory factory = null)
        //{
        //    throw new NotImplementedException();
        //}

        public override void Load(in StreamReader reader, in CFactory factory = null)
        {
            throw new NotImplementedException();
        }

        public override void Init(in int x, in int y, in Color color)
        {
            realObject.Init(x, y, color);
        }

        public override bool CheckMove(in int canvasWidth, in int canvasHeight, in int dX, in int dY)
        {
            return realObject.CheckMove(canvasWidth,canvasHeight, dX, dY);
        }
    }
}