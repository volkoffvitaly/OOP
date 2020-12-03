using OOP.Interfaces.Food_Chain;
using System.Drawing;

namespace OOP.Classes
{
    class Badger : Omnivorous<Badger, IFoodForBadger>,
        IFoodForFox, IFoodForLynx, IFoodForWolf, IFoodForBoar, IFoodForBear, IFoodForHuman
    {
        public Badger()
        {
            Color = Color.Pink;
        }
    }
}
