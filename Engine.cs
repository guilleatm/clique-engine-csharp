using SDL2;

namespace CliqueEngine;

public class Engine
{
	const int TARGET_FPS = 60;
	
	public static Engine instance = null!;

	bool run = false;
	RenderingServer renderingServer = null!;
	List<Node> resources = new List<Node>();

	public Engine()
	{
		instance = this;
	}

	public void Start()
	{
		const float TARGET_DELTA = 1000f / TARGET_FPS;

		uint lastFrameTicks = 0;

		renderingServer = new RenderingServer();


		// START

		Console.WriteLine("Engine Start");

		var mm = new MoveManager();
		

		for (int i = 0; i < resources.Count; i++)
		{
			resources[i].Start();
		}

		
		run = true;
		while ( run )
		{
			uint currentTicks = SDL.SDL_GetTicks();
			uint delta = currentTicks - lastFrameTicks;

			if (delta >= TARGET_DELTA)
			{
				Update(delta / 1000f);
				lastFrameTicks = currentTicks;
			}
			else
			{
				// float d = target_delta - delta;
				// SDL_Delay(d / 2);
			}
		}
	}

	void Update(float delta)
	{
		// Console.WriteLine("Engine Update");

		HandleSDLEvents();

		for (int i = 0; i < resources.Count; i++)
		{
			resources[i].Update(delta);
		}

		renderingServer.Render();
	}

	void HandleSDLEvents()
	{
		SDL.SDL_Event @event;
		while ( SDL.SDL_PollEvent(out @event) > 0 )
		{
			switch(@event.type)
			{
				case SDL.SDL_EventType.SDL_QUIT:
					run = false;
					break;
			}
		}
	}

	public void AddResource(Node behaviour)
	{
		resources.Add(behaviour);
	}

}