using CliqueEngine.Extensions;
using SDL2;

namespace CliqueEngine.UI;

public abstract class UIElement
{
	UIElement _parent = null!;
	public virtual UIElement parent
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
			_parent.size = Vector2f.one; // Updates parent.size
		}
	}

	public List<UIElement> children { get; private set; } = new List<UIElement>();

	Vector2f _size;
	public virtual Vector2f size
	{
		get => _size;
		set
		{
			SDL.SDL_Rect r = new SDL.SDL_Rect().From(position, Vector2f.zero);

			for (int i = 0; i < children.Count; i++)
			{
				r = r.Overlap(children[i].rect);
			}

			_size = new Vector2f(r.w, r.h);

			if (parent != null)
			{
				parent.size = default; // Updates parent size
			}
		}
	}
	public Vector2f position { get; set; }
	public SDL.SDL_Rect rect => new SDL.SDL_Rect().From(position, size);

	public virtual void Render()
	{
		for (int i = 0; i < children.Count; i++)
		{
			children[i].Render();
		}
	}

	public virtual void Free()
	{
		parent.children.Remove(this);

		for (int i = children.Count - 1; i >= 0; i--)
		{
			children[i].Free();
		}
	}

	protected void _renderRect(SDL.SDL_Color? color = null)
	{
		SDL.SDL_Rect rect = new SDL.SDL_Rect().From(position, size);

		_setColor(color ?? Color.grey);
		SDL.SDL_RenderFillRect(UIRoot.SDLRenderer, ref rect);
	    _setColor(Color.black);
	}

	void _setColor(SDL.SDL_Color color)
	{
	    SDL.SDL_SetRenderDrawColor(UIRoot.SDLRenderer, color.r, color.g, color.b, color.a);
	}

}