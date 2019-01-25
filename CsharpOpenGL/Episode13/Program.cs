using System;
using System.Drawing;
using OpenGL;
using Glfw3;
using Epsiode13.Models;
using Epsiode13.toolbox;
using Epsiode13.entities;
using System.Collections.Generic;



namespace Epsiode13
{

    class Epsiode13
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
            var window = Glfw.CreateWindow(width, height, "Unreal Engine 4");

           
            if (!window)
            {
                Glfw.Terminate();
                Environment.Exit(-1);
            }

            // set window icon
            Glfw.Image icon = Utils.getImage("..\\..\\res/logo.png");
            Glfw.SetWindowIcon(window, icon);

            // Make the window's context current
            Glfw.MakeContextCurrent(window);

            // create loder and renderer
            Loader loader = new Loader();


            // create model

            RawModel model = OBJLoader.loadObjModel("cube", loader);
            ModelTexture texture = new ModelTexture(loader.loadTexture("..\\..\\res/rgba.png"));
            TexturedModel staticModel = new TexturedModel(model, texture);
            staticModel.modelTexture.shineDamper = 10;
            staticModel.modelTexture.reflectivity = 1;
            Entity entity = new Entity(staticModel, new Vertex3f(0, 0, -25), 0, 0, 0, 2);
            Light light = new Light(new Vertex3f(0, 0, -20), new Vertex3f(1f, 1f, 1f));

            Camera camera = new Camera();

            List<Entity> allCubes = new List<Entity>();

            Random random = new Random();
            for (int i = 0; i < 200; i++)
            {
                double x = random.NextDouble() * 100 - 50;
                double y = random.NextDouble() * 100 - 50;
                double z = random.NextDouble() * -300;
                allCubes.Add(new Entity(staticModel, new Vertex3f((float)x, (float)y, (float)z), (float)random.NextDouble() * 180,
                    (float)random.NextDouble() * 180, 0, 1));
            }

            MasterRenderer renderer = new MasterRenderer(new WinowInfo(width, height));
          
            // Loop until the user closes the window
            while (!Glfw.WindowShouldClose(window))
            {
                camera.move(window);

                // Render here

                foreach (Entity cube in allCubes)
                {
                    renderer.processEntity(cube);
                }
              
                renderer.render(light, camera);

                //Swap front and back buffers
                Glfw.SwapBuffers(window);

                // Poll for and process events
                Glfw.PollEvents();
            }

            // clean memory
            renderer.cleanUP();
            loader.CleanUp();

            // terminate program
            Glfw.Terminate();
        }
    }
}
