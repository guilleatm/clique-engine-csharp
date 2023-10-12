using CliqueEngine.Extensions;
using SDL2;

namespace CliqueEngine.UI;

public class UIContent : UIElement
{
	SDL.SDL_Color def = new SDL.SDL_Color() { r = 0, g = 0, b = 0, a = 255 }; // C#
	SDL.SDL_Color ui = new SDL.SDL_Color() { r = 100, g = 100, b = 100, a = 200 };

	Vector2f _size;
	public override Vector2f size
	{
		get => _size;
		set
		{
			_size = value;
			if (parent != null)
			{
				parent.size = default; // Updates parent.size
			}
		}
	}

	public UIContent(Vector2f size)
	{
		this.size = size;
	}

	public override void Render()
	{
		SDL.SDL_Rect rect = new SDL.SDL_Rect().From(position, size);

		_setColor(ui);
		SDL.SDL_RenderFillRect(SDLRenderer, ref rect);
	    _setColor(def);
	}

	void _setColor(SDL.SDL_Color color)
	{
	    SDL.SDL_SetRenderDrawColor(SDLRenderer, color.r, color.g, color.b, color.a);
	}

}