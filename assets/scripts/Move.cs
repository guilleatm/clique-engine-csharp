using CliqueEngine;
using CliqueEngine.Nodes;

namespace Nodes;

public class Move : Renderable
{

	Vector2f limit = RenderingServer.instance.windowSize;
	Vector2f v;
	
	public Move(string path, Vector2f size, Vector2f position) : base(path, position)
	{

	}

	public override void Start()
	{
		Random r = new Random();
		float velocity = r.Next(1, 10) / 5f;
		
		v = new Vector2f(1, 1) * velocity;

		Engine.instance.onWindowResized += (int w, int h) => { limit.x = w; limit.y = h; };
	}

	public override void Update(float delta)
	{
		if (position.x > limit.x || position.x < 0)
		{
			v.x *= -1;
		}

		if (position.y > limit.y || position.y < 0)
		{
			v.y *= -1;
		}
		
		position += v;
	}
}