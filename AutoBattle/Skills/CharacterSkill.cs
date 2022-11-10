namespace AutoBattle
{
    public partial class Types
    {
        public abstract class CharacterSkill
        {
            public string SkillName;

            public abstract void ExecuteSkill(Character self, Character target, Grid battlefield);
        }
    }
}
