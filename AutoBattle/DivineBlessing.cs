using System;
using System.Collections.Generic;
using static AutoBattle.Types;

namespace AutoBattle
{
    public class DivineBlessing : CharacterSkillList
    {
        public DivineBlessing() : base("Divine Blessing")
        {
            skills = new List<CharacterSkill>
            {
                new HealSelf(10)
            };
        }
    }
}
