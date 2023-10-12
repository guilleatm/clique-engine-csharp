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


}
