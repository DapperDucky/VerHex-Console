using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Tome.Models;
internal static class Voxel
{
	public const int Height = 84; 
	public const int Width = 84;
	public const int Depth = 84;
	public const int MaxHeight = 121;
	public const int MaxWidth = 121;
	public const int MaxDepth = 121;
	public const int StandardVoxelLength = 84;
	
	public static List<(int, int, int, int, int)> GetInclineSteps(float height, float length, int wide = 0, int offset = 0)
	{
		// Hold the verts
		List<(int, int, int, int, int)> steps = new();

		// Figure out the size of each step
		float step = (height * 84) / length;

		// Calc the steps
		for (int i = 0; i <= length; i++)
		{
			float currentVert = step * i;
			int stepSize = i > 0 ? Convert.ToInt32(Math.Round(currentVert + offset)) - Convert.ToInt32(Math.Round(step * (i - 1))) : 0;
			int index1 = Convert.ToInt32(Math.Round(currentVert + offset));
			int index2 = Convert.ToInt32(Math.Round(StandardVoxelLength - (currentVert + wide)));
			int voxelIndexL = i;
			int voxelIndexH = Convert.ToInt32(Math.Floor(currentVert / StandardVoxelLength));

			steps.Add((index1, index2, voxelIndexL, voxelIndexH, stepSize));
		}
		return steps;
	}
}
