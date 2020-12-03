using OOP.Interfaces.Food_Chain;
using System.Drawing;

namespace OOP.Classes
{
    class Wolf : Carnivorous<Wolf, IFoodForWolf>,
        IFoodForBear, IFoodForHuman
    {
        public Wolf()
        {
            Color = Color.Gray;
        }
    }
}
