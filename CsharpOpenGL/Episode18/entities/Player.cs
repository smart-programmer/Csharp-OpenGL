using System;
using Episode18.Models;
using OpenGL;
using System.Collections.Generic;
using Glfw3;

namespace Episode18.entities 
{
    class Player : Entity
    {
        private const float RUN_SPEED = 40;
        private const float TURN_SPEED = 160;
        private const float GRAVITY = -110;
        private const float JUMP_POWER = 60;

        private const float TERRAIN_HIGHT = 0;

        private float currentSpeed = 0;
        private float currentTurnSpeed = 0;
        private float upwardsSpeed = 0;

        private bool isInAir = false;

        public Player(TexturedModel model, Vertex3f position, float rotX, float rotY, float rotZ, float scale):base(model,
            position, rotX, rotY, rotZ, scale)
        {
           
        }


        public void move(Glfw.Window window)
        {
            checkInputs(window);
            increaseRotation(0, currentTurnSpeed * Episode18.getFrameTimeSeconds(), 0);
            float distance = currentSpeed * Episode18.getFrameTimeSeconds();
            float dx = (float) (distance * Math.Sin(math.toRadians(rotY)));
            float dz = (float) (distance * Math.Cos(math.toRadians(rotY)));
            increasePosition(-dx, 0, dz);
            upwardsSpeed += GRAVITY * Episode18.getFrameTimeSeconds();
            increasePosition(0, upwardsSpeed * Episode18.getFrameTimeSeconds(), 0);
            if (position.y < TERRAIN_HIGHT)
            {
                upwardsSpeed = 0;
                isInAir = false;
                position.y = TERRAIN_HIGHT;
                
            }
        }

        private void jump()
        {
            if (!isInAir)
            {
                upwardsSpeed += JUMP_POWER;
                isInAir = true;
            }
        }

        public void checkInputs(Glfw.Window window)
        {
            bool W = Glfw.GetKey(window, (int)Glfw.KeyCode.W);
            bool S = Glfw.GetKey(window, (int)Glfw.KeyCode.S);
            bool D = Glfw.GetKey(window, (int)Glfw.KeyCode.D);
            bool A = Glfw.GetKey(window, (int)Glfw.KeyCode.A);
            bool space = Glfw.GetKey(window, (int)Glfw.KeyCode.Space);

            if (W)
            {
                currentSpeed = RUN_SPEED;
            }
            else if (S)
            {
                currentSpeed = -RUN_SPEED;
            }
            else
            {
                currentSpeed = 0;
            }


            if (D)
            {
                currentTurnSpeed = TURN_SPEED;
            }
            else if (A)
            {
                currentTurnSpeed = -TURN_SPEED;
            }
            else
            {
                currentTurnSpeed = 0;
            }

            if (space)
            {
                jump();
            }
        }
    }
}
