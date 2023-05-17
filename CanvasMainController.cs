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
        int moveOffset;
        float sizeOffset;
        Dictionary<string, Command> commands = new Dictionary<string, Command>();
        Stack<Command> history = new Stack<Command>();
        public CanvasMainController(in PictureBox pictureBox)
        {
            canvas = pictureBox;
            moveOffset = 6; sizeOffset = 4;
            commands.Add("up", new MoveCommand(0, -moveOffset));
            commands.Add("down", new MoveCommand(0, moveOffset));
            commands.Add("left", new MoveCommand(-moveOffset, 0));
            commands.Add("right", new MoveCommand(moveOffset, 0));

            commands.Add("plus", new ResizeCommand(sizeOffset, sizeOffset));
            commands.Add("minus", new ResizeCommand(-sizeOffset, -sizeOffset));

            commands.Add("remove", new RemoveCommand(shapes));
            commands.Add("create", new CreateCommand(shapes));
            commands.Add("color", new SetColorCommand());
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
        public bool SelectShape(in int X, in int Y)
        {
            bool result = false;
            for (int i = shapes.Count - 1; i >= 0; --i)
            {
                if (shapes[i].BelongPoint(X, Y))
                {
                    result = true;
                    if (resetOnSelection == false)
                        ResetAllSelect(i);
                    if (shapes[i] is ShapeFrameDecorator == false)
                        shapes[i] = new ShapeFrameDecorator(shapes[i]);
                    break;
                }
            }
            canvas.Refresh();
            return result;
        }
        public void CreateShape(in ColoredShape newShape)
        {
            Command command;
            commands.TryGetValue("create", out command);
            if (newShape.CheckSize(canvas.Width, canvas.Height, 0, 0, 0, 0))
            {
                ResetAllSelect();
                //shapes.Add(new ShapeFrameDecorator(newShape));
                Command newCommand = command.clone();
                newCommand.Execute(new ShapeFrameDecorator(newShape));
                history.Push(newCommand);
            }
            else
                return;
            canvas.Refresh();
        }
        public void RemoveSelectedShape()
        {
            Command command;
            commands.TryGetValue("remove", out command);
            ShapeFrameDecorator decorator;
            for (int i = 0; i < shapes.Count; i++)
            {
                decorator = shapes[i] as ShapeFrameDecorator;
                if (decorator != null)
                {
                    //shapes.RemoveAt(i);
                    Command newCommand = command.clone();
                    newCommand.Execute(shapes[i]);
                    history.Push(newCommand);
                    --i;
                }
            }
            if (shapes.Count != 0)
                shapes[shapes.Count - 1] = new ShapeFrameDecorator(shapes[shapes.Count - 1]);
            canvas.Refresh();
        }
        public bool MoveSelectedShape(in string key)
        {
            bool result = false;
            Command command;
            commands.TryGetValue(key, out command);
            if (command != null)
            {
                for (int i = 0; i < shapes.Count; i++)
                {
                    if (shapes[i] is ShapeFrameDecorator)
                    {
                        switch (key)
                        {
                            case "left":
                                if (shapes[i].CheckSize(canvas.Width, canvas.Height, 0, 0, (uint)moveOffset, 0))
                                    result = true;
                                break;
                            case "right":
                                if (shapes[i].CheckSize(canvas.Width, canvas.Height, 0, 0, 0, (uint)moveOffset))
                                    result = true;
                                break;
                            case "down":
                                if (shapes[i].CheckSize(canvas.Width, canvas.Height, 0, (uint)moveOffset, 0, 0))
                                    result = true;
                                break;
                            case "up":
                                if (shapes[i].CheckSize(canvas.Width, canvas.Height, (uint)moveOffset, 0, 0, 0))
                                    result = true;
                                break;
                        }
                        if (result)
                        {
                            Command newCommand = command.clone();
                            newCommand.Execute(shapes[i]);
                            history.Push(newCommand);
                        }
                    }
                }
            }
            canvas.Refresh();
            return result;
        }
        public bool SetSizeSelectedShape(in string key)
        {
            bool result = false;
            float widthOffset = sizeOffset, heightOffset = sizeOffset;
            Command command;
            commands.TryGetValue(key, out command);
            if (command != null)
            {

                for (int i = 0; i < shapes.Count; i++)
                {
                    if (shapes[i] is ShapeFrameDecorator)
                    {
                        switch (key)
                        {
                            case "plus":
                                if (shapes[i].CheckSize(canvas.Width, canvas.Height, (uint)heightOffset, (uint)heightOffset, (uint)widthOffset, (uint)widthOffset))
                                    result = true;
                                break;
                            case "minus":
                                result = true;
                                break;
                        }
                        if (result)
                        {
                            Command newCommand = command.clone();
                            newCommand.Execute(shapes[i]);
                            history.Push(newCommand);
                        }
                    }
                }
            }
            canvas.Refresh();
            return result;
        }
        public void SetColorSelectedShape(in Color color)
        {
            Command command;
            commands.TryGetValue("color", out command);
            for (int i = 0; i < shapes.Count; i++)
            {
                if (shapes[i] is ShapeFrameDecorator)
                {
                    //shapes[i].COLOR = color;
                    SetColorCommand newCommand = (SetColorCommand)command.clone();
                    newCommand.newColor = color;
                    newCommand.Execute(shapes[i]);
                    history.Push(newCommand);
                }
            }
            canvas.Refresh();
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
                if (FindShapes.Count == 1)
                {
                    CGroup singleGroup = FindShapes[0] as CGroup;
                    if (singleGroup != null)
                    {
                        for (int i = 0; i < singleGroup.shapes.Count; ++i)
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
        public void CancellationAction()
        {
            if (history.Count > 0)
            {
                Command lastCommand = history.Pop();
                lastCommand.Unexecute();
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
            if (File.Exists(fileName))
            {
                shapes.LoadShapes(fileName);
                shapes[shapes.Count - 1] = new ShapeFrameDecorator(shapes[shapes.Count - 1]);
                canvas.Refresh();
               
            }
        }
    }
}

