using System;
using System.Drawing;
using OpenGL;
using Glfw3;
using Episode17.Models;
using Episode17.toolbox;
using Episode17.entities;
using Episode17.terrains;
using Episode17.Textures;
using System.Collections.Generic;


// NOTE: i made the FAR_PLANE = 1500 insted of 1000


namespace Episode17
{

    class Episode17
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

            // create terrain texture pack
            TerrainTexture backgroundTexture = new TerrainTexture(loader.loadTexture("..\\..\\res/grassy2.png"));
            TerrainTexture rTexture = new TerrainTexture(loader.loadTexture("..\\..\\res/mud.png"));
            TerrainTexture gTexture = new TerrainTexture(loader.loadTexture("..\\..\\res/grassFlowers.png"));
            TerrainTexture bTexture = new TerrainTexture(loader.loadTexture("..\\..\\res/path.png"));
            TerrainTexturePack texturePack = new TerrainTexturePack(backgroundTexture, rTexture, gTexture, bTexture);

            // create blend map
            TerrainTexture blendMap = new TerrainTexture(loader.loadTexture("..\\..\\res/blendMap.png"));

            // create model
            RawModel fern = OBJLoader.loadObjModel("fern", loader);
            ModelTexture fernTexture = new ModelTexture(loader.loadTexture("..\\..\\res/flower.png"));
            TexturedModel texturedFern = new TexturedModel(fern, fernTexture);
            texturedFern.modelTexture.shineDamper = 10;
            texturedFern.modelTexture.reflectivity = 0;
            texturedFern.modelTexture.isHasTransparency = true;

            RawModel grass = OBJLoader.loadObjModel("grassModel", loader);
            ModelTexture grassTexture = new ModelTexture(loader.loadTexture("..\\..\\res/flower.png"));
            TexturedModel texturedGrass = new TexturedModel(grass, grassTexture);
            texturedGrass.modelTexture.shineDamper = 10;
            texturedGrass.modelTexture.reflectivity = 0;
            texturedGrass.modelTexture.isHasTransparency = true;
            texturedGrass.modelTexture.isUseFakeLighting = true;

            RawModel model = OBJLoader.loadObjModel("lowPolyTree", loader);
            ModelTexture texture = new ModelTexture(loader.loadTexture("..\\..\\res/lowPolyTree.png"));
            TexturedModel staticModel = new TexturedModel(model, texture);
            staticModel.modelTexture.shineDamper = 10;
            staticModel.modelTexture.reflectivity = 0;
            Entity entity = new Entity(staticModel, new Vertex3f(0, 0, -25), 0, 0, 0, 1);
            Light light = new Light(new Vertex3f(0, 0, -20), new Vertex3f(1f, 1f, 1f));

            Random rand = new Random();
            ModelTexture terrainTexture = new ModelTexture(loader.loadTexture("..\\..\\res/grass.png"));
            terrainTexture.shineDamper = 10;
            terrainTexture.reflectivity = 0;
            Terrain terrain = new Terrain(800, 0, loader, texturePack, blendMap);
            Terrain terrain2 = new Terrain(800, -1600, loader, texturePack, blendMap);

            RawModel tree = OBJLoader.loadObjModel("normal_tree", loader);
            ModelTexture treeTexture = new ModelTexture(loader.loadTexture("..\\..\\res/normal_tree.png"));
            TexturedModel texturedTree = new TexturedModel(tree, treeTexture);


            Random random = new Random();
            
            Camera camera = new Camera();

            List<Entity> allCubes = new List<Entity>();
            List<Entity> allGrass = new List<Entity>();
            List<Entity> allFerns = new List<Entity>();
            List<Entity> allTrees = new List<Entity>();

            for (int i = 0; i < 1000; i++)
            {
                double x = random.NextDouble() * -1600 + 800;
                double y = 0;
                double z = random.NextDouble() * -2410;
                allCubes.Add(new Entity(staticModel, new Vertex3f((float)x, (float)y, (float)z), 0, 0, 0, rand.Next(1, 4)));
            }
            for (int i = 0; i < 1000; i++)
            {
                double x = random.NextDouble() * -1600 + 800;
                double y = 0;
                double z = random.NextDouble() * -2410;
                allGrass.Add(new Entity(texturedGrass, new Vertex3f((float)x, (float)y, (float)z), 0, 0, 0, 3));
            }
            for (int i = 0; i < 1000; i++)
            {
                double x = random.NextDouble() * -1600 + 800;
                double y = 0;
                double z = random.NextDouble() * -2410;
                allFerns.Add(new Entity(texturedFern, new Vertex3f((float)x, (float)y, (float)z), 0, 0, 0, 3));
            }
           
            MasterRenderer renderer = new MasterRenderer(new WinowInfo(width, height));
          
            // Loop until the user closes the window
            while (!Glfw.WindowShouldClose(window))
            {
                //entity.increasePosition(0, 0, -0.1f);
                entity.increaseRotation(0, 1, 0);
                
                // Render here
                camera.move(window);

                renderer.processTerrain(terrain);
                renderer.processTerrain(terrain2);

                foreach (Entity cube in allCubes)
                {
                    renderer.processEntity(cube);
                }
                foreach (Entity gr in allGrass)
                {
                    renderer.processEntity(gr);
                }
                foreach (Entity fr in allFerns)
                {
                    renderer.processEntity(fr);
                }
                foreach (Entity tr in allTrees)
                {
                    renderer.processEntity(tr);
                }


                renderer.render(light, camera);

                //light.colour = new Vertex3f(r, g, b);

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
