using System.Net;
using System.Reflection;
using CliqueEngine.UI;
namespace CliqueEngine.Editor;

class HierarchyUI : UIElement
{
	VerticalLayout verticalLayout;
	Button addNodeBtn = new Button("Create Node");

	public HierarchyUI() : base()
	{

		verticalLayout = new VerticalLayout() { parent = this };

		addNodeBtn.onClick += DisplayNodeTypes;
		addNodeBtn.parent = verticalLayout;


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

		var types = new Type[] { typeof(WebHeaderCollection), typeof(Move), typeof(Renderable) };
		//var types = Assembly.GetExecutingAssembly().GetTypes();

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

		//Node node = (Node) Activator.CreateInstance(type, new object {});



		Move r = new Move("assets/frog_square_32x32.png", new Vector2f(0, 0), new Vector2f(32, 32));

		Label label = new Label($"{r.ID} {type.Name}");
		label.parent = verticalLayout;
	}
}