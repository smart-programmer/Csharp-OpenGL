using System;
using OpenGL;
using Glfw3;



namespace Episode3
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


            Renderer renderer = new Renderer();
            Loader loader = new Loader();

            // set data
            float[] vertices = {
                  -0.5f, 0.5f, 0f,//v0
    		-0.5f, -0.5f, 0f,//v1
    		0.5f, -0.5f, 0f,//v2
    		0.5f, 0.5f, 0f,//v3
    };

            uint[] indices = {
                  0,1,3,//top left triangle (v0, v1, v3)
    		3,1,2//bottom right triangle (v3, v1, v2)
    };


            RawModel model = loader.LoadToVao(vertices, indices);

            // Loop until the user closes the window
            while (!Glfw.WindowShouldClose(window))
            {
                // Render here
                renderer.prepare();
                renderer.render(model);

                //Swap front and back buffers
                Glfw.SwapBuffers(window);

                // Poll for and process events
                Glfw.PollEvents();
            }

            // clean memory
            loader.CleanUp();

            // terminate program
            Glfw.Terminate();
        }
    }
}
