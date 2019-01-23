using System;
using Glfw3;
using System.IO;
using System.Drawing;
using OpenGL;


/// <summary>
///  this is a utility file where i implement the functions/classes that are used in the ThinMatrix series and are lwjgl specific.
///  this file may also contain some additional helper functions that are project specific  
/// </summary>


namespace Episode11
{
    class Utils
    {
        public static string[] GetValidShaderStringArray(string fileName)
        {
            string text = File.ReadAllText(fileName);

            return new string[] { text };
        }

        public static Glfw.Image getImage(string path)
        {
            Glfw.Image image = new Glfw.Image();
            Bitmap bitmap = new Bitmap(path);
            image.Pixels = TextureLoader.SetImageBuffer(bitmap);
            image.Height = bitmap.Height;
            image.Width = bitmap.Width;
            return image;
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
            //image.RotateFlip(RotateFlipType.Rotate270FlipY);
            pixeslData = TextureLoader.SetImageBuffer(image);

            // flip buffer if Abgr
            //Array.Reverse(pixeslData);

            textureID = Gl.GenTexture();
            Gl.BindTexture(TextureTarget.Texture2d, textureID);

            int value = Gl.REPEAT;
            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureWrapS, ref value);
            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureWrapT, ref value);

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


        public static byte[] SetImageBuffer(Bitmap image)
        {
            image.RotateFlip(RotateFlipType.Rotate270FlipY);
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


    public class Matrix4f : Matrix4x4
    {
        public Matrix4f(float[] values) : base(values)
        {

        }
        public Matrix4f() : base()
        {

        }
        public static void translate(Vertex3f translation, Matrix4f src, out Matrix4f des)
        {
            Matrix4f translationMatrix = new Matrix4f();
            translationMatrix.SetIdentity();
            float[] matbuffer = translationMatrix.Buffer;
            matbuffer[12] = translation.x;
            matbuffer[13] = translation.y;
            matbuffer[14] = translation.z;
            Matrix tempmat = (Matrix)new Matrix(matbuffer, 4, 4).Multiply(src);
            des = new Matrix4f(tempmat.Buffer);
        }

        public static void rotate(float angle, Vertex3f axis, Matrix4f src, ref Matrix4f dest)
        {
            Matrix4f rotationMatrix = new Matrix4f();
            rotationMatrix.SetIdentity();
            float[] matbuffer = rotationMatrix.Buffer;
            if (axis.x != 0)
            {
                matbuffer[5] = (float)Math.Cos(angle);
                matbuffer[6] = (float)-Math.Sin(angle);
                matbuffer[9] = (float)Math.Sin(angle);
                matbuffer[10] = (float)Math.Cos(angle);
            }
            else if (axis.y != 0)
            {
                matbuffer[0] = (float)Math.Cos(angle);
                matbuffer[2] = (float)Math.Sin(angle);
                matbuffer[8] = (float)-Math.Sin(angle);
                matbuffer[10] = (float)Math.Cos(angle);
            }
            else if (axis.z != 0)
            {
                matbuffer[0] = (float)Math.Cos(angle);
                matbuffer[1] = (float)-Math.Sin(angle);
                matbuffer[4] = (float)Math.Sin(angle);
                matbuffer[5] = (float)Math.Cos(angle);
            }

            // apply rotation to src matrix
            Matrix tempmat = (Matrix)new Matrix(matbuffer, 4, 4).Multiply(src);

            // set dest matrix to the rotated matrix
            dest = new Matrix4f(tempmat.Buffer);
        }

        public static void scale(Vertex3f vector, Matrix4f src, ref Matrix4f dest)
        {
            Matrix4f scalerMatrix = new Matrix4f();
            scalerMatrix.SetIdentity();
            float[] matbuffer = scalerMatrix.Buffer;
            matbuffer[0] = vector.x;
            matbuffer[5] = vector.y;
            matbuffer[10] = vector.z;
            Matrix tempmat = (Matrix)new Matrix(matbuffer, 4, 4).Multiply(src);
            dest = new Matrix4f(tempmat.Buffer);
        }
    }


    class math
    {
        public static float toRadians(float angle)
        {
            return (angle) * ((float)Math.PI / 180);
        }
    }


    public class WinowInfo
    {
        public float windowWidth { set; get; }
        public float windowHeight { set; get; }

        public WinowInfo(float width, float height)
        {
            windowWidth = width;
            windowHeight = height;
        }
    }

}
