using OOP.Interfaces.Food_Chain;
using System.Drawing;

namespace OOP.Classes
{
    class Carrot : Plant,
        IFoodForRabbit, IFoodForHuman
    {
        public Carrot()
        {
            Color = Color.DarkOrange;
        }
    }
}
