using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using OOP.Enums;

namespace OOP.Classes
{
    static class Life
    {
        static private List<Point> SquaresToRedrawPerTick = new List<Point>();

        //private static bool Drought = false;
        private const int ChanceOfRandomEvent = 1000;
        private const int RADIUS_TO_SPAWN_PLANT = 15;
             

        //
        // Preparing Simulation
        static public List<Point> PrepareSimulation()
        {
            Map.SimulationObjects = new List<SimulationObject>();
            Map.Area = new List<Square>();

            for (int i = 0; i < Map.Width; ++i)
            {
                for (int j = 0; j < Map.Height; ++j)
                {
                    Map.Area.Add(new Square(j, i));
                }
            }

            InitializeObjects();
            return SquaresToRedrawPerTick;
        }



        static private void InitializeObjects()
        {
            InitializeObjects<Carrot>((int)NumOfObjects.CarrotMin, (int)NumOfObjects.CarrotMax);
            InitializeObjects<WhiteMushroom>((int)NumOfObjects.WhiteMushroomMin, (int)NumOfObjects.WhiteMushroomMax);
            InitializeObjects<Raspberry>((int)NumOfObjects.RaspberryMin, (int)NumOfObjects.RaspberryMax);

            InitializeObjects<Horse>((int)NumOfObjects.HorseMin, (int)NumOfObjects.HorseMax);
            InitializeObjects<Rabbit>((int)NumOfObjects.RabbitMin, (int)NumOfObjects.RabbitMax);
            InitializeObjects<Deer>((int)NumOfObjects.DeerMin, (int)NumOfObjects.DeerMax);

            InitializeObjects<Human>((int)NumOfObjects.HumanMin, (int)NumOfObjects.HumanMax);
            InitializeObjects<Bear>((int)NumOfObjects.BearMin, (int)NumOfObjects.BearMax);
            InitializeObjects<Badger>((int)NumOfObjects.BadgerMin, (int)NumOfObjects.BadgerMax);
            InitializeObjects<Boar>((int)NumOfObjects.BoarMin, (int)NumOfObjects.BoarMax);

            InitializeObjects<Fox>((int)NumOfObjects.FoxMin, (int)NumOfObjects.FoxMax);
            InitializeObjects<Wolf>((int)NumOfObjects.WolfMin, (int)NumOfObjects.WolfMax);
            InitializeObjects<Lynx>((int)NumOfObjects.LynxMin, (int)NumOfObjects.LynxMax);
        }

        static private void InitializeObjects<TType>(int minInitNumOfObjects, int maxInitNumOfObjects)
            where TType : SimulationObject, new()
        {
            int initNumOfObjects = Map.Random.Next(minInitNumOfObjects, maxInitNumOfObjects);

            for (int i = 0; i < initNumOfObjects; ++i)
            {
                var newObject = new TType();

                Map.SimulationObjects.Add(newObject);
                Map.Area[newObject.Coordinates.Y * Map.Width + newObject.Coordinates.X].Objects.Add(newObject);

                SquaresToRedrawPerTick.Add(newObject.Coordinates);
            }
        }
        // Preparing Simulation
        //



        //
        // Updating Simulation
        static public List<Point> UpdateSimulation()
        {
            SquaresToRedrawPerTick = new List<Point>();
            AddPlants();
            MoveUnits();
            //RandomEvent();
            return SquaresToRedrawPerTick;
        }



        static private void AddPlants()
        {
            AddPlants<Carrot>();
            AddPlants<WhiteMushroom>();
            AddPlants<Raspberry>();
        }

        static private void AddPlants<TType>()
            where TType : Plant, new()
        {
            Point randomPoint = new Point();
            int initNumOfObjects = Map.Random.Next((int)NumOfObjects.PlantsPerTickMin, (int)NumOfObjects.PlantsPerTickMax);

            List<SimulationObject> plants = Map.SimulationObjects.Where(gameObject => gameObject is TType).ToList();

            for (int i = 0; i < initNumOfObjects; ++i)
            {
                
                Plant randomPlant = (Plant)plants[Map.Random.Next(plants.Count)];

                randomPoint.X = Math.Max(Math.Min(randomPlant.Coordinates.X + Map.Random.Next(RADIUS_TO_SPAWN_PLANT * 2 + 1) - RADIUS_TO_SPAWN_PLANT, Map.Height - 1), 0);
                randomPoint.Y = Math.Max(Math.Min(randomPlant.Coordinates.Y + Map.Random.Next(RADIUS_TO_SPAWN_PLANT * 2 + 1) - RADIUS_TO_SPAWN_PLANT, Map.Height - 1), 0);

                //
                var newObject = new TType();
                newObject.Coordinates = randomPoint;
                // В идеале тут должен отрабатывать параметризованный конструктор
                // с аргументом {randomPoint}, но пока что это слишком сложно

                Map.SimulationObjects.Add(newObject);
                Map.Area[newObject.Coordinates.Y * Map.Width + newObject.Coordinates.X].Objects.Add(newObject);

                SquaresToRedrawPerTick.Add(newObject.Coordinates);
            }
        }

        static private void MoveUnits()
        {
            List<SimulationObject> units = Map.SimulationObjects.Where(unit => unit is Unit).ToList();

            for (int i = units.Count - 1; i >= 0; --i)
            {
                SquaresToRedrawPerTick.Add(units[i].Coordinates);
                Map.Area[units[i].Coordinates.Y * Map.Width + units[i].Coordinates.X].Objects.Remove(units[i]);

                if (units[i].IsAlive)
                {
                    SquaresToRedrawPerTick.Add(((Unit)units[i]).Step());
                }
            }
        }


        //static private void RandomEvent()
        //{
        //    if (Map.Random.Next(ChanceOfRandomEvent) == 1 && Drought == false) {
        //        MinTickNumOfPlants = 0;
        //        MaxTickNumOfPlants = 0;
        //        Drought = true;
        //    }

        //    else if (Map.Random.Next(ChanceOfRandomEvent) / 50 == 1 && Drought == true)
        //    {
        //        MinTickNumOfPlants = 15;
        //        MaxTickNumOfPlants = 20;
        //        Drought = false;
        //    }
        //}



        // Updating Simulation
        //
    }
}
