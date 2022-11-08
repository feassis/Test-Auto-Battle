# Test-Auto-Battle

## Step 2 - Removing Compilation Warnings From Standard Project

Warnings:

1- Variable: CharacterClass playerCharacterClass at Program.cs, is never used, and the reason for that is that it is useless, since this info is should be part of variable Character PlayerCharacter, 
because class should be under Character class domaim.
2- GetRandomInt was not being used - since the enemy and player "random" initial position was not random I've just changed to be random by creating a auxiliary method to return a valid random number

## Step 1 - Removing Compilation Errors From Standard Project

Erros:

1- At Grid.cs -> grids.Add(newBox) was being called at a point where it was not defined, the solution was to move it to the second for, after newBox initialization