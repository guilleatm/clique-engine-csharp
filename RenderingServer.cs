using SDL2;

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

	public void Render()
	{
		SDL.SDL_Rect destinationRect;
		SDL.SDL_Rect sourceRect;
		for (int i = 0; i < resources.Count(); i++)
		{
			Renderable r = resources[i];
			destinationRect = new SDL.SDL_Rect()
			{
				x = (int) r.position.x,
				y = (int) r.position.y,
				w = (int) r.size.x,
				h = (int) r.size.y,
			};

			sourceRect = new SDL.SDL_Rect()
			{
				x = 0,
				y = 0,
				w = (int) r.size.x,
				h = (int) r.size.y,
			};


			SDL.SDL_RenderCopy(SDLRenderer, resources[i].texture, ref sourceRect, ref destinationRect);
		}

		SDL.SDL_RenderPresent(SDLRenderer);
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
