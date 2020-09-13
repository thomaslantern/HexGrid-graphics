using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexGrid
{
    class Coordinates
    {
        public class Coords
        {
            public Coords()
            {
                currentPos = (0, 0);
            }
            public Coords(int posx, int posy)
            {
                currentPos = (posx, posy);
                x = posx;
                y = posy;
            }
            public int getX()
            {
                return x;
            }
            public int getY()
            {
                return y;
            }
            private int x { get; set; }
            private int y { get; set; }
            public (int, int) currentPos { get; set; }


        }
    }
}
