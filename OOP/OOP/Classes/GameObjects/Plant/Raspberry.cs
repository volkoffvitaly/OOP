using OOP.Interfaces.Food_Chain;
using System.Drawing;

namespace OOP.Classes
{
    class Raspberry : Plant,
        IFoodForDeer, IFoodForHorse, IFoodForHuman, IFoodForBear
    {
        public Raspberry()
        {
            Color = Color.DarkRed;
        }
    }
}
