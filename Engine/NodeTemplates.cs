using CliqueEngine.UI;

namespace CliqueEngine.Nodes;
public class Sprite : Node
{
	public Sprite() : base(new [] { typeof(Transform), typeof(Renderable)} ) {}
}

public class UIVerticalLayoutNode : Node
{
	public UIVerticalLayoutNode() : base(new [] { typeof(UIVerticalLayout)} ) {}
}

public class UIContentNode : Node
{
	public UIContentNode() : base(new [] { typeof(UIContent)} ) {}
}