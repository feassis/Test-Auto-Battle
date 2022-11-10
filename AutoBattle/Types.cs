using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle
{
    public class Types
    {
        public class GridBox
        {
            public int XIndex;
            public int YIndex;
            public bool Ocupied;
            public int Index;
            public int CharacterIndex = -1;

            public GridBox(int x, int y, bool ocupied, int index)
            {
                XIndex = x;
                YIndex = y;
                Ocupied = ocupied;
                Index = index;
            }
        }

        public abstract class CharacterSkill
        {
            public string SkillName;

            public abstract void ExecuteSkill(Character self, Character target, Grid battlefield);
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
