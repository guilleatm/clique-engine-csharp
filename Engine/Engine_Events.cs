namespace CliqueEngine;


// COMMON DELEGATES
public delegate void Method();


public partial class Engine
{

	public delegate void OnWindowResizedEventHandler(int width, int height);
	public event OnWindowResizedEventHandler onWindowResized;


	public delegate void OnEngineQuitEventHandler();
	public event OnEngineQuitEventHandler onEngineQuit;

	public delegate void OnClickEventHandler(Vector2f originPosition, Vector2f finalPosition);
	public event OnClickEventHandler onClick;
}
