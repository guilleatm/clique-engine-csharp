using SDL2;
using CliqueEngine.Extensions;
using CliqueEngine.Nodes;

namespace CliqueEngine.UI;

public class UIVerticalLayout : UILayout
{
	const int MARGIN = 5;
	public override void PrepareChildren()
	{
		int offset = 0;
		for (int i = 0; i < children.Count; i++)
		{
			Vector2f _size;
			if (children[i] is UILayout uILayout)
			{
				uILayout.PrepareChildren();
				_size = uILayout.GetSize();
			}
			else if (children[i] is UIContent uIContent)
			{
				_size = uIContent.GetSize();
			}
			else
			{
				throw new NullReferenceException($"{nameof(UIElement)} is not {nameof(UILayout)} or {nameof(UIContent)}.");
			}

			children[i].position = new Vector2f(0, offset);
			offset += (int) _size.y;

		}
	}

	public override Vector2f GetSize()
	{
		SDL.SDL_Rect rect = new SDL.SDL_Rect().From(Vector2f.zero, Vector2f.zero);
		for (int i = 0; i < children.Count; i++)
		{
			rect = rect.Overlap(children[i].rect);
		}

		size = new Vector2f(rect.w, rect.h);
		return size;
	}
}
