using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HexGrid
{
    public partial class hexGrid : Form
    {
        public hexGrid()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen myPen = new Pen(Color.Red);


            // Hex set-up
            int pixels = 30;
            int x = pixels;
            int y = pixels / 2;
            int origX = 100;
            int origY = 100;
            


            for (int rows = 1; rows <= 10; rows++)
            {
                int maxCols;
                if ((rows % 2) == 0) maxCols = 9;
                else maxCols = 10;
                
                for (int cols = 1; cols <= maxCols; cols++)
                {
                    int offsetX = ((rows - 1) % 2);
                    origX = 100 + ((cols - 1) * 2 * pixels) + (offsetX * pixels);
                    origY = 100 + ((rows - 1) * 2 * pixels);
                    Point[] hexPoints =
                    {
                    new Point(origX, origY),
                    new Point(origX + x, origY - y),
                    new Point(origX + (2*x), origY),
                    new Point(origX + (2*x), origY + y + pixels),
                    new Point(origX + x, origY + pixels + (2*y)),
                    new Point(origX, origY + y + pixels),
                    new Point(origX, origY)
                    };


                    g.DrawLines(myPen, hexPoints);
                
                }
            }


           
        }
    }
}
