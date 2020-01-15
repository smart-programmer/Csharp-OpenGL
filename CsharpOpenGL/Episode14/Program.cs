using System;
using System.Drawing;
using OpenGL;
using Glfw3;
using Episode14.Models;
using Episode14.toolbox;
using Episode14.entities;
using Episode14.terrains;
using System.Collections.Generic;

// NOTE: i increased the ambient light in the terrainFragmentShader and in the fragmentShader

namespace Episode14
{

    class Episode14
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
            RawModel model = OBJLoader.loadObjModel("Spruce", loader);
            ModelTexture texture = new ModelTexture(loader.loadTexture("..\\..\\res/branch.png"));
            TexturedModel staticModel = new TexturedModel(model, texture);
            staticModel.modelTexture.shineDamper = 10;
            staticModel.modelTexture.reflectivity = 0; 
            Light light = new Light(new Vertex3f(0, 0, -25), new Vertex3f(1f, 1f, 1f));


            ModelTexture terrainTexture = new ModelTexture(loader.loadTexture("..\\..\\res/b.jpeg"));
            terrainTexture.shineDamper = 10;
            terrainTexture.reflectivity = 0;
            Terrain terrain = new Terrain(0, -400, loader, terrainTexture);
            //Terrain terrain2 = new Terrain(1, -400, loader, terrainTexture);

            Camera camera = new Camera();

            List<Entity> allShapes = new List<Entity>();

            Random random = new Random();
            for (int i = 0; i < 500; i++)
            {
                double x = random.NextDouble() * 800 - 400;
                double y = 0;
                double z = random.NextDouble() * 800 - 800;
                allShapes.Add(new Entity(staticModel, new Vertex3f((float)x, (float)y, (float)z), 90, 0, 0, 1));
            }

            MasterRenderer renderer = new MasterRenderer(new WinowInfo(width, height));
          
            // Loop until the user closes the window
            while (!Glfw.WindowShouldClose(window))
            {   
                // Render here
                camera.move(window);

                renderer.processTerrain(terrain);
                //renderer.processTerrain(terrain2);

                foreach (Entity shape in allShapes)
                {
                    renderer.processEntity(shape);
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
