using OOP_Graphic_editor.Decorators;
using OOP_Graphic_editor.Shapes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Graphic_editor.Commands
{
    internal class ResizeCommand : Command
    {
        private int dW, dH, _canvasWidth, _canvasHeight;
        List<Size> sizeBeforeResize = new List<Size>();
        public ResizeCommand(in int canvasWidth, in int canvasHeight, in int dW, in int dH)
        {
            this.dW = dW; this.dH = dH; _canvasWidth = canvasWidth; _canvasHeight = canvasHeight;
        }
        public override bool Execute(in PaintList shapes)
        {
            this.shapes = shapes; bool result = false;
            uint dH = (uint)Math.Abs(this.dH), dW = (uint)Math.Abs(this.dW);
            for (int i = 0; i < shapes.Count; i++)
            {
                if (shapes[i] is CShapeFrameDecorator)
                {
                    sizeBeforeResize.Add(shapes[i].SIZE);
                    shapes[i].SetSize(shapes[i].WIDTH + this.dW, shapes[i].HEIGHT + this.dH);
                    if (shapes[i].CheckSize(_canvasWidth, _canvasHeight, 0, 0, 0, 0) == false)
                    {
                        shapes[i].SetSize(sizeBeforeResize.Last().Width, sizeBeforeResize.Last().Height);
                    }
                    else
                    {
                        result = true;
                    }

                }
            }
            return result;
        }
        public override void Unexecute()
        {
            int ii = 0;
            for (int i = 0; i < shapes.Count; i++)
            {
                if (shapes[i] is CShapeFrameDecorator)
                {
                    if (shapes[i].SIZE != sizeBeforeResize[ii])
                        shapes[i].SetSize(sizeBeforeResize[ii].Width, sizeBeforeResize[ii].Height);
                    ++ii;
                }
            }
        }
    }
}
