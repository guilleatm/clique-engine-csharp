// using CliqueEngine.Extensions;
// using CliqueEngine.UI;
// using SDL2;

// using CliqueEngine.Nodes;

// namespace CliqueEngine;

// public class RenderingServer
// {
// 	public static RenderingServer instance = null!;
// 	public Vector2f windowSize = new Vector2f(600, 600);
// 	nint window;
// 	nint SDLRenderer;

// 	UIRoot UIRoot = null!;

// 	List<Renderable> renderables = new List<Renderable>();

// 	public RenderingServer()
// 	{
// 		instance = this;
	
// 		Engine.instance.onWindowResized += (int width, int height) => windowSize = new Vector2f(width, height);


// 	}

// 	public void Start()
// 	{
// 		SDL.SDL_Init(SDL.SDL_INIT_VIDEO);

// 		SDL.SDL_WindowFlags windowFlags = 	SDL.SDL_WindowFlags.SDL_WINDOW_INPUT_FOCUS |
// 											SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE;

// 		SDL.SDL_CreateWindowAndRenderer((int) windowSize.x, (int) windowSize.y, windowFlags, out window, out SDLRenderer);

// 		string file = "assets/frog_square_32x32.png";
// 		nint icon = SDL_image.IMG_Load(file);
// 		if (icon == nint.Zero)
// 		{
// 			throw new FileNotFoundException($"File: {file} not found");
// 		}
// 		SDL.SDL_SetWindowIcon(window, icon);
// 		SDL.SDL_SetWindowTitle(window, "CLIQUE ENGINE");

// 		UIRoot = new UIRoot(SDLRenderer);

// 		// window = SDL.SDL_CreateWindow("CLIQUE ENGINE", SDL.SDL_WINDOWPOS_CENTERED, SDL.SDL_WINDOWPOS_CENTERED, WINDOW_SIZE, WINDOW_SIZE, SDL.SDL_WindowFlags.SDL_WINDOW_INPUT_FOCUS);
// 		// SDLRenderer = SDL.SDL_CreateRenderer(window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);

// 	}

// 	public void CreateEditorUI()
// 	{
// 		Editor.EditorUI editor = new Editor.EditorUI(UIRoot);
// 	}

// 	public void Render()
// 	{
// 		SDL.SDL_Rect destinationRect = new SDL.SDL_Rect();
// 		SDL.SDL_Rect sourceRect = new SDL.SDL_Rect();
// 		for (int i = 0; i < renderables.Count(); i++)
// 		{
// 			// No texture set yet
// 			if (renderables[i].texture == nint.Zero) continue;
			
// 			//destinationRect = destinationRect.From(renderables[i].position, renderables[i].size);
// 			sourceRect = sourceRect.From(Vector2f.zero, renderables[i].size);

// 			SDL.SDL_RenderCopy(SDLRenderer, renderables[i].texture, ref sourceRect, ref destinationRect);
// 			//SDL.SDL_RenderCopyF(SDLRenderer, resources[i].texture, ref sourceRect, ref destinationRect);
// 		}

// 		UIRoot.Render();

// 		SDL.SDL_RenderPresent(SDLRenderer);
// 		SDL.SDL_RenderClear(SDLRenderer);
// 	}

// 	public nint CreateTexture(Renderable renderable, string path)
// 	{
// 		return SDL_image.IMG_LoadTexture(SDLRenderer, path);
// 	}

// 	public void AddResource(Renderable renderable)
// 	{
// 		renderables.Add(renderable);
// 	}

// 	public void FreeResource(Renderable renderable)
// 	{
// 		renderables.Remove(renderable);
// 	}

// 	~RenderingServer()
// 	{
// 		Console.WriteLine("Rendering Server Destroyed");
// 		SDL.SDL_DestroyRenderer(SDLRenderer);
// 		SDL.SDL_DestroyWindow(window);

// 		SDL.SDL_Quit();
// 	}
// }
