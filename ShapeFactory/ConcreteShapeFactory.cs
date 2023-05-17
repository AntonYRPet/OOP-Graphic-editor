using OOP_Graphic_editor.Decorators;
using OOP_Graphic_editor.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Graphic_editor.ShapeFactory
{
    class ConcreteShapeFactory : CShapeFactory
    {
        public override AbstractShape createShape(in char code)
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
        public override void Remove(in AbstractShape shape)
        {
            if (shapes.Remove(shape) == false && shape is ShapeFrameDecorator)
            {
                ShapeFrameDecorator decorator;
                ShapeFrameDecorator shapeDecorator = shape as ShapeFrameDecorator;
                for (int i = 0; i < shapes.Count; i++)
                {
                    decorator = shapes[i] as ShapeFrameDecorator;
                    if(decorator != null)
                    {
                        if(decorator.realObject == shapeDecorator.realObject)
                            shapes.RemoveAt(i);
                    }
                }
            }
        }
    }
}
