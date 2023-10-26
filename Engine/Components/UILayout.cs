using SDL2;
using CliqueEngine.Extensions;
using CliqueEngine.Nodes;

namespace CliqueEngine.UI;

public abstract class UILayout : UIElement
{
	public override void Start()
	{
		node.onNodeAdded += OnNodeAdded;
		node.onNodeRemoved += OnNodeRemoved;

		PrepareChildren();
		size = GetSize();
	}

	public virtual void PrepareChildren() {}

	public override Vector2f GetSize()
	{
		if (size != Vector2f.zero) return size;

		SDL.SDL_Rect rect = new SDL.SDL_Rect().From(Vector2f.zero, Vector2f.zero);
		for (int i = 0; i < children.Count; i++)
		{
			rect = rect.Overlap(children[i].rect);
		}

		size = new Vector2f(rect.w, rect.h);
		return size;
	}

	void OnNodeAdded(Node node)
	{
		throw new NotImplementedException();
	}

	void OnNodeRemoved(Node node)
	{
		throw new NotImplementedException();
	}

	public override void Draw(nint renderer)
	{
		DrawRectOutline(renderer, Color.white);
	}
}