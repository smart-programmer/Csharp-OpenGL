﻿using System;
using OpenGL;
using Glfw3;

namespace Episode14.entities
{
    public class Camera
    {
        public Vertex3f positin = new Vertex3f(0, 5, -10);
        public float pitch { set; get; } 
        public float yaw { set; get; } 
        public float roll { set; get; } 

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
                positin.z -= 1.7f;
            }
            if (S)
            {
                positin.z += 1.7f;
            }
            if (D)
            {
                positin.x += 1.7f;
            }
            if (A)
            {
                positin.x -= 1.7f;
            }
            if (space)
            {
                positin.y += 1.7f;
            }
            if (c)
            {
                positin.y -= 1.7f;
            }

        }
       
    }
}
