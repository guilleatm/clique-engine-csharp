using CliqueEngine.Extensions;
using CliqueEngine.UI;
using SDL2;

namespace CliqueEngine;
abstract class WindowBase
{
	protected nint renderer;
	List<WindowBase> children = null!;
	
	protected SDL.SDL_FRect normalizedRect;
	protected SDL.SDL_Rect rect;	
	
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

	public virtual void Render()
	{
		DrawRect(Color.white);

		if (children == null) return;
		for (int i = 0; i < children.Count; i++)
		{
			children[i].Render();
		}
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
	void DrawRect(SDL.SDL_Color color)
	{
		SDL.SDL_SetRenderDrawColor(renderer, color);
		SDL.SDL_RenderDrawRect(renderer, ref rect);
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
}


class SubWindow : WindowBase
{
	public SubWindow(SDL.SDL_FRect _normalizedRect)
	{
		normalizedRect = _normalizedRect;
	}
}

class Window : WindowBase
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

	public override void Render()
	{
		base.Render();

		SDL.SDL_RenderPresent(renderer);
		SDL.SDL_RenderClear(renderer);
	}
}