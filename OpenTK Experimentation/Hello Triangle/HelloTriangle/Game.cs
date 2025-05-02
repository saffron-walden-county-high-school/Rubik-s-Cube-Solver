using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Hello_Triangle;

public class Game : GameWindow
{
	int VertexBufferObject;
	int VertexArrayObject;
	Shader shader;
	static string AssetsPath = "../../../Assets";
	static string ShadersPath = Path.Combine(AssetsPath, "Shaders");
	
	private float[] vertices = [
		-0.5f, -0.5f, 0.0f,
		0.5f, -0.5f, 0.0f,
		0.0f, 0.5f, 0.0f
	];
	public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
		: base(gameWindowSettings, nativeWindowSettings)
	{
	}
	
	protected override void OnLoad()
	{
		base.OnLoad();
		
		GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
		
		// Code Goes Here
		VertexBufferObject = GL.GenBuffer();
		GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
		GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
		VertexArrayObject = GL.GenVertexArray();
		GL.BindVertexArray(VertexArrayObject);
		GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
		GL.EnableVertexAttribArray(0);
		shader = new Shader(Path.Combine(ShadersPath, "shader.vert"), Path.Combine(ShadersPath, "shader.frag"));
		shader.Use();
	}
	
	protected override void OnRenderFrame(FrameEventArgs args)
	{
		base.OnRenderFrame(args);
		
		GL.Clear(ClearBufferMask.ColorBufferBit);
		
		// Code Goes Here
		shader.Use();
		GL.BindVertexArray(VertexArrayObject);
		GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
		SwapBuffers();
	}
	
	
	protected override void OnUpdateFrame(FrameEventArgs args)
	{
		base.OnUpdateFrame(args);
		
		if (KeyboardState.IsKeyDown(Keys.Escape))
		{
			Close();
		}
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