using System;
using System.Collections.Generic;
using MainProject.Models;
using MainProject.Textures;


namespace MainProject.terrains
{
    class Terrain
    {
        private static float SIZE = 1600; // the terrain needs to be squared for textures
        private static uint VERTEX_COUNT = 128;

        public  float x { set; get; }
        public float z { set; get; }
        public RawModel model { set; get; }
        public TerrainTexturePack texturePack { set; get; }
        public TerrainTexture blendMap { set; get; }

        public Terrain(int gridX, int gridZ, Loader loader, TerrainTexturePack texturePack, TerrainTexture blendMap)
        {
            this.texturePack = texturePack;
            this.blendMap = blendMap;
            this.x = gridX;
            this.z = gridZ;
            this.model = generateTerrain(loader);
        }


        private RawModel generateTerrain(Loader loader)
        {
            uint count = VERTEX_COUNT * VERTEX_COUNT;
            float[] vertices = new float[count * 3];
            float[] normals = new float[count * 3];
            float[] textureCoords = new float[count * 2];
            uint[] indices = new uint[6 * (VERTEX_COUNT - 1) * (VERTEX_COUNT - 1)];
            int vertexPointer = 0;
            for (int i = 0; i < VERTEX_COUNT; i++)
            {
                for (int j = 0; j < VERTEX_COUNT; j++)
                {
                    // NOIE: i changed the next 3 lines for better terrain positioning
                    vertices[vertexPointer * 3] = -SIZE + (float)j / ((float)VERTEX_COUNT - 1) * SIZE;
                    vertices[vertexPointer * 3 + 1] = 0;
                    vertices[vertexPointer * 3 + 2] = -SIZE + (float)i / ((float)VERTEX_COUNT - 1) * SIZE;
                    normals[vertexPointer * 3] = 0;
                    normals[vertexPointer * 3 + 1] = 1;
                    normals[vertexPointer * 3 + 2] = 0;
                    textureCoords[vertexPointer * 2] = (float)j / ((float)VERTEX_COUNT - 1);
                    textureCoords[vertexPointer * 2 + 1] = (float)i / ((float)VERTEX_COUNT - 1);
                    vertexPointer++;
                }
            }
            int pointer = 0;
            for (uint gz = 0; gz < VERTEX_COUNT - 1; gz++)
            {
                for (uint gx = 0; gx < VERTEX_COUNT - 1; gx++)
                {
                    uint topLeft = (gz * VERTEX_COUNT) + gx;
                    uint topRight = topLeft + 1;
                    uint bottomLeft = ((gz + 1) * VERTEX_COUNT) + gx;
                    uint bottomRight = bottomLeft + 1;
                    indices[pointer++] = topLeft;
                    indices[pointer++] = bottomLeft;
                    indices[pointer++] = topRight;
                    indices[pointer++] = topRight;
                    indices[pointer++] = bottomLeft;
                    indices[pointer++] = bottomRight;
                }
            }
            return loader.LoadToVao(vertices, textureCoords, normals, indices);
        }

    }
}
