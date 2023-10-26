namespace CliqueEngine.Components;

public class Behaviour : Component
{
	//public bool enabled { get; protected set; } = true;
	public virtual void Start() {}
	public virtual void Update( float delta ) {}


	public Behaviour()
	{
		BehavioursService.instance.AddResource(this);
	}

}
