namespace CliqueEngine.UI;

public class VerticalLayout : UIElement
{
	const int MARGIN = 5;
	Vector2f up = new Vector2f(0, 1);
	public override void Render()
	{
		_renderRect(Color.green);

		Vector2f _position = position;
		for (int i = 0; i < children.Count; i++)
		{
			children[i].position = _position;
			children[i].Render();
			_position += children[i].size.Scale(up) + up * MARGIN;
		}
	}
}
