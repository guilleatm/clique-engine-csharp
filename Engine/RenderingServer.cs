using CliqueEngine.Extensions;
using CliqueEngine.UI;
using SDL2;

using CliqueEngine.Nodes;

namespace CliqueEngine;

public class RenderingServer
{
	public static RenderingServer instance = null!;
	nint window;
	nint SDLRenderer;

	UIRoot UIRoot = null!;

	List<Renderable> renderables = new List<Renderable>();

	public RenderingServer()
	{
		instance = this;
	}

	public void Start()
	{
		const int WINDOW_SIZE = 600;

		SDL.SDL_Init(SDL.SDL_INIT_VIDEO);

		SDL.SDL_WindowFlags windowFlags = 	SDL.SDL_WindowFlags.SDL_WINDOW_INPUT_FOCUS |
											SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE;

		SDL.SDL_CreateWindowAndRenderer(WINDOW_SIZE, WINDOW_SIZE, windowFlags, out window, out SDLRenderer);

		string file = "assets/frog_square_32x32.png";
		nint icon = SDL_image.IMG_Load(file);
		if (icon == nint.Zero)
		{
			throw new FileNotFoundException($"File: {file} not found");
		}
		SDL.SDL_SetWindowIcon(window, icon);

		
		// window = SDL.SDL_CreateWindow("CLIQUE ENGINE", SDL.SDL_WINDOWPOS_CENTERED, SDL.SDL_WINDOWPOS_CENTERED, WINDOW_SIZE, WINDOW_SIZE, SDL.SDL_WindowFlags.SDL_WINDOW_INPUT_FOCUS);
		// SDLRenderer = SDL.SDL_CreateRenderer(window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);

		UIRoot = new UIRoot(SDLRenderer);

		Editor.HierarchyUI h = new Editor.HierarchyUI(UIRoot);









	
	}

	public void Render()
	{
		SDL.SDL_Rect destinationRect = new SDL.SDL_Rect();
		SDL.SDL_Rect sourceRect = new SDL.SDL_Rect();
		for (int i = 0; i < renderables.Count(); i++)
		{
			Renderable r = renderables[i];
			
			destinationRect = destinationRect.From(r.position, r.size);
			sourceRect = sourceRect.From(Vector2f.zero, r.size);
			//_updateRect(ref destinationRect, (int) r.position.x, (int) r.position.y, (int) r.size.x, (int) r.size.y);
			//_updateRect(ref sourceRect, 0, 0, (int) r.size.x, (int) r.size.y);

			SDL.SDL_RenderCopy(SDLRenderer, renderables[i].texture, ref sourceRect, ref destinationRect);
			//SDL.SDL_RenderCopyF(SDLRenderer, resources[i].texture, ref sourceRect, ref destinationRect);
		}

		// var a = new SDL.SDL_Rect() {x = 0, y = 0, w = 100, h = 100};

		// SDL.SDL_Color def = new SDL.SDL_Color() { r = 0, g = 0, b = 0, a = 255 };
		// SDL.SDL_Color ui = new SDL.SDL_Color() { r = 100, g = 100, b = 100, a = 200 };


		// SetColor(ui);
		// SDL.SDL_RenderFillRect(SDLRenderer, ref a);
	    // SetColor(def);


		UIRoot.Render();



		SDL.SDL_RenderPresent(SDLRenderer);
		SDL.SDL_RenderClear(SDLRenderer);
	}

	void SetColor(SDL.SDL_Color color)
	{
	    SDL.SDL_SetRenderDrawColor(SDLRenderer, color.r, color.g, color.b, color.a);
	}

	public nint CreateTexture(Renderable renderable, string path)
	{
		return SDL_image.IMG_LoadTexture(SDLRenderer, path);
	}

	public void AddResource(Renderable renderable)
	{
		renderables.Add(renderable);
	}

	void _updateRect(ref SDL.SDL_Rect rect, int x, int y, int w, int h)
	{
		rect.x = x;	rect.y = y;
		rect.w = w;	rect.h = h;
	}

	void _updateRect(ref SDL.SDL_FRect rect, float x, float y, float w, float h)
	{
		rect.x = x;	rect.y = y;
		rect.w = w;	rect.h = h;
	}

	~RenderingServer()
	{
		Console.WriteLine("Rendering Server Destroyed");
		SDL.SDL_DestroyRenderer(SDLRenderer);
		SDL.SDL_DestroyWindow(window);

		SDL.SDL_Quit();
	}
}
