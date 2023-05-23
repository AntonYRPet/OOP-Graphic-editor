using OOP_Graphic_editor.Decorators;
using OOP_Graphic_editor.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Graphic_editor.ShapeFactory
{
    public class CShapeFactory : CFactory
    {
        public override AbstractShape СreateShape(in char code)
        {
            AbstractShape shape = null;
            switch (code)
            {
                case 'C':
                    shape = new CCircle();
                    break;
                case 'T':
                    shape = new CTriangle();
                    break;
                case 'R':
                    shape = new CRectangle();
                    break;
                case 'G':
                    shape = new CGroup();
                    break;
            }
            return shape;
        }
    }
}
