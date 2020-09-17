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
        List<Creature> creatureList = new List<Creature>() { };
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
                    graphPath.AddPolygon(hexPoints);
                    gridMap.Add(new Hex()
                    {
                        points = hexPoints,
                        Brush = currentBrush,
                        Coords = new Coords(coordX, rows),
                        graphicsPath = graphPath
                    }); ;



                    int arrayPos = ((((rows - 1) * 10) - ((rows - 1) - ((rows - 1) % 2)) / 2) + cols - 1);
                    g.FillPath(gridMap[arrayPos].Brush, gridMap[arrayPos].graphicsPath);
                    g.DrawLines(myPen, gridMap[arrayPos].points);
                    
                    
                    

                    

                }
            }

            


            

            



        }
        
        void FillAllHexes(List<Creature> creatureList)
        {
            foreach (Creature critter in creatureList)
            {

                int creatureY = critter.XYPos.getY();
                int creatureX = critter.XYPos.getX();
                int rowsSoFar = creatureY - 1;
                int evenRowsSoFar = (rowsSoFar - (rowsSoFar % 2)) / 2;
                int activeRowColumns = (creatureX + (creatureX % 2)) / 2;
                int hexSoFar = (rowsSoFar * 10) - evenRowsSoFar + activeRowColumns;// 1 for each even row so far
                int arrayPosition = hexSoFar - 1;
                switch (critter.Name)
                {
                    case "obstacle":

                        gridMap[arrayPosition].Brush = new SolidBrush(Color.Brown);
                        break;

                    default:
                        gridMap[arrayPosition].Brush = new SolidBrush(Color.Blue);
                        break;
                }
                Region myRegion = new Region(gridMap[arrayPosition].graphicsPath);



                this.Invalidate(myRegion);
                this.Update();
                
            }
        }

        static string RotateDirection(string direction)
        {
            if (direction == "NE") return "E";
            else if (direction == "E") return "SE";
            else if (direction == "SE") return "SW";
            else if (direction == "SW") return "W";
            else if (direction == "W") return "NW";
            else if (direction == "NW") return "End";
            else return "NE";
        }

        static Coords MoveTo(Coords coord, string direction)
        {

            int moveX;
            int moveY;

            if (direction == "NW")
            {
                moveX = -1;
                moveY = -1;
            }
            else if (direction == "NE")
            {
                moveX = 1;
                moveY = -1;
            }
            else if ((direction == "SE") || (direction == "SW"))
            {
                moveX = 1;
                moveY = 1;
                if (direction == "SW") moveX = -1;

            }
            else if (direction == "E")
            {
                moveX = 2;
                moveY = 0;
            }

            else if (direction == "W")
            {
                moveX = -2;
                moveY = 0;
            }

            // End; do not move
            else
            {
                moveX = -coord.getX();
                moveY = -coord.getY();
            }

            int newCoordX = coord.getX() + moveX;
            int newCoordY = coord.getY() + moveY;

            return new Coords(newCoordX, newCoordY);

        }

        static bool ObstacleCheck(Coords nextCoord, List<Coords> obstacles)
        {
            foreach (Coords coord in obstacles)
            {
                if ((nextCoord.getX() == coord.getX()) && (nextCoord.getY() == coord.getY())) return true;
            }
            return false;
        }

       void PossiblePaths(
             Coords currentPosition,
             List<Coords> obstacleList,
             string moveDirection,
             List<Coords> pathSoFar,
             List<Coords> exhaustedCoords,
             int movesLeft,
             List<List<Coords>> currentPaths,
             List<string> directionList)
        {


            while (directionList[0] != "End")
            {
                
                Coords nextCoord = MoveTo(currentPosition, moveDirection);
                //MessageBox.Show("Next COORD: " + nextCoord.getX() + ", " + nextCoord.getY());
                if (movesLeft != 0)
                {




                    // The following code will run if either:
                    // - The creature is out of movement,
                    // - There is one movement left but we've already gone every path from the currentPosition, or
                    // - We've returning or are on a position where we've already gone NW
                }
                if (moveDirection == "End")
                {
                    // Add path to list
                    List<Coords> addedPath = new List<Coords>(pathSoFar);
                    currentPaths.Add(addedPath);

                    // Add coord to exhausted list
                    Coords newExhausted = new Coords(currentPosition.getX(), currentPosition.getY());
                    exhaustedCoords.Add(newExhausted);

                    // Remove last item in path 
                    
                    pathSoFar.RemoveAt(pathSoFar.Count - 1);
                    


                    // Pop "End" off direction list
                    directionList.RemoveAt(directionList.Count - 1);

                    // Rotate the direction before it, update the list
                    moveDirection = RotateDirection(directionList[(directionList.Count - 1)]);
                    directionList[(directionList.Count - 1)] = moveDirection;



                    // Add +1 Movement?
                    movesLeft += 1;

                    currentPosition = pathSoFar[(pathSoFar.Count - 1)];

                }
                else if (movesLeft == 0)
                {
                    // Add path to list, remove last item in path
                    List<Coords> addedPath = new List<Coords>(pathSoFar);
                    currentPaths.Add(addedPath);

                    // Pop "End" off list


                    pathSoFar.RemoveAt(pathSoFar.Count - 1);


                    //if you went NW, no more going from that coordinate
                    if (moveDirection == "NW")
                    {

                        Coords newExhausted = new Coords((pathSoFar[(pathSoFar.Count - 1)].getX()), pathSoFar[(pathSoFar.Count - 1)].getY());
                        pathSoFar.RemoveAt(pathSoFar.Count - 1);
                        exhaustedCoords.Add(newExhausted);
                        movesLeft += 1;
                    }

                    
                    directionList.RemoveAt(directionList.Count - 1);


                    moveDirection = RotateDirection(directionList[(directionList.Count - 1)]);
                    directionList[(directionList.Count - 1)] = moveDirection;

                    movesLeft += 1;
                    currentPosition = pathSoFar[(directionList.Count - 1)];


                }

                // Obstacle and valid coordinate check


                else if ((nextCoord.getX() <= 0) ||
                    (nextCoord.getY() <= 0) ||
                    (nextCoord.getX() > 19) ||
                    (nextCoord.getY() > 10) ||
                    (ObstacleCheck(nextCoord, obstacleList)) ||
                    (ObstacleCheck(nextCoord, pathSoFar)) ||
                    (ObstacleCheck(nextCoord, exhaustedCoords)))
                {
                    // Fail to move; rotate and try again!
                  /*  MessageBox.Show("Failed: can't move to (" + nextCoord.getX().ToString() + ", " +
                        nextCoord.getY().ToString() + ")");*/


                    if (moveDirection == "NW")
                    {
                        List<Coords> addedPath = new List<Coords>(pathSoFar);
                        currentPaths.Add(addedPath);
                        exhaustedCoords.Add(currentPosition);

                        if (directionList.Count == 1) moveDirection = "End";
                        else
                        {
                            directionList.RemoveAt(directionList.Count - 1);
                            RotateDirection(directionList[(directionList.Count - 1)]);
                        }
                        moveDirection = directionList[(directionList.Count - 1)];
                        movesLeft += 1;
                        pathSoFar.RemoveAt(pathSoFar.Count - 1);
                        if (pathSoFar.Count == 1)
                        {
                            MessageBox.Show("DIRECTION LIST IS " + directionList.Count.ToString());
                            MessageBox.Show("DIRECTINO IS: " + moveDirection);
                            MessageBox.Show(pathSoFar[0].ToString());
                        }
                        else currentPosition = pathSoFar[(pathSoFar.Count - 1)]; 
                        
                    }
                    else
                    {
                        moveDirection = RotateDirection(moveDirection);
                        directionList[(directionList.Count - 1)] = moveDirection;
                    }
                }


                else
                {
                    // Success!
                   /* MessageBox.Show("Add coordinates: (" + nextCoord.getX().ToString() + ", " +
                        nextCoord.getY().ToString() + ")");*/
                    int creatureY = nextCoord.getY();
                    int creatureX = nextCoord.getX();
                    int rowsSoFar = creatureY - 1;
                    int evenRowsSoFar = (rowsSoFar - (rowsSoFar % 2)) / 2;
                    int activeRowColumns = (creatureX + (creatureX % 2)) / 2;
                    int hexSoFar = (rowsSoFar * 10) - evenRowsSoFar + activeRowColumns;
                    int arrayPosition = hexSoFar - 1;
                    Region myRegion = new Region(gridMap[arrayPosition].graphicsPath);
                    gridMap[arrayPosition].Brush = new SolidBrush(Color.Green);
                    
                    this.Invalidate(myRegion);
                    
                    pathSoFar.Add(nextCoord);

                    currentPosition = nextCoord;
                    movesLeft -= 1;
                    moveDirection = "NE";
                    directionList.Add(moveDirection);


                }   
            }

            this.Update();
            
            




            
        }
   
            private void Randomizer_Click(object sender, EventArgs e)
        {
            var coordTestList = new List<Creature>();
            Random random = new Random();
            int itemCount = random.Next(10) + 1;
            
            MessageBox.Show(itemCount.ToString());
            creatureList = new List<Creature> { };
            for (int item = 1; item <= itemCount; item++)
            {
                int xCoord;
                int yCoord = random.Next(10) + 1;
                int randMove = random.Next(10) + 1;
                if (yCoord % 2 == 1) xCoord = ((random.Next(10) + 1) * 2) - (yCoord % 2);
                else xCoord = ((random.Next(9) + 1) * 2);
                
                string creatureName;
                if (item == 1)
                {
                    creatureName = "First";
                    MessageBox.Show(randMove.ToString());
                }
                else creatureName = "obstacle";

                Creature creature = 
                new Creature
                {
                    Name = creatureName,
                    XYPos = new Coords(xCoord, yCoord),
                    Movement = randMove
                };
                coordTestList.Add(creature);
                creatureList.Add(creature);


            }
            
            FillAllHexes(coordTestList);
        }

        private void PossibleMoves_Click(object sender, EventArgs e)
        {
            Creature creature = creatureList[0];
            List<Coords> obstacles = new List<Coords>() { };
            for (int obstcl = 1; obstcl < (creatureList.Count - 1); obstcl++)
            {
                obstacles.Add(creatureList[obstcl].XYPos);
            }
            
           
            PossiblePaths(
                creatureList[0].XYPos, 
                obstacles, "NE", 
                new List<Coords>() { creatureList[0].XYPos }, 
                new List<Coords>() { }, 
                creatureList[0].Movement, 
                new List<List<Coords>>() { }, 
                new List<String>() {"NE"});


}

} 
}

