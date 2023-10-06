namespace CliqueEngine;

public abstract class Node
{
	public bool enabled;

	public virtual void Start() {}
	public virtual void Update( float delta ) {}
}
