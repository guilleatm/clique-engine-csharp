using System.Reflection;
using CliqueEngine.UI;
using CliqueEngine.Nodes;

namespace CliqueEngine.Editor;

class HierarchyUI : UIElement
{
	VerticalLayout verticalLayout;
	Button addNodeBtn;

	public InspectorUI? inspector { get; set; }

	public HierarchyUI(UIRoot root) : base()
	{
		parent = root;
		
		verticalLayout = new VerticalLayout() { parent = this };
		addNodeBtn = new Button("Create Node") { parent = verticalLayout };

		addNodeBtn.onClick += DisplayNodeTypes;

		List<Node> nodes = Engine.instance.nodes;
		for (int i = 0; i < nodes.Count; i++)
		{
			HierarchyLabel label = new HierarchyLabel(nodes[i], this) { parent = verticalLayout };
		}
	}

	void DisplayNodeTypes()
	{
		VerticalLayout nodeOptions = new VerticalLayout() { parent = addNodeBtn };

		Method removeNodeOptionsUI = () => nodeOptions.Free();

		string namespaceName = "CliqueEngine.Nodes";
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
		// This is slow but ParameterInfo.GetParameters() has to be overriden in derived classes
		try
		{
			Node? node = (Node?) Activator.CreateInstance(type);

			if (node == null) return;

			HierarchyLabel label = new HierarchyLabel(node, this) { parent = verticalLayout };
		}
		catch (MissingMethodException _)
		{
			Console.WriteLine($"Couldn't create node of type {type.Name}");
		}
	}
}