using System;
using System.Collections.Generic;
using static AutoBattle.Types;

namespace AutoBattle
{
    public class PoisonCoatedArrows : CharacterSkillList
    {
        public PoisonCoatedArrows() : base("Poison Coated Arrows")
        {
            skills = new List<CharacterSkill>
            {
                new DamageEnemy(5),
                new ImproveDamage(1)
            };
        }
    }
}
