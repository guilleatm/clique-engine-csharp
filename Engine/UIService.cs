using CliqueEngine;
using CliqueEngine.UI;
using CliqueEngine.Extensions;
using SDL2;

public class UIService : IService
{
	public static UIService instance = null!;

	List<UIElement> uiElements = new List<UIElement>();
	
	nint renderer;
	UIRoot root;

	public UIService()
	{
		instance = this;

		RenderingService.CreateWindowAndRenderer();
		SDL.SDL_GetRenderer(renderer);
	}

	public void AddResource(IComponent resource)
	{
		uiElements.Add((UIElement) resource);
	}

	public void Start()
	{

	}

	public void Update(float delta)
	{
		SDL.SDL_Rect destinationRect = new SDL.SDL_Rect();
		SDL.SDL_Rect sourceRect = new SDL.SDL_Rect();
		for (int i = 0; i < uiElements.Count; i++)
		{
			// // No texture set yet
			// if (uiElements[i].texture == nint.Zero) continue;
			
			// destinationRect = destinationRect.From(uiElements[i].position, uiElements[i].size);
			// sourceRect = sourceRect.From(Vector2f.zero, uiElements[i].size);

			// SDL.SDL_RenderCopy(renderer, uiElements[i].texture, ref sourceRect, ref destinationRect);
		}

		SDL.SDL_RenderPresent(renderer);
		SDL.SDL_RenderClear(renderer);
	}
}