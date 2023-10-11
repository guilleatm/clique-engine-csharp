namespace CliqueEngine;

public unsafe class Node
{

	const int MAX_COMPONENTS = 10;

	int*[] components = new int*[MAX_COMPONENTS];
	int componentCount = 0;

	public Node()
	{
		Engine.instance.AddResource(this);
	}

	public void AddComponent<C>(int* component) where C : IComponent
	{
		((C*) component)->index = componentCount;
		components[componentCount++] = component;
	}

	// void Start()
	// {
	// 	for (int i = 0; i < components.Count; i++)
	// 	{
	// 		components[i].Start();
	// 	}
	// }

	// void Update()
	// {
	// 	for (int i = 0; i < components.Count; i++)
	// 	{
	// 		components[i].Update();
	// 	}
	// }
}


public interface IComponent
{
	public int index { get; set; }
}