using OOP_Graphic_editor.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Graphic_editor.Commands
{
    internal class CommandsGroup : AbstractCommand
    {
        private List<AbstractCommand> commands = new List<AbstractCommand>();
        public CommandsGroup(in List<AbstractCommand> commands)
        {
            this.commands = commands;
        }
        public CommandsGroup(params AbstractCommand[] commands)
        {
            for (int i = 0; i < commands.Count(); ++i)
            {
                this.commands.Add(commands[i]);
            }
        }
        public override bool Execute(in PaintList shapes)
        {
            bool result = false;
            for(int i=0; i<commands.Count; ++i)
            {
                if(commands[i].Execute(shapes))
                    result = true;
            }
            return result;
        }
        public override void Unexecute()
        {
            for (int i = commands.Count-1; i >=0; --i)
            {
                commands[i].Unexecute();
            }
        }
    }
}
