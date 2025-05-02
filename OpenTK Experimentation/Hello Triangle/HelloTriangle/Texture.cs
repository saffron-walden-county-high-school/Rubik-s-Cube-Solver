using System;
using OpenTK.Graphics.OpenGL;
using StbImageSharp;

namespace Hello_Triangle;

public class Texture
{
	private int Handle;
	private string path = "Assets/Textures/wall.jpg";

	// public Texture()
	// {
	// 	Handle = GL.GenTexture();
	// 	GL.BindTexture(TextureTarget.Texture2D, Handle);
	// 	
	// 	// stb_image loads from the top-left pixel, whereas OpenGL loads from the bottom-left, causing the texture to be flipped vertically.
	// 	// This will correct that, making the texture display properly.
	// 	StbImage.stbi_set_flip_vertically_on_load(1);
	// 	
	// 	// Load the image.
	// 	ImageResult image = ImageResult.FromStream(File.OpenRead(path), ColorComponents.RedGreenBlueAlpha);
	// 	
	// 	GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
	// }
}