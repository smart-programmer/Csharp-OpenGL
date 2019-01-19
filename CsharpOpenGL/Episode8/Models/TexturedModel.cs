using System;

namespace Episode8.Models
{
    class TexturedModel
    {
        public RawModel rawModel { set; get; }
        public ModelTexture modelTexture { set; get; }

        public TexturedModel(RawModel model, ModelTexture texture)
        {
            rawModel = model;
            modelTexture = texture;
        }
    }
}
