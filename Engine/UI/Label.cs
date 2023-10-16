using System.Runtime.InteropServices;
using SDL2;
using CliqueEngine.Extensions;

namespace CliqueEngine.UI;

public class Label : UIContent
{
	const int FONT_SIZE = 20;
	nint texture;
	nint surface;
	Vector2f textureSize;
	
	string _text = null!;
	public string text
	{
		get
		{
			return _text;
		}
		set
		{
			_text = value;
			// if (surface != nint.Zero)
			// {
			// 	SDL.SDL_FreeSurface(surface);
			// }

			surface = SDL_ttf.TTF_RenderText_Solid(UIRoot.UIFont, text, Color.white);
			texture = SDL.SDL_CreateTextureFromSurface(UIRoot.SDLRenderer, surface);

			SDL.SDL_QueryTexture(texture, out uint format, out int access, out int width, out int height);
			textureSize = new Vector2f(width, height);

			this.size = new Vector2f(text.Length, 1f) * FONT_SIZE;
		}
	}

	public Label() : base(Vector2f.zero)
	{
	}
	
	public Label(string text) : base(new Vector2f(text.Length, 1f) * FONT_SIZE)
	{
		this.text = text;
	}

	public override void Render()
	{
		//_renderRect(Color.yellow);
		base.Render();

		if (text == null) return;

		SDL.SDL_Rect source = new SDL.SDL_Rect().From(Vector2f.zero, textureSize);
		SDL.SDL_Rect destination = new SDL.SDL_Rect().From(position, size);
		SDL.SDL_RenderCopy(UIRoot.SDLRenderer, texture, ref source, ref destination);
	}

	public override void Free()
	{
		// TODO: something wrong here, double free?
		//SDL.SDL_FreeSurface(surface);
		base.Free();
	}
}
