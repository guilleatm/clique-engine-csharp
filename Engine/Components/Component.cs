using CliqueEngine.Nodes;

public class Component : IComponent
{
	Node _node;
	public Node node
	{ 
		get => _node;
		set
		{
			if (_node != null) throw new InvalidOperationException("You can not set the node of a component");
			_node = value;
		}
	}

	public virtual void OnCreated() {}

}