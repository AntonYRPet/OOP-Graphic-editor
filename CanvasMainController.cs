using OOP_Graphic_editor.Decorators;
using OOP_Graphic_editor.ShapeFactory;
using OOP_Graphic_editor.Shapes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_Graphic_editor
{
    internal class CanvasMainController
    {
        public readonly PictureBox canvas;
        private ConcreteShapeFactory shapes = new ConcreteShapeFactory();
        public bool resetOnSelection = false;
        string fileName = "Shapes.txt";
        public CanvasMainController(in PictureBox pictureBox)
        {
            canvas = pictureBox;
        }
        public void Refresh(in Graphics graphics)
        {
            for (int i = 0; i < shapes.Count; i++)
                shapes[i].Draw(graphics);
        }
        private void ResetAllSelect(int? indexMiss = null)
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
            for (int i = shapes.Count - 1; i >= 0; --i)
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
        public void CreateShape(in ColoredShape newShape)
        {
            if (newShape.CheckSize(canvas.Width, canvas.Height, 0, 0, 0, 0))
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
            if (shapes.Count != 0)
                shapes[shapes.Count - 1] = new ShapeFrameDecorator(shapes[shapes.Count - 1]);
            canvas.Refresh();
        }
        public bool MoveSelectedShape(in int dX, in int dY)
        {
            bool result = false;

            for (int i = 0; i < shapes.Count; i++)
            {
                if (shapes[i] is ShapeFrameDecorator)
                {
                    if (dX > 0 && shapes[i].CheckSize(canvas.Width, canvas.Height, 0, 0, 0, (uint)dX))
                    {
                        shapes[i].Move(dX, 0); result = true;
                    }
                    if (dX < 0 && shapes[i].CheckSize(canvas.Width, canvas.Height, 0, 0, (uint)-dX, 0))
                    {
                        shapes[i].Move(dX, 0); result = true;
                    }
                    if (dY > 0 && shapes[i].CheckSize(canvas.Width, canvas.Height, 0, (uint)dY, 0, 0))
                    {
                        shapes[i].Move(0, dY); result = true;
                    }
                    if (dY < 0 && shapes[i].CheckSize(canvas.Width, canvas.Height, (uint)-dY, 0, 0, 0))
                    {
                        shapes[i].Move(0, dY); result = true;
                    }
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
                if (shapes[i] is ShapeFrameDecorator)
                {
                    if (widthOffset>=0 && heightOffset>=0 && shapes[i].CheckSize(canvas.Width, canvas.Height, (uint)heightOffset, (uint)heightOffset, (uint)widthOffset, (uint)widthOffset)==false)
                    {
                        continue;
                    }
                    shapes[i].SetSize(shapes[i].WIDTH + widthOffset, shapes[i].HEIGHT + heightOffset);
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
            //for (int i = 0; i < shapes.Count; i++)
            //{
            //    if (shapes[i].CheckSize(canvas.Width + dWidth, canvas.Height + dHeight) == false)
            //    {
            //        return false;
            //    }
            //}
            return true;
        }
        public void GroupShape()
        {
            ShapeFrameDecorator decorator;
            List<AbstractShape> FindShapes = new List<AbstractShape>();
            for (int i = 0; i < shapes.Count; i++)
            {
                decorator = shapes[i] as ShapeFrameDecorator;
                if (decorator != null)
                {
                    FindShapes.Add(decorator.realObject);
                    shapes.RemoveAt(i);
                    --i;
                }
            }
            if (FindShapes.Count > 0)
            {
                if(FindShapes.Count == 1)
                {
                    CGroup singleGroup = FindShapes[0] as CGroup;
                    if(singleGroup != null)
                    {
                        for(int i = 0; i<singleGroup.shapes.Count; ++i)
                        {
                            shapes.Add(new ShapeFrameDecorator(singleGroup.shapes[i]));
                        }
                        shapes.Remove(singleGroup);
                        canvas.Refresh();
                        return;
                    }
                }
                CGroup group = new CGroup(FindShapes);
                shapes.Add(new ShapeFrameDecorator(group));
            }
            canvas.Refresh();
        }
        public void SaveShape()
        {
            if (shapes.Count == 0)
            {
                File.Delete(fileName); return;
            }
            else
            {
                File.Create(fileName).Close();
                for (int i = 0; i < shapes.Count; i++)
                {
                    shapes[i].Save(fileName, true);
                }
                File.AppendAllText(fileName, "\n&");
            }
        }
        public void LoadShape()
        {
            if(File.Exists(fileName))
            {
                shapes.LoadShapes(fileName);
                shapes[shapes.Count - 1] = new ShapeFrameDecorator(shapes[shapes.Count - 1]);
                canvas.Refresh();
            }
        }
    }
}

