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
        AbstractShape shape;
        public CreateCommand(in AbstractShape shape)
        {
            this.shape = shape;
        }
        public override bool Execute(in PaintList shapes)
        {
            this.shapes = shapes;
            shapes.Add(shape);
            return true;
        }
        public override void Unexecute()
        {
            shapes.Remove(shape);
        }
    }
}
