using static AutoBattle.Types;

namespace AutoBattle
{
    public class Archer : Character
    {
        public Archer(CharacterClass characterClass, int index, string name) : base(characterClass, index, name)
        {
            BaseDamage += 15;
            DamageMultiplier = 1.3f;
            Health -= 10;

            Skill = new PoisonCoatedArrows();
        }
    }
}
