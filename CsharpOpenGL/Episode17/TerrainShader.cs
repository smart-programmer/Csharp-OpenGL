using System;
using OpenGL;
using System.Collections.Generic;
using Episode17.entities;
using Episode17.toolbox;

namespace Episode17
{
    class TerrainShader : ShaderProgram
    {
        public static string[] vertexString = Shaders.terrainVertexShader;
        public static string[] fragmentString = Shaders.terrainFragmentShader;
        public int location_transformationMatrix;
        public int location_projectionMatrix;
        public int location_viewMatrix;
        public int location_lightPosition;
        public int location_lightColour;
        public int location_shineDamper;
        public int location_reflectivity;
        public int location_skyColour;
        public int location_backgroundTexture;
        public int location_rTexture;
        public int location_gTexture;
        public int location_bTexture;
        public int location_blendMap;

        public TerrainShader() : base(vertexString, fragmentString)
        {

        }


        protected override void bindAttributes()
        {
            base.bindAttribute(0, "position");
            base.bindAttribute(1, "textureCoords");
            base.bindAttribute(2, "normal");
        }

        protected override void getAllUniformLocations()
        {
            location_transformationMatrix = base.getUinformLocation("transformationMatrix");
            location_projectionMatrix = base.getUinformLocation("projectionMatrix");
            location_viewMatrix = base.getUinformLocation("viewMatrix");
            location_lightPosition = base.getUinformLocation("lighPosition");
            location_lightColour = base.getUinformLocation("lightColour");
            location_shineDamper = base.getUinformLocation("shineDamper");
            location_reflectivity = base.getUinformLocation("reflectivity");
            location_skyColour = base.getUinformLocation("skyColour");
            location_backgroundTexture = base.getUinformLocation("backgroundTexture");
            location_rTexture = base.getUinformLocation("rTexture");
            location_gTexture = base.getUinformLocation("gTexture");
            location_bTexture = base.getUinformLocation("bTexture");
            location_blendMap = base.getUinformLocation("blendMap");
        }

        public void connectTextureUnits()
        {
            base.loadInt(location_backgroundTexture, 0);
            base.loadInt(location_rTexture, 1);
            base.loadInt(location_gTexture, 2);
            base.loadInt(location_bTexture, 3);
            base.loadInt(location_blendMap, 4);
        }

        public void loadSkyColour(float r, float g, float b)
        {
            base.loadVector(location_skyColour, new Vertex3f(r, g, b));
        }

        public void loadVariables(float dapmer, float reflectivity)
        {
            base.loadFloat(location_shineDamper, dapmer);
            base.loadFloat(location_reflectivity, reflectivity);
        }

        public void loadTransformationMatrix(Matrix4f matrix)
        {
            base.loadMatrix(location_transformationMatrix, matrix);
        }

        public void loadLight(Light light)
        {
            base.loadVector(location_lightPosition, light.position);
            base.loadVector(location_lightColour, light.colour);
        }

        public void loadProjectionMatrix(Matrix4f projection)
        {
            base.loadMatrix(location_projectionMatrix, projection);
        }

        public void loadViewMatrix(Camera camera)
        {
            Matrix4f viewMatrix = Maths.createViewMatrix(camera);
            base.loadMatrix(location_viewMatrix, viewMatrix);
        }
    }
}

