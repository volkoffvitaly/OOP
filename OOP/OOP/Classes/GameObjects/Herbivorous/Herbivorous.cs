using System.Drawing;
using OOP.Interfaces.Food_Chain;

namespace OOP.Classes
{
    abstract class Herbivorous<TPartner, TFood> : Unit<TPartner, TFood>
        where TPartner : Unit, new()
        where TFood : IFoodForHerbivorous
    {

    }
}
