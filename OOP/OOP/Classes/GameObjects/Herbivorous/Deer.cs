using OOP.Interfaces.Food_Chain;
using System.Drawing;

namespace OOP.Classes
{
    class Deer : Herbivorous<Deer, IFoodForDeer>,
        IFoodForHuman, IFoodForWolf, IFoodForBear, IFoodForLynx
    {
        public Deer()
        {
            Color = Color.Black;
        }
    }
}
