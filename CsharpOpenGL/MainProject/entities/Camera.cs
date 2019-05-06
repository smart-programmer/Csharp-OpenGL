using System;
using OpenGL;
using Glfw3;

namespace MainProject.entities
{
    public class Camera
    {
        private float distanceFromPlayer = 50;
        private float angleAroundPlayer = 0;

        public Vertex3f positin = new Vertex3f(0, 100, 140);
        public float pitch { set; get; } // how high or low the camera is aiming
        public float yaw { set; get; } // how much left or right the camera is aiming
        public float roll { set; get; } // how much the camera is tilted

        private Player player;

        private Glfw.Window window;
        private WinowInfo winowInfo;

        public Camera(Player player, Glfw.Window window, WinowInfo winow)
        {
            this.player = player;
            this.window = window;
            this.winowInfo = winowInfo;
        }

        public void move()
        {
            //bool W = Glfw.GetKey(window, (int)Glfw.KeyCode.Up);
            //bool D = Glfw.GetKey(window, (int)Glfw.KeyCode.Right);
            //bool A = Glfw.GetKey(window, (int)Glfw.KeyCode.Left);
            //bool S = Glfw.GetKey(window, (int)Glfw.KeyCode.Down);
            //bool space = Glfw.GetKey(window, (int)Glfw.KeyCode.RightControl);
            //bool c = Glfw.GetKey(window, (int)Glfw.KeyCode.C);

            //if (W)
            //{
            //    positin.z -= 2f;
            //}
            //if (S)
            //{
            //    positin.z += 2f;
            //}
            //if (D)
            //{
            //    positin.x += 2f;
            //}
            //if (A)
            //{
            //    positin.x -= 2f;
            //}
            //if (space)
            //{
            //    positin.y += 2f;
            //}
            //if (c)
            //{
            //    positin.y -= 2f;
            //}
            setScrollFunction(window);
            calculatePitch();
            calculateAngleAroundPlayer();
            float horizontalDistance = calculateHorizontalDistance();
            float verticalDistance = calculateVerticalDistance();
            calculateCameraPosition(horizontalDistance, verticalDistance);
            yaw = 180 - (player.rotY + angleAroundPlayer);
        }

        private void calculateCameraPosition(float horizontalDistance, float verticalDistance)
        {
            float theta = player.rotY + angleAroundPlayer;
            float offsetX = (float)(horizontalDistance * Math.Sin(math.toRadians(theta)));
            float offsetZ = (float)(horizontalDistance * Math.Cos(math.toRadians(theta)));
            positin.x = player.position.x - offsetX;
            positin.z = player.position.z - offsetZ;
            positin.y = player.position.y + verticalDistance;
        }

        private float calculateHorizontalDistance()
        {
            return (float)(distanceFromPlayer * Math.Cos(math.toRadians(pitch)));
        }

        private float calculateVerticalDistance()
        {
            return (float)(distanceFromPlayer * Math.Sin(math.toRadians(pitch)));
        }

        private void calculateZoom(double yoffset)
        {
            float zoomLevel = (float)yoffset;
            distanceFromPlayer -= zoomLevel;
            Console.WriteLine(zoomLevel);
        }

        private void calculatePitch()
        {
            if (Glfw.GetMouseButton(window, Glfw.MouseButton.Button1))
            {
                double xPos;
                double yPos;
                Glfw.GetCursorPos(window, out xPos, out yPos);
                Console.WriteLine(xPos + "  " + yPos);
                float pitchChange = (float)yPos * 0.01f;
                pitch -= pitchChange;
            }
        }

        private void calculateAngleAroundPlayer()
        {
            if (Glfw.GetMouseButton(window, Glfw.MouseButton.Button2))
            {
                double xPos;
                double yPos;
                Glfw.GetCursorPos(window, out xPos, out yPos);
                float angleChange = (float)xPos * 0.004f;
                angleAroundPlayer -= angleChange;
            }
        }

        // Glfw functions

        public void scrollInput(Glfw.Window window, double xoffset, double yoffset)
        {
            calculateZoom(yoffset);
        }

        public void setScrollFunction(Glfw.Window window)
        {
            Glfw.SetScrollCallback(window, scrollInput);
        }

    }
}
