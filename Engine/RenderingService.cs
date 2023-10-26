using SDL2;
using CliqueEngine.Nodes;
using CliqueEngine.Extensions;
using CliqueEngine.Components;


namespace CliqueEngine;

public class RenderingService : IService
{
	public static RenderingService instance = null!;
	public static nint window; // C# not sure


	nint renderer;
	List<Renderable> renderables = new List<Renderable>();
	
	public RenderingService()
	{
		instance = this;

		CreateWindowAndRenderer();
		renderer = SDL.SDL_GetRenderer(window);

	}

	public static void CreateWindowAndRenderer()
	{
		// If video has been initialized return
		if (SDL.SDL_WasInit(SDL.SDL_INIT_VIDEO) != 0) return;

		SDL.SDL_Init(SDL.SDL_INIT_VIDEO);

		SDL.SDL_WindowFlags windowFlags = 	SDL.SDL_WindowFlags.SDL_WINDOW_INPUT_FOCUS |
											SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE;

		nint _window, _renderer;
		SDL.SDL_CreateWindowAndRenderer((int) Engine.instance.windowSize.x, (int) Engine.instance.windowSize.y, windowFlags, out _window, out _renderer);

		string file = "assets/frog_square_32x32.png";
		nint icon = SDL_image.IMG_Load(file);
		if (icon == nint.Zero)
		{
			throw new FileNotFoundException($"File: {file} not found");
		}
		SDL.SDL_SetWindowIcon(_window, icon);
		SDL.SDL_SetWindowTitle(_window, "Clique Engine");

		window = _window;
	}
	
	public void AddResource(IComponent resource)
	{
		renderables.Add((Renderable) resource);
	}

	public void Start() {}

	public void Update(float delta)
	{
		SDL.SDL_Rect destinationRect = new SDL.SDL_Rect();
		SDL.SDL_Rect sourceRect = new SDL.SDL_Rect();
		for (int i = 0; i < renderables.Count; i++)
		{
			// No texture set yet
			if (renderables[i].texture == nint.Zero) continue;
			
			destinationRect = destinationRect.From(renderables[i].transform.position, renderables[i].size);
			sourceRect = sourceRect.From(Vector2f.zero, renderables[i].size);

			SDL.SDL_RenderCopy(renderer, renderables[i].texture, ref sourceRect, ref destinationRect);
		}


		// #C Depending if there is UIService or not
		// SDL.SDL_RenderPresent(renderer);
		// SDL.SDL_RenderClear(renderer);
	}

	public nint CreateTexture(string path)
	{
		return SDL_image.IMG_LoadTexture(renderer, path);
	}
}