using System;
using MainProject.Textures;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject.Models
{
    public class TexturedModel
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
