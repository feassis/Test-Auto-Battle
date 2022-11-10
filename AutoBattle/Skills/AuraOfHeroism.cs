using static AutoBattle.Types;
using System.Collections.Generic;

namespace AutoBattle
{
    public class AuraOfHeroism : CharacterSkillList
    {
        public AuraOfHeroism() : base("Aura Of Heroism")
        {
            skills = new List<CharacterSkill>
            {
                new HealSelf(3),
                new ImproveDamage(1)
            };
        }
    }
}
