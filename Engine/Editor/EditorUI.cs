using System.Reflection;
using CliqueEngine.UI;
using CliqueEngine.Nodes;

namespace CliqueEngine.Editor;

class EditorUI
{
	public EditorUI(UIRoot root)
	{
		InspectorUI inspector = new InspectorUI(root)
		{
			localPosition = new Vector2f(RenderingServer.instance.windowSize.x, 0)
		};
		
		HierarchyUI hierarchy = new HierarchyUI(root)
		{
			localPosition = Vector2f.zero,
			inspector = inspector
		};



	}
}