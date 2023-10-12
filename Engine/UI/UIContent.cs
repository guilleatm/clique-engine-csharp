using CliqueEngine.Extensions;
using SDL2;

namespace CliqueEngine.UI;

public class UIContent : UIElement
{
	Vector2f _size;
	public override Vector2f size
	{
		get => _size;
		set
		{
			if (parent != null)
			{
				parent.size = Vector2f.one; // Updates parent.size
			}
		}
	}

	public UIContent(Vector2f size)
	{
		this._size = size; // It is important this._size instead of this.size
	}

	public override void Render()
	{
		//_renderRect(Color.white);

		base.Render();		
	}
}