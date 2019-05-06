using System;
using OpenGL;
using System.Collections.Generic;
using Episode18.terrains;
using Episode18.Models;
using Episode18.toolbox;
using Episode18.Textures;


namespace Episode18
{
    class TerrainRenderer
    {
        private TerrainShader shader { set; get; }


        public TerrainRenderer(TerrainShader shader, Matrix4f projectionMAtrix)
        {
            this.shader = shader;
            this.shader.start();
            shader.loadProjectionMatrix(projectionMAtrix);
            shader.connectTextureUnits();
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
            bindTextures(terrain);
            shader.loadVariables(1, 0);
        }

        private void bindTextures(Terrain terrain)
        {
            TerrainTexturePack texturePack = terrain.texturePack;
            Gl.ActiveTexture(TextureUnit.Texture0); // activate texture
            Gl.BindTexture(TextureTarget.Texture2d, texturePack.backgroundTexture.textureID); // pass coords
            Gl.ActiveTexture(TextureUnit.Texture1);
            Gl.BindTexture(TextureTarget.Texture2d, texturePack.rTexture.textureID);
            Gl.ActiveTexture(TextureUnit.Texture2);
            Gl.BindTexture(TextureTarget.Texture2d, texturePack.gTexture.textureID);
            Gl.ActiveTexture(TextureUnit.Texture3);
            Gl.BindTexture(TextureTarget.Texture2d, texturePack.bTexture.textureID);
            Gl.ActiveTexture(TextureUnit.Texture4);
            Gl.BindTexture(TextureTarget.Texture2d, terrain.blendMap.textureID);
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
