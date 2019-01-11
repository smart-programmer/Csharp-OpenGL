using System;
using System.IO;
using System.Drawing;
using OpenGL;

namespace MainProject
{
    class Utils
    {
        public static string[] GetValidShaderStringArray(string fileName)
        {
            string text = File.ReadAllText(fileName);

            return new string[] { text };
        }
    }


    public class Texture
    {
        public Bitmap image { set; get; }
        public byte[] pixeslData { set; get; }
        public uint textureID { set; get; }


        public Texture(Bitmap bitmap)
        {
            this.image = bitmap;
            this.pixeslData = new byte[image.Height * image.Width * 4];
            //int index = 0;
            //for (int i = 0; i < image.Width; i++)
            //{
            //    for (int j = 0; j < image.Height; j++)
            //    {
            //        Color pixel= image.GetPixel(i, j);
            //        byte red = pixel.R;
            //        byte green = pixel.G;
            //        byte blue = pixel.B;
            //        byte Alpha = pixel.A;


            //        pixeslData[index] = red;
            //        index++;
            //        pixeslData[index] = green;
            //        index++;
            //        pixeslData[index] = blue;
            //        index++;
            //        pixeslData[index] = Alpha;
            //        index++;
            //    }
            //}
            pixeslData = TextureLoader.SetImageBuffer(image);

            // flip buffer
            //index = 0;
            //for (int i = pixeslData.Length; i > 0; i--)
            //{
            //    pixeslData[i - 1] = pixeslData[index];
            //    index++;
            //}
            textureID = Gl.GenTexture();
            Gl.BindTexture(TextureTarget.Texture2d, textureID);
            int value = Gl.REPEAT;
            //Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureWrapS, ref value);
            //Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureWrapT, ref value);
            value = Gl.NEAREST;
            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMinFilter, ref value);
            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMagFilter, ref value);
            Gl.GenerateMipmap(TextureTarget.Texture2d);
            Gl.TexImage2D(TextureTarget.Texture2d, 0, InternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pixeslData);
            Gl.BindTexture(TextureTarget.Texture2d, 0);
        }
    }


    public class TextureLoader
    {
        public static Texture getTexture(string path)
        {
            return new Texture(new Bitmap(path));
        }

        public static byte[] ReadByte(Bitmap image)
        {
            var bitmapData = image.LockBits(new System.Drawing.Rectangle(0, 0, image.Width, image.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, image.PixelFormat);
            var length = bitmapData.Stride * bitmapData.Height;
            byte[] bytes = new byte[length];
            System.Runtime.InteropServices.Marshal.Copy(bitmapData.Scan0, bytes, 0, length);
            image.UnlockBits(bitmapData);
            return bytes;
        }

        public static byte[] SetImageBuffer(Bitmap image)
        {
            byte[] data = new byte[image.Width * image.Height * 4]; // 4 is because width * height == nuumber of pixels in image but every pixel has 4 bytes which are Red Green Blue Alpha
            int index = 0;
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    Color pixel = image.GetPixel(i, j);
                    byte red = pixel.R;
                    byte green = pixel.G;
                    byte blue = pixel.B;
                    byte Alpha = pixel.A;


                    data[index] = red;
                    index++;
                    data[index] = green;
                    index++;
                    data[index] = blue;
                    index++;
                    data[index] = Alpha;
                    index++;
                }
            }

            return data;
        }
    }
}
