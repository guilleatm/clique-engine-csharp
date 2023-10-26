using SDL2;
using CliqueEngine.Nodes;
using CliqueEngine.Extensions;

namespace CliqueEngine;

public partial class Engine
{
	const int TARGET_FPS = 60;	
	public static Engine instance = null!;

	bool quit;
	bool run;
	public bool IsRunnig => run;

	List<IService> services = new List<IService>();

	public Engine()
	{
		instance = this;
	}

	public void Start(bool editor = false)
	{
		const int TARGET_ELAPSED_MS = 1000 / TARGET_FPS;

		SDL.SDL_Init(SDL.SDL_INIT_EVENTS);



		RenderingService renderingService = new RenderingService();
		BehavioursService behavioursService = new BehavioursService();
		UIService uiService = new UIService();


		services.AddRange( new List<IService>() { renderingService, behavioursService, uiService } );


		// Start services
		for (int i = 0; i < services.Count; i++)
		{
			services[i].Start();
		}

		

		uint lastFrameStart_ms = 0;

		quit = false;
		run = true;
		while ( !quit )
		{
			uint start_ms = SDL.SDL_GetTicks();

			HandleSDLEvents();

			if (run)
			{
				float delta = (start_ms - lastFrameStart_ms) / 1000f;

				// Update services
				for (int i = 0; i < services.Count; i++)
				{
					services[i].Update(delta);
				}
			}

			lastFrameStart_ms = start_ms;

			uint elapsed_ms = SDL.SDL_GetTicks() - start_ms;

			if (elapsed_ms < TARGET_ELAPSED_MS)
			{
				SDL.SDL_Delay(TARGET_ELAPSED_MS - elapsed_ms);
			}
		}

		onEngineQuit?.Invoke();

	}


	public void Pause()
	{
		run = false;
	}

	public void Resume()
	{
		run = true;
	}

	// void _startBehaviours()
	// {
	// 	while( behavioursToStart.Count > 0)
	// 	{
	// 		Behaviour b = behavioursToStart.Dequeue();
	// 		if (b.enabled)
	// 		{
	// 			b.Start();
	// 		}
	// 	}
	// }


	Vector2f mousePosition;
	void HandleSDLEvents()
	{

		SDL.SDL_Event @event;
		while ( SDL.SDL_PollEvent(out @event) > 0 )
		{
			switch(@event.type)
			{
				case SDL.SDL_EventType.SDL_QUIT:
					quit = true;
					break;
				case SDL.SDL_EventType.SDL_WINDOWEVENT:

					if (@event.window.windowEvent == SDL.SDL_WindowEventID.SDL_WINDOWEVENT_RESIZED)
					{
						onWindowResized?.Invoke(@event.window.data1, @event.window.data2);
					}
					break;


				// CLICK EVENT
				case SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN:
					mousePosition = new Vector2f(@event.button.x, @event.button.y);
				break;
				case SDL.SDL_EventType.SDL_MOUSEBUTTONUP:
					onClick?.Invoke(mousePosition, new Vector2f(@event.button.x, @event.button.y));
				break;
				
				// MOUSE MOVEMENT
				case SDL.SDL_EventType.SDL_MOUSEMOTION:
					onMouseMoved?.Invoke(new Vector2f(@event.motion.x, @event.motion.y));
				break;

				

			}
		}
	}

	// public void AddResource(Node node)
	// {
	// 	nodes.Add(node);
	// 	onNodeAdded?.Invoke(node);
	// }

	// public void FreeResource(Node node)
	// {
	// 	nodes.Remove(node);
	// }

	// public void AddResource(Behaviour behaviour)
	// {
	// 	behaviours.Add(behaviour);
	// 	behavioursToStart.Enqueue(behaviour);
	// }

}