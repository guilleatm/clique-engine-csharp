using CliqueEngine.UI;

namespace CliqueEngine.Editor;

class HierarchyUI : UIElement
{
	public HierarchyUI() : base()
	{
		VerticalLayout verticalLayout = new VerticalLayout();

		Button addNodeBtn = new Button("Create Node");
		addNodeBtn.parent = verticalLayout;

		for (int i = 0; i < 10; i++)
		{
			Label label = new Label($"Label {i}");
			label.parent = verticalLayout;
		}

		verticalLayout.parent = this;
	}
}