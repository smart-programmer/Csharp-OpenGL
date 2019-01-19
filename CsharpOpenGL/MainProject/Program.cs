using System;
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
            //..\\..\\libs / glfw - 3.2.1.bin.WIN32 / lib - mingw
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

            //        float[] vertices = {
            //              -0.5f, 0.5f, 0f,//v0
            //		-0.5f, -0.5f, 0f,//v1
            //		0.5f, -0.5f, 0f,//v2
            //		0.5f, 0.5f, 0f,//v3
            //};

            //        uint[] indices = {
            //              0,1,3,//top left triangle (v0, v1, v3)
            //		3,1,2//bottom right triangle (v3, v1, v2)
            //};


            //        float[] textureCoords = {
            //            0,0,
            //            1,0,
            //            1,1,
            //            0,1,

            //};
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

            // texture coords for fliped buffer
            //    float[] textureCoords_fliped_buffer =
            //    {
            //        1,1,
            //        0,1,
            //        0,0,
            //        1,0,
            //};

            // create model
            RawModel model = loader.LoadToVao(vertices, textureCoords, indices);
            ModelTexture texture = new ModelTexture(loader.loadTexture("..\\..\\res/c.jpeg"));
            TexturedModel staticModel = new TexturedModel(model, texture);
            
            Entity entity = new Entity(staticModel, new Vertex3f(0, 0, -5), 0, 0, 0, 1);
            Entity entity2 = new Entity(staticModel, new Vertex3f(-1, 0, -3), 0, 0, 0, 1);
            Entity entity3 = new Entity(staticModel, new Vertex3f(0, -2, -7), 0, 0, 0, 1);
            Entity entity4 = new Entity(staticModel, new Vertex3f(-4, -2, -4), 0, 0, 0, 1);
            Entity entity5 = new Entity(staticModel, new Vertex3f(3, 3, -7), 0, 0, 0, 1);
            Entity entity6 = new Entity(staticModel, new Vertex3f(1, -6, -10), 0, 0, 0, 1);
            Entity entity7 = new Entity(staticModel, new Vertex3f(2, 0, -2), 0, 0, 0, 1);
            Entity entity8 = new Entity(staticModel, new Vertex3f(-3, -2, -7), 0, 0, 0, 1);
            Entity entity9 = new Entity(staticModel, new Vertex3f(-4, 2, -6), 0, 0, 0, 1);
            Entity entity10 = new Entity(staticModel, new Vertex3f(3, -2, -6), 0, 0, 0, 1);

            Camera camera = new Camera();
            // Loop until the user closes the window
            while (!Glfw.WindowShouldClose(window))
            {
                //bool W = Glfw.GetKey(window, (int)Glfw.KeyCode.W);
                //entity.increasePosition(0, 0, -0.1f);
                entity.increaseRotation(1, 1, 0);
                entity2.increaseRotation(1, 0, 1);
                entity3.increaseRotation(1, 1, 1);
                entity4.increaseRotation(1, 1, 1);
                entity5.increaseRotation(1, 2, 1);
                entity6.increaseRotation(3, 1, 1);
                entity7.increaseRotation(1, 0, 2);
                entity8.increaseRotation(0, 0, 1);
                entity9.increaseRotation(1, 1, 0);
                entity10.increaseRotation(1, 0, 1);
                // Render here
                camera.move(window);
                renderer.prepare();
                shader.start();
                shader.loadViewMatrix(camera);
                renderer.render(entity, shader);
                renderer.render(entity2, shader);
                renderer.render(entity3, shader);
                renderer.render(entity4, shader);
                renderer.render(entity5, shader);
                renderer.render(entity6, shader);
                renderer.render(entity7, shader);
                renderer.render(entity8, shader);
                renderer.render(entity9, shader);
                renderer.render(entity10, shader);
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
