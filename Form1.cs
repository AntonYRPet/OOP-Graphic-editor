using OOP_Graphic_editor.Decorators;
using OOP_Graphic_editor.Shapes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_Graphic_editor
{
    public partial class Form1 : Form
    {
        enum Action
        {
            select, circle, rectangle, triangle
        }
        List<ColoredShape> shapes = new List<ColoredShape>();
        Action actionOnCanvas = Action.select;
        CanvasMainController canvasMainController;
        Color currentColor = Color.Blue;
        int currentFormWidth;
        int currentFormHeight;
        int currentFormX;
        int currentFormY;
        bool moveFlag = false;
        public Form1()
        {
            InitializeComponent();

            setColorButton.BackColor = currentColor;
            currentFormHeight = this.Height;
            currentFormWidth = this.Width;
        }
        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            moveFlag = true;
            switch (actionOnCanvas)
            {
                case Action.select:
                    canvasMainController.SelectShape(e.X, e.Y);
                    break;
                case Action.circle:
                    canvasMainController.CreateShape(new CCircle(e.X, e.Y, currentColor));
                    break;
                case Action.rectangle:
                    canvasMainController.CreateShape(new CRectangle(e.X, e.Y, currentColor));
                    break;
                case Action.triangle:
                    canvasMainController.CreateShape(new CTriangle(e.X, e.Y, currentColor));
                    break;
                default:
                    break;
            }
        }
        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (moveFlag && canvasMainController.SelectShape(e.X, e.Y))
            {
                if(e.X>currentFormX)
                    canvasMainController.MoveSelectedShape("right");
                if(e.X<currentFormX)
                    canvasMainController.MoveSelectedShape("left");
                if(e.Y>currentFormY)
                    canvasMainController.MoveSelectedShape("down");
                if(e.Y<currentFormY)
                    canvasMainController.MoveSelectedShape("up");
            }
            currentFormX = e.X;
            currentFormY = e.Y;
        }
        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            moveFlag = false;
        }
        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            canvasMainController.Refresh(e.Graphics);
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    canvasMainController.MoveSelectedShape("left");
                    break;
                case Keys.Right:
                    canvasMainController.MoveSelectedShape("right");
                    break;
                case Keys.Up:
                    canvasMainController.MoveSelectedShape("up");
                    break;
                case Keys.Down:
                    canvasMainController.MoveSelectedShape("down");
                    break;
                case Keys.Oemplus:
                    canvasMainController.SetSizeSelectedShape("plus");
                    break;
                case Keys.OemMinus:
                    canvasMainController.SetSizeSelectedShape("minus");
                    break;
                case Keys.ControlKey:
                    canvasMainController.resetOnSelection = true;
                    break;
                case Keys.Delete:
                    canvasMainController.RemoveSelectedShape();
                    break;
                case Keys.G:
                    canvasMainController.GroupShape();
                    break;
                case Keys.S:
                    canvasMainController.SaveShape();
                    break;
                case Keys.Z:
                    canvasMainController.CancellationAction();
                    break;
            }
        }
        private void selectButton_Click(object sender, EventArgs e)
        {
            actionOnCanvas = Action.select;
        }
        private void circleButton_Click(object sender, EventArgs e)
        {
            actionOnCanvas = Action.circle;
        }
        private void rectangleButton_Click(object sender, EventArgs e)
        {
            actionOnCanvas = Action.rectangle;
        }
        private void triangleButton_Click(object sender, EventArgs e)
        {
            actionOnCanvas = Action.triangle;
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.ControlKey:
                    canvasMainController.resetOnSelection = false;
                    break;
            }
        }
        private void setColorButton_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            canvasMainController.SetColorSelectedShape(colorDialog1.Color);
            currentColor = colorDialog1.Color;
            setColorButton.BackColor = currentColor;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (canvasMainController.SetCanvasSize(Width - currentFormWidth, Height - currentFormHeight) == false)
            {
                Width = currentFormWidth; Height = currentFormHeight;
                MessageBox.Show("An object outside the workspace!");
            }
            else
            {
                currentFormWidth = Width; currentFormHeight = Height;
            }
        }
        string SystemFileName = "SystemSize.txt";
        private void Form1_Load(object sender, EventArgs e)
        {
            canvasMainController = new CanvasMainController(canvas);
            canvasMainController.LoadShape();
            if (File.Exists(SystemFileName))
            {

                using (StreamReader reader = new StreamReader(SystemFileName))
                {
                    string readBuf = reader.ReadLine();
                    string[] buf = readBuf.Split(' ');
                    this.Size = new Size(int.Parse(buf[0]), int.Parse(buf[1]));
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
           
            canvasMainController.SaveShape();
            File.Create(SystemFileName).Close();
            File.AppendAllText(SystemFileName, this.Width + " " + this.Height);
        }
    }
}