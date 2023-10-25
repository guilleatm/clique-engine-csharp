using CliqueEngine.Nodes;

public interface IComponent
{
	public Node node { get; set; }

	/// <summary>
	/// Called when Node constructor has finished and all components are created.
	/// From here, it is safe to access other Node Components
	/// </summary>
	public void Created() {}
}