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


            FillHex(3, 3, "Blue", myPen);
            

            void FillHex(int hexRow, int hexCol, string colour, Pen hexPen)
            {
                
              




                int offsetX = ((hexRow - 1) % 2);
                beginHexX = startX + ((hexCol - 1) * 2 * pixels) + (offsetX * pixels);
                beginHexY = startY + ((hexRow - 1) * 2 * pixels);
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


                // Create solid brush.
                SolidBrush redBrush = new SolidBrush(Color.Blue);

                // Create graphics path object and add ellipse.
                GraphicsPath graphPath = new GraphicsPath();
                graphPath.AddPolygon(hexPoints);


                // Fill graphics path to screen.
                g.FillPath(redBrush, graphPath);

                g.DrawLines(hexPen, hexPoints);
            }



        }

        private void Randomizer_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            int obstacleCount = random.Next(11);
            for (int obstacle = 1; obstacle <= obstacleCount; obstacle++)
            {
                int yCoord = random.Next(5);
                MessageBox.Show("I LOVE YOU TIMES " + obstacle.ToString() + " out of " + obstacleCount.ToString());
            }
        }
    }
}
