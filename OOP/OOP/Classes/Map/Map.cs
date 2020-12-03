using System;
using System.Collections.Generic;
using System.Drawing;

namespace OOP.Classes
{
    static class Map
    {
		static public Random Random = new Random();
		static public int Width = 1000;
		static public int Height = 1000;
		static public int Multiplier = 5;
        static public List<SimulationObject> SimulationObjects;
        static public List<Square> Area;
		static public Color Color = Color.LimeGreen;
	}
}
