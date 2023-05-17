using OOP_Graphic_editor.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Graphic_editor.Commands
{
    internal class MoveCommand : Command
    {
        private int _dx, _dy;
        public override Command clone()
        {
            return new MoveCommand(_dx, _dy);
        }
        public MoveCommand(in int dx, in int dy)
        {
            _dx = dx; _dy = dy; shape = null;
        }
        public override void Execute(in AbstractShape shape)
        {
            if(shape != null)
            {
                this.shape = shape;
                this.shape.Move(_dx, _dy);
            }
        }
        public override void Unexecute()
        {
            if (shape != null)
                this.shape.Move(-_dx, -_dy);
        }
    }
}
