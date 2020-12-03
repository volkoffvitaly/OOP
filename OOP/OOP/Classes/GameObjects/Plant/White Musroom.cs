using OOP.Interfaces.Food_Chain;
using System.Drawing;

namespace OOP.Classes
{
    class WhiteMushroom : Plant,
        IFoodForHuman, IFoodForBear, IFoodForBoar, IFoodForDeer, IFoodForHorse
    {
        public WhiteMushroom()
        {
            Color = Color.White;
        }
    }
}
