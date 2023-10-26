using CliqueEngine.UI;

namespace CliqueEngine.Nodes;
public class Sprite : Node
{
	public Sprite() : base(new [] { typeof(Transform), typeof(Renderable)} ) {}
}

public class UILayoutNode : Node
{
	public UILayoutNode() : base(new [] { typeof(UILayout)} ) {}
}

// public class UIContentNode : Node
// {
// 	public UIContentNode() : base(new [] { typeof(UIContent)} ) {}
// }