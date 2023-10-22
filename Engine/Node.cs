using CliqueEngine.UI;
using CliqueEngine.Extensions;
using SDL2;
using System.ComponentModel.Design.Serialization;

namespace CliqueEngine.Nodes;

public interface IParentable
{
	public IParentable parent { get; set; }
	public List<IParentable> children { get; set; }

	public void AddChild(IParentable child)
	{
		child.parent = this;
		children.Add(child);
	}
}

public class Node : IParentable
{
	public IParentable parent { get; set; } = null!;
	public List<IParentable> children { get; set; } = new List<IParentable>();
	protected List<Component> components;

	public Node()
	{
		children = new List<IParentable>();
		components = new List<Component>();
	}

	public Node(Component[] _components) : this()
	{
		for (int i = 0; i < _components.Length; i++)
		{
			Add(_components[i]);
		}
	}

	public void Start()
	{
		for (int i = 0; i < components.Count; i++)
		{
			components[i].Start();
		}
	
		for (int i = 0; i < children.Count; i++)
		{
			(children[i] as Node)!.Start();
		}
	}

	public void Update()
	{
		for (int i = 0; i < components.Count; i++)
		{
			components[i].Update();
		}

		for (int i = 0; i < children.Count; i++)
		{
			(children[i] as Node)!.Update();
		}
	}

	// MANAGE COMPONENTS

	public void Add<T>(T component) where T : Component
	{
		component.node = this;
		components.Add(component);
	}
	public T Get<T>() where T : Component
	{
		return (T) components.Find( d => d is T )!;
	}
	public bool TryGet<T>(out T component) where T : Component
	{
		component = (components.Find( d => d is T ) as T)!;
		return component != null;
	}

}

public abstract class Component
{
	public Node node = null!;
	public virtual void Start() {}
	public virtual void Update() {}
}

public class Transform : Component
{
	public Vector2f position;
	public float rotation;
}

public class Texture : Component
{
	nint texture;
	Vector2f size;

	Transform transform = null!;
	
	public override void Start()
	{

		texture = Window.active.CreateTexture("assets/frog_square_32x32.png");

		SDL.SDL_QueryTexture(texture, out uint format, out int access, out int width, out int height);
		size = new Vector2f(width, height);

		transform = node.Get<Transform>();
	}

	public override void Update()
	{
		SDL.SDL_Rect source = new SDL.SDL_Rect().From(Vector2f.zero, size);
		SDL.SDL_Rect destination = new SDL.SDL_Rect().From(transform.position, size);

		Window.active.Draw(texture, ref source, ref destination);		
	}
}

public class Label : UIContent
{
	nint texture;
	string text = "Label";
	public override void Start()
	{
		SDL_ttf.TTF_Init();
		nint font = SDL_ttf.TTF_OpenFont("assets/fonts/Hack-Regular.ttf", 20);

		nint surface = SDL_ttf.TTF_RenderText_Solid(font, text, Color.white);
		texture = SDL.SDL_CreateTextureFromSurface(Window.active.renderer, surface);
		SDL.SDL_FreeSurface(surface);

		SDL.SDL_QueryTexture(texture, out uint format, out int access, out int width, out int height);
		size = new Vector2f(width, height);
		topUIElement.GetSize(); // Update size
	}

	public override void Update()
	{
		SDL.SDL_Rect source = new SDL.SDL_Rect().From(Vector2f.zero, size);
		SDL.SDL_Rect destination = new SDL.SDL_Rect().From(transform.position, size);

		Window.active.Draw(texture, ref source, ref destination);		
	}
}

public class UILayout : UIElement
{
	bool waitFrame = true; // We wait one frame for the UIContent nodes to start
	public override void Start()
	{
		size = GetSize(); // It is more to propagate the UIRoot
	}

	public override Vector2f GetSize(UIElement? _topUIElement = null)
	{
		topUIElement = _topUIElement ?? this;
		SDL.SDL_Rect rect = new SDL.SDL_Rect().From(Vector2f.zero, Vector2f.zero);
		for (int i = 0; i < node.children.Count; i++)
		{
			bool hasUIElement = (node.children[i] as Node)!.TryGet<UIElement>(out UIElement uiElement);
		
			if (hasUIElement)
			{
				Vector2f elementPosition = uiElement.transform.position;
				Vector2f elementSize = uiElement.GetSize(topUIElement);
				SDL.SDL_Rect elementRect = new SDL.SDL_Rect().From(elementPosition, elementSize);
			
				rect.Overlap(elementRect);
			}
		}
		return new Vector2f(rect.w, rect.h);
	}

	void SetPosition()
	{
		const int MARGIN = 10;
		int offset = 0;
		for (int i = 0; i < node.children.Count; i++)
		{
			bool hasUIElement = (node.children[i] as Node)!.TryGet<UIElement>(out UIElement uiElement);
		
			if (hasUIElement)
			{

				if (uiElement is UILayout uiLayout)
				{
					uiLayout.SetPosition();
				}
				else if (uiElement is UIContent uIContent)
				{
					uIContent.transform.position = new Vector2f(0, offset);
					offset += (int) uIContent.size.y + MARGIN;
				}
			}
		}
	}
	public override void Update()
	{
		if (waitFrame)
		{
			SetPosition();
			waitFrame = false;
		}

		SDL.SDL_Rect rect = new SDL.SDL_Rect().From(transform.position, size);
		Window.active.DrawRect(Color.green, ref rect);
	}
}

public class UIContent : UIElement
{
	public override Vector2f GetSize(UIElement? _topUIElement = null)
	{
		if (_topUIElement == null) throw new NullReferenceException($"{nameof(_topUIElement)} can not be null on {nameof(UIContent)}.{nameof(GetSize)}.");
		topUIElement = _topUIElement;
		return size;
	}
}




// NODES //

class Sprite : Node
{
	public Sprite() : base(new Component[] { new Transform(), new Texture() } ) {}
}

class Spatial : Node
{
	public Spatial() : base(new Component[] { new Transform() } ) {}
}

class LabelNode : Node
{
	public LabelNode() : base(new Component[] { new Transform(), new Label() } ) {}
}

class UILayoutNode : Node
{
	public UILayoutNode() : base(new Component[] { new Transform(), new UILayout() } ) {}
}