using CliqueEngine.Extensions;
using SDL2;

namespace CliqueEngine.UI;

public abstract class UIElement
{
	public List<UIElement> children { get; private set; } = new List<UIElement>();
	public SDL.SDL_Rect rect => new SDL.SDL_Rect().From(position, size);


	protected UIElement _parent = null!;
	public virtual UIElement parent
	{
		get => _parent;
		set
		{
			_parent = value;
			_parent.AddChildren(this);
		}
	}
	protected Vector2f _size;
	public virtual Vector2f size
	{
		get => _size;
		set => _size = value;
	}


	public Vector2f localPosition { get; set; }
	public virtual Vector2f position
	{ 
		get
		{
			if (parent == null) return Vector2f.zero;
			return parent.position + localPosition;
		}
	}



	public virtual void Render()
	{
		_renderRect(Color.green);

		for (int i = 0; i < children.Count; i++)
		{
			children[i].Render();
		}
	}

	public virtual void Free()
	{
		parent.children.Remove(this);

		parent.UpdateSize();

		for (int i = children.Count - 1; i >= 0; i--)
		{
			children[i].Free();
		}
	}

	public virtual void AddChildren(UIElement child)
	{
		child.localPosition = Vector2f.zero;
		children.Add(child);
		UpdateSize();
	}

	protected void UpdateSize()
	{
		SDL.SDL_Rect r = new SDL.SDL_Rect().From(localPosition, Vector2f.zero);
		for (int i = 0; i < children.Count; i++)
		{
			r = r.Overlap( new SDL.SDL_Rect().From(children[i].localPosition, children[i].size) );
		}
		size = new Vector2f(r.w, r.h);

		if (this is UIRoot) return;

		parent.UpdateSize();

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