# Test-Auto-Battle

## Step 8 - Reviewing Project (PR Link: )

1- Code Style: adapting variable names to code style
2- Short Tech description: Doc Adding short tech description
3- Folder Organization: Organizing files on folders
4- explaning architecture choice on character skill

## Step 7 - Adding Class Specific Features (PR Link: https://github.com/feassis/Test-Auto-Battle/pull/7)

1- Feat - refactoring Character.cs - making Character class abstract and implementing a class for each Character Class
2- Feat - refactoring attack method -adding damage multiplier to damage flow
3- Feat - implementing character skill -creating character skills, assing for each class and calling at start of character turn. I created a abstract class called character skill that has a virtual method to execute skill. With this each of its chieldren can implement 
whatever is needed.
Observation: I've chosen to use behaviour architecture to character skill system, the idea behind this is to be able to easily re use code which is excellent to create new content and give a game designer power to do so.

## Step 6 - Refactoring Combat Flow (PR Link: https://github.com/feassis/Test-Auto-Battle/pull/6)

1- Fix - extracting variable - refactoring attack method to console display correct damage value.
2- Feat - displaying game state - adding log to HandleTurn() method to display current game state.
3- Fix - using method input - fixing bug on TakeDamage() to no longer remove character's base damage from Health, clamping Heath min value to 0
4- Feat - displayng game state - moving current game state disple to start of turn
5- Feat - new end game log - loging end game.
6- Feat - moving display game state to method - creating display game state method and calling at start and end of turn
7- Feat - new log on character death - adding log to say character died, and adding character name to constructor
8- Fix - dead character don't attack - fixing bug where a dead character could attack

## Step 5 - Changing Movement To Move To All 8 directions (PR Link: https://github.com/feassis/Test-Auto-Battle/pull/5)

1- Feat - implementing GetClosestPositionToTarget() - The first step of my implementation is to be able to get the neighbour closest to target. 
To achieve this two methods were implemented: GetNeighbourClosetToTarget(Grid battlefield) and GetNeighbourDistance(Grid battlefield, GridBox positiontoBeEvaluated)
2- Feat - removing useless else - removing movement section from else, since it is not needed because the if above has a return;
3- Feat - implementing 8 diretional movement - implementing method to move character to closest position and replacing section of movement on StartTurn()

## Step 4 - Drawing Character On Grid (PR Link: https://github.com/feassis/Test-Auto-Battle/pull/4)

1- Feat - moving method call - making player and enemy alocation be independent and randomizing player order
2- Fix - moving method call - at DrawBattlefield method instead of getting correct position it was creating a new one, fixed that to get correct grid box
3- Feat - showing character index on grid drawing - changing DrawBattlefield to draw character index instead of X, in order to do this I am changing GridBox to store a Character index;
4- Feat - code generalization - changing DrawBattlefield to not need grid size as a input since the grid already has this info
5- Fix - avoiding out of range exception - during refactor of DrawBattlefield I've found a bug: if a Character is on grid's border the CheckCloseTargets(Grid battlefield) method was throwing an exeption when tring to access a position outside the grid. 
Since I've born in august my task would be to: The character can attack/walk in 8 directions. to do that one method that I would implement is to get a valid neibouthood, and this method would help to solve this bug. 
An other bug that I've found is that on CheckCloseTarget() method Up and Down bool were getting each other's value
6- Fix - moving method call -during creation of GetNeighbourhood Method I've noticed that of the movement options was drawing battlefiled before changes

## Step 3 - Refactoring Character (PR Link: https://github.com/feassis/Test-Auto-Battle/pull/3)

1- Fix - allocation to wrong variable - there is a bug where: when creating enemy, player's data is modified but enemy's are not. 
To fix this I am moving this value alocations to Character constructor, since thay all use the same base value;
2- Feat - moving method call - moving call to create enemy to be independant to player creation
	motivation: removing dependance is a good pratice because it facilitates maintenance on long term projects
3- Feat - avoid code repetition - changing create player character method to no longer use switch case and repeat code
4- Feat - Removing Start Game From Setup - start game should not be part of setup, since the game itself is not part o its setup
5- Fix - Implementing AllPlayer.Sort()

## Step 2 - Removing Compilation Warnings From Standard Project (PR Link: https://github.com/feassis/Test-Auto-Battle/pull/2)

Warnings:

1- Fix - change variable placement: CharacterClass playerCharacterClass at Program.cs, is never used, and the reason for that is that it is useless, 
since this info is should be part of variable Character PlayerCharacter, because class should be under Character class domaim.
2- Fix - Using GetRandomInt - GetRandomInt was not being used - since the enemy and player "random" initial position was not random.
I've just changed to be random by creating a auxiliary method to return a valid random number
3- Fix - Variable definition hiding class variable - Fixing Player Current Location was never used. 
At AlocatePlayerCharacter() a new variable was being created with the same name and type making it alocate player to a local variable position. 
4- Fix - change placement of return statement- Return statement was at wrong postion at Character.cs.StartTurn(), chaged its position to after battleField is draw

## Step 1 - Removing Compilation Errors From Standard Project (PR Link: https://github.com/feassis/Test-Auto-Battle/pull/1)

Erros:

1- Fix - change placement of adition to list- At Grid.cs -> grids.Add(newBox) was being called at a point where it was not defined, the solution was to move it to the second for, after newBox initialization
