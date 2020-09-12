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
            int pixels = 25;
            int x = pixels;
            int y = pixels / 2;
            int beginHexX;
            int beginHexY;
            int startX = 150;
            int startY = 150;
            


            for (int rows = 1; rows <= 10; rows++)
            {
                int maxCols;
                if ((rows % 2) == 0) maxCols = 9;
                else maxCols = 10;
                
                for (int cols = 1; cols <= maxCols; cols++)
                {
                    int offsetX = ((rows - 1) % 2);
                    beginHexX = startX + ((cols - 1) * 2 * pixels) + (offsetX * pixels);
                    beginHexY = startY + ((rows - 1) * 2 * pixels);
                    Point[] hexPoints =
                    {
                    new Point(beginHexX, beginHexY),
                    new Point(beginHexX + x, beginHexY - y),
                    new Point(beginHexX + (2*x), beginHexY),
                    new Point(beginHexX + (2*x), beginHexY + y + pixels),
                    new Point(beginHexX + x, beginHexY + pixels + (2*y)),
                    new Point(beginHexX, beginHexY + y + pixels),
                    new Point(beginHexX, beginHexY)
                    };


                    g.DrawLines(myPen, hexPoints);
                
                }
            }


           
        }
    }
}
