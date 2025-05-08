// See https://aka.ms/new-console-template for more information

using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Hello_Triangle;

internal abstract class Program
{
	public static void Main(string[] args)
	{
		var nativeWindowSettings = new NativeWindowSettings()
		{
			ClientSize = new Vector2i(800, 600),
			Title = "Hello_Triangle",
			Flags = ContextFlags.ForwardCompatible,
		};
		
		using (var game = new Game(GameWindowSettings.Default, nativeWindowSettings))
		{
			game.Run();
		}
	}
}