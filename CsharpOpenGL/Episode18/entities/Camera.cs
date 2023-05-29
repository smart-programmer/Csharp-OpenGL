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
        private const float move_speed = 4.0f;

        public Camera() { }

        public void move(Glfw.Window window)
        {
            bool forward = Glfw.GetKey(window, (int)Glfw.KeyCode.Up);
            bool back = Glfw.GetKey(window, (int)Glfw.KeyCode.Down);
            bool right = Glfw.GetKey(window, (int)Glfw.KeyCode.Right);
            bool left = Glfw.GetKey(window, (int)Glfw.KeyCode.Left);
            bool rctrl = Glfw.GetKey(window, (int)Glfw.KeyCode.RightControl);
            bool C = Glfw.GetKey(window, (int)Glfw.KeyCode.C);

            if (forward)
            {
                this.positin.z -= move_speed;
            }
            if (back)
            {
                this.positin.z += move_speed;
            }
            if (right)
            {
                this.positin.x += move_speed;
            }
            if (left)
            {
                this.positin.x -= move_speed;
            }
            if (rctrl)
            {
                this.positin.y += move_speed;
            }
            if (C)
            {
                this.positin.y -= move_speed;
            }


        }
       
    }
}
