using System;
using OpenGL;
using Episode7.Models;
using System.Collections.Generic;

namespace Episode7
{
    class Loader
    {
        private List<uint> VAOS = new List<uint>();
        private List<uint> VBOS = new List<uint>();
        private List<uint> TEXTURES = new List<uint>();

        public RawModel LoadToVao(float[] positions, float[] textureCoords, uint[] indices)
        {
            uint vaoID = this.CreateVao();
            bindindicesBuffer(indices);
            this.StoreDataInAttributeList(0, 3, positions);
            this.StoreDataInAttributeList(1, 2, textureCoords);
            this.unbindVao();
            return new RawModel(vaoID, indices.Length);
        }


        private uint CreateVao()
        {
            uint vaoID = Gl.GenVertexArray();
            VAOS.Add(vaoID);
            Gl.BindVertexArray(vaoID);
            
            return vaoID;
        }
        

        private void StoreDataInAttributeList(uint attribute_number, int coordinateSize, float[] data)
        {
            uint vboID = Gl.GenBuffer();
            VBOS.Add(vboID);
            Gl.BindBuffer(BufferTarget.ArrayBuffer, vboID);
            Gl.BufferData(BufferTarget.ArrayBuffer, (uint)(data.Length * sizeof(float)), data, BufferUsage.StaticDraw); 
            Gl.VertexAttribPointer(attribute_number, coordinateSize, VertexAttribType.Float, false, 0, IntPtr.Zero); 
            Gl.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        private void unbindVao()
        {
            Gl.BindVertexArray(0);
        }

        private void bindindicesBuffer(uint[] indices)
        {
            uint vboId = Gl.GenBuffer();
            VBOS.Add(vboId);
            Gl.BindBuffer(BufferTarget.ElementArrayBuffer, vboId);
            Gl.BufferData(BufferTarget.ElementArrayBuffer, (uint)(indices.Length * sizeof(uint)), indices, BufferUsage.StaticDraw);
        }

        public uint loadTexture(string path)
        {
            Texture texture = null;
            try
            {
                texture = TextureLoader.getTexture(path);
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            uint textureID = texture.textureID;
            TEXTURES.Add(textureID);
            return textureID;
        }


        public void CleanUp()
        {
            uint[] vaosArray = VAOS.ToArray();

            Gl.DeleteVertexArrays(vaosArray);

            uint[] vbosArray = VBOS.ToArray();

            Gl.DeleteBuffers(vbosArray);

            uint[] texturesArray = TEXTURES.ToArray();

            Gl.DeleteTextures(texturesArray);
            
        }
    }
}
