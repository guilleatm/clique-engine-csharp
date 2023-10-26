using SDL2;
using CliqueEngine.Extensions;
using CliqueEngine.Nodes;

namespace CliqueEngine.UI;

public abstract class UILayout : UIElement
{
	public override void Start()
	{
		base.Start();
		node.onNodeAdded += OnNodeAdded;
		node.onNodeRemoved += OnNodeRemoved;

		PrepareChildren();
		size = GetSize();
	}

	public virtual void PrepareChildren() {}

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