using static AutoBattle.Types;
using System;

namespace AutoBattle
{
    public class DamageEnemy : CharacterSkill
    {
        private readonly int damage;

        public DamageEnemy(int damage)
        {
            this.damage = damage;
        }

        public override void ExecuteSkill(Character self, Character target, Grid battlefield)
        {
            if (!self.CheckCloseTargets(battlefield))
            {
                return;
            }

            target.TakeDamage(damage);
            Console.WriteLine($"Character {self.Name} using skill to cause {damage} damage on {target}");
        }
    }
}
