using static AutoBattle.Types;

namespace AutoBattle
{
    public class Warrior : Character
    {
        public Warrior(CharacterClass characterClass, int index, string name) : base(characterClass, index, name)
        {
            BaseDamage += 20;
            DamageMultiplier = 1.1f;
            Health += 10;
        }
    }
}
