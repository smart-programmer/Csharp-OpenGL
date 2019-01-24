﻿using System;
using System.Collections.Generic;


namespace MainProject
{
    class ModelTexture
    {
        public uint textureId { set; get; }

        public float shineDamper = 1;
        public float reflectivity = 0;

        public ModelTexture(uint id)
        {
            textureId = id;
        }
    }
}
