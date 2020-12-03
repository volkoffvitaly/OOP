using OOP.Interfaces.Food_Chain;
using System.Drawing;

namespace OOP.Classes
{
    class Boar : Omnivorous<Boar, IFoodForBoar>,
        IFoodForHuman, IFoodForWolf, IFoodForBear
    {
        public Boar()
        {
            Color = Color.Violet;
        }
    }
}
