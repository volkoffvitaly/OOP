using OOP.Interfaces.Food_Chain;
using System.Drawing;

namespace OOP.Classes
{
    class Bear : Omnivorous<Bear, IFoodForBear>,
        IFoodForHuman
    {
        public Bear()
        {
            Color = Color.Brown;
        }
    }
}
