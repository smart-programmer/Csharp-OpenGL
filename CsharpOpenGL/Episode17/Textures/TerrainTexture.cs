using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Episode17.Textures
{
    class TerrainTexture
    {
        public uint textureID { set; get; }

        public TerrainTexture(uint textureID)
        {
            this.textureID = textureID;
        }
    }
}
