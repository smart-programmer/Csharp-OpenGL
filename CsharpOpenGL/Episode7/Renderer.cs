using System;
using OpenGL;
using Episode7.toolbox;
using Episode7.Models;


namespace Episode7
{
    class Renderer
    {
        public void prepare()
        {
            Gl.Clear(ClearBufferMask.ColorBufferBit);
            Gl.ClearColor(1, 0, 0, 1);
        }


        public void render(TexturedModel texturedModel, StaticShader shader)
        {
            RawModel model = texturedModel.rawModel;
            Gl.BindVertexArray(model.vaoID);
            Gl.EnableVertexAttribArray(0);
            Gl.EnableVertexAttribArray(1);
            // uncomment lines to see shape 
            //Matrix4f mat = Maths.createTransformationMatrix(new Vertex3f(0, 0, 0), 0, 0, 0, 1);
            //shader.loadTransformationMatrix(mat);
            Gl.ActiveTexture(TextureUnit.Texture0); // activate texture
            Gl.BindTexture(TextureTarget.Texture2d, texturedModel.modelTexture.textureId); // pass coords
            Gl.DrawElements(PrimitiveType.Triangles, model.vertexCount, DrawElementsType.UnsignedInt, IntPtr.Zero);
            Gl.DisableVertexAttribArray(0);
            Gl.DisableVertexAttribArray(1);
            Gl.BindVertexArray(0);
        }

    }
}
