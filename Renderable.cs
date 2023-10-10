using SDL2;

namespace CliqueEngine;

public class Renderable : Node
{
	public Vector2f position;
	public Vector2f size;
	public nint texture;


	public Renderable(string path, Vector2f position, Vector2f size) : base()
	{
		this.position = position;
		this.size = size;
		RenderingServer.instance.CreateResource(this, path, out texture);

		SDL.SDL_SetTextureBlendMode(texture, SDL.SDL_BlendMode.SDL_BLENDMODE_NONE);

		RenderingServer.instance.AddResource(this);
	}

}
