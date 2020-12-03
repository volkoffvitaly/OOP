using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using OOP.Enums;
using OOP.Interfaces.Food_Chain;

namespace OOP.Classes
{
    abstract class Unit : SimulationObject
    {
        public readonly Gender Gender;
        public int Satiety;
        public int ReproduceAbilityValue;

        public const int RADIUS_OF_SEARCH = 50;

        public const int INIT_SATIETY_VALUE = 150;
        public const int SATIETY_PER_STEP = 1;
        public const int SATIETY_TRESHOLD = 100;

        public const int INIT_REPRODUCE_ABILITY_VALUE = 0;
        public const int REPRODUCE_ABILITY_PER_STEP = 1;
        public const int REPRODUCE_ABILITY_TRESHOLD = 25;

        public Unit()
        {
            Gender = (Gender)Map.Random.Next(2);
            Satiety = INIT_SATIETY_VALUE;
            ReproduceAbilityValue = INIT_REPRODUCE_ABILITY_VALUE;
        }

        protected abstract void reproduce();
        protected abstract void eat();
        public abstract Point Step();
    }



    abstract class Unit<TPartner, TFood> : Unit
        where TPartner : Unit, new()
        where TFood : IFood
    {
        public override Point Step()
        {
            var newPosition = new Point();

            // Умер от голода
            if (SATIETY_PER_STEP >= Satiety)
            {
                newPosition = die(this);
            }
            // Готов размножаться
            else if (Satiety > SATIETY_TRESHOLD && ReproduceAbilityValue >= REPRODUCE_ABILITY_TRESHOLD)
            {
                Point target = searchPartner();
                newPosition = moveToTarget(target);

                // Если юнит встал на клетку с партнером
                if (target == newPosition)
                {
                    reproduce();
                }
            }
            // Голоден
            else if (Satiety < SATIETY_TRESHOLD)
            {
                Point target = searchFood();
                newPosition = moveToTarget(target);

                // Если юнит встал на клетку с пищей
                if (target == newPosition) {
                    eat();
                }
            }
            // Нет цели
            else
            {
                newPosition = randomMove();
            }


            updateStats();
            return newPosition;
        }


        //
        // Changing stats and status
        private void updateStats()
        {
            Satiety -= SATIETY_PER_STEP;
            ReproduceAbilityValue += REPRODUCE_ABILITY_PER_STEP;
        }

        private Point die(SimulationObject obj)
        {
            Map.Area[Coordinates.Y * Map.Width + Coordinates.X].Objects.Remove(obj);
            Map.SimulationObjects.Remove(obj);
            obj.IsAlive = false;

            return Coordinates;
        }
        // Changing stats and status
        //



        //
        // Search
        private Point searchFood()
        {
            return searchTarget<TFood>();
        }

        private Point searchPartner()
        {
            return searchTarget<TPartner>();
        }

        private Point searchTarget<TTarget>()
        {
            Point nearestTarget = new Point(-1, -1);

            int stepsMin = RADIUS_OF_SEARCH;
            int stepsXmin = 0, stepsYmin = 0;

            foreach (var gameObject in Map.SimulationObjects.Where(gameObject => gameObject is TTarget))
            {
                // Если ищем партнера, то особь такого же вида,
                // противоположный пол, сытость и готовность к размножению - must have
                if (Satiety > SATIETY_TRESHOLD)
                {
                    if (gameObject is Unit unit)
                    {
                        if (unit.Gender == this.Gender || unit.GetType() != this.GetType() ||
                            unit.ReproduceAbilityValue <= REPRODUCE_ABILITY_TRESHOLD ||
                            unit.Satiety <= SATIETY_TRESHOLD)
                        {
                            continue;
                        }
                    }
                }
                //

                int stepsX = Math.Abs(Coordinates.X - gameObject.Coordinates.X);
                int stepsY = Math.Abs(Coordinates.Y - gameObject.Coordinates.Y);

                if (stepsX + stepsY < stepsMin)
                {
                    stepsMin = stepsX + stepsY;
                    stepsXmin = stepsX;
                    stepsYmin = stepsY;

                    nearestTarget = gameObject.Coordinates;
                }
            }

            return nearestTarget;
        }
        // Search
        //



        // 
        // Eating
        protected override void eat()
        {
            List<SimulationObject> foods = Map.Area[Coordinates.Y * Map.Width + Coordinates.X].Objects.Where(food => food is TFood).ToList();

            if (foods.Count != 0)
            {
                die(foods[Map.Random.Next(foods.Count)]);
                Satiety = INIT_SATIETY_VALUE;
            }
        }

        protected override void reproduce()
        {
            if (Gender == 0)  // Male, cause we must spawn "child" unit only once for couple
            {
                List<SimulationObject> partners = Map.Area[Coordinates.Y * Map.Width + Coordinates.X].Objects.Where(partner => partner is TPartner).ToList();

                if (partners.Count != 0)
                {
                    foreach (var p in partners)
                    {
                        if (Gender != ((Unit)p).Gender &&
                            ((Unit)p).Satiety > SATIETY_TRESHOLD &&
                            ((Unit)p).ReproduceAbilityValue > REPRODUCE_ABILITY_TRESHOLD)
                        {
                            var child = new TPartner();
                            child.Coordinates = Coordinates;

                            Map.SimulationObjects.Add(child);
                            Map.Area[Coordinates.Y * Map.Width + Coordinates.X].Objects.Add(child);

                            ReproduceAbilityValue = 0;
                            ((Unit)p).ReproduceAbilityValue = 0;
                            break;
                        }
                    }
                }
            }
        }
        // Eating
        //



        // 
        // Moving
        private Point moveToTarget(Point target)
        {
            // TODO:
            Point newPosition = Coordinates;

            // Цель не найдена (отсутствует на карте/находится вне радиуса поиска)
            if (target.X == -1 && target.Y == -1)
                newPosition = randomMove();
            // Иначе движение до цели
            else if (target.X != Coordinates.X || target.Y != Coordinates.Y)
                newPosition = directMove(target);

            return newPosition;
        }

        private Point randomMove()  // Or skip move
        {
            var axis = (Direction)Random.Next(2);

            switch (axis)
            {
                case Direction.Horizontal:
                    Coordinates.X = Math.Max(Math.Min(Coordinates.X + Map.Random.Next(3) - 1, Map.Height - 1), 0);   // Случайный сдвиг по X на 1 клетку
                    break;
                case Direction.Vertical:
                    Coordinates.Y = Math.Max(Math.Min(Coordinates.Y + Map.Random.Next(3) - 1, Map.Height - 1), 0);   // Случайный сдвиг по Y на 1 клетку
                    break;
            }


            Map.Area[Coordinates.Y * Map.Width + Coordinates.X].Objects.Add(this);
            return Coordinates;
        }

        private Point directMove(Point target)
        {
            var axis = (Direction)Random.Next(2);

            int stepsX = Math.Abs(target.X - Coordinates.X);
            int stepsY = Math.Abs(target.Y - Coordinates.Y);


            if (stepsX == 0)
                axis = Direction.Vertical;
            else if (stepsY == 0)
                axis = Direction.Horizontal;


            switch (axis)
            {
                case Direction.Horizontal:
                    if (Coordinates.X < target.X) 
                        Coordinates.X += 1;
                    else 
                        Coordinates.X -= 1;
                    break;
                case Direction.Vertical:
                    if (Coordinates.Y < target.Y) 
                        Coordinates.Y += 1;
                    else 
                        Coordinates.Y -= 1;
                    break;
            }

            Map.Area[Coordinates.Y * Map.Width + Coordinates.X].Objects.Add(this);
            return Coordinates;
        }
        // Moving
        //
    }
}
