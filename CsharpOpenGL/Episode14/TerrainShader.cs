using System;
using System.Collections.Generic;
using Episode14.entities;
using Episode14.toolbox;

namespace Episode14
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

