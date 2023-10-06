namespace CliqueEngine;

public abstract class Node
{
	public bool enabled = false;

	public virtual void Start() {}
	public virtual void Update( float delta ) {}

	public Node()
	{
		Engine.instance.AddResource(this);
	}
}
