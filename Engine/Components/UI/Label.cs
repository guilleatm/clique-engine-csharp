using System.Runtime.InteropServices;
using SDL2;
using CliqueEngine.Extensions;
using CliqueEngine.UI;
using System.Globalization;

namespace CliqueEngine.Components.UI;

public class Label : UIContent
{
	string text = string.Empty;
	nint texture;

	public void SetText(string _text)
	{
		text = _text;
		texture = UIService.instance.CreateTextTexture(_text);
		SDL.SDL_QueryTexture(texture, out uint format, out int access, out int width, out int height);
		size = new Vector2f(width, height);

		if (parent is UILayout uiLayout)
		{
			uiLayout.UpdateLayout();
		}
	}

	public override void Start()
	{
		base.Start();

		//SetText(text);
	}

	public override void Draw(nint renderer)
	{
		if (texture == nint.Zero) return;

		base.Draw(renderer);

		SDL.SDL_Rect sourceRect = new SDL.SDL_Rect().From(Vector2f.zero, size);
		SDL.SDL_Rect destinationRect = new SDL.SDL_Rect().From(globalPosition, size);

		SDL.SDL_RenderCopy(renderer, texture, ref sourceRect, ref destinationRect);
	}
}

// 	public Label(int maxLenght) : base(new Vector2f(maxLenght, 1f) * UIRoot.UI_FONT_SIZE)
// 	{
// 	}
	
// 	public Label(string text) : base(new Vector2f(text.Length, 1f) * UIRoot.UI_FONT_SIZE)
// 	{
// 		this.text = text;
// 	}

// 	public override void Render()
// 	{
// 		//_renderRect(Color.yellow);
// 		base.Render();

// 		if (text == null) return;

// 		SDL.SDL_Rect source = new SDL.SDL_Rect().From(Vector2f.zero, textureSize);
// 		SDL.SDL_Rect destination = new SDL.SDL_Rect().From(position, textureSize);
// 		SDL.SDL_RenderCopy(UIRoot.SDLRenderer, texture, ref source, ref destination);
// 	}
// }
