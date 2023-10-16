using CliqueEngine;
using CliqueEngine.Nodes;

namespace Nodes;

public class FrogSpawner : Behaviour
{

	public override void Start()
	{
		for (int i = 0; i < 3; i++)
		{
			Frog m = new Frog();
		}
	}


	// public override void Update(float delta)
	// {
	// 	timer += delta;

	// 	if (timer > 1f)
	// 	{
	// 		timer = 0;
	// 		Console.WriteLine($"FPS: {1f / delta}");
	// 	}
	// }
}


public class CrazyFrogSpawner : Behaviour
{

	public override void Start()
	{
		for (int i = 0; i < 1000; i++)
		{
			Frog m = new Frog();
		}
	}
}
