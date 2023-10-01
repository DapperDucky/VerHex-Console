using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tome.Models;
internal class Vertex2D
{
	public const int MaxValue = 121;
	public const int MinValue = -121;
	public const int DefaultValue = 0;

	public int X { get; set; }
    public int Y { get; set; }
	
    public Vertex2D(int? x = null, int? y = null)
	{
		// Validate nothing obvious is wrong
		if (x is not null && x > MaxValue)
		{ // We need a valid value for the vertex position
			throw new ArgumentOutOfRangeException(nameof(x), $"The value provided to {nameof(x)} cannot be greater than the minimum value of {MaxValue}");
		}
		else if (x is not null && x < MinValue)
		{ // We need a valid value for the vertex position
			throw new ArgumentOutOfRangeException(nameof(x), $"The value provided to {nameof(x)} cannot be less than the minimum value of {MinValue}");
		}
		else if (y is not null && y > MaxValue)
		{ // We need a valid value for the vertex position
			throw new ArgumentOutOfRangeException(nameof(y), $"The value provided to {nameof(y)} cannot be greater than the minimum value of {MaxValue}");
		}
		else if (y is not null && y < MinValue)
		{ // We need a valid value for the vertex position
			throw new ArgumentOutOfRangeException(nameof(y), $"The value provided to {nameof(y)} cannot be less than the minimum value of {MinValue}");
		}

		X = y ?? 0;
		Y = y ?? 0;
	}
}
