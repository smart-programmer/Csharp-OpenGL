using System;
using OpenGL;
using Glfw3;

namespace Episode18.entities
{
    public class Camera
    {
        public Vertex3f positin = new Vertex3f(0, 100, 140);
        public float pitch { set; get; } 
        public float yaw { set; get; }
        public float roll { set; get; }

        public Camera() { }

        public void move(Glfw.Window window)
        {

        }
       
    }
}
