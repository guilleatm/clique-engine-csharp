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
		renderer = SDL.SDL_GetRenderer(RenderingService.window);
	}

	public void AddResource(IComponent resource)
	{
		uiElements.Add((UIElement) resource);
	}

	public void Start()
	{
		for (int i = 0; i < uiElements.Count; i++)
		{
			uiElements[i].Start();
		}
	}

	public void Update(float delta)
	{
		for (int i = 0; i < uiElements.Count; i++)
		{
			uiElements[i].Draw(renderer);
		}

		SDL.SDL_RenderPresent(renderer);
		SDL.SDL_RenderClear(renderer);
	}
}