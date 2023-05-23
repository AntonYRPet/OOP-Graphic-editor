using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
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
            size.Width = Xmax - Xmin;
            size.Height = Ymax - Ymin;
            posCenter.X = Xmin + (int)(size.Width / 2);
            posCenter.Y = Ymin + (int)(size.Height / 2);
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
        public override void Move(in int dX, in int dY)
        {
            base.Move(dX, dY);
            for (int i = 0; i < shapes.Count; ++i)
                shapes[i].Move(dX, dY);
        }
        public override bool SetSize(in float newWidth, in float newHeight)
        {
            bool flag = false;
            for (int i = 0; i < shapes.Count; ++i)
                if(shapes[i].SetSize(shapes[i].WIDTH + newWidth - size.Width, shapes[i].HEIGHT + newHeight - size.Height))
                    flag = true;
            if (flag)
                base.SetSize(newWidth, newHeight);
            return flag;
        }
        public override void Save(in string fileName)
        {
            File.AppendAllText(fileName, "Group " + shapes.Count + "\n");
            for(int i=0; i< shapes.Count; ++i)
                shapes[i].Save(fileName);
        }
        public override void Load(in StreamReader reader, in CFactory factory = null)
        {
            string[] words = reader.ReadLine().Split(' ');
            List<AbstractShape> shapes = new List<AbstractShape>();
            AbstractShape shape;
            for (int i = 0; i < Convert.ToInt32(words[1]); ++i)
            {
                shape = factory.СreateShape(Convert.ToChar(reader.Read()));
                if(shape != null)
                {
                    if (shape is CGroup)
                        shape.Load(reader, factory);
                    else
                        shape.Load(reader);
                    shapes.Add(shape);
                }
            }
            CreateGroup(shapes);
        }
        public override void Init(in int x, in int y, in Color color)
        {
            return;
        }
    }
}