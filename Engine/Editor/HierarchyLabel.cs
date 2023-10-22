// using CliqueEngine;
// using CliqueEngine.Nodes;
// using CliqueEngine.UI;

// namespace CliqueEngine.Editor;

// class HierarchyLabel : Button
// {
// 	HierarchyUI hierarchy;
// 	Node related;
// 	public HierarchyLabel(Node related, HierarchyUI hierarchy) : base(related.GetType().Name)
// 	{
// 		this.hierarchy = hierarchy;
// 		this.related = related;

// 		onClick += OnClicked;
// 	}

// 	void OnClicked()
// 	{
// 		if(hierarchy.inspector == null) return;

// 		hierarchy.inspector.Inspect(related);
		
// 		if (hierarchy.grabber != null)
// 		{
// 			hierarchy.grabber.Free();
// 		}
// 		hierarchy.grabber = new Grabber(related);
// 	}
// }