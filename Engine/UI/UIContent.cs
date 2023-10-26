using System.Net.Http.Headers;
using CliqueEngine.Extensions;
using SDL2;

namespace CliqueEngine.UI;

class UIContent : UIElement
{
	public UIContent() : base()
	{
		size = new Vector2f(60, 60);
	}

	public override Vector2f GetSize() => size;
	public override void Draw(nint renderer)
	{
		DrawRect(renderer, Color.green, offset: 1);
	}
}




//  class UIContent : UIElement
// {
// 	public override Vector2f size
// 	{
// 		get => _size;
// 		set
// 		{
// 			// _size = value;
// 			// if (parent == null) return;
// 			// this.parent.UpdateSize();
// 		}
// 	}

// 	public UIContent(Vector2f size)
// 	{
// 		this._size = size;
// 	}

// 	public override void Render()
// 	{
// 		base.Render();
// 	}

// 	public override void AddChildren(UIElement child)
// 	{
// 		child.localPosition = size;
// 		_parent.children.Add(child);
// 		UpdateSize();
// 	}
// }