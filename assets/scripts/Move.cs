using CliqueEngine;
using CliqueEngine.Nodes;

namespace Nodes;

public class Move : Renderable
{

	int width = 600;
	int height = 600;
	public Move(string path, Vector2f size, Vector2f position) : base(path, size, position)
	{

	}

	public override void Start()
	{
		Console.WriteLine("movable start");
		vel = r.Next(1, 10) / 5f;
		dir = new Vector2f(1, 1) * vel;

		Engine.instance.onWindowResized += (int w, int h) => { width = w; height = h; };


	}

	Vector2f dir;
	float vel = .3f;
	Random r = new Random();

	public override void Update(float delta)
	{
		if (position.x > width || position.x < 0)
		{
			dir.x *= -1;
		}

		if (position.y > height || position.y < 0)
		{
			dir.y *= -1;
		}

		// Console.WriteLine("movable update");
		
		position += dir;
	}
}