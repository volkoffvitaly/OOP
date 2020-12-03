using System;
using System.Drawing;
using OOP.Interfaces.Food_Chain;

namespace OOP.Classes
{
    abstract class Omnivorous <TPartner, TFood> : Unit<TPartner, TFood>
        where TPartner : Unit, new()
        where TFood : IFoodForOmnivorous
    {

    }
}
