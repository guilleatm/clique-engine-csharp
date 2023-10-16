using CliqueEngine.Nodes;

namespace CliqueEngine;


// COMMON DELEGATES
public delegate void Method();
public delegate void OnNodeEventHandler(Node node);



public partial class Engine
{

	public delegate void OnWindowResizedEventHandler(int width, int height);
	public event OnWindowResizedEventHandler onWindowResized;


	public delegate void OnEngineQuitEventHandler();
	public event OnEngineQuitEventHandler onEngineQuit;

	public delegate void OnClickEventHandler(Vector2f originPosition, Vector2f finalPosition);
	public event OnClickEventHandler onClick;

	public event OnNodeEventHandler onNodeAdded;

	public delegate void OnMouseMovedEventHandler(Vector2f position);
	public event OnMouseMovedEventHandler onMouseMoved;
}
