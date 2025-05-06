using System.Diagnostics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Hello_Triangle;

public class Game : GameWindow
{
	int VertexBufferObject;
	int ElementBufferObject;
	int VertexArrayObject;
	Shader shader;
	private Stopwatch timer;
	static string AssetsPath = "../../../Assets";
	static string ShadersPath = Path.Combine(AssetsPath, "Shaders");
	
	private readonly float[] vertices =
	{
		// positions        // colors
		0.5f, -0.5f, 0.0f,  1.0f, 0.0f, 0.0f,   // bottom right
		-0.5f, -0.5f, 0.0f,  0.0f, 1.0f, 0.0f,   // bottom left
		0.5f,  0.5f, 0.0f,  0.0f, 0.0f, 1.0f,    // top right
		-0.5f,  0.5f, 0.0f,  1.0f, 1.0f, 0.0f,   // top left
		0.0f, 1.0f, 0.0f,  1.0f, 0.0f, 1.0f,   // top center
	};
	
	uint[] indices =
	[	// note that we start from 0!
		0, 1, 3,   // first triangle
		0, 2, 3,    // second triangle
		2, 3, 4   // Make it a pentagon
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
		GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
		GL.EnableVertexAttribArray(0);

		GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
		GL.EnableVertexAttribArray(1);
		ElementBufferObject = GL.GenBuffer();
		GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
		GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
		shader = new Shader(Path.Combine(ShadersPath, "shader.vert"), Path.Combine(ShadersPath, "shader.frag"));
		shader.Use();

		timer = new Stopwatch();
		timer.Start();
	}
	
	protected override void OnRenderFrame(FrameEventArgs e)
	{
		base.OnRenderFrame(e);

		// render
		// clear the colorbuffer
		GL.Clear(ClearBufferMask.ColorBufferBit);

		// be sure to activate the shader
		shader.Use();

		// update the uniform color
		double timeValue = timer.Elapsed.TotalSeconds;
		float greenValue = (float)Math.Sin(timeValue) / 2.0f + 0.5f;
		int vertexColorLocation = GL.GetUniformLocation(shader.Handle, "ourColor");
		GL.Uniform4(vertexColorLocation, 0.0f, greenValue, 0.0f, 1.0f);


		// now render the triangle
		GL.BindVertexArray(VertexArrayObject);
		GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

		// swap buffers
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