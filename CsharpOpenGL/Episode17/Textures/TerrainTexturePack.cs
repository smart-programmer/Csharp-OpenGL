using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Episode17.Textures
{
    class TerrainTexturePack
    {
        public TerrainTexture backgroundTexture { set; get; }
        public TerrainTexture rTexture { set; get; }
        public TerrainTexture gTexture { set; get; }
        public TerrainTexture bTexture { set; get; }

        public TerrainTexturePack(TerrainTexture backgroundTexture, TerrainTexture rTexture,
            TerrainTexture gTexture, TerrainTexture bTexture)
        {
            this.backgroundTexture = backgroundTexture;
            this.rTexture = rTexture;
            this.gTexture = gTexture;
            this.bTexture = bTexture;
        }
    }
}
