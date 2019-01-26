using System;
using OpenGL;
using Episode14.toolbox;
using Episode14.Models;
using Episode14.entities;
using System.Collections.Generic;


namespace Episode14
{
    class EntityRenderer
    {
        private const float FOV = 70;
        private const float NEAR_PLANE = 0.1f;
        private const float FAR_PLANE = 1000;

        private StaticShader shader { set; get; }

        public EntityRenderer(StaticShader Shader, Matrix4f projectionMatrix)
        {
            this.shader = Shader;
            shader.start();
            shader.loadProjectionMatrix(projectionMatrix);
            shader.stop();
        }


        public void render(Dictionary<TexturedModel, List<Entity>> entities)
        {
            foreach (TexturedModel model in entities.Keys)
            {
                prepareTexturedModel(model);
                List<Entity> batch = entities[model];
                foreach (Entity entity in batch)
                {
                    prepareInstance(entity);
                    Gl.DrawElements(PrimitiveType.Triangles, model.rawModel.vertexCount, DrawElementsType.UnsignedInt, IntPtr.Zero);
                }
                unbindTexture();
            }
        }

        private void prepareTexturedModel(TexturedModel model)
        {
            RawModel rawModel = model.rawModel;
            Gl.BindVertexArray(rawModel.vaoID);
            Gl.EnableVertexAttribArray(0);
            Gl.EnableVertexAttribArray(1);
            Gl.EnableVertexAttribArray(2);
            ModelTexture texture = model.modelTexture;
            shader.loadVariables(texture.shineDamper, texture.reflectivity);
            Gl.ActiveTexture(TextureUnit.Texture0);
            Gl.BindTexture(TextureTarget.Texture2d, model.modelTexture.textureId); 
        }

        private void unbindTexture()
        {
            Gl.DisableVertexAttribArray(0);
            Gl.DisableVertexAttribArray(1);
            Gl.DisableVertexAttribArray(2);
            Gl.BindVertexArray(0);
        }

        private void prepareInstance(Entity entity)
        {
            Matrix4f transformationMatrix = Maths.createTransformationMatrix(entity.position, entity.rotX, entity.rotY, entity.rotZ, entity.scale);
            shader.loadTransformationMatrix(transformationMatrix);
        }

    }
}
