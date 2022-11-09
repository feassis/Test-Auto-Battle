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
        public GridBox currentBox;
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
            else
            {   // if there is no target close enough, calculates in wich direction this character should move to be closer to a possible target
                if(this.currentBox.xIndex > Target.currentBox.xIndex)
                {
                    if ((battlefield.grids.Exists(x => x.Index == currentBox.Index - 1)))
                    {
                        currentBox.ocupied = false;
                        battlefield.grids[currentBox.Index] = currentBox;
                        currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index - 1));
                        currentBox.ocupied = true;
                        battlefield.grids[currentBox.Index] = currentBox;
                        Console.WriteLine($"Player {CharacterIndex} walked left\n");
                        battlefield.DrawBattlefield();

                        return;
                    }
                } else if(currentBox.xIndex < Target.currentBox.xIndex)
                {
                    currentBox.ocupied = false;
                    battlefield.grids[currentBox.Index] = currentBox;
                    currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index + 1));
                    currentBox.ocupied = true;
                    battlefield.grids[currentBox.Index] = currentBox;
                    Console.WriteLine($"Player {CharacterIndex} walked right\n");
                    battlefield.DrawBattlefield();
                    return;
                }

                if (this.currentBox.yIndex > Target.currentBox.yIndex)
                {
                    this.currentBox.ocupied = false;
                    battlefield.grids[currentBox.Index] = currentBox;
                    this.currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index - battlefield.xLenght));
                    this.currentBox.ocupied = true;
                    battlefield.grids[currentBox.Index] = currentBox;
                    Console.WriteLine($"Player {CharacterIndex} walked up\n");
                    battlefield.DrawBattlefield();
                    return;
                }
                else if(this.currentBox.yIndex < Target.currentBox.yIndex)
                {
                    this.currentBox.ocupied = true;
                    battlefield.grids[currentBox.Index] = this.currentBox;
                    this.currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index + battlefield.xLenght));
                    this.currentBox.ocupied = false;
                    battlefield.grids[currentBox.Index] = currentBox;
                    Console.WriteLine($"Player {CharacterIndex} walked down\n");
                    battlefield.DrawBattlefield();
                    return;
                }
            }
        }

        //method to get valid neighbourhood of character
        private List<GridBox> GetNeighborhood(Grid battlefield)
        {
            List<GridBox> neighbors = new List<GridBox>();

            int sizeInY = battlefield.yLength;

            bool isOnLeftBorder = currentBox.Index % sizeInY == 0;
            bool isOnRightBorder = currentBox.Index % sizeInY == sizeInY - 1;
            bool isOnUpBorder = currentBox.Index < sizeInY;
            bool isOnDownBorder = currentBox.Index >= battlefield.grids.Count - sizeInY;

            //Adding 
            // [ ] [X] [ ]
            // [ ]  C  [ ]
            // [ ] [ ] [ ]
            if (!isOnUpBorder)
            {
               neighbors.Add(battlefield.grids.Find(x => x.Index == currentBox.Index - battlefield.xLenght));
            }

            //Adding 
            // [ ] [ ] [ ]
            // [ ]  C  [ ]
            // [ ] [X] [ ]
            if (!isOnDownBorder)
            {
                neighbors.Add(battlefield.grids.Find(x => x.Index == currentBox.Index + battlefield.xLenght));
            }

            //Adding 
            // [ ] [ ] [ ]
            // [X]  C  [ ]
            // [ ] [ ] [ ]
            if (!isOnLeftBorder)
            {
                neighbors.Add(battlefield.grids.Find(x => x.Index == currentBox.Index - 1));
            }

            //Adding 
            // [ ] [ ] [ ]
            // [ ]  C  [X]
            // [ ] [ ] [ ]
            if (!isOnRightBorder)
            {
                neighbors.Add(battlefield.grids.Find(x => x.Index == currentBox.Index + 1));
            }

            //Adding 
            // [X] [ ] [ ]
            // [ ]  C  [ ]
            // [ ] [ ] [ ]
            if (!isOnUpBorder && !isOnLeftBorder)
            {
                neighbors.Add(battlefield.grids.Find(x => x.Index == currentBox.Index - battlefield.xLenght - 1));
            }

            //Adding 
            // [ ] [ ] [X]
            // [ ]  C  [ ]
            // [ ] [ ] [ ]
            if (!isOnUpBorder && !isOnRightBorder)
            {
                neighbors.Add(battlefield.grids.Find(x => x.Index == currentBox.Index - battlefield.xLenght + 1));
            }

            //Adding 
            // [ ] [ ] [ ]
            // [ ]  C  [ ]
            // [X] [ ] [ ]
            if (!isOnDownBorder && !isOnLeftBorder)
            {
                neighbors.Add(battlefield.grids.Find(x => x.Index == currentBox.Index + battlefield.xLenght - 1));
            }

            //Adding 
            // [ ] [ ] [ ]
            // [ ]  C  [ ]
            // [ ] [ ] [X]
            if (!isOnDownBorder && !isOnRightBorder)
            {
                neighbors.Add(battlefield.grids.Find(x => x.Index == currentBox.Index + battlefield.xLenght + 1));
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
