namespace CliqueEngine.Nodes;

public abstract class Node
{
	public event OnNodeEventHandler onFreed;

	public int ID;
	public Vector2f position = Vector2f.zero;


	public Node()
	{
		Engine.instance.AddResource(this);
		ID = Engine.instance.nodeCount;
	}

	public virtual void Free()
	{
		Engine.instance.FreeResource(this);
		onFreed?.Invoke(this);
	}
	
}