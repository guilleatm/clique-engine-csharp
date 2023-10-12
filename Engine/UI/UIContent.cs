using CliqueEngine.Extensions;
using SDL2;

namespace CliqueEngine.UI;

public class UIContent : UIElement
{
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
		_renderRect();

		base.Render();		
	}

	void _renderRect()
	{
		SDL.SDL_Rect rect = new SDL.SDL_Rect().From(position, size);

		_setColor(Color.grey);
		SDL.SDL_RenderFillRect(UIRoot.SDLRenderer, ref rect);
	    _setColor(Color.black);
	}

	void _setColor(SDL.SDL_Color color)
	{
	    SDL.SDL_SetRenderDrawColor(UIRoot.SDLRenderer, color.r, color.g, color.b, color.a);
	}

}