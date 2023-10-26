using CliqueEngine;
using CliqueEngine.UI;
using CliqueEngine.Extensions;
using SDL2;
using CliqueEngine.Components.UI;

namespace CliqueEngine.UI;

public class UIService : IService
{
	public static UIService instance = null!;

	List<UIElement> uiElements = new List<UIElement>();
	
	nint renderer;
	UIRoot root;

	nint font;


	public UIService()
	{
		instance = this;

		RenderingService.CreateWindowAndRenderer();
		renderer = SDL.SDL_GetRenderer(RenderingService.window);
	
		SDL_ttf.TTF_Init();
		font = SDL_ttf.TTF_OpenFont("assets/fonts/Hack-Regular.ttf", 20);

		root = new UIRoot();
		uiElements.Remove(root);
	}

	public void AddResource(IComponent resource)
	{
		UIElement uiElement = (UIElement) resource;
		uiElement.parent = root;
		uiElements.Add(uiElement);
	}

	public void Start()
	{
		for (int i = 0; i < uiElements.Count; i++)
		{
			uiElements[i].PreStart();
		}

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

	public nint CreateTextTexture(string text)
	{
		nint surface = SDL_ttf.TTF_RenderText_Solid(font, text, Color.white);
		nint texture = SDL.SDL_CreateTextureFromSurface(renderer, surface);
		SDL.SDL_FreeSurface(surface);

		return texture;
	}
}