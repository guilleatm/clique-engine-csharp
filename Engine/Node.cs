namespace CliqueEngine;

public abstract class Node
{
	public int ID;

	public Node()
	{
		Engine.instance.AddResource(this);
		ID = Engine.instance.nodeCount;
	}
	
}