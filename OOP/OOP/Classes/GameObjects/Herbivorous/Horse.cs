using OOP.Interfaces.Food_Chain;
using OOP.Enums;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace OOP.Classes
{
    class Horse : Herbivorous<Horse, IFoodForHorse>,
        IFoodForHuman, IFoodForWolf, IFoodForBear, IFoodForLynx
    {
        
        public Horse()
        {
            Color = Color.MediumVioletRed;
        }
    }
}
