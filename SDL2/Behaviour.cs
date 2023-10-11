namespace CliqueEngine;

public class Behaviour : Node
{
	public bool enabled { get; protected set; } = true;
	public virtual void Start() {}
	public virtual void Update( float delta ) {}


	public Behaviour() : base()
	{
		Engine.instance.AddResource(this);
	}

}
