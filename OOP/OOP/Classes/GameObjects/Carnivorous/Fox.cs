using OOP.Interfaces.Food_Chain;
using System.Drawing;

namespace OOP.Classes
{
    class Fox : Carnivorous<Fox, IFoodForFox>,
        IFoodForHuman, IFoodForBear, IFoodForLynx, IFoodForWolf, IFoodForBadger
    {
        public Fox()
        {
            Color = Color.Orange;
        }
    }
}
