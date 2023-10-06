using CliqueEngine;

public class MoveManager : Node
{
	public override void Start()
	{
		Console.WriteLine("MoveManager start");
		Random r = new Random();

		for (int i = 0; i < 1500; i++)
		{
			int x = r.Next(600);
			int y = r.Next(600);;

			Move m = new Move("assets/frog_square_32x32.png", new Vector2f(x, y), new Vector2f(32, 32));
		}
	}

	public override void Update(float delta)
	{
		// Console.WriteLine("manager update");
	}
}
