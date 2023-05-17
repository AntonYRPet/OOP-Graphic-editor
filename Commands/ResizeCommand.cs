using OOP_Graphic_editor.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Graphic_editor.Commands
{
    internal class ResizeCommand : Command
    {
        private float _dw, _dh;
        public ResizeCommand(in float dw, in float dh)
        {
            _dw = dw;
            _dh = dh;
        }
        public override Command clone()
        {
            return new ResizeCommand(_dw, _dh);
        }
        public override void Execute(in AbstractShape shape)
        {
            if (shape != null)
            {
                this.shape = shape;
                this.shape.SetSize(shape.WIDTH + _dw, shape.HEIGHT + _dh);
            }
        }
        public override void Unexecute()
        {
            if (shape != null)
            {
                shape.SetSize(shape.WIDTH - _dw, shape.HEIGHT - _dh);
            }
        }
    }
}
