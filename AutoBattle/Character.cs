using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static AutoBattle.Types;

namespace AutoBattle
{
    public class Character
    {
        public string Name { get; set; }
        public float Health;
        public float BaseDamage;
        public float DamageMultiplier { get; set; }
        public GridBox CurrentBox;
        public int CharacterIndex;
        public CharacterClass CharacterClass;
        
        public Character Target { get; set; } 
        
        public Character(CharacterClass characterClass, int index)
        {
            CharacterClass = characterClass;
            Health = 100;
            BaseDamage = 20;
            CharacterIndex = index;
        }

        public bool TakeDamage(float amount)
        {
            if((Health -= BaseDamage) <= 0)
            {
                Die();
                return true;
            }
            return false;
        }

        public void Die()
        {
            //TODO >> maybe kill him?
        }

        public void WalkTO(bool CanWalk)
        {

        }

        public void StartTurn(Grid battlefield)
        {

            if (CheckCloseTargets(battlefield)) 
            {
                Attack(Target);  

                return;
            }

            // if there is no target close enough, calculates in wich direction this character should move to be closer to a possible target
            if (this.CurrentBox.xIndex > Target.CurrentBox.xIndex)
            {
                if ((battlefield.grids.Exists(x => x.Index == CurrentBox.Index - 1)))
                {
                    CurrentBox.ocupied = false;
                    battlefield.grids[CurrentBox.Index] = CurrentBox;
                    CurrentBox = (battlefield.grids.Find(x => x.Index == CurrentBox.Index - 1));
                    CurrentBox.ocupied = true;
                    battlefield.grids[CurrentBox.Index] = CurrentBox;
                    Console.WriteLine($"Player {CharacterIndex} walked left\n");
                    battlefield.DrawBattlefield();

                    return;
                }
            }
            else if (CurrentBox.xIndex < Target.CurrentBox.xIndex)
            {
                CurrentBox.ocupied = false;
                battlefield.grids[CurrentBox.Index] = CurrentBox;
                CurrentBox = (battlefield.grids.Find(x => x.Index == CurrentBox.Index + 1));
                CurrentBox.ocupied = true;
                battlefield.grids[CurrentBox.Index] = CurrentBox;
                Console.WriteLine($"Player {CharacterIndex} walked right\n");
                battlefield.DrawBattlefield();
                return;
            }

            if (this.CurrentBox.yIndex > Target.CurrentBox.yIndex)
            {
                this.CurrentBox.ocupied = false;
                battlefield.grids[CurrentBox.Index] = CurrentBox;
                this.CurrentBox = (battlefield.grids.Find(x => x.Index == CurrentBox.Index - battlefield.xLenght));
                this.CurrentBox.ocupied = true;
                battlefield.grids[CurrentBox.Index] = CurrentBox;
                Console.WriteLine($"Player {CharacterIndex} walked up\n");
                battlefield.DrawBattlefield();
                return;
            }
            else if (this.CurrentBox.yIndex < Target.CurrentBox.yIndex)
            {
                this.CurrentBox.ocupied = true;
                battlefield.grids[CurrentBox.Index] = this.CurrentBox;
                this.CurrentBox = (battlefield.grids.Find(x => x.Index == CurrentBox.Index + battlefield.xLenght));
                this.CurrentBox.ocupied = false;
                battlefield.grids[CurrentBox.Index] = CurrentBox;
                Console.WriteLine($"Player {CharacterIndex} walked down\n");
                battlefield.DrawBattlefield();
                return;
            }
        }

        private GridBox GetNeighbourClosetToTarget(Grid battlefield)
        {
            var neighbours = GetNeighborhood(battlefield);

            var neighboursDistance = new List<NeighbourDistance>();

            foreach (var neighbour in neighbours)
            {
                neighboursDistance.Add(GetNeighbourDistance(battlefield, neighbour));
            }

            neighboursDistance.Sort((NeighbourDistance n1, NeighbourDistance n2) => n1.distanceSquared.CompareTo(n2.distanceSquared));

            return neighboursDistance[0].Position;
        }

        private NeighbourDistance GetNeighbourDistance(Grid battlefield, GridBox positiontoBeEvaluated)
        {
            int targetLinePosition = Target.CurrentBox.Index / battlefield.yLength;
            int targetColPosition = Target.CurrentBox.Index % battlefield.yLength;

            int evaluatedLinePosition = positiontoBeEvaluated.Index / battlefield.yLength;
            int evaluatedColPosition = positiontoBeEvaluated.Index % battlefield.yLength;

            int absDistanceOnLine = Math.Abs(targetLinePosition - evaluatedLinePosition);
            int absDistanceOnCol = Math.Abs(targetColPosition - evaluatedColPosition);

            double distanceSquared = Math.Pow(absDistanceOnLine, 2) + Math.Pow(absDistanceOnCol, 2);

            return new NeighbourDistance(positiontoBeEvaluated, distanceSquared);
        }

        private struct NeighbourDistance
        {
            public GridBox Position;
            public double distanceSquared;

            public NeighbourDistance(GridBox position, double distanceSquared)
            {
                Position = position;
                this.distanceSquared = distanceSquared;
            }
        }

        //method to get valid neighbourhood of character
        private List<GridBox> GetNeighborhood(Grid battlefield)
        {
            List<GridBox> neighbors = new List<GridBox>();

            int sizeInY = battlefield.yLength;

            bool isOnLeftBorder = CurrentBox.Index % sizeInY == 0;
            bool isOnRightBorder = CurrentBox.Index % sizeInY == sizeInY - 1;
            bool isOnUpBorder = CurrentBox.Index < sizeInY;
            bool isOnDownBorder = CurrentBox.Index >= battlefield.grids.Count - sizeInY;

            //Adding 
            // [ ] [X] [ ]
            // [ ]  C  [ ]
            // [ ] [ ] [ ]
            if (!isOnUpBorder)
            {
               neighbors.Add(battlefield.grids.Find(x => x.Index == CurrentBox.Index - battlefield.xLenght));
            }

            //Adding 
            // [ ] [ ] [ ]
            // [ ]  C  [ ]
            // [ ] [X] [ ]
            if (!isOnDownBorder)
            {
                neighbors.Add(battlefield.grids.Find(x => x.Index == CurrentBox.Index + battlefield.xLenght));
            }

            //Adding 
            // [ ] [ ] [ ]
            // [X]  C  [ ]
            // [ ] [ ] [ ]
            if (!isOnLeftBorder)
            {
                neighbors.Add(battlefield.grids.Find(x => x.Index == CurrentBox.Index - 1));
            }

            //Adding 
            // [ ] [ ] [ ]
            // [ ]  C  [X]
            // [ ] [ ] [ ]
            if (!isOnRightBorder)
            {
                neighbors.Add(battlefield.grids.Find(x => x.Index == CurrentBox.Index + 1));
            }

            //Adding 
            // [X] [ ] [ ]
            // [ ]  C  [ ]
            // [ ] [ ] [ ]
            if (!isOnUpBorder && !isOnLeftBorder)
            {
                neighbors.Add(battlefield.grids.Find(x => x.Index == CurrentBox.Index - battlefield.xLenght - 1));
            }

            //Adding 
            // [ ] [ ] [X]
            // [ ]  C  [ ]
            // [ ] [ ] [ ]
            if (!isOnUpBorder && !isOnRightBorder)
            {
                neighbors.Add(battlefield.grids.Find(x => x.Index == CurrentBox.Index - battlefield.xLenght + 1));
            }

            //Adding 
            // [ ] [ ] [ ]
            // [ ]  C  [ ]
            // [X] [ ] [ ]
            if (!isOnDownBorder && !isOnLeftBorder)
            {
                neighbors.Add(battlefield.grids.Find(x => x.Index == CurrentBox.Index + battlefield.xLenght - 1));
            }

            //Adding 
            // [ ] [ ] [ ]
            // [ ]  C  [ ]
            // [ ] [ ] [X]
            if (!isOnDownBorder && !isOnRightBorder)
            {
                neighbors.Add(battlefield.grids.Find(x => x.Index == CurrentBox.Index + battlefield.xLenght + 1));
            }

            return neighbors;
        }

        // Check in x and y directions if there is any character close enough to be a target.
        bool CheckCloseTargets(Grid battlefield)
        {
            var neighbours = GetNeighborhood(battlefield);

            foreach (var neighbour in neighbours)
            {
                if (neighbour.ocupied)
                {
                    return true;
                }
            }

            return false;
        }

        public void Attack (Character target)
        {
            var rand = new Random();
            target.TakeDamage(rand.Next(0, (int)BaseDamage));
            Console.WriteLine($"Player {CharacterIndex} is attacking the player {Target.CharacterIndex} and did {BaseDamage} damage\n");
        }
    }
}
