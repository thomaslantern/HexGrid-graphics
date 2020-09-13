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




            void FillAllHexes(List<Creature> creatureList)
            {
                foreach (Creature critter in creatureList)
                {
                    string colour;
                    switch (critter.Name)
                    {
                        case "obstacle":

                            colour = "Brown";
                            break;

                        default:
                            colour = "Blue";
                            break;
                    }
                    FillHex(critter.XYPos.getX(), critter.XYPos.getY(), colour, myPen);
                }
            }


            void FillHex(int hexCol, int hexRow, string colour, Pen hexPen)
            {






                //int offsetX = ((hexRow) % 2);
                beginHexX = startX + ((hexCol - 1) * pixels);// + (offsetX * pixels);
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

                SolidBrush redBrush = new SolidBrush(Color.Blue);

                // Create solid brush.
                switch (colour)
                {
                    case "Blue":
                        {
                            redBrush = new SolidBrush(Color.Blue);
                        }
                        break;
                    case "Brown":
                        {
                            redBrush = new SolidBrush(Color.Brown);
                        } break;
                }
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
            var coordTestList = new List<Creature>();
            Random random = new Random();
            int itemCount = random.Next(11);
            for (int item = 1; item <= itemCount; item++)
            {
                int xCoord;
                int yCoord = random.Next(10);
                int randMove = random.Next(11);
                if ((yCoord % 2) == 1) xCoord = random.Next(11) * 2 - (yCoord % 2);
                else xCoord = random.Next(10) * 2;
                string creatureName;
                if (item == 1) creatureName = "First";
                else creatureName = "obstacle";

                coordTestList.Add(new Creature
                {
                    Name = creatureName,
                    XYPos = new Coords(xCoord, yCoord),
                    Movement = randMove
                });




            }
           //FillAllHexes(coordTestList);
        }
    } 
}

