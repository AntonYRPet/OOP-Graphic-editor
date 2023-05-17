using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace OOP_Graphic_editor.Shapes
{
    internal class CGroup : BaseShape
    {
        public readonly List<AbstractShape> shapes = new List<AbstractShape>();
        private static int depth = 0;
        public CGroup(in List<AbstractShape> shapes)
        {
            CreateGroup(shapes);
        }
        public CGroup() { }
        public void CreateGroup(in List<AbstractShape> shapes)
        {
            int Xmax = shapes[0].X + (int)shapes[0].WIDTH / 2, Ymax = shapes[0].Y + (int)shapes[0].HEIGHT / 2, Xmin = shapes[0].X - (int)shapes[0].WIDTH / 2, Ymin = shapes[0].Y - (int)shapes[0].HEIGHT / 2;
            for (int i = 0; i < shapes.Count; i++)
            {
                this.shapes.Add(shapes[i]);
                Xmax = Math.Max(Xmax, shapes[i].X + (int)shapes[i].WIDTH / 2);
                Ymax = Math.Max(Ymax, shapes[i].Y + (int)shapes[i].HEIGHT / 2);
                Xmin = Math.Min(Xmin, shapes[i].X - (int)shapes[i].WIDTH / 2);
                Ymin = Math.Min(Ymin, shapes[i].Y - (int)shapes[i].HEIGHT / 2);
            }
            width = Xmax - Xmin;
            height = Ymax - Ymin;
            x = Xmin + (int)(width / 2);
            y = Ymin + (int)(height / 2);
            Xmax = shapes[0].X; Ymax = shapes[0].Y; Xmin = shapes[0].X; Ymin = shapes[0].Y;
            for (int i = 0; i < shapes.Count; i++)
            {
                Xmax = Math.Max(Xmax, shapes[i].X + (int)shapes[i].MIN_WIDTH / 2);
                Ymax = Math.Max(Ymax, shapes[i].Y + (int)shapes[i].MIN_HEIGHT / 2);
                Xmin = Math.Min(Xmin, shapes[i].X - (int)shapes[i].MIN_WIDTH / 2);
                Ymin = Math.Min(Ymin, shapes[i].Y - (int)shapes[i].MIN_HEIGHT / 2);
            }
            minWidth = (Xmax - Xmin);
            minHeight = (Ymax - Ymin);
        }
        public override Color COLOR
        {
            get
            {
                return shapes[0].COLOR;
            }
            set
            {
                for (int i = 0; i < shapes.Count; ++i)
                    shapes[i].COLOR = value;
            }
        }
        public override bool BelongPoint(in int xChecked, in int yChecked)
        {
            for (int i = 0; i < shapes.Count; ++i)
                if (shapes[i].BelongPoint(xChecked, yChecked))
                    return true;
            return false;
        }
        public override void Draw(in Graphics graphics)
        {
            for (int i = 0; i < shapes.Count; ++i)
                shapes[i].Draw(graphics);
        }

        public override void Save(in string fileName, bool flag = false)
        {
            //File.AppendAllText(fileName, "Group ( ");
            File.AppendAllText(fileName, "Group " + depth + " (");
            for (int i = 0; i < shapes.Count; ++i)
            {
                if (shapes[i] is CGroup)
                    ++depth;
                shapes[i].Save(fileName, false);
                if (shapes[i] is CGroup)
                    --depth;
                File.AppendAllText(fileName, " ");
            }
            if (flag)
                File.AppendAllText(fileName, ")\n");
        }
        public override void Move(in int dX, in int dY)
        {
            base.Move(dX, dY);
            for (int i = 0; i < shapes.Count; ++i)
                shapes[i].Move(dX, dY);
        }
        public override void SetSize(in float newWidth, in float newHeight)
        {
            for (int i = 0; i < shapes.Count; ++i)
                shapes[i].SetSize(shapes[i].WIDTH + newWidth - width, shapes[i].HEIGHT + newHeight - height);
            base.SetSize(newWidth, newHeight);
        }
        //public override void Load(in StreamReader reader, in int beginIndex = 0, CShapeFactory factory = null)
        //{
        //    string readonlyBuf = reader.ReadLine();
        //    int index = 0;
        //    while (readonlyBuf[index] != '(')
        //        ++index;
        //    index += 2;
        //    AbstractShape shape;
        //    shape = factory.createShape(readonlyBuf[index]);
        //    if (shape != null)
        //    {
        //        shape.Load(reader,index);
        //        shapes.Add(shape);
        //    }
        //}
        private int GetDepth(in string Fileinfo)
        {
            string strDepth = "";
            for (int i = 6; Fileinfo[i] != ' '; ++i)
            {
                strDepth += Fileinfo[i];
            }
            return int.Parse(strDepth);
        }
        public override void Load(ref string newFileInfo, CShapeFactory factory = null)
        {
            /*
            //List<AbstractShape> shapes = new List<AbstractShape>();
            //newFileInfo = newFileInfo.Remove(0, newFileInfo.IndexOf("(") + 2);
            //while(newFileInfo.Length > 2)
            //{
            //    if (newFileInfo[0] == ' ')
            //        newFileInfo = newFileInfo.Remove(0,1);
            //    AbstractShape shape = factory.createShape(newFileInfo[0]);
            //    if (shape != null)
            //    {
            //        if (shape is CGroup)
            //        {
            //            if (shapes.Count > 0 && shapes.Last() is CGroup!=true)
            //                break;
            //            shape.Load(ref newFileInfo, factory);
            //        }
            //        else
            //            shape.Load(ref newFileInfo);
            //        shapes.Add(shape);
            //    }

            //    if (newFileInfo[0] != 'G')
            //        newFileInfo = newFileInfo.Remove(0, newFileInfo.IndexOf(";") + 2);
            //}
            //CreateGroup(shapes);
            */
            List<AbstractShape> shapes = new List<AbstractShape>();
            depth = GetDepth(newFileInfo);
            newFileInfo = newFileInfo.Remove(0, newFileInfo.IndexOf("(") + 1);
            while (newFileInfo.Length > 2)
            {
                if (newFileInfo[0] == ' ')
                    newFileInfo = newFileInfo.Remove(0, 1);
                AbstractShape shape = factory.createShape(newFileInfo[0]);
                if (shape != null)
                {
                    if (shape is CGroup)
                    {
                        if(GetDepth(newFileInfo) <= depth)
                            break;
                        shape.Load(ref newFileInfo, factory);
                    }
                    else
                        shape.Load(ref newFileInfo);
                    shapes.Add(shape);
                }
                if (shape is CGroup != true)
                    newFileInfo = newFileInfo.Remove(0, newFileInfo.IndexOf(";") + 2);
            }
            CreateGroup(shapes);
            --depth;
        }
    }
}