﻿using OOP_Graphic_editor.Decorators;
using OOP_Graphic_editor.Shapes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Graphic_editor.Commands
{
    class SetColorCommand : Command
    {
        Color selectColor;
        List<Color> colors = new List<Color>();
        public SetColorCommand(Color color)
        {
            selectColor = color;
        }
        public override bool Execute(in PaintList shapes)
        {
            this.shapes = shapes; bool result = false;
            for (int i = 0; i < shapes.Count; ++i)
            {
                if(shapes[i] is CShapeFrameDecorator)
                {
                    colors.Add(shapes[i].COLOR);
                    shapes[i].COLOR = selectColor;
                    result = true;
                }
            }
            return result;
        }
        public override void Unexecute()
        {
            int ii = 0;
            for (int i = 0; i < shapes.Count; ++i)
            {
                if (shapes[i] is CShapeFrameDecorator)
                {
                    shapes[i].COLOR = colors[ii];
                    ++ii;
                }
            }
        }
    }
}
