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
        Graphics g;
        Pen myPen = new Pen(Color.Red);
        
        int pixels = 25;
        
        int beginHexX;
        int beginHexY;
        int startX = 150;
        int startY = 150;
        List<Hex> gridMap = new List<Hex>() { };
        

        private void Grid_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;



            // Hex set-up
            int x = pixels;
            int y = pixels / 2;



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
                    int coordX = (rows + (rows % 2)) / 2;
                    Brush currentBrush = new SolidBrush(Color.White);

                    GraphicsPath graphPath = new GraphicsPath();
                    gridMap.Add(new Hex()
                    {
                        points = hexPoints,
                        Brush = currentBrush,
                        Coords = new Coords(coordX, rows),
                        graphicsPath = graphPath
                    }); ;



                    int arrayPos = ((((rows - 1) * 10) - ((rows - 1) - ((rows - 1) % 2)) / 2) + cols - 1);
                    g.DrawLines(myPen, gridMap[arrayPos].points);
                    
                    g.FillPath(gridMap[arrayPos].Brush, gridMap[arrayPos].graphicsPath);

                    

                }
            }




            

            



        }

        
        void FillAllHexes(List<Creature> creatureList)
        {
            foreach (Creature critter in creatureList)
            {
                string colour;
                int creatureY = critter.XYPos.getY();
                int creatureX = critter.XYPos.getX();
                int arrayPosition = ((creatureY - 1) * 10) + ((creatureX + (creatureX % 2)) / 2) - 1;
                switch (critter.Name)
                {
                    case "obstacle":

                        gridMap[arrayPosition].Brush = new SolidBrush(Color.Brown);
                        break;

                    default:
                        gridMap[arrayPosition].Brush = new SolidBrush(Color.Blue);
                        break;
                }
                
                MessageBox.Show("FILLALLHEXES: ");
                MessageBox.Show(critter.Name);
                MessageBox.Show(critter.XYPos.getX().ToString() + " " + critter.XYPos.getY().ToString());
                MessageBox.Show(critter.Movement.ToString());
                
                ///THIS THIS THIS NEXT (BELOW)


                //this.Invalidate(gridMap[arrayPosition].graphicsPath);
                
                //FillHex(critter.XYPos.getX(), critter.XYPos.getY(), colour, myPen, e);
            }
        }

        /*void FillHex(int hexCol, int hexRow, string colour, Pen hexPen, Graphics e)
        {
            
            
            // Create solid brush.
            switch (colour)
            {
                case "Blue":
                    {
                        redBrush = new SolidBrush(Color.Purple);
                    }
                    break;
                case "Brown":
                    {
                        redBrush = new SolidBrush(Color.Brown);
                    }
                    break;
            }
            // Create graphics path object and add ellipse.
            GraphicsPath graphPath = new GraphicsPath();
            graphPath.AddPolygon(hexPoints);

            MessageBox.Show(g.ToString());
            MessageBox.Show(redBrush.Color.ToString());
            MessageBox.Show(graphPath.ToString());


            
        }*/

        private void Randomizer_Click(object sender, EventArgs e)
        {
            var coordTestList = new List<Creature>();
            Random random = new Random();
            int itemCount = random.Next(10) + 1;
            MessageBox.Show(itemCount.ToString());
            for (int item = 1; item <= itemCount; item++)
            {
                
                int yCoord = random.Next(10) + 1;
                int randMove = random.Next(10) + 1;
                int xCoord = ((random.Next(10) + 1) * 2) - (yCoord % 2);
                
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
            
            FillAllHexes(coordTestList);
        }
    } 
}

