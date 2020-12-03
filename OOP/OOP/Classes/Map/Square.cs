using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace OOP.Classes
{
    class Square
    {
        public List<SimulationObject> Objects;
        public readonly Point Coordinates;

        public Square(int x, int y)
        {
            Objects = new List<SimulationObject>();
            Coordinates = new Point(x, y);
        }
    }
}
