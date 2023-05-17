using OOP_Graphic_editor.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Graphic_editor.Commands
{
    internal abstract class Command
    {
        protected AbstractShape shape;
        public abstract void Execute(in AbstractShape shape);
        public abstract void Unexecute();
        public abstract Command clone();
    }
}
