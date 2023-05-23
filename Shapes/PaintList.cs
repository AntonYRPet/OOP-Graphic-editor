using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_Graphic_editor.Shapes
{
    public class PaintList : List<AbstractShape>
    {
        public void LoadShapes(in string fileName, in CFactory factory)
        {
            if (File.Exists(fileName))
            {
                AbstractShape shape;
                using (StreamReader reader = new StreamReader(fileName))
                {
                    string shapeInfo = "";
                    while (true)
                    {
                        if (reader.EndOfStream == false)
                        {
                            shapeInfo = Convert.ToChar(reader.Read()).ToString();
                            shape = factory.СreateShape(shapeInfo[0]);
                            if (shape != null)
                            {
                                if (shape is CGroup)
                                    shape.Load(reader, factory);
                                else
                                    shape.Load(reader);
                                this.Add(shape);
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
    }
}
