using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static AutoBattle.Types;

namespace AutoBattle
{
    public class Grid
    {
        public List<GridBox> Grids = new List<GridBox>();
        public int XLenght;
        public int YLength;
        public Grid(int Lines, int Columns)
        {
            XLenght = Lines;
            YLength = Columns;
            Console.WriteLine("The battle field has been created\n");
            for (int i = 0; i < Lines; i++)
            {
                for(int j = 0; j < Columns; j++)
                {
                    GridBox newBox = new GridBox(j, i, false, (Columns * i + j));
                    Grids.Add(newBox);
                    Console.Write($"{newBox.Index}\n");
                }
            }
        }

        // prints the matrix that indicates the tiles of the battlefield
        public void DrawBattlefield()
        {
            for (int i = 0; i < XLenght; i++)
            {
                for (int j = 0; j < YLength; j++)
                {
                    GridBox currentgrid = Grids[i * XLenght + j];
                    if (currentgrid.Ocupied)
                    {
                        Console.Write($"[{currentgrid.CharacterIndex}]\t");
                    }
                    else
                    {
                        Console.Write($"[ ]\t");
                    }
                }

                Console.Write(Environment.NewLine + Environment.NewLine);
            }

            Console.Write(Environment.NewLine + Environment.NewLine);
        }

    }
}
