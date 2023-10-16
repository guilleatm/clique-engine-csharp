using System.Text.RegularExpressions;
using System.Reflection;
using CliqueEngine.UI;
using CliqueEngine.Nodes;

namespace CliqueEngine.Editor;

class InspectorUI : UIElement
{
	VerticalLayout verticalLayout;

	public InspectorUI(UIRoot root) : base()
	{
		parent = root;
		anchor = new Vector2f(1, 0);
		verticalLayout = new VerticalLayout() { parent = this };
		Label header = new Label("Inspector") { parent = verticalLayout,  };

		Engine.instance.onWindowResized += (int widht, int height) => localPosition = new Vector2f(widht, 0);
	}

	public void Inspect(Node node)
	{
		Reset();

		// Header
		(verticalLayout.children[0] as Label)!.SetText($"{GetPrettyTypeName(node.GetType())}");

		FieldInfo[] fields = node.GetType().GetFields();

		for (int i = 0; i < fields.Length; i++)
		{
			string type_str = GetPrettyTypeName(fields[i].FieldType);
			Label l = new Label($"{type_str} {fields[i].Name}: {fields[i].GetValue(node)}") { parent = verticalLayout };
			verticalLayout.AddChildren(l);
		}
	}

	string GetPrettyTypeName(Type t)
	{
		int i = t.Name.LastIndexOf('.');
		if (i == -1) return t.Name;
		return t.Name.Substring(i, t.Name.Length - 1);
	}

	void Reset()
	{
		// Remove labels of previous node (except header)
		for (int i = verticalLayout.children.Count - 1; i > 0; i--)
		{
			verticalLayout.children[i].Free();
		}
	}
}