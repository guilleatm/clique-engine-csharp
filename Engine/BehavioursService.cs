using CliqueEngine;
using CliqueEngine.Nodes;
using Nodes;

public class BehavioursService : IService
{
	public static BehavioursService instance = null!;

	List<Behaviour> behaviours = new List<Behaviour>();


	public BehavioursService()
	{
		instance = this;
	}

	public void AddResource(IComponent resource)
	{
		behaviours.Add((Behaviour) resource);
	}

	public void Start()
	{

		Node n = new Node();
		n.AddComponent<Transform>();
		n.AddComponent<Frog>();
		var r = n.AddComponent<Renderable>();
		r.SetTexture("assets/frog_square_32x32.png");
		for (int i = 0; i < behaviours.Count; i++)
		{
			behaviours[i].Start();
		}
	}

	public void Update(float delta)
	{
		for (int i = 0; i < behaviours.Count; i++)
		{
			behaviours[i].Update(delta);
		}
	}
}