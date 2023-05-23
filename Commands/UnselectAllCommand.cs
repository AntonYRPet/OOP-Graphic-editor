using OOP_Graphic_editor.Decorators;
using OOP_Graphic_editor.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Graphic_editor.Commands
{
    internal class UnselectAllCommand : Command
    {
        List<CShapeFrameDecorator> decorators = new List<CShapeFrameDecorator>();
        public override bool Execute(in PaintList shapes)
        {
            this.shapes = shapes;
            CShapeFrameDecorator decorator; bool result = false;
            for (int i = 0; i < shapes.Count; ++i)
            {
                decorator = shapes[i] as CShapeFrameDecorator;
                if (decorator != null)
                {
                    result = true;
                    decorators.Add(decorator);
                    shapes[i] = decorator.realObject;
                }
            }
            return result;
        }
        public override void Unexecute()
        {
            int index;
            for (int i = 0; i < shapes.Count; ++i)
            {
                index = decorators.FindIndex(item => item.realObject == shapes[i]);
                if (index != -1)
                {
                    shapes[i] = decorators[index];
                    decorators.RemoveAt(index);
                }
            }
        }
    }
}
