using OOP_Graphic_editor.Decorators;
using OOP_Graphic_editor.ShapeFactory;
using OOP_Graphic_editor.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Graphic_editor.Commands
{
    internal class CreateCommand : Command
    {
        ConcreteShapeFactory shapes;
        public CreateCommand(in ConcreteShapeFactory shapes)
        {
            this.shapes = shapes;
        }
        public override Command clone()
        {
            return new CreateCommand(shapes);
        }
        public override void Execute(in AbstractShape shape)
        {
            if (shape != null)
            {
                this.shape = shape;
                shapes.Add(shape);
            }
        }
        public override void Unexecute()
        {
            shapes.Remove(shape);
            if(shapes.Count > 0)
                shapes[shapes.Count - 1] = new ShapeFrameDecorator(shapes[shapes.Count - 1]);
        }
    }
}
