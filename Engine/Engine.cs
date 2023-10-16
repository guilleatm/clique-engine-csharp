using SDL2;
using CliqueEngine.Nodes;

namespace CliqueEngine;

public partial class Engine
{
	const int TARGET_FPS = 60;
	public const bool EDITOR = true;
	
	public static Engine instance = null!;

	bool run = false;
	RenderingServer renderingServer = null!;
	public List<Node> nodes { get; private set; } = new List<Node>();
	List<Behaviour> behaviours = new List<Behaviour>();
	Queue<Behaviour> behavioursToStart = new Queue<Behaviour>();

	public int nodeCount => nodes.Count;

	public Engine()
	{
		instance = this;
		renderingServer = new RenderingServer();
	}

	public void Start()
	{
		const int TARGET_ELAPSED_MS = 1000 / TARGET_FPS;

		SDL.SDL_Init(SDL.SDL_INIT_EVENTS);
		renderingServer.Start();

		// START

		Console.WriteLine("Engine Start");


		_startBehaviours();

		uint lastFrameStart_ms = 0;

		run = true;
		while ( run )
		{
			uint start_ms = SDL.SDL_GetTicks();
			float delta = (start_ms - lastFrameStart_ms) / 1000f;

			_startBehaviours();
			Update(delta);

			lastFrameStart_ms = start_ms;

			uint elapsed_ms = SDL.SDL_GetTicks() - start_ms;

			if (elapsed_ms < TARGET_ELAPSED_MS)
			{
				SDL.SDL_Delay(TARGET_ELAPSED_MS - elapsed_ms);
			}
		}

		onEngineQuit?.Invoke();

	}

	void _startBehaviours()
	{
		while( behavioursToStart.Count > 0)
		{
			Behaviour b = behavioursToStart.Dequeue();
			if (b.enabled)
			{
				b.Start();
			}
		}
	}

	void Update(float delta)
	{
		// Console.WriteLine("Engine Update");

		HandleSDLEvents();

		for (int i = 0; i < behaviours.Count; i++)
		{
			if (behaviours[i].enabled)
			{
				behaviours[i].Update(delta);
			}
		}

		renderingServer.Render();
	}

	Vector2f mousePosition;
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
			}
		}
	}

	public void AddResource(Node node)
	{
		nodes.Add(node);
		onNodeAdded?.Invoke(node);
	}

	public void AddResource(Behaviour behaviour)
	{
		behaviours.Add(behaviour);
		behavioursToStart.Enqueue(behaviour);
	}

}