using System;
using OOP.Interfaces.Food_Chain;

namespace OOP.Classes
{
    abstract class Carnivorous<TPartner, TFood> : Unit<TPartner, TFood>
        where TPartner : Unit, new()
        where TFood : IFoodForCarnivorous
    {

    }
}
