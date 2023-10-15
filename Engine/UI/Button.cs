using System.Runtime.InteropServices;
using SDL2;
using CliqueEngine.Extensions;
using CliqueEngine;

namespace CliqueEngine.UI;

public class Button : UIContent
{
	const int FONT_SIZE = 20;
	string text;
	nint textTexture;

	public event Method onClick = null!;
	bool selected = false;
	
	public Button(string text) : base(new Vector2f(text.Length, 1f) * FONT_SIZE)
	{
		this.text = text;

		nint surface = SDL_ttf.TTF_RenderText_Solid(UIRoot.UIFont, text, Color.white);
		//nint surface = SDL_ttf.TTF_RenderText_Shaded(UIRoot.UIFont, text, Color.white, Color.black);

		textTexture = SDL.SDL_CreateTextureFromSurface(UIRoot.SDLRenderer, surface);

		Engine.instance.onClick += HandleClickEvent;
	}

	public override void Render()
	{
		if (selected)
		{
			_renderFillRect(Color.yellow);
		}
		else
		{
			_renderFillRect(Color.green);
		}

		base.Render();
	
		//_renderRect(Color.grey);
		
		SDL.SDL_Rect source = new SDL.SDL_Rect().From(Vector2f.zero, size);
		SDL.SDL_Rect destination = new SDL.SDL_Rect().From(position, size);
		SDL.SDL_RenderCopy(UIRoot.SDLRenderer, textTexture, ref source, ref destination);
	}

	void HandleClickEvent(Vector2f originPosition, Vector2f finalPosition)
	{
		SDL.SDL_Rect clickRect = new SDL.SDL_Rect().From(originPosition, (finalPosition - originPosition).Abs());

		bool clicked = rect.Contains(clickRect);
		selected = clicked;

		if (clicked)
		{
			onClick?.Invoke();
		}
	}

	public override void Free()
	{
		onClick = null!;
		base.Free();
	}
}
