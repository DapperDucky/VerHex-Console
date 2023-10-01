// See https://aka.ms/new-console-template for more information
using System;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;
using Tome;
using Tome.Models;

bool init = false;
bool awaitInstructions = true;
string command = string.Empty;
int height = 0;
int length = 0;
int width = 0;
int heightOffset = 0;


do
{
	// If we haven't yet, say hello
	if (!init)
	{ // Say hello
		TomeIO.Welcome();
		init = true;
	}

	// Get the users command
	command = TomeIO.PromptCommand();

	var commandArguments = Regex.Split(command, " ");

	
	switch (commandArguments[0])
	{
		case "h":
		case "H":
		case "help":
			TomeIO.Help();
			break;
		case "r":
		case "R":
		case "reset":
			height = 0;
			length = 0;
			width = 0;
			TomeIO.Reset();
			break;
		case "set":
			string setVariable = commandArguments.Length >= 2 ? commandArguments[1] : string.Empty;
			string? setValue = commandArguments.Length >= 3 ? commandArguments[2] : string.Empty;
			bool isVoxel = false;

			isVoxel = char.IsLetter(setValue?.ToCharArray().Last() ?? '0') ? setValue?.ToCharArray().Last() == 'V' : false;
			setValue = char.IsLetter(setValue?.ToCharArray().Last() ?? '0') ? setValue!.Remove(setValue.Length - 1, 1) : setValue;

			switch (setVariable)
			{
				case "height": 
					height = TomeIO.GetNumericInput(setValue, height, isVoxel, "Height successfully set", "Invalid height");
					break;
				case "length":
					length = TomeIO.GetNumericInput(setValue, length, isVoxel, "Length successfully set", "Invalid length");
					break;
				case "width":
					width = TomeIO.GetNumericInput(setValue, width, isVoxel, "Width successfully set", "Invalid width");
					break;
				default:
					TomeIO.InvalidInput("Invalid command argument");
					break;
			}
			break;
		case "o":
		case "O":
		case "options":
			TomeIO.WriteLine($"\tVariable\t\tValue\tRequired\tDescription", ConsoleColor.Yellow);
			TomeIO.WriteLine($"---------------------------------------------------------------------------------------------", ConsoleColor.Yellow);
			TomeIO.WriteLine($"\theight\t\t\t{height}\tYES\t\tThe total height of the angle of in voxels", ConsoleColor.Yellow);
			TomeIO.WriteLine($"\tlength\t\t\t{length}\tYES\t\tThe total length of the angle of in voxels", ConsoleColor.Yellow);
			TomeIO.WriteLine($"\twidth\t\t\t{width}\tNO\t\tThe total width of each voxel in the line", ConsoleColor.Yellow);
			TomeIO.WriteLine($"\theightOffset\t\t{heightOffset}\tNO\t\tThe number of verticies offset to start the angle", ConsoleColor.Yellow);
			break;
		case "c":
		case "C":
		case "calc":
			TomeIO.CalculateVertices(commandArguments.Length >= 2 ? commandArguments[1] : string.Empty);
			break;
		case "exit":
			TomeIO.GoodBye();
			awaitInstructions = false;
			break;
		case "vh":
		case "verhex":
			TomeIO.VerHex(height, length, width);
			break;
		default:
			TomeIO.UnknownCommand();
			break;
	}
}
while (awaitInstructions);


//////Console.WriteLine("Hello, World!");
////////Console.WriteLine(Voxel.GetInclineSteps(1, 24));
//////int line = 0;

//////foreach(var step in Voxel.GetInclineSteps(8, 17, 0, 21))
//////{
//////	line++;
		
//////	if (line % 4 == 1)
//////	{
//////		Console.WriteLine($"Chunk {(line / 4) + 1}", ConsoleColor.Red);
//////	}

//////	int voxPos = step.Item1 / 84;
//////	int stepPos = (step.Item1 % 84);
//////	Console.ForegroundColor = ConsoleColor.Red;
//////	Console.WriteLine($"{line}:\t{stepPos}|{84 - stepPos}|{stepPos-21}\t||\t{step.Item2 % 84}", ConsoleColor.Green);

//////	if (line % 4 == 0)
//////	{
//////		Console.WriteLine(new string('-',25));
//////	}
//////}

//////ConsoleColor[] consoleColors
//////		   = (ConsoleColor[])ConsoleColor
//////				 .GetValues(typeof(ConsoleColor));

//////Console.WriteLine("List of available "
//////						  + "Console Colors:");
//////foreach (var color in consoleColors)
//////	Console.WriteLine(color);
//////Console.ReadLine();