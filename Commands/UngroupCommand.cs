using OOP_Graphic_editor.Decorators;
using OOP_Graphic_editor.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Graphic_editor.Commands
{
    class UngroupCommand : Command
    {
        //CGroup group;
        RemoveCommand removeCommand = new RemoveCommand();
        List<SelectCommand> selectCommands = new List<SelectCommand>();
        List<CreateCommand> createCommands = new List<CreateCommand>();
        public override bool Execute(in PaintList shapes)
        {
            this.shapes = shapes;
            CShapeFrameDecorator singleDecorator = (CShapeFrameDecorator)shapes.Find(item => item is CShapeFrameDecorator == true);
            CGroup group = singleDecorator.realObject as CGroup;
            if (group == null)
                return false;
            for (int i = 0; i < group.shapes.Count; ++i)
            {
                createCommands.Add(new CreateCommand(group.shapes[i]));
                createCommands.Last().Execute(shapes);

                selectCommands.Add(new SelectCommand(group.shapes[i]));
            }
            removeCommand.Execute(shapes);
            for (int i = 0; i < selectCommands.Count; ++i)
            {
                selectCommands[i].Execute(shapes);
            }
            return true;
        }

        public override void Unexecute()
        {
            for (int i = selectCommands.Count - 1; i >= 0; --i)
                selectCommands[i].Unexecute();
            removeCommand.Unexecute();
            for (int i = createCommands.Count - 1; i >= 0; --i)
                createCommands[i].Unexecute();
        }
    }
}
