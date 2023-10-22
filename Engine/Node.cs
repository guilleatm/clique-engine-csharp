using System.Linq.Expressions;
using CliqueEngine.Extensions;
using SDL2;

namespace CliqueEngine.Nodes;

public class Node
{
	public WindowBase window;
	List<Component> components;

	public Node()
	{
		components = new List<Component>();
	}

	public void Start()
	{
		for (int i = 0; i < components.Count; i++)
		{
			components[i].Start();
		}		
	}
	public void Update()
	{
		for (int i = 0; i < components.Count; i++)
		{
			components[i].Update();
		}	
	}

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

public class Sprite : Component
{
	nint texture;
	Vector2f size;

	Transform transform = null!;
	
	public Sprite()
	{

	}

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