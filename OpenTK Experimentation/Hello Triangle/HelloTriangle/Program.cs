// See https://aka.ms/new-console-template for more information

using OpenTK.Windowing.Desktop;

namespace Hello_Triangle;

internal abstract class Program
{
	public static void Main(string[] args)
	{
		using (Game game = new Game(GameWindowSettings.Default, NativeWindowSettings.Default))
		{
			game.Run();
		}
	}
}