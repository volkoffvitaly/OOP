using OOP.Interfaces.Food_Chain;
using System.Drawing;

namespace OOP.Classes
{
    class Rabbit : Herbivorous<Rabbit, IFoodForRabbit>,
        IFoodForFox, IFoodForLynx, IFoodForWolf, IFoodForBoar, IFoodForBear, IFoodForHuman
    {
        public Rabbit()
        {
            Color = Color.Gold;
        }
    }
}
