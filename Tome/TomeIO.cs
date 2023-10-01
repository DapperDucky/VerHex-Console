using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tome.Models;

namespace Tome;
internal class TomeIO
{
	public static string GetInput(string prompt, ConsoleColor color = ConsoleColor.Magenta)
	{
		Console.ForegroundColor = color;
		Console.Write(string.IsNullOrWhiteSpace(prompt) ? ">" : prompt);
		Console.ForegroundColor = ConsoleColor.White;
		return Console.ReadLine() ?? string.Empty;
	}

	public static void WriteLine(string text, ConsoleColor color)
	{
		Console.ForegroundColor = color;
		Console.WriteLine(text);
	}

	public static void Write(string text, ConsoleColor color)
	{
		Console.ForegroundColor = color;
		Console.Write(text);
	}

	public static void Welcome()
	{
		TomeIO.WriteLine("Welcome to VerHex Alpha v0.1.2", ConsoleColor.Magenta);
		TomeIO.WriteLine(string.Empty, ConsoleColor.White);
	}

	public static void GoodBye()
	{
		TomeIO.WriteLine("Goodbye", ConsoleColor.Magenta);
		Thread.Sleep(2500);
		TomeIO.WriteLine(string.Empty, ConsoleColor.White);
	}

	public static string PromptCommand()
	{
		TomeIO.WriteLine("Enter a command or use the help command for a list of commands", ConsoleColor.DarkYellow);
		TomeIO.WriteLine(string.Empty, ConsoleColor.White);
		return TomeIO.GetInput(string.Empty);
	}

	public static void InvalidInput(string text = "Unknown Error")
	{
		TomeIO.WriteLine(text, ConsoleColor.Red);
	}

	public static void ValidInput(string text = "Success")
	{
		TomeIO.WriteLine(text, ConsoleColor.Green);
	}

	public static void UnknownCommand(string text = "Unknown command")
	{
		InvalidInput(text);
	}

	public static void Help()
	{
		TomeIO.WriteLine($"\thelp \t\t - \t\t List useful commands", ConsoleColor.Yellow);
		TomeIO.WriteLine($"\th \t\t - \t\t List useful commands", ConsoleColor.Yellow);
		TomeIO.WriteLine($"\tcalc \t\t - \t\t Calculates the number of verticies in a line of voxels where the length in voxels is provided by the user", ConsoleColor.Yellow);
		TomeIO.WriteLine($"\tverhex \t\t - \t\t Plot verticies along a line of a 2d plane to achieve a specified angle", ConsoleColor.Yellow);
		TomeIO.WriteLine($"\treset \t\t - \t\t Resets VerHex and all settings", ConsoleColor.Yellow);
		TomeIO.WriteLine($"\tset \t\t - \t\t set the value of the specified variable", ConsoleColor.Yellow);
		TomeIO.WriteLine($"\t\t ex: set height 10\t\t\tSets the number of voxels for the height variable to 100", ConsoleColor.Yellow);
		//TomeIO.WriteLine($"\t\t ex: set height 100v\t\t\tSets the number of verticies for the height variable to 100", ConsoleColor.Yellow);
		//TomeIO.WriteLine($"\t\t ex: set height 4V\t\t\tSets the number of verticies for the height variable to 4 Voxel lengths or 336 verticies", ConsoleColor.Yellow);
		TomeIO.WriteLine($"\texit \t\t - \t\t Exit VerHex", ConsoleColor.Yellow);
	}

	public static int GetNumericInput(string argument, int defaultValue = 0, bool isVoxel = false, string successMessage = "success", string failureMessage = "Invalid input")
	{
		if (Int32.TryParse(argument, out int value) && value > 0)
		{
			ValidInput(successMessage);
			ValidInput(string.Empty);
			return value * (isVoxel ? 84 : 1);
		}
		else
		{
			InvalidInput(failureMessage);
			InvalidInput(string.Empty);
			return defaultValue;
		}
	}

	public static void CalculateVertices(string numberOfVoxels)
	{
		if (Int32.TryParse(numberOfVoxels, out int length) && length > 0)
		{
			TomeIO.Write($"There are ", ConsoleColor.Yellow);
			Console.BackgroundColor = ConsoleColor.DarkGreen;
			TomeIO.Write($"{84 * length}", ConsoleColor.Black);
			Console.BackgroundColor = ConsoleColor.Black;
			TomeIO.Write($" verticies in a line of ", ConsoleColor.Yellow);
			Console.BackgroundColor = ConsoleColor.DarkGreen;
			TomeIO.Write($"{length}", ConsoleColor.Black);
			Console.BackgroundColor = ConsoleColor.Black;
			TomeIO.Write($" voxels", ConsoleColor.Yellow);
			TomeIO.WriteLine(string.Empty, ConsoleColor.Yellow);
		}
		else
		{
			InvalidInput("Invalid input");
			InvalidInput("Command calc requires a non-negative non-zero integer value as an argument");
			InvalidInput("ex: calc 4");
			InvalidInput(string.Empty);
		}
	}

	public static void Reset()
	{
		Console.Clear();
		Welcome();
	}

	public static void Set()
	{
		Console.Clear();
		Welcome();
	}

	public static void VerHex(int height, int length, int width, int offset = 0)
	{
		if (height <= 0)
		{
			InvalidInput("Invalid height");
			InvalidInput(string.Empty);
		}
		else if (length <= 0)
		{
			InvalidInput("Invalid length");
			InvalidInput(string.Empty);
		}
		else if (width <= 0)
		{
			InvalidInput("Invalid width");
			InvalidInput(string.Empty);
		}
		else
		{
			int line = 0;
			bool isChunkOffset = false;
			ConsoleColor chunkColor = ConsoleColor.Yellow;

			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.Write($"L\tUP|UW\tDP|DW\tVI\tSS");
			
			foreach (var step in Voxel.GetInclineSteps(height, length, width, offset))
			{
				bool isRowOffset = line % 2 == 1;
				chunkColor = isRowOffset ? ConsoleColor.DarkYellow : ConsoleColor.Yellow;
				chunkColor = isRowOffset ? ConsoleColor.DarkCyan : ConsoleColor.Cyan;
				Console.ForegroundColor = chunkColor;

				if (line % 4 == 0)
				{
					isChunkOffset = !isChunkOffset;

					if (isChunkOffset)
					{
						chunkColor = isRowOffset ? ConsoleColor.DarkCyan : ConsoleColor.Cyan;
					}
					else
					{
						chunkColor = isRowOffset ? ConsoleColor.DarkYellow : ConsoleColor.Yellow;
					}

					Console.WriteLine(string.Empty);
					Console.BackgroundColor = chunkColor;
					//Console.ForegroundColor = ConsoleColor.Black;
					//Console.WriteLine($"|Chunk {(line / 4) + 1}|");
					Console.BackgroundColor = ConsoleColor.Black;
					Console.ForegroundColor = chunkColor;
					Console.WriteLine(new string('-', 50));
				}
				else
				{
					if (isChunkOffset)
					{
						chunkColor = isRowOffset ? ConsoleColor.DarkCyan : ConsoleColor.Cyan;
					}
					else
					{
						chunkColor = isRowOffset ? ConsoleColor.DarkYellow : ConsoleColor.Yellow;
					}
					Console.ForegroundColor = chunkColor;
				}

				int voxPos = step.Item1 / 84;
				int stepPos = (step.Item1 % 84);

				int upPoint = stepPos;
				int upWidth = upPoint - width;

				int downPoint = 84 - stepPos;
				int downWidth = stepPos - width;

				Console.ForegroundColor = chunkColor;
				Console.WriteLine($"|{line}:\t{stepPos}|{upWidth}\t{downPoint}|{downWidth}\t[{step.Item3},{step.Item4}]\t{step.Item5}", chunkColor);

				line++;
			}
		}
	}
}
