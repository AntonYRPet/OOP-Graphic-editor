using OOP_Graphic_editor.ShapeFactory;
using OOP_Graphic_editor.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Graphic_editor.Commands
{
    internal class RemoveCommand : Command
    {
        ConcreteShapeFactory shapes;
        int index;
        public RemoveCommand(in ConcreteShapeFactory shapes)
        {
            this.shapes = shapes;
        }
        public override Command clone()
        {
            return new RemoveCommand(shapes);
        }
        public override void Execute(in AbstractShape shape)
        {
            if(shape != null)
            {
                AbstractShape tempShape = shape;
                index = shapes.FindIndex(element => element == tempShape);
                this.shape = shape;
                shapes.Remove(shape);
            }
        }
        public override void Unexecute()
        {
            shapes.Insert(index, shape);
        }
    }
}
