using OOP_Graphic_editor.Decorators;
using OOP_Graphic_editor.Shapes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Graphic_editor.Commands
{
    internal class MoveCommand : Command
    {
        private int _dx, _dy, canvasWidth, canvasHeight;
        List<Point> posBeforeMoving = new List<Point>();
        public MoveCommand(in int canvasWidth, in int canvasHeight, in int dx, in int dy)
        {
            _dx = dx; _dy = dy; this.canvasWidth = canvasWidth; this.canvasHeight = canvasHeight;
        }
        public override bool Execute(in PaintList shapes)
        {
            this.shapes = shapes; bool flag = false;
            for (int i = 0; i < shapes.Count; i++)
            {
                if (shapes[i] is CShapeFrameDecorator)
                {
                    posBeforeMoving.Add(shapes[i].POS_CENTER);
                    if (shapes[i].CheckMove(canvasWidth, canvasHeight, _dx, _dy))
                    {
                        shapes[i].Move(_dx, _dy);
                        flag = true;
                    }
                }
            }
            return flag;
        }
        public override void Unexecute()
        {
            int ii = 0;
            for (int i = 0; i < shapes.Count; i++)
            {
                if (shapes[i] is CShapeFrameDecorator && shapes[i].POS_CENTER != posBeforeMoving[ii])
                {
                    shapes[i].Move(-_dx, -_dy); ++ii;
                }
            }
        }
    }
}
