using System;
using OpenGL;
using Glfw3;
using Episode8.Models;
using Episode8.toolbox;
using Episode8.entities;



namespace Episode8
{

    class Episode8
    {
        public static int width = 1221, height = 666;

        static void Main(string[] args)
        {
            // Initialize OpenGL
            Gl.Initialize();
            
            // If the library isn't in the environment path we need to set it
            Glfw.ConfigureNativesDirectory("..\\..\\libs/glfw-3.2.1.bin.WIN32/lib-mingw");

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

            // create shaders after creating opengl context
            StaticShader shader = new StaticShader();


            // create loder and renderer
            Loader loader = new Loader();
            Renderer renderer = new Renderer(shader, new WinowInfo(width, height));

            // set data

            float[] vertices = {
                -0.5f,0.5f,-0.5f,
                -0.5f,-0.5f,-0.5f,
                0.5f,-0.5f,-0.5f,
                0.5f,0.5f,-0.5f,

                -0.5f,0.5f,0.5f,
                -0.5f,-0.5f,0.5f,
                0.5f,-0.5f,0.5f,
                0.5f,0.5f,0.5f,

                0.5f,0.5f,-0.5f,
                0.5f,-0.5f,-0.5f,
                0.5f,-0.5f,0.5f,
                0.5f,0.5f,0.5f,

                -0.5f,0.5f,-0.5f,
                -0.5f,-0.5f,-0.5f,
                -0.5f,-0.5f,0.5f,
                -0.5f,0.5f,0.5f,

                -0.5f,0.5f,0.5f,
                -0.5f,0.5f,-0.5f,
                0.5f,0.5f,-0.5f,
                0.5f,0.5f,0.5f,

                -0.5f,-0.5f,0.5f,
                -0.5f,-0.5f,-0.5f,
                0.5f,-0.5f,-0.5f,
                0.5f,-0.5f,0.5f

        };

            float[] textureCoords = {

                0,0,
                0,1,
                1,1,
                1,0,
                0,0,
                0,1,
                1,1,
                1,0,
                0,0,
                0,1,
                1,1,
                1,0,
                0,0,
                0,1,
                1,1,
                1,0,
                0,0,
                0,1,
                1,1,
                1,0,
                0,0,
                0,1,
                1,1,
                1,0


        };

            uint[] indices = {
                0,1,3,
                3,1,2,
                4,5,7,
                7,5,6,
                8,9,11,
                11,9,10,
                12,13,15,
                15,13,14,
                16,17,19,
                19,17,18,
                20,21,23,
                23,21,22

        };

            // create model
            RawModel model = loader.LoadToVao(vertices, textureCoords, indices);
            ModelTexture texture = new ModelTexture(loader.loadTexture("..\\..\\res/c.jpeg"));
            TexturedModel staticModel = new TexturedModel(model, texture);
            
            Entity entity = new Entity(staticModel, new Vertex3f(0, 0, -5), 0, 0, 0, 1);
           

            Camera camera = new Camera();

            // Loop until the user closes the window
            while (!Glfw.WindowShouldClose(window))
            {
                entity.increaseRotation(1, 1, 0);
                
                // Render here
                camera.move(window);
                renderer.prepare();
                shader.start();
                shader.loadViewMatrix(camera);
                renderer.render(entity, shader);
                shader.stop();

                //Swap front and back buffers
                Glfw.SwapBuffers(window);

                // Poll for and process events
                Glfw.PollEvents();
            }

            // clean memory
            shader.cleanUP();
            loader.CleanUp();

            // terminate program
            Glfw.Terminate();
        }
    }
}
