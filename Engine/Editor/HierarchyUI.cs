using CliqueEngine.UI;
namespace CliqueEngine.Editor;

class HierarchyUI : UIElement
{
	VerticalLayout verticalLayout = new VerticalLayout();
	public HierarchyUI() : base()
	{

		Button addNodeBtn = new Button("Create Node");
		addNodeBtn.onClick += CreateNode;
		addNodeBtn.parent = verticalLayout;


		List<Node> nodes = Engine.instance.nodes;
		for (int i = 0; i < nodes.Count; i++)
		{
			Label label = new Label($"{nodes[i].GetType().Name}");
			label.parent = verticalLayout;
		}

		verticalLayout.parent = this;
	}

	void CreateNode()
	{
		Move r = new Move("assets/frog_square_32x32.png", new Vector2f(0, 0), new Vector2f(32, 32));

		Label label = new Label($"{r.ID} {r.GetType().Name}");
		label.parent = verticalLayout;
	}
}