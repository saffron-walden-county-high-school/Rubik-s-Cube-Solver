using System.Diagnostics;
using System.Net.Mime;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Hello_Triangle;

public class Game : GameWindow
{
	
	private readonly float[] vertices =
	{
		// Position        Texture coordinates
		0.5f,  0.5f, 0.0f, 1.0f, 1.0f, // top right
		0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
		-0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
		-0.5f,  0.5f, 0.0f, 0.0f, 1.0f,  // top left
		0.0f, 0.8f, 0.0f, 0.5f, 0.5f, // top center
	};
	
	private readonly uint[] indices =
	[
		// note that we start from 0!
		0, 1, 3,   // first triangle
		1, 2, 3,    // second triangle
		0, 3, 4		// make it a pentagon
	];
	
	int VertexBufferObject;
	int ElementBufferObject;
	int VertexArrayObject;
	
	Shader shader;
	Texture texture0;
	Texture texture1;
	
	static string AssetsPath = "../../../Assets";
	static string ShadersPath = Path.Combine(AssetsPath, "Shaders");
	static string TexturesPath = Path.Combine(AssetsPath, "Textures");
	

	public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
		: base(gameWindowSettings, nativeWindowSettings)
	{
	}
	
	protected override void OnLoad()
	{
		base.OnLoad();
		
		GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
		
		// Code Goes Here
		VertexArrayObject = GL.GenVertexArray();
		GL.BindVertexArray(VertexArrayObject);
		
		VertexBufferObject = GL.GenBuffer();
		GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
		GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
		
		ElementBufferObject = GL.GenBuffer();
		GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
		GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
		
		shader = new Shader(Path.Combine(ShadersPath, "shader.vert"), Path.Combine(ShadersPath, "shader.frag"));
		shader.Use();
		
		int vertexLocation = shader.GetAttribLocation("aPosition");
		GL.EnableVertexAttribArray(vertexLocation);
		GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

		int texCoordLocation = shader.GetAttribLocation("aTexCoord");
		GL.EnableVertexAttribArray(texCoordLocation);
		GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

		texture0 = Texture.LoadFromFile(Path.Combine(TexturesPath, "wall.jpg"));
		texture1 = Texture.LoadFromFile(Path.Combine(TexturesPath, "RedSquare.jpg"));
		shader.SetInt("texture0", 0);
		shader.SetInt("texture1", 1);
		texture0.Use(TextureUnit.Texture0);
	}
	
	protected override void OnRenderFrame(FrameEventArgs e)
	{
		base.OnRenderFrame(e);

		// render
		// clear the colourbuffer
		GL.Clear(ClearBufferMask.ColorBufferBit);
		GL.BindVertexArray(VertexArrayObject);
		
		texture0.Use(TextureUnit.Texture0);
		texture1.Use(TextureUnit.Texture1);
		shader.Use();
		
		GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
		
		// swap buffers
		Context.SwapBuffers();
	}
	
	
	protected override void OnUpdateFrame(FrameEventArgs args)
	{
		base.OnUpdateFrame(args);
		
		if (KeyboardState.IsKeyDown(Keys.Escape))
		{
			Close();
		}
	}

	protected override void OnResize(ResizeEventArgs e)
	{
		base.OnResize(e);
		
		GL.Viewport(0, 0, Size.X, Size.Y);
	}

	protected override void OnUnload()
	{
		GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
		GL.BindVertexArray(0);
		GL.UseProgram(0);
		GL.DeleteBuffer(VertexBufferObject);
		GL.DeleteVertexArray(VertexArrayObject);
		GL.DeleteProgram(shader.Handle);
		base.OnUnload();
	}
}