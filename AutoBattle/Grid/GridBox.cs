namespace AutoBattle
{
    public partial class Types
    {
        public class GridBox
        {
            public int XIndex;
            public int YIndex;
            public bool Ocupied;
            public int Index;
            public int CharacterIndex = -1;

            public GridBox(int x, int y, bool ocupied, int index)
            {
                XIndex = x;
                YIndex = y;
                Ocupied = ocupied;
                Index = index;
            }
        }
    }
}
