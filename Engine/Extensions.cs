using System.Reflection;
using SDL2;

namespace CliqueEngine.Extensions;

public static class Extensions
{

	public static SDL.SDL_Rect From(this SDL.SDL_Rect rect, Vector2f position, Vector2f size)
	{
		return new SDL.SDL_Rect()
		{
			x = (int) position.x,
			y = (int) position.y,
			w = (int) size.x,
			h = (int) size.y
		};

		// rect.x = (int) position.x;
		// rect.y = (int) position.y;
		// rect.w = (int) size.x;
		// rect.h = (int) size.y;
		// return rect;
	}

	public static SDL.SDL_FRect From(this SDL.SDL_FRect rect, Vector2f position, Vector2f size)
	{
		return new SDL.SDL_FRect()
		{
			x = position.x,
			y = position.y,
			w = size.x,
			h = size.y
		};
	}

	public static SDL.SDL_Rect Translate(this SDL.SDL_Rect rect, Vector2f position)
	{
		return new SDL.SDL_Rect()
		{
			x = (int) (rect.x + position.x),
			y = (int) (rect.y + position.y),
			w = rect.w,
			h = rect.h
		};
	}

	public static SDL.SDL_Rect Overlap(this SDL.SDL_Rect rect, SDL.SDL_Rect other)
	{
		return new SDL.SDL_Rect()
		{
			x = Math.Min(rect.x, other.x),
			y = Math.Min(rect.y, other.y),
			w = Math.Max(rect.x + rect.w, other.x + other.w),
			h = Math.Max(rect.y + rect.h, other.y + other.h)
		};
	}

	public static bool Contains(this SDL.SDL_Rect rect, SDL.SDL_Rect other)
	{
		return rect.x < other.x && rect.y < other.y && rect.x + rect.w > other.x + other.w && rect.y + rect.h > other.y + other.h;
	}

	public static string GetPrettyName(this Type type)
	{
		int i = type.Name.LastIndexOf('.');
		if (i == -1) return type.Name;
		return type.Name.Substring(i, type.Name.Length - 1);
	}

}
