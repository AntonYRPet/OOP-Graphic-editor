using OOP_Graphic_editor.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Graphic_editor.Commands
{
    public abstract class AbstractCommand
    {
        public abstract bool Execute(in PaintList shapes);
        public abstract void Unexecute();
    }
    internal abstract class Command:AbstractCommand
    {
        protected PaintList shapes;
        //public abstract void Execute(in List<AbstractShape> shapes);
        //public abstract void Unexecute();
    }
}
