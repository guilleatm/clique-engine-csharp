using SDL2;
using CliqueEngine.Nodes;
using CliqueEngine.Extensions;

namespace CliqueEngine;

public class RenderingService : IService
{
	public static RenderingService instance = null!;

	nint renderer;
	List<Renderable> renderables = new List<Renderable>();
	
	public RenderingService()
	{
		Vector2f WINDOW_SIZE = new Vector2f(600, 600);
		instance = this;

		SDL.SDL_Init(SDL.SDL_INIT_VIDEO);

		SDL.SDL_WindowFlags windowFlags = 	SDL.SDL_WindowFlags.SDL_WINDOW_INPUT_FOCUS |
											SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE;

		nint window;
		SDL.SDL_CreateWindowAndRenderer((int) WINDOW_SIZE.x, (int) WINDOW_SIZE.y, windowFlags, out window, out renderer);

		string file = "assets/frog_square_32x32.png";
		nint icon = SDL_image.IMG_Load(file);
		if (icon == nint.Zero)
		{
			throw new FileNotFoundException($"File: {file} not found");
		}
		SDL.SDL_SetWindowIcon(window, icon);
		SDL.SDL_SetWindowTitle(window, "Clique Engine");
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

		SDL.SDL_RenderPresent(renderer);
		SDL.SDL_RenderClear(renderer);
	}
}