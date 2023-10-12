using CliqueEngine.UI;

namespace CliqueEngine.Editor;

class HierarchyUI : UIElement
{
	public HierarchyUI() : base()
	{
		VerticalLayout verticalLayout = new VerticalLayout();

		Button addNodeBtn = new Button("Create Node");
		addNodeBtn.onClick += CreateNode;
		addNodeBtn.parent = verticalLayout;

		for (int i = 0; i < 10; i++)
		{
			Label label = new Label($"Label {i}");
			label.parent = verticalLayout;
		}

		verticalLayout.parent = this;
	}

	void CreateNode()
	{
		Move r = new Move("assets/frog_square_32x32.png", new Vector2f(0, 0), new Vector2f(32, 32));
	}
}