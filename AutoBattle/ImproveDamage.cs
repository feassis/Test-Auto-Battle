using static AutoBattle.Types;
using System;

namespace AutoBattle
{
    public class ImproveDamage : CharacterSkill
    {
        private readonly int damageImprovement;

        public ImproveDamage(int damageImprovement)
        {
            this.damageImprovement = damageImprovement;
        }

        public override void ExecuteSkill(Character self, Character target, Grid battlefield)
        {
            self.BaseDamage += damageImprovement;
            Console.WriteLine($"Character {self.Name} improve its damage on {damageImprovement}");
        }
    }
}
