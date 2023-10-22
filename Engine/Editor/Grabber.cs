// using CliqueEngine.Extensions;
// using CliqueEngine.Nodes;
// using SDL2;

// namespace CliqueEngine.Editor;

// class Grabber : Renderable
// {
// 	Node node;
// 	bool selected = false;

// 	public Grabber(Node n) : base("Engine/Editor/2d_handle.png", n.position)
// 	{
// 		node = n;
// 		Engine.instance.onClick += OnClick;
// 		Engine.instance.onMouseMoved += OnMove;
// 	}

// 	void OnClick(Vector2f originPosition, Vector2f finalPosition)
// 	{
// 		SDL.SDL_Rect rect = new SDL.SDL_Rect().From(position, size);
// 		SDL.SDL_Rect click = new SDL.SDL_Rect().From(originPosition, (finalPosition - originPosition).Abs());


// 		selected = rect.Contains(click);
// 	}

// 	void OnMove(Vector2f mousePosition)
// 	{
// 		position = node.position;

// 		if (!selected) return;

// 		node.position = mousePosition;
// 	}

// }