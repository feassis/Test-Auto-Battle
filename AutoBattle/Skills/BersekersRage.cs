using System;
using System.Collections.Generic;
using static AutoBattle.Types;

namespace AutoBattle
{
    public class BersekersRage : CharacterSkillList
    {
        public BersekersRage() : base("Berserker's Rage")
        {
            skills = new List<CharacterSkill>
            {
                new ImproveDamage(3)
            };
        }
    }
}
