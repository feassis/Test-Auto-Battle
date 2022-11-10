using System;
using static AutoBattle.Character;
using static AutoBattle.Grid;
using System.Collections.Generic;
using System.Linq;
using static AutoBattle.Types;

namespace AutoBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            Grid grid = new Grid(5, 5);
            GridBox playerCurrentLocation;
            GridBox enemyCurrentLocation;
            Character playerCharacter = null;
            Character enemyCharacter;
            List<Character> allPlayers = new List<Character>();
            int currentTurn = 0;
            int numberOfPossibleTiles = grid.Grids.Count;
            
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
                playerCharacter = CreateCharacter(characterClass, 0, "Player");

                return true;
            }

            void CreateEnemyCharacter()
            {
                //randomly choose the enemy class and set up vital variables
                var rand = new Random();
                int randomInteger = rand.Next(1, 4);
                CharacterClass enemyClass = (CharacterClass)randomInteger;
                Console.WriteLine($"Enemy Class Choice: {enemyClass}");
                enemyCharacter = CreateCharacter(enemyClass, 1, "AI");
            }

            Character CreateCharacter(CharacterClass characterClass, int index, string name)
            {
                return characterClass switch
                {
                    CharacterClass.Archer => new Archer(characterClass, index, name),
                    CharacterClass.Cleric => new Cleric(characterClass, index, name),
                    CharacterClass.Paladin => new Paladin(characterClass, index, name),
                    CharacterClass.Warrior => new Warrior(characterClass, index, name),
                    _ => throw new ArgumentOutOfRangeException($"Class not implemented")
                };
            }

            void StartGame()
            {
                //populates the character variables and targets
                enemyCharacter.Target = playerCharacter;
                playerCharacter.Target = enemyCharacter;
                allPlayers.Add(playerCharacter);
                allPlayers.Add(enemyCharacter);
                AlocatePlayers();
                StartTurn();
            }

            void StartTurn()
            {
                DisplayGameState();

                if (currentTurn == 0)
                {
                    int random = GetRandomInt(0, int.MaxValue);

                    if(random % 2 == 0)
                    {
                        allPlayers.Reverse();
                    }
                }

                foreach(Character character in allPlayers)
                {
                    character.StartTurn(grid);
                }

                DisplayGameState();

                currentTurn++;
                HandleTurn();
            }

            void HandleTurn()
            {
                if (playerCharacter.Health == 0)
                {
                    Console.Write(Environment.NewLine + Environment.NewLine);
                    Console.WriteLine("😥You've Lost😥");
                    Console.Write(Environment.NewLine + Environment.NewLine);

                    return;
                } 
                else if (enemyCharacter.Health == 0)
                {
                    Console.Write(Environment.NewLine + Environment.NewLine);
                    Console.WriteLine("🎉Congratulations You've Won🎉");
                    Console.Write(Environment.NewLine + Environment.NewLine);

                    return;
                } 
                else
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
                GridBox RandomLocation = (grid.Grids.ElementAt(random));
                Console.Write($"{random}\n");
                if (!RandomLocation.Ocupied)
                {
                    playerCurrentLocation = RandomLocation;
                    RandomLocation.Ocupied = true;
                    RandomLocation.CharacterIndex = playerCharacter.CharacterIndex;
                    grid.Grids[random] = RandomLocation;
                    playerCharacter.CurrentBox = grid.Grids[random];
                } else
                {
                    AlocatePlayerCharacter();
                }
            }

            void AlocateEnemyCharacter()
            {
                int random = GetRandomNumberPositionOnGrid();
                GridBox RandomLocation = (grid.Grids.ElementAt(random));
                Console.Write($"{random}\n");
                if (!RandomLocation.Ocupied)
                {
                    enemyCurrentLocation = RandomLocation;
                    RandomLocation.Ocupied = true;
                    RandomLocation.CharacterIndex = enemyCharacter.CharacterIndex;
                    grid.Grids[random] = RandomLocation;
                    enemyCharacter.CurrentBox = grid.Grids[random];
                    grid.DrawBattlefield();
                }
                else
                {
                    AlocateEnemyCharacter();
                }
            }

            int GetRandomNumberPositionOnGrid()
            {
                return GetRandomInt(0, grid.XLenght * grid.YLength);
            }

            void DisplayGameState()
            {
                Console.Write(Environment.NewLine + Environment.NewLine);
                Console.WriteLine("---------Game - State---------");
                Console.WriteLine($"Player Name: {playerCharacter.Name}");
                Console.WriteLine($"Player Index: {playerCharacter.CharacterIndex}");
                Console.WriteLine($"Player HP: {playerCharacter.Health}");
                Console.WriteLine($"Enemy Name: {enemyCharacter.Name}");
                Console.WriteLine($"Enemy Index: {enemyCharacter.CharacterIndex}");
                Console.WriteLine($"Enemy HP: {enemyCharacter.Health}");
                Console.WriteLine("------------------------------");
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
        }
    }
}
