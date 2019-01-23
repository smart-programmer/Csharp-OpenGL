using System;
using OpenGL;

namespace MainProject.entities
{
    public class Light
    {
        public Vertex3f position { set; get; }
        public Vertex3f colour { set; get; }

        public Light (Vertex3f Position, Vertex3f Colour)
        {
            position = Position;
            colour = Colour;
        }
    }
}
