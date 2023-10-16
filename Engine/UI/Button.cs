using System.Runtime.InteropServices;
using SDL2;
using CliqueEngine.Extensions;
using CliqueEngine;

namespace CliqueEngine.UI;

public class Button : Label
{	

	public event Method onClick = null!;
	bool selected = false;
	
	public Button(string text) : base(text)
	{
		Engine.instance.onClick += HandleClickEvent;
	}

	public override void Render()
	{
		if (selected)
		{
			_renderFillRect(Color.yellow);
		}
		else
		{
			_renderFillRect(Color.green);
		}

		base.Render();

	}

	void HandleClickEvent(Vector2f originPosition, Vector2f finalPosition)
	{
		SDL.SDL_Rect clickRect = new SDL.SDL_Rect().From(originPosition, (finalPosition - originPosition).Abs());

		bool clicked = rect.Contains(clickRect);
		selected = clicked;

		if (clicked)
		{
			onClick?.Invoke();
		}
	}

	public override void Free()
	{
		onClick = null!;
		base.Free();
	}
}
