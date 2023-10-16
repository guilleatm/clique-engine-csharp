using CliqueEngine;
using CliqueEngine.Nodes;

namespace Nodes;

public class Frog : Renderable
{

	Vector2f limit = RenderingServer.instance.windowSize;
	Vector2f v;
	
	public Frog() : base("assets/frog_square_32x32.png", Vector2f.zero)
	{

	}

	public override void Start()
	{
		Random r = new Random();
		float velocity = r.Next(1, 40) / 5f;
		
		v = new Vector2f( (float) r.NextDouble(), (float) r.NextDouble()).Normalized() * velocity;

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