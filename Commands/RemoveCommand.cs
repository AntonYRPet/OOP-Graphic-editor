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
    internal class RemoveCommand : Command
    {
        private List<AbstractShape>removedShapes = new List<AbstractShape>();
        public override bool Execute(in PaintList shapes)
        {
            this.shapes = shapes;
            for (int i = 0; i < shapes.Count; ++i)
            {
                if (shapes[i] is CShapeFrameDecorator)
                {
                    removedShapes.Add(shapes[i]);
                    shapes.RemoveAt(i);
                    --i;
                }
            }
            return true;
        }
        public override void Unexecute()
        {
            for(int i = 0;i < removedShapes.Count; ++i)
                shapes.Add(removedShapes[i]);
        }
    }
}
