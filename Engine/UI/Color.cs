using SDL2;

namespace CliqueEngine.UI;

public static class Color
{
	public static SDL.SDL_Color black = new SDL.SDL_Color() { r = 0, g = 0, b = 0, a = 255 };
	public static SDL.SDL_Color white = new SDL.SDL_Color() { r = 255, g = 255, b = 255, a = 255 };
	public static SDL.SDL_Color grey = new SDL.SDL_Color() { r = 100, g = 100, b = 100, a = 100 };
}
