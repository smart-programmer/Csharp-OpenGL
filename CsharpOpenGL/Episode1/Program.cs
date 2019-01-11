using System;
using OpenGL;
using Glfw3;


namespace Episode1
{
    public class MainClass
    {

        public static int width = 1200, height = 700;

        static void Main(string[] args)
        {
            // Initialize OpenGL
            Gl.Initialize();
           

            // If the library isn't in the environment path we need to set it
            Glfw.ConfigureNativesDirectory("..\\..\\libs");

            // Initialize the GLFW
            if (!Glfw.Init())
                Environment.Exit(-1);

            // Create a windowed mode window and its OpenGL context
            var window = Glfw.CreateWindow(width, height, "OpenGL/Glfw");
            if (!window)
            {
                Glfw.Terminate();
                Environment.Exit(-1);
            }

            // Make the window's context current
            Glfw.MakeContextCurrent(window);



            // Loop until the user closes the window
            while (!Glfw.WindowShouldClose(window))
            {
                // Render here
                ////

                //Swap front and back buffers
                Glfw.SwapBuffers(window);

                // Poll for and process events
                Glfw.PollEvents();
            }

         
            // terminate program
            Glfw.Terminate();
        }
    }
}
