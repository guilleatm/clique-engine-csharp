using SDL2;
using CliqueEngine.Extensions;
using CliqueEngine.Nodes;
using CliqueEngine.UI;

namespace CliqueEngine.Components.UI;

public abstract class UILayout : UIElement
{
	public override void Start()
	{
		node.onNodeAdded += OnNodeAdded;
		node.onNodeRemoved += OnNodeRemoved;

		UpdateLayout();
	}

	public virtual void PrepareChildren() {}

	public override Vector2f GetSize()
	{
		SDL.SDL_Rect rect = new SDL.SDL_Rect().From(Vector2f.zero, Vector2f.zero);
		for (int i = 0; i < children.Count; i++)
		{
			rect = rect.Overlap(children[i].rect);
		}

		return new Vector2f(rect.w, rect.h);
	}

	public virtual void UpdateLayout()
	{
		PrepareChildren();
		size = GetSize();

		if (parent is UILayout uiLayout)
		{
			uiLayout.UpdateLayout();
		}
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