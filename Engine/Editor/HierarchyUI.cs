using System.Reflection;
using CliqueEngine.UI;
using CliqueEngine.Nodes;

namespace CliqueEngine.Editor;

class HierarchyUI : UIElement
{
	VerticalLayout verticalLayout;
	Button addNodeBtn;

	public HierarchyUI(UIElement root) : base()
	{
		parent = root;

		verticalLayout = new VerticalLayout() { parent = this };
		addNodeBtn = new Button("Create Node") { parent = verticalLayout };

		addNodeBtn.onClick += DisplayNodeTypes;

		List<Node> nodes = Engine.instance.nodes;
		for (int i = 0; i < nodes.Count; i++)
		{
			Label label = new Label($"{nodes[i].GetType().Name}") { parent = verticalLayout };
		}
	}

	void DisplayNodeTypes()
	{
		VerticalLayout nodeOptions = new VerticalLayout() { parent = addNodeBtn };

		Method removeNodeOptionsUI = () => nodeOptions.Free();

		string namespaceName = nameof(CliqueEngine.Nodes);
		string customNamespace = nameof(Nodes);
		Type[] types = Assembly.GetExecutingAssembly().GetTypes().Where( d => d.Namespace != null && (d.Namespace == namespaceName || d.Namespace!.StartsWith( customNamespace ))).ToArray();

		for (int i = 0; i < types.Length; i++)
		{
			int _index = i;

			Button b = new Button($"{types[i].Name}") { parent = nodeOptions };
			b.onClick += () => CreateNode(types[_index]);
			b.onClick += removeNodeOptionsUI;
		}

	}

	void CreateNode(Type type)
	{

		Node? node = (Node?) Activator.CreateInstance(type);

		if (node == null) return;

		Label label = new Label($"{node.ID} {type.Name}") { parent = verticalLayout };
	}
}