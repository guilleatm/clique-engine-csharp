using System.Runtime.InteropServices;
using SDL2;
using CliqueEngine.Extensions;
using System.Reflection;
using CliqueEngine.Nodes;


namespace CliqueEngine.UI;

public class RealtimeLabel : UIContent
{
	const int FONT_SIZE = 20;
	const int MAX_LENGHT = 16; 
	nint texture;
	nint surface;
	Node node;
	FieldInfo fieldInfo;

	public string text => fieldInfo.GetValue(node)!.ToString()!;
	
	public RealtimeLabel(Node n, FieldInfo field) : base(new Vector2f(MAX_LENGHT, 1) * FONT_SIZE)
	{
		node = n;
		fieldInfo = field;
	}

	public override void Render()
	{
		string _text = text;

		surface = SDL_ttf.TTF_RenderText_Solid(UIRoot.UIFont, _text, Color.white);
		texture = SDL.SDL_CreateTextureFromSurface(UIRoot.SDLRenderer, surface);
		
		//_renderRect(Color.yellow);
		base.Render();

		SDL.SDL_QueryTexture(texture, out uint format, out int access, out int width, out int height);
		Vector2f currentSize = new Vector2f(width, height);

		SDL.SDL_Rect source = new SDL.SDL_Rect().From(Vector2f.zero, currentSize);
		SDL.SDL_Rect destination = new SDL.SDL_Rect().From(position, currentSize);
		SDL.SDL_RenderCopy(UIRoot.SDLRenderer, texture, ref source, ref destination);
	}

	public override void Free()
	{
		SDL.SDL_FreeSurface(surface);
		base.Free();
	}
}
