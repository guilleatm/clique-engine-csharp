using CliqueEngine.Extensions;
using SDL2;

namespace CliqueEngine.UI;

public class UIRoot : UIElement
{
	const int UI_FONT_SIZE = 10;

	public static nint SDLRenderer;
	public static nint UIFont;

	public sealed override UIElement parent
	{
		get => throw new NullReferenceException($"{nameof(UIRoot)} does not have {nameof(parent)}.");
		set => throw new InvalidOperationException($"Trying to set {nameof(parent)} of {nameof(UIRoot)}.");
	}

	public override Vector2f position
	{ 
		get => Vector2f.zero;
	}


	public UIRoot(nint _SDLRenderer)
	{
		SDLRenderer = _SDLRenderer;
		this.localPosition = Vector2f.zero;
		this.size = Vector2f.zero;

		SDL_ttf.TTF_Init();
		UIFont = SDL_ttf.TTF_OpenFont("assets/fonts/Hack-Regular.ttf", UI_FONT_SIZE);
	}
}