using static AutoBattle.Types;
using System;

namespace AutoBattle
{
    public class HealSelf : CharacterSkill
    {
        private readonly int healAmount;

        public HealSelf(int healAmount)
        {
            this.healAmount = healAmount;
        }

        public override void ExecuteSkill(Character self, Character target, Grid battlefield)
        {
            self.Health += healAmount;
            Console.WriteLine($"Character {self.Name} healed for {healAmount}");
        }
    }
}
