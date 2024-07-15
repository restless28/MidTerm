using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace MidTermProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics; // Obtain the graphics object from PaintEventArgs

            // Define the first layer of gradient background (blue to black)
            Rectangle area = new Rectangle(0, 0, this.Width - 1, this.Height);
            LinearGradientBrush backgroundBrush1 = new LinearGradientBrush(
                area,
                Color.DodgerBlue, 
                Color.Black,      
                LinearGradientMode.Vertical);

            graphics.FillRectangle(backgroundBrush1, area);

            // Define the second layer of gradient background (yellow with transparency)
            LinearGradientBrush backgroundBrush2 = new LinearGradientBrush(
                area,
                Color.White, 
                Color.Transparent,                
                LinearGradientMode.Vertical);

            graphics.FillRectangle(backgroundBrush2, area);


        }


    }
}
