using CliqueEngine.Extensions;
using CliqueEngine.Nodes;
using SDL2;

namespace CliqueEngine.UI;

public class UIRoot : UILayout
{
	public UIRoot() : base()
	{
		position = Vector2f.zero;
		size = Engine.instance.windowSize;
		Engine.instance.onWindowResized += OnWindowResized;
	}

	public override Vector2f GetSize() => size;

	void OnWindowResized (int width, int height)
	{
		size = new Vector2f(width, height);
	}
}

// namespace CliqueEngine.UI;

// public class UIRoot : UIElement
// {
// 	public const int UI_FONT_SIZE = 20;

// 	public static nint SDLRenderer;
// 	public static nint UIFont;

// 	public sealed override UIElement parent
// 	{
// 		get => throw new NullReferenceException($"{nameof(UIRoot)} does not have {nameof(parent)}.");
// 		set => throw new InvalidOperationException($"Trying to set {nameof(parent)} of {nameof(UIRoot)}.");
// 	}

// 	public override Vector2f position
// 	{ 
// 		get => Vector2f.zero;
// 	}


// 	public UIRoot(nint _SDLRenderer)
// 	{
// 		SDLRenderer = _SDLRenderer;
// 		this.localPosition = Vector2f.zero;
// 		this.size = Vector2f.zero;

// 		SDL_ttf.TTF_Init();
// 		UIFont = SDL_ttf.TTF_OpenFont("assets/fonts/Hack-Regular.ttf", UI_FONT_SIZE);
// 	}
// }