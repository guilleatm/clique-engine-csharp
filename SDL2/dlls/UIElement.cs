using CliqueEngine.Extensions;
using SDL2;

namespace CliqueEngine.UI;

public abstract class UIElement
{
	public virtual nint SDLRenderer { get; protected set; }
	public virtual UIElement parent { get; set; } = null!;


	public virtual List<UIElement> children { get; private set; } = new List<UIElement>();


	public virtual Vector2f size { get; set; }

	public abstract void Render();

	// Vector2f relativePosition;

	public SDL.SDL_Rect rect => new SDL.SDL_Rect().From(Vector2f.zero, size);






}


public class UIRoot : UIElement
{
	public override nint SDLRenderer { get; protected set; }
	
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

	public UIRoot(nint SDLRenderer, Vector2f size)
	{
		this.SDLRenderer = SDLRenderer;
		this.size = size;
	}

	public override void Render()
	{
		for (int i = 0; i < children.Count; i++)
		{
			children[i].Render();
		}
	}
}


public class UIContent : UIElement
{
	public override nint SDLRenderer
	{
		get => parent.SDLRenderer;
		protected set { throw new InvalidOperationException($"Trying to set {nameof(SDLRenderer)} of {nameof(UIContent)}."); }
	}

	SDL.SDL_Color def = new SDL.SDL_Color() { r = 0, g = 0, b = 0, a = 255 };
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

	UIElement _parent;
	public override UIElement parent
	{
		get => _parent;
		set
		{
			if (_parent != null)
			{
				_parent.children.Remove(this);
			}
			_parent = value;
			_parent.children.Add(this);
			_parent.size = default; // Updates parent.size
		}
	}

	public UIContent(Vector2f size)
	{
		this.size = size;
	}

	public override void Render()
	{
		SDL.SDL_Rect rect = new SDL.SDL_Rect().From(new Vector2f(0, 0), size);

		SetColor(ui);
		SDL.SDL_RenderFillRect(SDLRenderer, ref rect);
	    SetColor(def);
	}


	void SetColor(SDL.SDL_Color color)
	{
	    SDL.SDL_SetRenderDrawColor(SDLRenderer, color.r, color.g, color.b, color.a);
	}

}