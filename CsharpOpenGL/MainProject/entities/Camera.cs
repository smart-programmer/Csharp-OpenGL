using System;
using OpenGL;
using Glfw3;

namespace MainProject.entities
{
    public class Camera
    {
        public Vertex3f positin = new Vertex3f(0, 0, 0);
        public float pitch { set; get; } // how high or low the camera is aiming
        public float yaw { set; get; } // how much left or right the camera is aiming
        public float roll { set; get; } // how much the camera is tilted

        public Camera() { }

        public void move(Glfw.Window window)
        {
            bool W = Glfw.GetKey(window, (int)Glfw.KeyCode.W);
            bool D = Glfw.GetKey(window, (int)Glfw.KeyCode.D);
            bool A = Glfw.GetKey(window, (int)Glfw.KeyCode.A);
            bool S = Glfw.GetKey(window, (int)Glfw.KeyCode.S);
            bool space = Glfw.GetKey(window, (int)Glfw.KeyCode.Space);
            bool c = Glfw.GetKey(window, (int)Glfw.KeyCode.C);

            if (W)
            {
                positin.z -= 0.1f;
            }
            if (S)
            {
                positin.z += 0.1f;
            }
            if (D)
            {
                positin.x += 0.1f;
            }
            if (A)
            {
                positin.x -= 0.1f;
            }
            if (space)
            {
                positin.y += 0.1f;
            }
            if (c)
            {
                positin.y -= 0.1f;
            }

        }
       
    }
}
