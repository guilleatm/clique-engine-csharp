using CliqueEngine.Extensions;
using SDL2;

namespace CliqueEngine.UI;

public class UIContent : UIElement
{
	public override Vector2f size
	{
		get => _size;
		set {}
	}

	public UIContent(Vector2f size)
	{
		this._size = size;
	}

	public override void Render()
	{
		base.Render();
		_renderRect(Color.grey);
	}

	public override void AddChildren(UIElement child)
	{
		child.localPosition = size;
		_parent.children.Add(child);
		UpdateSize();
	}
}