// using SDL2;

// namespace CliqueEngine.Nodes;

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
