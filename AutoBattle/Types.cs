using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle
{
    public class Types
    {
        public class GridBox
        {
            public int xIndex;
            public int yIndex;
            public bool ocupied;
            public int Index;
            public int CharacterIndex = -1;

            public GridBox(int x, int y, bool ocupied, int index)
            {
                xIndex = x;
                yIndex = y;
                this.ocupied = ocupied;
                Index = index;
            }
        }

        public struct CharacterSkills
        {
            string Name;
            float damage;
            float damageMultiplier;
        }

        public enum CharacterClass : uint
        {
            Paladin = 1,
            Warrior = 2,
            Cleric = 3,
            Archer = 4
        }
    }
}
