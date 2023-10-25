using System.Diagnostics;
using CliqueEngine;
using CliqueEngine.Nodes;

namespace Nodes;

public class Frog : Behaviour
{
	float time = 0;
	public override void Start()
	{
		Console.WriteLine("Frog Start");
	}

	public override void Update (float delta)
	{
		time += delta;
		Console.WriteLine(time);
	}
}