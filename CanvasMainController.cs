using OOP_Graphic_editor.Decorators;
using OOP_Graphic_editor.Shapes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_Graphic_editor
{
    internal class CanvasMainController
    {
        public readonly PictureBox canvas;
        private List<AbstractShape> shapes = new List<AbstractShape>();
        public bool resetOnSelection = false;
        public CanvasMainController(in PictureBox pictureBox)
        {
            canvas = pictureBox;
        }
        public void Refresh(in Graphics graphics)
        {
            for (int i = 0; i < shapes.Count; i++)
                shapes[i].Draw(graphics);
        }
        private void ResetAllSelect(int? indexMiss=null)
        {
            ShapeFrameDecorator decorator;
            for (int i = 0; i < shapes.Count; i++)
            {
                if (indexMiss == i)
                    continue;
                decorator = shapes[i] as ShapeFrameDecorator;
                if (decorator != null)
                    shapes[i] = decorator.realObject;
            }
        }
        public void SelectShape(in int X, in int Y)
        {
            for (int i = shapes.Count-1; i >= 0; --i)
            {
                if (shapes[i].BelongPoint(X, Y))
                {
                    if (resetOnSelection == false)
                        ResetAllSelect(i);
                    if (shapes[i] is ShapeFrameDecorator == false)
                        shapes[i] = new ShapeFrameDecorator(shapes[i]);
                    break;
                }
            }
            canvas.Refresh();
        }
        public void CreateShape(in BaseShape newShape)
        {
            if (newShape.CheckSize(canvas.Width, canvas.Height, null, null))
            {
                ResetAllSelect();
                shapes.Add(new ShapeFrameDecorator(newShape));
            }
            else
                return;
            canvas.Refresh();
        }
        public void RemoveSelectedShape()
        {
            ShapeFrameDecorator decorator;
            for (int i = 0; i < shapes.Count; i++)
            {
                decorator = shapes[i] as ShapeFrameDecorator;
                if (decorator != null)
                {
                    shapes.RemoveAt(i);
                    --i;
                }
            }
            if(shapes.Count !=0)
                shapes[shapes.Count - 1] = new ShapeFrameDecorator(shapes[shapes.Count - 1]);
            canvas.Refresh();
        }
        public bool MoveSelectedShape(in int dX, in int dY)
        {
            bool result = false;

            for (int i = 0; i < shapes.Count; i++)
            {
                if (shapes[i] is ShapeFrameDecorator && shapes[i].Move(canvas.Width, canvas.Height, shapes[i].X + dX, shapes[i].Y + dY))
                {
                    result = true;
                }
            }
            canvas.Refresh();
            return result;
        }
        public bool SetSizeSelectedShape(in float widthOffset, in float heightOffset)
        {
            bool result = false;
            for (int i = 0; i < shapes.Count; i++)
            {
                if (shapes[i] is ShapeFrameDecorator && shapes[i].SetSize(canvas.Width, canvas.Height, shapes[i].WIDTH + widthOffset, shapes[i].HEIGHT + heightOffset))
                {
                    result = true;
                }
            }
            canvas.Refresh();
            return result;
        }
        public void SetColorSelectedShape(in Color color)
        {
            for (int i = 0; i < shapes.Count; i++)
            {
                if (shapes[i] is ShapeFrameDecorator)
                {
                    shapes[i].COLOR = color;
                }
            }
            canvas.Refresh();
        }
        public bool SetCanvasSize(in int dWidth, in int dHeight)
        {
            for(int i=0; i< shapes.Count; i++)
            {
                if (shapes[i].CheckSize(canvas.Width+dWidth, canvas.Height+dHeight, null, null) == false)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
