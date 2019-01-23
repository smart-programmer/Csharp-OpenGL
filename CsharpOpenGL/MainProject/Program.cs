using System;
using System.Drawing;
using OpenGL;
using Glfw3;
using MainProject.Models;
using MainProject.toolbox;
using MainProject.entities;



namespace MainProject
{

    class MainClass
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

            // create shaders after creating opengl context
            StaticShader shader = new StaticShader();


            // create loder and renderer
            Loader loader = new Loader();
            Renderer renderer = new Renderer(shader, new WinowInfo(width, height));
            
            // create model
            RawModel model = OBJLoader.loadObjModel("stall", loader);
            ModelTexture texture = new ModelTexture(loader.loadTexture("..\\..\\res/stallTexture.png"));
            TexturedModel staticModel = new TexturedModel(model, texture);
            
            Entity entity = new Entity(staticModel, new Vertex3f(0, 0, -25), 0, 0, 0, 2);
            Light light = new Light(new Vertex3f(0, 0, -20), new Vertex3f(1f, 1f, 1f));
            //Entity entity2 = new Entity(staticModel, new Vertex3f(-10, 40, -3), 0, 0, 0, 1);
            //Entity entity3 = new Entity(staticModel, new Vertex3f(20, -30, -20), 0, 0, 0, 1);
            //Entity entity4 = new Entity(staticModel, new Vertex3f(-24, -1, -10), 0, 0, 0, 1);
            //Entity entity5 = new Entity(staticModel, new Vertex3f(40, 10, -17), 0, 0, 0, 1);
            //Entity entity6 = new Entity(staticModel, new Vertex3f(20, -50, -30), 0, 0, 0, 1);
            //Entity entity7 = new Entity(staticModel, new Vertex3f(10, 30, -2), 0, 0, 0, 1);
            //Entity entity8 = new Entity(staticModel, new Vertex3f(-30, -20, -21), 0, 0, 0, 1);
            //Entity entity9 = new Entity(staticModel, new Vertex3f(-30, 2, -30), 0, 0, 0, 1);
            //Entity entity10 = new Entity(staticModel, new Vertex3f(20, -30, -6), 0, 0, 0, 1);
            //Random rand = new Random();
            //int r = rand.Next(0, 1);
            //int g = rand.Next(0, 1);
            //int b = rand.Next(0, 1);

            Camera camera = new Camera();
            // Loop until the user closes the window
            while (!Glfw.WindowShouldClose(window))
            {
                //bool W = Glfw.GetKey(window, (int)Glfw.KeyCode.W);
                //entity.increasePosition(0, 0, -0.1f);
                entity.increaseRotation(0, 1, 0);
                //entity2.increaseRotation(1, 0, 1);
                //entity3.increaseRotation(1, 1, 1);
                //entity4.increaseRotation(1, 1, 1);
                //entity5.increaseRotation(1, 2, 1);
                //entity6.increaseRotation(3, 1, 1);
                //entity7.increaseRotation(1, 0, 2);
                //entity8.increaseRotation(0, 0, 1);
                //entity9.increaseRotation(1, 1, 0);
                //entity10.increaseRotation(1, 0, 1);
                // Render here
                camera.move(window);
                renderer.prepare();
                shader.start();
                shader.loadLight(light);
                shader.loadViewMatrix(camera);
                renderer.render(entity, shader);
                //renderer.render(entity2, shader);
                //renderer.render(entity3, shader);
                //renderer.render(entity4, shader);
                //renderer.render(entity5, shader);
                //renderer.render(entity6, shader);
                //renderer.render(entity7, shader);
                //renderer.render(entity8, shader);
                //renderer.render(entity9, shader);
                //renderer.render(entity10, shader);
                shader.stop();
                //light.colour = new Vertex3f(r, g, b);


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
