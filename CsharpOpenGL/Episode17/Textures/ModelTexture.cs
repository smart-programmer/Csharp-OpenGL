using System;
using System.Collections.Generic;


namespace Episode17.Textures
{
    class ModelTexture
    {
        public uint textureId { set; get; }

        public float shineDamper = 1;
        public float reflectivity = 0;

        public bool isHasTransparency = false;
        public bool isUseFakeLighting = false;

        public ModelTexture(uint id)
        {
            textureId = id;
        }
    }
}
