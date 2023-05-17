using OOP_Graphic_editor.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_Graphic_editor
{
    public class CShapeFactory
    {
        protected List<AbstractShape> shapes = new List<AbstractShape>();
        public int Count
        {
            get { return shapes.Count; }
        }
        public int FindIndex(Predicate<AbstractShape> match)
        {
            return shapes.FindIndex(match);
        }
        public void Insert(in int index, in AbstractShape shape)
        {
            shapes.Insert(index, shape);
        }
        public AbstractShape this[in int index]
        {
            get
            {
                return shapes[index];
            }
            set
            {
                shapes[index] = value;
            }
        }
        public void Add(in AbstractShape shape)
        {
            shapes.Add(shape);
        }
        public void RemoveAt(in int index)
        {
            shapes.RemoveAt(index);
        }
        public virtual void Remove(in AbstractShape shape)
        {
            shapes.Remove(shape);
        }
        public virtual AbstractShape createShape(in char code)
        {
            return null;
        }
        public void LoadShapes(in string fileName)
        {
            if (File.Exists(fileName))
            {
                AbstractShape shape;
                using (StreamReader reader = new StreamReader(fileName))
                {
                    string code = reader.ReadLine();
                    while (code != null && code!="")
                    {
                        shape = createShape(code[0]);
                        if (shape != null)
                        {
                            if(shape is CGroup)
                                shape.Load(ref code, this);
                            else
                                shape.Load(ref code);
                            shapes.Add(shape);
                        }
                        code = reader.ReadLine();
                    }
                }
            }

        }
    }
}
