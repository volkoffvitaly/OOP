using System;
using System.Drawing;
using OOP.Interfaces.Food_Chain;

namespace OOP.Classes
{
    abstract class SimulationObject
    {
        protected static Random Random = new Random();
                
        public Point Coordinates;
        public Color Color;
        public bool IsAlive;

        public SimulationObject()
        {
            Coordinates = randomPoint();
            IsAlive = true;
        }

        protected Point randomPoint()
        {
            return new Point(Random.Next(Map.Height), Random.Next(Map.Width));
        }
    }
}