using NHibernate.Mapping;
using OOP_Graphic_editor.Commands;
using OOP_Graphic_editor.Decorators;
using OOP_Graphic_editor.ShapeFactory;
using OOP_Graphic_editor.Shapes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_Graphic_editor
{
    internal class CanvasMainController
    {
        public readonly PictureBox canvas;
        private PaintList shapes = new PaintList();
        Stack<AbstractCommand> history = new Stack<AbstractCommand>();
        CShapeFactory factory = new CShapeFactory();
        public bool resetAfterSelection = false;
        string fileName = "Shapes.txt";
        Color currentColor = Color.Blue;
        public CanvasMainController(in PictureBox pictureBox)
        {
            canvas = pictureBox;
        }
        public void Refresh(in Graphics graphics)
        {
            for (int i = 0; i < shapes.Count; i++)
                shapes[i].Draw(graphics);
        }
        public void SelectShape(in int X, in int Y)
        {
            bool flag = false;
            for (int i = 0; i < shapes.Count; ++i)
            {
                if (shapes[i].BelongPoint(X, Y))
                {
                    flag = true; break;
                }
            }
            if (flag)
            {
                UnselectAllCommand unselectAll = new UnselectAllCommand();
                SelectCommand select = new SelectCommand(X, Y);
                if (!resetAfterSelection)
                {
                    unselectAll.Execute(shapes);
                    if (select.Execute(shapes))
                        history.Push(new CommandsGroup(unselectAll, select));
                    else
                        unselectAll.Unexecute();
                }
                else
                {
                    if (select.Execute(shapes))
                        history.Push(select);
                }
                canvas.Refresh();
            }
        }
        public void CreateShape(in string code, in int x, in int y)
        {
            AbstractShape shape = factory.СreateShape(code[0]);
            shape.Init(x, y, currentColor);
            if (shape != null && shape.CheckSize(canvas.Width, canvas.Height, 0, 0, 0, 0))
            {
                CommandsGroup commandGroup = new CommandsGroup(new UnselectAllCommand(), new CreateCommand(shape), new SelectCommand(shape));
                commandGroup.Execute(shapes);
                history.Push(commandGroup);
                canvas.Refresh();
            }
        }
        public void RemoveSelectedShape()
        {
            RemoveCommand remove = new RemoveCommand();
            SelectCommand select;
            remove.Execute(shapes);
            if (shapes.Count > 0)
            {
                select = new SelectCommand(shapes.Last());
                select.Execute(shapes);
                history.Push(new CommandsGroup(remove, select));
            }
            else
            {
                history.Push(remove);
            }
            canvas.Refresh();
        }
        public bool MoveSelectedShape(in int dX, in int dY)
        {
            MoveCommand move = new MoveCommand(canvas.Width, canvas.Height, dX, dY);
            if (move.Execute(shapes))
            {
                history.Push(move);
                canvas.Refresh();
                return true;
            }
            return false;
        }
        public bool SetSizeSelectedShape(in int dW, in int dH)
        {
            ResizeCommand resize = new ResizeCommand(canvas.Width, canvas.Height, dW, dH);
            if (resize.Execute(shapes))
            {
                history.Push(resize);
                canvas.Refresh();
                return true;
            }
            return false;
        }
        public void SetColorSelectedShape(in Color color)
        {
            SetColorCommand setColorCommand = new SetColorCommand(color);
            if(setColorCommand.Execute(shapes))
            {
                history.Push(setColorCommand);
                canvas.Refresh();
            }
        }
        public bool SetCanvasSize(in int dWidth, in int dHeight)
        {
            for (int i = 0; i < shapes.Count; i++)
            {
                if (shapes[i].CheckSize(canvas.Width + dWidth, canvas.Height + dHeight, 0, 0, 0, 0) == false)
                {
                    return false;
                }
            }
            return true;
        }
        public void GroupShape()
        {
            GroupCommand groupCommand = new GroupCommand();
            if (groupCommand.Execute(shapes))
            {
                history.Push(new CommandsGroup(groupCommand));
            }
            else
            {
                UngroupCommand ungroupCommand = new UngroupCommand();
                if (ungroupCommand.Execute(shapes))
                    history.Push(ungroupCommand);
            }
            canvas.Refresh();
        }
        public void CancellationAction()
        {
            if (history.Count > 0)
            {
                AbstractCommand lastCommand = history.Pop();
                lastCommand.Unexecute();
            }
            canvas.Refresh();
        }
        public void SaveShape()
        {
            //if (shapes.Count == 0)
            //{
            //    File.Delete(fileName); return;
            //}
            //else
            {
                File.Create(fileName).Close();
                for (int i = 0; i < shapes.Count; i++)
                {
                    shapes[i].Save(fileName);
                }
            }
        }
        public void LoadShape(string file_name = null)
        {
            if(file_name != null)
                fileName = file_name;
            if (File.Exists(fileName))
            {
                shapes = new PaintList(); history = new Stack<AbstractCommand>();
                shapes.LoadShapes(fileName, factory);
                shapes[shapes.Count - 1] = new CShapeFrameDecorator(shapes[shapes.Count - 1]);
                canvas.Refresh();

            }
        }
    }
}

