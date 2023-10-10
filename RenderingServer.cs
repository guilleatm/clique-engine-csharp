﻿using SDL2;

namespace CliqueEngine;

public class RenderingServer
{
	public static RenderingServer instance = null!;
	nint window;
	nint SDLRenderer;

	List<Renderable> resources = new List<Renderable>();


	public RenderingServer() /*: base (typeof(RenderingServer))*/
	{
		instance = this;
	
		const int WINDOW_SIZE = 600;

		SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING); // #C

		window = SDL.SDL_CreateWindow("CLIQUE ENGINE", SDL.SDL_WINDOWPOS_CENTERED, SDL.SDL_WINDOWPOS_CENTERED, WINDOW_SIZE, WINDOW_SIZE, SDL.SDL_WindowFlags.SDL_WINDOW_INPUT_FOCUS);
		SDLRenderer = SDL.SDL_CreateRenderer(window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);
	}

	void _updateRect(ref SDL.SDL_Rect rect, int x, int y, int w, int h)
	{
		rect.x = x;	rect.y = y;
		rect.w = w;	rect.h = h;
	}

	public void Render()
	{
		SDL.SDL_Rect destinationRect = new SDL.SDL_Rect();
		SDL.SDL_Rect sourceRect = new SDL.SDL_Rect();
		for (int i = 0; i < resources.Count(); i++)
		{
			Renderable r = resources[i];
			
			_updateRect(ref destinationRect, (int) r.position.x, (int) r.position.y, (int) r.size.x, (int) r.size.y);
			_updateRect(ref sourceRect, 0, 0, (int) r.size.x, (int) r.size.y);

			SDL.SDL_RenderCopy(SDLRenderer, resources[i].texture, ref sourceRect, ref destinationRect);
		}


		SDL.SDL_RenderPresent(SDLRenderer);
		SDL.SDL_RenderClear(SDLRenderer);
	}

	public void CreateResource(Renderable renderable, string path, out nint texture)
	{
		texture = SDL_image.IMG_LoadTexture(SDLRenderer, path);
	}

	public void AddResource(Renderable renderable)
	{
		resources.Add(renderable);
	}

	~RenderingServer()
	{
		Console.WriteLine("Rendering Server Destroyed");
		SDL.SDL_DestroyRenderer(SDLRenderer);
		SDL.SDL_DestroyWindow(window);

		SDL.SDL_Quit();
	}
}
