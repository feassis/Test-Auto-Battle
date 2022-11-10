using static AutoBattle.Types;

namespace AutoBattle
{
    public class Cleric : Character
    {
        public Cleric(CharacterClass characterClass, int index, string name) : base(characterClass, index, name)
        {
            BaseDamage += 3;
            DamageMultiplier = 1.0f;
            Health += 20;

            skill = new DivineBlessing();
        }
    }
}
