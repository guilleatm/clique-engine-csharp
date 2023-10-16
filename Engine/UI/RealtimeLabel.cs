using System.Runtime.InteropServices;
using SDL2;
using CliqueEngine.Extensions;
using System.Reflection;
using CliqueEngine.Nodes;


namespace CliqueEngine.UI;

public class RealtimeLabel : Label
{
	Node node;
	FieldInfo fieldInfo;

	public RealtimeLabel(Node n, FieldInfo field) : base()
	{
		this.node = n;
		this.fieldInfo = field;
	}

	public override void Render()
	{
		this.text = fieldInfo.GetValue(node)!.ToString()!;
		base.Render();
	}
}
