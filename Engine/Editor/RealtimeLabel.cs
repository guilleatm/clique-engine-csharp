using System.Runtime.InteropServices;
using SDL2;
using CliqueEngine.Extensions;
using System.Reflection;
using CliqueEngine.Nodes;
using CliqueEngine.UI;


namespace CliqueEngine.Editor;

public class RealtimeLabel : Label
{
	Node node;
	FieldInfo fieldInfo;

	public RealtimeLabel(Node n, FieldInfo field, int maxLenght = 32) : base(maxLenght)
	{
		this.node = n;
		this.fieldInfo = field;
	}

	public override void Render()
	{
		this.text = $"{fieldInfo.FieldType.GetPrettyName()} {fieldInfo.Name}: {fieldInfo.GetValue(node)!.ToString()!}";
		base.Render();
	}
}
