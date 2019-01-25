using System;
using System.Collections.Generic;
using System.Collections;
using Epsiode13.Models;
using Epsiode13.entities;

namespace Epsiode13
{
    class MasterRenderer
    {
        public StaticShader shader { set; get; }
        private Renderer renderer { set; get; }

        private Dictionary<TexturedModel, List<Entity>> entities = new Dictionary<TexturedModel, List<Entity>>();

        public MasterRenderer(WinowInfo window)
        {
            this.shader = new StaticShader();
            this.renderer = new Renderer(shader, window);
        }


        public void render(Light sun, Camera camera)
        {
            renderer.prepare();
            shader.start();
            shader.loadLight(sun);
            shader.loadViewMatrix(camera);
            renderer.render(entities);
            shader.stop();
            entities.Clear();
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
        }
    }
}
