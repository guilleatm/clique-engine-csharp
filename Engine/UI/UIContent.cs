using CliqueEngine.Extensions;
using SDL2;

namespace CliqueEngine.UI;

public class UIContent : UIElement
{
	public override Vector2f size
	{
		get => _size;
		set
		{
			value = _size;
			this.parent.UpdateSize();
		}
	}

	public UIContent(Vector2f size)
	{
		this._size = size;
	}

	public override void Render()
	{
		base.Render();
	}

	public override void AddChildren(UIElement child)
	{
		child.localPosition = size;
		_parent.children.Add(child);
		UpdateSize();
	}
}