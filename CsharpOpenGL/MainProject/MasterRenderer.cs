using System;
using OpenGL;
using System.Collections.Generic;
using System.Collections;
using MainProject.Models;
using MainProject.entities;
using MainProject.terrains;

namespace MainProject
{
    class MasterRenderer
    {
        private const float FOV = 70;
        private const float NEAR_PLANE = 0.1f;
        private const float FAR_PLANE = 1000;

        private Matrix4f projectionMatrix = new Matrix4f();

        private StaticShader shader { set; get; }
        private EntityRenderer renderer { set; get; }

        private TerrainRenderer terrainRenderer { set; get; }
        private TerrainShader terrainShader = new TerrainShader();

        private Dictionary<TexturedModel, List<Entity>> entities = new Dictionary<TexturedModel, List<Entity>>();
        List<Terrain> terrains = new List<Terrain>();

        private WinowInfo window { set; get; }

        public MasterRenderer(WinowInfo window)
        {
            this.window = window;
            Gl.Enable(EnableCap.CullFace);
            Gl.CullFace(CullFaceMode.Back);
            createProjectionMatrix();
            this.shader = new StaticShader();
            this.renderer = new EntityRenderer(shader, projectionMatrix);
            this.terrainRenderer = new TerrainRenderer(terrainShader, projectionMatrix);
        }


        public void render(Light sun, Camera camera)
        {
            prepare();
            shader.start();
            shader.loadLight(sun);
            shader.loadViewMatrix(camera);
            renderer.render(entities);
            shader.stop();
            terrainShader.start();
            terrainShader.loadLight(sun);
            terrainShader.loadViewMatrix(camera);
            terrainRenderer.render(terrains);
            terrainShader.stop();
            terrains.Clear();
            entities.Clear();
        }

        public void processTerrain(Terrain terrain)
        {
            terrains.Add(terrain);
        }

        public void processEntity(Entity entity)
        {
            TexturedModel model = entity.model;
            if (entities.ContainsKey(model))
            {
                entities[model].Add(entity);
            }
            else
            {
                List<Entity> newbatch = new List<Entity>();
                newbatch.Add(entity);
                entities.Add(model, newbatch);
            }
        }

        public void cleanUP()
        {
            shader.cleanUP();
            terrainShader.cleanUP();
        }

        public void prepare()
        {
            Gl.Enable(EnableCap.DepthTest);
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Gl.ClearColor(0, 1, 1, 1);
        }

        private void createProjectionMatrix()
        {
            float aspectRatio = (float)window.windowWidth / window.windowHeight;
            float y_scale = 1f / (float)Math.Tan(math.toRadians(FOV / 2f));
            float x_scale = y_scale / aspectRatio;
            float frustum_length = FAR_PLANE - NEAR_PLANE;
            Console.WriteLine(window.windowWidth);
            Console.WriteLine(window.windowHeight);

            float[] matbuffer = projectionMatrix.Buffer;


            matbuffer[0] = x_scale;
            matbuffer[5] = y_scale;
            matbuffer[10] = -((FAR_PLANE + NEAR_PLANE) / frustum_length);
            matbuffer[11] = -1;
            matbuffer[14] = -((2 * NEAR_PLANE * FAR_PLANE) / frustum_length);
            matbuffer[15] = 0;

            this.projectionMatrix = new Matrix4f(matbuffer);

        }
    }
}
