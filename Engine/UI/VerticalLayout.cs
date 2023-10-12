namespace CliqueEngine.UI;

public class VerticalLayout : UIElement
{
	const int MARGIN = 5;
	int offset = 0;

	public override void AddChildren(UIElement child)
	{
		child.localPosition = new Vector2f(0, offset);
		children.Add(child);

		offset += (int) child.size.y + MARGIN;

		UpdateSize();
	}
}
