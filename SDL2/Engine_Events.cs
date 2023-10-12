namespace CliqueEngine;

public partial class Engine
{
	public delegate void OnWindowResizedEventHandler(int width, int height);
	public event OnWindowResizedEventHandler onWindowResized;


	public delegate void OnEngineQuitEventHandler();
	public event OnEngineQuitEventHandler onEngineQuit;
}
