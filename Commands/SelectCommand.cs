using OOP_Graphic_editor.Decorators;
using OOP_Graphic_editor.Shapes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Graphic_editor.Commands
{
    internal class SelectCommand : Command
    {
        private readonly int x, y;
        AbstractShape shape;
        public SelectCommand(in int X, in int Y)
        {
            x = X; y = Y;
        }
        public SelectCommand(in AbstractShape shape)
        {
            this.shape = shape;
        }
        public override bool Execute(in PaintList shapes)
        {
            this.shapes = shapes;
            if (shape == null)
            {
                for (int i = 0; i < shapes.Count; i++)
                {
                    if (shapes[i].BelongPoint(x, y) && shapes[i] is CShapeFrameDecorator == false)
                    {
                        shapes[i] = new CShapeFrameDecorator(shapes[i]);
                        shape = shapes[i];
                        return true;
                    }
                }
                return false;
            }
            else
            {
                int index = shapes.FindIndex(item => item == shape);
                shapes[index] = new CShapeFrameDecorator(shapes[index]);
                shape = shapes[index];
                return true;
            }
        }
        public override void Unexecute()
        {
            CShapeFrameDecorator decorator; int index = shapes.FindIndex(item => item == shape);
            decorator = shapes[index] as CShapeFrameDecorator;
            shapes[index] = decorator.realObject;
        }
    }
}