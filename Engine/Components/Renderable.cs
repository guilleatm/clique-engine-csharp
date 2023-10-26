using SDL2;

namespace CliqueEngine.Components;

public class Renderable : Component
{
	public Transform transform = null!;
	public Vector2f size;
	public nint texture;

	public Renderable()
	{
		RenderingService.instance.AddResource(this);
	}

	public void SetTexture(string path)
	{
		texture = RenderingService.instance.CreateTexture(path);

		SDL.SDL_QueryTexture(texture, out uint format, out int access, out int w, out int h);
		size = new Vector2f(w, h);
	}

	public override void OnCreated()
	{
		transform = node.GetComponent<Transform>();
	}

}

// public class Renderable : Behaviour
// {
// 	public Vector2f size;
// 	public nint texture;

// 	public Renderable(string path, Vector2f position) : base()
// 	{
// 		//this.position = position;
// 		SetTexture(path);
// 		RenderingServer.instance.AddResource(this);
// 	}

// 	public Renderable() : base()
// 	{
// 		RenderingServer.instance.AddResource(this);
// 	}

// 	public void SetTexture(string path)
// 	{
// 		this.texture = RenderingServer.instance.CreateTexture(this, path);
// 		SDL.SDL_SetTextureBlendMode(texture, SDL.SDL_BlendMode.SDL_BLENDMODE_MUL);
	
// 		SDL.SDL_QueryTexture(texture, out uint fomat, out int access, out int w, out int h);
// 		this.size = new Vector2f(w, h);
// 	}

// 	// public override void Free()
// 	// {
// 	// 	RenderingServer.instance.FreeResource(this);
// 	// 	base.Free();
// 	// }

// }
