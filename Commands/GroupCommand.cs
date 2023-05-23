using OOP_Graphic_editor.Decorators;
using OOP_Graphic_editor.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Graphic_editor.Commands
{
    internal class GroupCommand : Command
    {
        private CreateCommand createCommand;
        private RemoveCommand removeCommand = new RemoveCommand();
        private SelectCommand selectCommand;
        public override bool Execute(in PaintList shapes)
        {
            this.shapes = shapes;
            List<AbstractShape> FindShapes = new List<AbstractShape>();
            CShapeFrameDecorator decorator;
            for (int i = 0; i < shapes.Count; i++)
            {
                decorator = shapes[i] as CShapeFrameDecorator;
                if (decorator != null)
                {
                    FindShapes.Add(decorator.realObject);
                }
            }
            if (FindShapes.Count > 1)
            {
                removeCommand.Execute(shapes);
                createCommand = new CreateCommand(new CGroup(FindShapes));
                createCommand.Execute(shapes);
                selectCommand = new SelectCommand(shapes[shapes.Count - 1]);
                selectCommand.Execute(shapes);
                return true;
            }
            return false;
        }
        public override void Unexecute()
        {
            selectCommand.Unexecute();
            createCommand.Unexecute();
            removeCommand.Unexecute();
        }
    }
}
