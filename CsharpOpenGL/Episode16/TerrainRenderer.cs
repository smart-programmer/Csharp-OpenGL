using System;
using OpenGL;
using System.Collections.Generic;
using Episode16.terrains;
using Episode16.Models;
using Episode16.toolbox;


namespace Episode16
{
    class TerrainRenderer
    {
        private TerrainShader shader { set; get; }


        public TerrainRenderer(TerrainShader shader, Matrix4f projectionMAtrix)
        {
            this.shader = shader;
            this.shader.start();
            shader.loadProjectionMatrix(projectionMAtrix);
            this.shader.stop();
        }

        public void render(List<Terrain> terrains)
        {
            foreach (Terrain terrain in terrains)
            {
                prepareTerrain(terrain);
                loadModelMatrix(terrain);
                Gl.DrawElements(PrimitiveType.Triangles, terrain.model.vertexCount, DrawElementsType.UnsignedInt, IntPtr.Zero);
                unbindTexture();
            }
        }

        private void prepareTerrain(Terrain terrain)
        {
            RawModel rawModel = terrain.model;
            Gl.BindVertexArray(rawModel.vaoID);
            Gl.EnableVertexAttribArray(0);
            Gl.EnableVertexAttribArray(1);
            Gl.EnableVertexAttribArray(2);
            ModelTexture texture = terrain.texture;
            shader.loadVariables(texture.shineDamper, texture.reflectivity);
            Gl.ActiveTexture(TextureUnit.Texture0);
            Gl.BindTexture(TextureTarget.Texture2d, texture.textureId); 
        }

        private void unbindTexture()
        {
            Gl.DisableVertexAttribArray(0);
            Gl.DisableVertexAttribArray(1);
            Gl.DisableVertexAttribArray(2);
            Gl.BindVertexArray(0);
        }

        private void loadModelMatrix(Terrain terrain)
        {
            Matrix4f transformationMatrix = Maths.createTransformationMatrix(new Vertex3f(terrain.x, 0, terrain.z), 0, 0, 0, 1);
            shader.loadTransformationMatrix(transformationMatrix);
        }
    }
}
