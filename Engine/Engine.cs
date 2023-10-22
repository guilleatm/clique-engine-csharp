﻿using SDL2;
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
	Window window;
	// List<Behaviour> behaviours = new List<Behaviour>();
	// Queue<Behaviour> behavioursToStart = new Queue<Behaviour>();

	public Engine()
	{
		instance = this;
	}

	public void Start(bool editor = false)
	{
		const int TARGET_ELAPSED_MS = 1000 / TARGET_FPS;

		SDL.SDL_Init(SDL.SDL_INIT_EVENTS);
		
		window = new Window(new Vector2f(600, 600));


		SubWindow window2 = new SubWindow(new SDL.SDL_FRect().From(new Vector2f(.2f, .1f), Vector2f.one * .5f));
		window.AddChild(window2);

		UILayoutNode layout = new UILayoutNode();
		LabelNode n = new LabelNode();
		LabelNode n1 = new LabelNode();
		LabelNode n2 = new LabelNode();

		(layout as IParentable).AddChild(n);
		(layout as IParentable).AddChild(n1);
		(layout as IParentable).AddChild(n2);

		UILayoutNode layout2 = new UILayoutNode();
		(layout2 as IParentable).AddChild(n1);

		window2.AddNode(layout);

		window.Start();

		


		//_startBehaviours();

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

				Update(delta);
			}

			window.Update();

			//renderingServer.Render();


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

	void Update(float delta)
	{
		// _startBehaviours();

		// for (int i = 0; i < behaviours.Count; i++)
		// {
		// 	if (behaviours[i].enabled)
		// 	{
		// 		behaviours[i].Update(delta);
		// 	}
		// }
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