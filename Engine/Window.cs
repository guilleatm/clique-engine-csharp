using CliqueEngine.Extensions;
using CliqueEngine.Nodes;
using CliqueEngine.UI;
using SDL2;

namespace CliqueEngine;

interface INodeContainer
{
	public List<Node> nodes { get; set; }
	public void AddNode(Node node);
}


public abstract class WindowBase : INodeContainer
{
	public static WindowBase active => stack.Peek();
	static Stack<WindowBase> stack = new Stack<WindowBase>();

	public nint renderer;
	List<WindowBase> children = new List<WindowBase>();
	
	protected SDL.SDL_FRect normalizedRect;
	protected SDL.SDL_Rect rect;

	// SPREAD CALLS
	public virtual void Start()
	{
		stack.Push(this);

		for (int i = 0; i < children.Count; i++)
		{
			children[i].Start();
		}

		for (int i = 0; i < nodes.Count; i++)
		{
			nodes[i].Start();
		}

		stack.Pop();
	}

	public virtual void Update()
	{
		stack.Push(this);
		
		SDL.SDL_Rect r = new SDL.SDL_Rect().From(Vector2f.zero, new Vector2f(rect.w, rect.h));
		DrawRect(Color.white, ref r);
		for (int i = 0; i < children.Count; i++)
		{
			children[i].Update();
		}

		for (int i = 0; i < nodes.Count; i++)
		{
			nodes[i].Update();
		}

		stack.Pop();
	}

	
	// MANAGE WINDOWS
	public void AddChild(WindowBase child)
	{
		if (children == null)
		{
			children = new List<WindowBase>();
		}
		
		child.renderer = renderer;
		SetChildRect(child);
		children.Add(child);
	}

	protected void ResizeChildren()
	{
		if (children == null) return;
		for (int i = 0; i < children.Count; i++)
		{
			SetChildRect(children[i]);
			children[i].ResizeChildren();
		}
	}
	public void DrawRect(SDL.SDL_Color color, ref SDL.SDL_Rect destinationRect)
	{
		SDL.SDL_Rect translatedDestinationRect = destinationRect.Translate(new Vector2f(rect.x, rect.y));

		SDL.SDL_SetRenderDrawColor(renderer, color);
		SDL.SDL_RenderDrawRect(renderer, ref translatedDestinationRect);
	    SDL.SDL_SetRenderDrawColor(renderer, Color.black);
	}

	void SetChildRect(WindowBase child)
	{
		child.rect = new SDL.SDL_Rect()
		{
			x = (int) (rect.w * child.normalizedRect.x),
			y = (int) (rect.h * child.normalizedRect.y),
			w = (int) (rect.w * child.normalizedRect.w),
			h = (int) (rect.h * child.normalizedRect.h)
		};
	}


	// MANAGE NODES
	public List<Node> nodes { get; set; } = new List<Node>();

	public void AddNode(Node node)
	{
		nodes.Add(node);
	}

	// DRAW CALLS
	public void Draw(nint texture, ref SDL.SDL_Rect sourceRect, ref SDL.SDL_Rect destinationRect)
	{
		SDL.SDL_Rect translatedDestinationRect = destinationRect.Translate(new Vector2f(rect.x, rect.y));
		SDL.SDL_RenderCopy(renderer, texture, ref sourceRect, ref translatedDestinationRect);
	}

	public nint CreateTexture(string path)
	{
		return SDL_image.IMG_LoadTexture(renderer, path);
	}
}


public class SubWindow : WindowBase
{
	public SubWindow(SDL.SDL_FRect _normalizedRect)
	{
		normalizedRect = _normalizedRect;
	}
}

public class Window : WindowBase
{
	public Window(Vector2f size)
	{
		Console.WriteLine($"Video status: {SDL.SDL_WasInit(SDL.SDL_INIT_VIDEO)}");

		if (SDL.SDL_WasInit(SDL.SDL_INIT_VIDEO) == 0)
		{
			SDL.SDL_Init(SDL.SDL_INIT_VIDEO);
		}

		SDL.SDL_WindowFlags windowFlags = 	SDL.SDL_WindowFlags.SDL_WINDOW_INPUT_FOCUS |
											SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE;

		nint window;
		SDL.SDL_CreateWindowAndRenderer((int) size.x, (int) size.y, windowFlags, out window, out renderer);

		string file = "assets/frog_square_32x32.png";
		nint icon = SDL_image.IMG_Load(file);
		if (icon == nint.Zero)
		{
			throw new FileNotFoundException($"File: {file} not found");
		}
		SDL.SDL_SetWindowIcon(window, icon);
		SDL.SDL_SetWindowTitle(window, "CLIQUE ENGINE");

		
		rect = new SDL.SDL_Rect().From(Vector2f.zero, size);
		//normalizedRect = new SDL.SDL_Rect().From(Vector2f.zero, Vector2f.one);

		Engine.instance.onWindowResized += (int width, int height) => 
		{
			rect.w = width;
			rect.h = height;
			ResizeChildren();
		};
	}

	public override void Update()
	{
		base.Update();

		SDL.SDL_RenderPresent(renderer);
		SDL.SDL_RenderClear(renderer);
	}
}