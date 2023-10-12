namespace CliqueEngine.UI;

public class VerticalLayout : UIElement
{
	const int MARGIN = 5;
	Vector2f up = new Vector2f(0, 1);
	int offset = 0;
	// public override void Render()
	// {
	// 	_renderRect(Color.green);

	// 	for (int i = 0; i < children.Count; i++)
	// 	{
	// 		children[i].Render();
	// 	}
	// }

	public override void AddChildren(UIElement child)
	{
		child.localPosition = new Vector2f(0, offset);
		children.Add(child);

		offset += (int) child.size.y + MARGIN;
	}
}
