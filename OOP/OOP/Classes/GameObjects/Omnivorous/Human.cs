using OOP.Interfaces.Food_Chain;
using System.Drawing;

namespace OOP.Classes
{
    class Human : Omnivorous<Human, IFoodForHuman>,
        IFoodForBear, IFoodForWolf, IFoodForBoar
    {
        public Human()
        {
            Color = Color.Blue;
        }
    }
}
