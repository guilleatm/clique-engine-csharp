// namespace CliqueEngine.UI;

// public class HorizontalLayout : UIElement
// {
// 	const int MARGIN = 5;
// 	int offset = 0;

// 	public override void AddChildren(UIElement child)
// 	{
// 		child.localPosition = new Vector2f(offset, 0);
// 		children.Add(child);

// 		offset += (int) child.size.x + MARGIN;

// 		UpdateSize();
// 	}

// 	protected override void FreeChildren(UIElement _children)
// 	{
// 		base.FreeChildren(_children);
// 		offset -= (int) _children.size.x + MARGIN;
// 	}

// 	public override void Render()
// 	{
// 		_renderRect(Color.grey);
// 		base.Render();
// 	}
// }
