using System;
using OpenGL;
using MainProject.Models;


namespace MainProject
{
    class Renderer
    {
        public void prepare()
        {
            Gl.Clear(ClearBufferMask.ColorBufferBit);
            Gl.ClearColor(1, 0, 0, 1);
        }


        public void render(TexturedModel texturedModel)
        {
            RawModel model = texturedModel.rawModel;
            Gl.BindVertexArray(model.vaoID);
            Gl.EnableVertexAttribArray(0);
            Gl.EnableVertexAttribArray(1);
            Gl.ActiveTexture(TextureUnit.Texture0);
            Gl.BindTexture(TextureTarget.Texture2d, texturedModel.modelTexture.textureId);
            Gl.DrawElements(PrimitiveType.Triangles, model.vertexCount, DrawElementsType.UnsignedInt, IntPtr.Zero);
            Gl.DisableVertexAttribArray(0);
            Gl.DisableVertexAttribArray(1);
            Gl.BindVertexArray(0);
        }

    }
}
