# Test-Auto-Battle

## Step 4 - Drawing Character On Grid

1- making player and enemy alocation be independent
2- at DrawBattlefield method instead of getting correct position it was creating a new one, fixed that to get correct grid box
3- changing DrawBattlefield to draw character index instead of X, in order to do this I am changing GridBox to store a Character index;
4- changing DrawBattlefield to not need grid size as a input since the grid already has this info
5- during refactor of DrawBattlefield I've found a bug: if a Character is on grid's border the CheckCloseTargets(Grid battlefield) method was throwing an exeption when tring to access a position outside the grid. 
Since I've born in august my task would be to: The character can attack/walk in 8 directions. to do that one method that I would implement is to get a valid neibouthood, and this method would help to solve this bug. 
An other bug that I've found is that on CheckCloseTarget() method Up and Down bool were getting each other's value
6- during creation of GetNeighbourhood Method I've noticed that of the movement options was drawing battlefiled before changes

## Step 3 - Refactoring Character (PR Link: https://github.com/feassis/Test-Auto-Battle/pull/3)

1- there is a bug where: when creating enemy, player's data is modified but enemy's are not. To fix this I am moving this value alocations to Character constructor, since thay all use the same base value;
2- moving call to create enemy to be independant to player creation;
3- changing create player character method to no longer use switch case and repeat code
4- start game should not be part of setup, since the game itself is not part o its setup
5- Implementing AllPlayer.Sort()

## Step 2 - Removing Compilation Warnings From Standard Project (PR Link: https://github.com/feassis/Test-Auto-Battle/pull/2)

Warnings:

1- Variable: CharacterClass playerCharacterClass at Program.cs, is never used, and the reason for that is that it is useless, since this info is should be part of variable Character PlayerCharacter, 
because class should be under Character class domaim.
2- GetRandomInt was not being used - since the enemy and player "random" initial position was not random I've just changed to be random by creating a auxiliary method to return a valid random number
3- Fixing Player Current Location was never used. At AlocatePlayerCharacter() a new variable was being created with the same name and type making it alocate player to a local variable position. 
4- Return statement was at wrong postion at Character.cs.StartTurn(), chaged its position to after battleField is draw

## Step 1 - Removing Compilation Errors From Standard Project (PR Link: https://github.com/feassis/Test-Auto-Battle/pull/1)

Erros:

1- At Grid.cs -> grids.Add(newBox) was being called at a point where it was not defined, the solution was to move it to the second for, after newBox initialization