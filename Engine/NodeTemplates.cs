using CliqueEngine.UI;
using CliqueEngine.Components;


namespace CliqueEngine.Nodes;
public class Sprite : Node
{
	public Sprite() : base(new [] { typeof(Transform), typeof(Renderable)} ) {}
}

public class UIVerticalLayoutNode : Node
{
	public UIVerticalLayoutNode() : base(new [] { typeof(UIVerticalLayout)} ) {}
}

public class UIHorizontalLayoutNode : Node
{
	public UIHorizontalLayoutNode() : base(new [] { typeof(UIHorizontalLayout)} ) {}
}

public class UIContentNode : Node
{
	public UIContentNode() : base(new [] { typeof(UIContent)} ) {}
}