using System;
using OpenGL;

namespace Episode5
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
            Gl.DrawElements(PrimitiveType.Triangles, model.vertexCount, DrawElementsType.UnsignedInt, IntPtr.Zero);
            Gl.DisableVertexAttribArray(0);
            Gl.BindVertexArray(0);
        }

    }
}
