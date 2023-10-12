using CliqueEngine.Extensions;
using SDL2;

namespace CliqueEngine.UI;

public class UIRoot : UIElement
{
	const int UI_FONT_SIZE = 10;

	public static nint SDLRenderer;
	public static nint UIFont;

	//public override nint SDLRenderer { get; protected set; }
	public override UIElement parent
	{
		get => throw new NullReferenceException($"{nameof(UIRoot)} does not have {nameof(parent)}.");
		set => throw new InvalidOperationException($"Trying to set {nameof(parent)} of {nameof(UIRoot)}.");
	}

	Vector2f _size;
	public override Vector2f size
	{
		get => _size;
		set
		{
			SDL.SDL_Rect s = new SDL.SDL_Rect().From(Vector2f.zero, Vector2f.zero);
			for (int i = 0; i < children.Count; i++)
			{
				s.Overlap(children[i].rect);
			}
			_size = new Vector2f(s.w, s.h);
		}
	}

	public UIRoot(nint _SDLRenderer, Vector2f size)
	{
		SDLRenderer = _SDLRenderer;
		this.position = Vector2f.zero;
		this.size = size;

		SDL_ttf.TTF_Init();
		UIFont = SDL_ttf.TTF_OpenFont("assets/fonts/Hack-Regular.ttf", UI_FONT_SIZE);
	}

	public override void Render()
	{
		for (int i = 0; i < children.Count; i++)
		{
			children[i].Render();
		}
	}
}