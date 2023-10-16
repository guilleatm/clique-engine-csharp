using System.Globalization;

namespace CliqueEngine;

public struct Vector2f
{
	public static Vector2f zero = new Vector2f(0, 0);
	public static Vector2f one = new Vector2f(1f, 1f);
	public float x;
	public float y;

	public Vector2f(float x, float y)
	{
		this.x = x;
		this.y = y;
	}

	public float Magnitude()
	{
		return MathF.Sqrt((x * x) + (y * y));
	}

	public Vector2f Normalized()
	{
		float magnitude = Magnitude();
		if (magnitude > 1e-6f)
		{
			float inv = 1.0f / magnitude;		
			return new Vector2f(x * inv, y * inv);
		}
		return new Vector2f(x, y);
	}

	public Vector2f Scale(Vector2f s)
	{
		return new Vector2f(x * s.x, y * s.y);
	}

	public Vector2f Abs()
	{
		return new Vector2f(Math.Abs(x), Math.Abs(y));
	}

	public static Vector2f operator +(Vector2f v1, Vector2f v2)
	{
		return new Vector2f(v1.x + v2.x, v1.y + v2.y);
	}

	public static Vector2f operator -(Vector2f v1, Vector2f v2)
	{
		return new Vector2f(v1.x - v2.x, v1.y - v2.y);
	}

	public static Vector2f operator *(Vector2f v1, float f)
	{
		return new Vector2f(v1.x * f, v1.y * f);
	}

	public static Vector2f operator /(Vector2f v1, float f)
	{
		return new Vector2f(v1.x / f, v1.y / f);
	}

	public override string ToString()
	{
		return string.Format("({0} | {1})", string.Format("{0:0.0}", x), string.Format("{0:0.0}", y));
	}
}
