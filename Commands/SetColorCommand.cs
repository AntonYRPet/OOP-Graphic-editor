using OOP_Graphic_editor.Shapes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Graphic_editor.Commands
{
    internal class SetColorCommand : Command
    {
        public Color newColor;
        Color oldColor;
        public SetColorCommand(in Color color)
        {
            this.newColor = color;
        }
        public SetColorCommand()
        {
        }
        public override Command clone()
        {
            return new SetColorCommand(newColor);
        }

        public override void Execute(in AbstractShape shape)
        {
            this.shape = shape;
            oldColor = shape.COLOR;
            shape.COLOR = newColor;
        }
        public override void Unexecute()
        {
            shape.COLOR = oldColor;
        }
    }
}
