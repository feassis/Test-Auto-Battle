using System;
using static AutoBattle.Character;
using static AutoBattle.Grid;
using System.Collections.Generic;
using System.Linq;
using static AutoBattle.Types;
using System.Linq;

namespace AutoBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            Grid grid = new Grid(5, 5);
            GridBox PlayerCurrentLocation;
            GridBox EnemyCurrentLocation;
            Character PlayerCharacter = null;
            Character EnemyCharacter = null;
            List<Character> AllPlayers = new List<Character>();
            int currentTurn = 0;
            int numberOfPossibleTiles = grid.grids.Count;
            
            Setup(); 

            void Setup()
            {
                while (!GetPlayerChoice());
                CreateEnemyCharacter();
            }

            StartGame();

            bool GetPlayerChoice()
            {
                //asks for the player to choose between for possible classes via console.
                Console.WriteLine("Choose Between One of this Classes:\n");
                Console.WriteLine("[1] Paladin, [2] Warrior, [3] Cleric, [4] Archer");
                //store the player choice in a variable
                string choice = Console.ReadLine();

                return CreatePlayerCharacter(Int32.Parse(choice));
            }

            bool CreatePlayerCharacter(int classIndex)
            {
                var enumOptions = Enum.GetValues(typeof(CharacterClass)).Cast<CharacterClass>().ToList();
                if (!enumOptions.Contains((CharacterClass)classIndex))
                {
                    Console.WriteLine("Choose a valid option\n");
                    return (false);
                }

                CharacterClass characterClass = (CharacterClass)classIndex;
                Console.WriteLine($"Player Class Choice: {characterClass}");
                PlayerCharacter = new Character(characterClass, 0);

                return true;
            }

            void CreateEnemyCharacter()
            {
                //randomly choose the enemy class and set up vital variables
                var rand = new Random();
                int randomInteger = rand.Next(1, 4);
                CharacterClass enemyClass = (CharacterClass)randomInteger;
                Console.WriteLine($"Enemy Class Choice: {enemyClass}");
                EnemyCharacter = new Character(enemyClass, 1);
            }

            void StartGame()
            {
                //populates the character variables and targets
                EnemyCharacter.Target = PlayerCharacter;
                PlayerCharacter.Target = EnemyCharacter;
                AllPlayers.Add(PlayerCharacter);
                AllPlayers.Add(EnemyCharacter);
                AlocatePlayers();
                StartTurn();

            }

            void StartTurn(){

                if (currentTurn == 0)
                {
                    int random = GetRandomInt(0, int.MaxValue);

                    if(random % 2 == 0)
                    {
                        AllPlayers.Reverse();
                    }
                }

                foreach(Character character in AllPlayers)
                {
                    character.StartTurn(grid);
                }

                currentTurn++;
                HandleTurn();
            }

            void HandleTurn()
            {
                if(PlayerCharacter.Health == 0)
                {
                    return;
                } else if (EnemyCharacter.Health == 0)
                {
                    Console.Write(Environment.NewLine + Environment.NewLine);

                    // endgame?

                    Console.Write(Environment.NewLine + Environment.NewLine);

                    return;
                } else
                {
                    Console.Write(Environment.NewLine + Environment.NewLine);
                    Console.WriteLine("Click on any key to start the next turn...\n");
                    Console.Write(Environment.NewLine + Environment.NewLine);

                    ConsoleKeyInfo key = Console.ReadKey();
                    StartTurn();
                }
            }

            int GetRandomInt(int min, int max)
            {
                var rand = new Random();
                int index = rand.Next(min, max);
                return index;
            }

            void AlocatePlayers()
            {
                AlocatePlayerCharacter();
                AlocateEnemyCharacter();
            }

            void AlocatePlayerCharacter()
            {
                int random = GetRandomNumberPositionOnGrid();
                GridBox RandomLocation = (grid.grids.ElementAt(random));
                Console.Write($"{random}\n");
                if (!RandomLocation.ocupied)
                {
                    PlayerCurrentLocation = RandomLocation;
                    RandomLocation.ocupied = true;
                    RandomLocation.CharacterIndex = PlayerCharacter.CharacterIndex;
                    grid.grids[random] = RandomLocation;
                    PlayerCharacter.currentBox = grid.grids[random];
                } else
                {
                    AlocatePlayerCharacter();
                }
            }

            void AlocateEnemyCharacter()
            {
                int random = GetRandomNumberPositionOnGrid();
                GridBox RandomLocation = (grid.grids.ElementAt(random));
                Console.Write($"{random}\n");
                if (!RandomLocation.ocupied)
                {
                    EnemyCurrentLocation = RandomLocation;
                    RandomLocation.ocupied = true;
                    RandomLocation.CharacterIndex = EnemyCharacter.CharacterIndex;
                    grid.grids[random] = RandomLocation;
                    EnemyCharacter.currentBox = grid.grids[random];
                    grid.DrawBattlefield(5 , 5);
                }
                else
                {
                    AlocateEnemyCharacter();
                }
            }

            int GetRandomNumberPositionOnGrid()
            {
                return GetRandomInt(0, grid.xLenght * grid.yLength);
            }
        }
    }
}
