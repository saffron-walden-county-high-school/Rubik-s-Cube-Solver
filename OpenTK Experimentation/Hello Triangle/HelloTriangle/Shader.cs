using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Hello_Triangle;

public class Shader
{
	public int Handle;

	public Shader(string vertexPath, string fragmentPath)
	{
		string vertexShaderSource = File.ReadAllText(vertexPath);

		string fragmentShaderSource = File.ReadAllText(fragmentPath);
		
		int vertexShader = GL.CreateShader(ShaderType.VertexShader);
		GL.ShaderSource(vertexShader, vertexShaderSource);

		int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
		GL.ShaderSource(fragmentShader, fragmentShaderSource);

		GL.CompileShader(vertexShader);

		GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out int vertSuccess);
		if (vertSuccess == 0)
		{
			string infoLog = GL.GetShaderInfoLog(vertexShader);
			Console.WriteLine(infoLog);
		}

		
		GL.CompileShader(fragmentShader);

		GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out int fragSuccess);
		if (fragSuccess == 0)
		{
			string infoLog = GL.GetShaderInfoLog(fragmentShader);
			Console.WriteLine(infoLog);
		}
		
		Handle = GL.CreateProgram();

		GL.AttachShader(Handle, vertexShader);
		GL.AttachShader(Handle, fragmentShader);

		GL.LinkProgram(Handle);

		GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out int handleSuccess);
		if (handleSuccess == 0)
		{
			string infoLog = GL.GetProgramInfoLog(Handle);
			Console.WriteLine(infoLog);
		}
		
		GL.DetachShader(Handle, vertexShader);
		GL.DetachShader(Handle, fragmentShader);
		GL.DeleteShader(fragmentShader);
		GL.DeleteShader(vertexShader);
	}
	public void Use()
	{
		GL.UseProgram(Handle);
	}
	
	private bool disposedValue = false;

	protected virtual void Dispose(bool disposing)
	{
		if (disposedValue) return;
		GL.DeleteProgram(Handle);

		disposedValue = true;
	}

	~Shader()
	{
		if (disposedValue == false)
		{
			Console.WriteLine("GPU Resource leak! Did you forget to call Dispose()?");
		}
	}


	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}
}

