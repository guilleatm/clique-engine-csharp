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
		const int TARGET_ELAPSED_MS = 1000 / TARGET_FPS;

		SDL.SDL_Init(SDL.SDL_INIT_EVENTS);
		renderingServer = new RenderingServer();


		// START

		Console.WriteLine("Engine Start");

		var mm = new MoveManager();
		

		for (int i = 0; i < resources.Count; i++)
		{
			resources[i].Start();
		}

		uint lastFrameStart_ms = 0;

		run = true;
		while ( run )
		{
			uint start_ms = SDL.SDL_GetTicks();
			float delta = (start_ms - lastFrameStart_ms) / 1000f;

			Update(TARGET_ELAPSED_MS / 1000f);

			lastFrameStart_ms = start_ms;

			uint elapsed_ms = SDL.SDL_GetTicks() - start_ms;

			if (elapsed_ms < TARGET_ELAPSED_MS)
			{
				SDL.SDL_Delay(TARGET_ELAPSED_MS - elapsed_ms);
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