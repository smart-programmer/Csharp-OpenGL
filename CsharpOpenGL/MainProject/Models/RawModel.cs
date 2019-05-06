using System;
using System.Collections.Generic;


namespace MainProject.Models
{
    public class RawModel
    {
        public uint vaoID { set; get; } 
        public int vertexCount { set; get; } 


        public RawModel(uint VAO_ID, int VERTEX_COUNT)
        {
            this.vaoID = VAO_ID;
            this.vertexCount = VERTEX_COUNT;
        }
    }
}
