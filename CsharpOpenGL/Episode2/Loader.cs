using System;
using OpenGL;
using System.Collections.Generic;


namespace Episode2
{
    class Loader
    {
        private List<uint> VAOS = new List<uint>();
        private List<uint> VBOS = new List<uint>();

        public RawModel LoadToVao(float[] positions)
        {
            uint vaoID = this.CreateVao();
            this.StoreDataInAttributeList(0, positions);
            this.unbindVao();
            return new RawModel(vaoID, positions.Length / 3);
        }


        private uint CreateVao()
        {
            uint vaoID = Gl.GenVertexArray();
            VAOS.Add(vaoID);
            Gl.BindVertexArray(vaoID);
            return vaoID;
        }


        private void StoreDataInAttributeList(uint attribute_number, float[] data)
        {
            uint vboID = Gl.GenBuffer();
            VBOS.Add(vboID);
            Gl.BindBuffer(BufferTarget.ArrayBuffer, vboID);
            Gl.BufferData(BufferTarget.ArrayBuffer, (uint)(data.Length * sizeof(float)), data, BufferUsage.StaticDraw);
            Gl.VertexAttribPointer(attribute_number, 3, VertexAttribType.Float, false, 0, IntPtr.Zero);
            Gl.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        private void unbindVao()
        {
            Gl.BindVertexArray(0);
        }


        public void CleanUp()
        {
            uint[] vaosArray = VAOS.ToArray();

            Gl.DeleteVertexArrays(vaosArray);

            uint[] vbosArray = VBOS.ToArray();

            Gl.DeleteBuffers(vbosArray);
        }
    }
}
