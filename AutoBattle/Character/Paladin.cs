using static AutoBattle.Types;

namespace AutoBattle
{
    public class Paladin : Character
    {
        public Paladin(CharacterClass characterClass, int index, string name) : base(characterClass, index, name)
        {
            BaseDamage += 5;
            DamageMultiplier = 1.1f;
            Health += 30;

            skill = new AuraOfHeroism();
        }
    }
}
