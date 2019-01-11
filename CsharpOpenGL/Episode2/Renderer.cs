using System;
using OpenGL;


namespace Episode2
{
    class Renderer
    {
        public void prepare()
        {
            Gl.Clear(ClearBufferMask.ColorBufferBit);
            Gl.ClearColor(1, 0, 0, 1);
        }


        public void render(RawModel model)
        {
            Gl.BindVertexArray(model.vaoID);
            Gl.EnableVertexAttribArray(0);
            Gl.DrawArrays(PrimitiveType.Triangles, 0, model.vertexCount);
            Gl.DisableVertexAttribArray(0);
            Gl.BindVertexArray(0);
        }

    }
}
