﻿using SDL2;
using CliqueEngine.Extensions;
using CliqueEngine.Nodes;

using CliqueEngine.UI;

namespace CliqueEngine.Components.UI;
public abstract class UIElement : Component
{
	public Vector2f localPosition;
	public virtual Vector2f globalPosition => parent.globalPosition + localPosition;
	public Vector2f size;

	public SDL.SDL_Rect rect => new SDL.SDL_Rect().From(localPosition, size);

	protected List<UIElement> children = new List<UIElement>();
	public UIElement parent = null!;

	public UIElement()
	{
		UIService.instance.AddResource(this);
	}

	public void PreStart()
	{
		for (int i = 0; i < node.children.Count; i++)
		{
			if ( node.children[i].TryGetComponent<UIElement>(out UIElement uiElement) )
			{
				uiElement.parent = this;
				children.Add(uiElement);
			}
		}
	}

	public virtual void Start() {}

	/// <summary>
	/// This method traverse all UI tree.
	/// </summary>
	/// <returns>The size of the UIElement</returns>
	public virtual Vector2f GetSize() => throw new InvalidOperationException($"{nameof(GetSize)} of {GetType()} is not overwritten.");

	public virtual void Draw(nint renderer) {}

	protected void DrawRect(nint renderer, SDL.SDL_Color color, int offset = 0)
	{
		SDL.SDL_Rect rect = new SDL.SDL_Rect().From(globalPosition + Vector2f.one * offset, size - Vector2f.one * 2 * offset);
		SDL.SDL_Color previous = GetColor(renderer);
		SetColor(renderer, color);
		SDL.SDL_RenderFillRect(renderer, ref rect);
	    SetColor(renderer, previous);
	}

	protected void DrawRectOutline(nint renderer, SDL.SDL_Color color)
	{
		SDL.SDL_Rect rect = new SDL.SDL_Rect().From(globalPosition, size);
		SDL.SDL_Color previous = GetColor(renderer);
		SetColor(renderer, color);
		SDL.SDL_RenderDrawRect(renderer, ref rect);
	    SetColor(renderer, previous);
	}

	protected void SetColor(nint renderer, SDL.SDL_Color color)
	{
	    SDL.SDL_SetRenderDrawColor(renderer, color.r, color.g, color.b, color.a);
	}

	protected SDL.SDL_Color GetColor(nint renderer)
	{
		SDL.SDL_Color color = new SDL.SDL_Color();
	    SDL.SDL_GetRenderDrawColor(renderer, out color.r, out color.g, out color.b, out color.a);
		return color;
	}
}


// public abstract class UIElementOld
// {
// 	public List<UIElementOld> children { get; private set; } = new List<UIElementOld>();
// 	public SDL.SDL_Rect rect => new SDL.SDL_Rect().From(position, size);
// 	public Vector2f anchor = Vector2f.zero;
// 	public bool enabled = true;


// 	protected UIElementOld _parent = null!;
// 	public virtual UIElementOld parent
// 	{
// 		get => _parent;
// 		set
// 		{
// 			_parent = value;
// 			_parent.AddChildren(this);
// 		}
// 	}
// 	protected Vector2f _size;
// 	public virtual Vector2f size
// 	{
// 		get => _size;
// 		set => _size = value;
// 	}

// 	public Vector2f localPosition { get; set; }
// 	public virtual Vector2f position
// 	{ 
// 		get
// 		{
// 			if (parent == null) return Vector2f.zero;
// 			return parent.position + localPosition - size.Scale(anchor);
// 		}
// 	}



// 	public virtual void Render()
// 	{
// 		if (!enabled) return;
// 		for (int i = 0; i < children.Count; i++)
// 		{
// 			children[i].Render();
// 		}
// 	}

// 	public virtual void Free()
// 	{
// 		parent.FreeChildren(this);

// 		parent.UpdateSize();

// 		for (int i = children.Count - 1; i >= 0; i--)
// 		{
// 			children[i].Free();
// 		}
// 	}

// 	protected virtual void FreeChildren(UIElementOld _children)
// 	{
// 		children.Remove(_children);
// 	}

// 	public virtual void AddChildren(UIElementOld child)
// 	{
// 		child.localPosition = Vector2f.zero;
// 		children.Add(child);
// 		UpdateSize();
// 	}

// 	public void UpdateSize() // protected better but :(
// 	{
// 		SDL.SDL_Rect r = new SDL.SDL_Rect().From(Vector2f.zero, Vector2f.zero);
// 		for (int i = 0; i < children.Count; i++)
// 		{
// 			r = r.Overlap( new SDL.SDL_Rect().From(children[i].localPosition, children[i].size) );
// 		}
// 		size = new Vector2f(r.w, r.h);

// 		if (this is UIRoot) return;

// 		if (parent == null)
// 		{
// 			throw new NullReferenceException($"UIElement of type {GetType()} has no parent.");
// 		}

// 		parent.UpdateSize();
// 	}

// 	protected void _renderFillRect(SDL.SDL_Color? color = null)
// 	{
// 		SDL.SDL_Rect rect = new SDL.SDL_Rect().From(position, size);

// 		_setColor(color ?? Color.grey);
// 		SDL.SDL_RenderFillRect(UIRoot.SDLRenderer, ref rect);
// 	    _setColor(Color.black);
// 	}

// 	protected void _renderRect(SDL.SDL_Color? color = null)
// 	{
// 		SDL.SDL_Rect rect = new SDL.SDL_Rect().From(position, size);

// 		_setColor(color ?? Color.grey);
// 		SDL.SDL_RenderDrawRect(UIRoot.SDLRenderer, ref rect);
// 	    _setColor(Color.black);
// 	}

// 	void _setColor(SDL.SDL_Color color)
// 	{
// 	    SDL.SDL_SetRenderDrawColor(UIRoot.SDLRenderer, color.r, color.g, color.b, color.a);
// 	}

// }