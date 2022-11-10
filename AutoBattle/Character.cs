using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static AutoBattle.Types;

namespace AutoBattle
{
    public abstract class Character
    {
        public string Name { get; set; }
        public float Health;
        public float BaseDamage;
        public float DamageMultiplier { get; set; }
        public GridBox CurrentBox;
        public int CharacterIndex;
        public CharacterClass CharacterClass;
        
        protected CharacterSkill skill;
        
        public Character Target { get; set; }

        private bool isDead;
        
        public Character(CharacterClass characterClass, int index, string name)
        {
            CharacterClass = characterClass;
            Health = 100;
            BaseDamage = 20;
            CharacterIndex = index;
            Name = name;
        }

        public bool TakeDamage(float amount)
        {
            Health = Math.Clamp(Health - amount, 0, int.MaxValue);
            if(Health <= 0)
            {
                Die();
                return true;
            }
            return false;
        }

        public void Die()
        {
            isDead = true;
            Console.WriteLine($"Character {Name} has died");
        }

        public void StartTurn(Grid battlefield)
        {
            if (isDead)
            {
                return;
            }

            skill.ExecuteSkill(this, Target, battlefield);

            if (CheckCloseTargets(battlefield)) 
            {
                Attack(Target);  

                return;
            }

            // if there is no target close enough, calculates in wich direction this character should move to be closer to a possible target
            MoveCharacterCloserToTarget(battlefield);
        }

        private void MoveCharacterCloserToTarget(Grid battlefield)
        {
            var goToPosition = GetNeighbourClosetToTarget(battlefield);

            var gotTopositionPreviousOcupied = goToPosition.Ocupied;
            var gotTopositionPreviousCharacterIndex = goToPosition.CharacterIndex;

            goToPosition.Ocupied = CurrentBox.Ocupied;
            goToPosition.CharacterIndex = CurrentBox.CharacterIndex;

            CurrentBox.Ocupied = gotTopositionPreviousOcupied;
            CurrentBox.CharacterIndex = gotTopositionPreviousCharacterIndex;

            CurrentBox = goToPosition;

            Console.WriteLine($"Player {CharacterIndex} walked to l: {CurrentBox.Index / battlefield.YLength} c: {CurrentBox.Index % battlefield.YLength}\n");
            battlefield.DrawBattlefield();
        }

        //this method returns the closest position of character's target
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

        //this method get the distance of a position to the character's target
        private NeighbourDistance GetNeighbourDistance(Grid battlefield, GridBox positiontoBeEvaluated)
        {
            int targetLinePosition = Target.CurrentBox.Index / battlefield.YLength;
            int targetColPosition = Target.CurrentBox.Index % battlefield.YLength;

            int evaluatedLinePosition = positiontoBeEvaluated.Index / battlefield.YLength;
            int evaluatedColPosition = positiontoBeEvaluated.Index % battlefield.YLength;

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

            int sizeInY = battlefield.YLength;

            bool isOnLeftBorder = CurrentBox.Index % sizeInY == 0;
            bool isOnRightBorder = CurrentBox.Index % sizeInY == sizeInY - 1;
            bool isOnUpBorder = CurrentBox.Index < sizeInY;
            bool isOnDownBorder = CurrentBox.Index >= battlefield.Grids.Count - sizeInY;

            //Adding 
            // [ ] [X] [ ]
            // [ ]  C  [ ]
            // [ ] [ ] [ ]
            if (!isOnUpBorder)
            {
               neighbors.Add(battlefield.Grids.Find(x => x.Index == CurrentBox.Index - battlefield.XLenght));
            }

            //Adding 
            // [ ] [ ] [ ]
            // [ ]  C  [ ]
            // [ ] [X] [ ]
            if (!isOnDownBorder)
            {
                neighbors.Add(battlefield.Grids.Find(x => x.Index == CurrentBox.Index + battlefield.XLenght));
            }

            //Adding 
            // [ ] [ ] [ ]
            // [X]  C  [ ]
            // [ ] [ ] [ ]
            if (!isOnLeftBorder)
            {
                neighbors.Add(battlefield.Grids.Find(x => x.Index == CurrentBox.Index - 1));
            }

            //Adding 
            // [ ] [ ] [ ]
            // [ ]  C  [X]
            // [ ] [ ] [ ]
            if (!isOnRightBorder)
            {
                neighbors.Add(battlefield.Grids.Find(x => x.Index == CurrentBox.Index + 1));
            }

            //Adding 
            // [X] [ ] [ ]
            // [ ]  C  [ ]
            // [ ] [ ] [ ]
            if (!isOnUpBorder && !isOnLeftBorder)
            {
                neighbors.Add(battlefield.Grids.Find(x => x.Index == CurrentBox.Index - battlefield.XLenght - 1));
            }

            //Adding 
            // [ ] [ ] [X]
            // [ ]  C  [ ]
            // [ ] [ ] [ ]
            if (!isOnUpBorder && !isOnRightBorder)
            {
                neighbors.Add(battlefield.Grids.Find(x => x.Index == CurrentBox.Index - battlefield.XLenght + 1));
            }

            //Adding 
            // [ ] [ ] [ ]
            // [ ]  C  [ ]
            // [X] [ ] [ ]
            if (!isOnDownBorder && !isOnLeftBorder)
            {
                neighbors.Add(battlefield.Grids.Find(x => x.Index == CurrentBox.Index + battlefield.XLenght - 1));
            }

            //Adding 
            // [ ] [ ] [ ]
            // [ ]  C  [ ]
            // [ ] [ ] [X]
            if (!isOnDownBorder && !isOnRightBorder)
            {
                neighbors.Add(battlefield.Grids.Find(x => x.Index == CurrentBox.Index + battlefield.XLenght + 1));
            }

            return neighbors;
        }

        // Check in x and y directions if there is any character close enough to be a target.
        public bool CheckCloseTargets(Grid battlefield)
        {
            var neighbours = GetNeighborhood(battlefield);

            foreach (var neighbour in neighbours)
            {
                if (neighbour.Ocupied)
                {
                    return true;
                }
            }

            return false;
        }

        public void Attack (Character target)
        {
            if (isDead)
            {
                return;
            }
            
            var rand = new Random();

            int randomDmg = rand.Next(0, (int)BaseDamage);
            randomDmg = (int)Math.Round(randomDmg * DamageMultiplier);
            target.TakeDamage(randomDmg);
            Console.WriteLine($"Player {CharacterIndex} is attacking the player {Target.CharacterIndex} and did {randomDmg} damage\n");
        }
    }
}
