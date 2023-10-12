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
			_parent.size = default; // Updates parent.size
		}
	}

	public List<UIElement> children { get; private set; } = new List<UIElement>();

	public virtual Vector2f size { get; set; }
	public Vector2f position { get; set; }
	public SDL.SDL_Rect rect => new SDL.SDL_Rect().From(position, size);

	public virtual void Render()
	{
		for (int i = 0; i < children.Count; i++)
		{
			children[i].Render();
		}
	}
}