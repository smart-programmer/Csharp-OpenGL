using System;
using System.Drawing;
using OpenGL;
using Glfw3;
using Episode12.Models;
using Episode12.toolbox;
using Episode12.entities;



namespace Episode12
{

    class Episode12
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
            RawModel model = OBJLoader.loadObjModel("dragon", loader);
            ModelTexture texture = new ModelTexture(loader.loadTexture("..\\..\\res/purple.jpeg"));
            TexturedModel staticModel = new TexturedModel(model, texture);
            staticModel.modelTexture.shineDamper = 10;
            staticModel.modelTexture.reflectivity = 0.0000001f;
            
            Entity entity = new Entity(staticModel, new Vertex3f(0, 0, -25), 0, 0, 0, 2);
            Light light = new Light(new Vertex3f(0, 0, -20), new Vertex3f(1f, 1f, 1f));

            Camera camera = new Camera();

            // Loop until the user closes the window
            while (!Glfw.WindowShouldClose(window))
            {
                entity.increaseRotation(0, 1, 0);
                
                // Render here
                camera.move(window);
                renderer.prepare();
                shader.start();
                shader.loadLight(light);
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
