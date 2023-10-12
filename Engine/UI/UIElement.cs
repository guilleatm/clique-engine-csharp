using CliqueEngine.Extensions;
using SDL2;

namespace CliqueEngine.UI;

public abstract class UIElement
{
	// public virtual nint SDLRenderer
	// {
	// 	get => parent.SDLRenderer;
	// 	protected set { throw new InvalidOperationException($"Trying to set {nameof(SDLRenderer)} of {nameof(UIContent)}."); }
	// }

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

	public virtual List<UIElement> children { get; private set; } = new List<UIElement>();

	public virtual Vector2f size { get; set; }
	public virtual Vector2f position { get; set; }
	public SDL.SDL_Rect rect => new SDL.SDL_Rect().From(position, size);

	public abstract void Render();
}