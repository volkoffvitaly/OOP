using OOP.Interfaces.Food_Chain;
using System.Drawing;

namespace OOP.Classes
{
    class Lynx : Carnivorous<Lynx, IFoodForLynx>,
        IFoodForBear, IFoodForWolf, IFoodForHuman
    {
        public Lynx()
        {
            Color = Color.Red;
        }
    }
}
