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
    public abstract class CFactory
    {
        public virtual AbstractShape СreateShape(in char code)
        {
            return null;
        }
    }
}
