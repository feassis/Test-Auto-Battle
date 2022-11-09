using static AutoBattle.Types;
using System;
using System.Collections.Generic;

namespace AutoBattle
{
    public abstract class CharacterSkillList : CharacterSkill
    {
        protected List<CharacterSkill> skills;

        public CharacterSkillList(string skillName)
        {
            SkillName = skillName;
        }

        public override void ExecuteSkill(Character self, Character target, Grid battlefield)
        {
            Console.WriteLine($"Character{self.Name} is using {SkillName}");

            foreach (var skill in skills)
            {
                skill.ExecuteSkill(self, target, battlefield);
            }
        }
    }
}
