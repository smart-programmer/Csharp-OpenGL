using System;
using OpenGL;
using Episode18.entities;
using Episode18.toolbox;
using System.Text;
using System.IO;

namespace Episode18
{
    class Shaders
    {
        public static string[] vertexShader = Utils.GetValidShaderStringArray("..\\..\\shaders/vertexShader.txt");
        public static string[] fragmentShader = Utils.GetValidShaderStringArray("..\\..\\shaders/fragmentShader.txt");
        public static string[] terrainVertexShader = Utils.GetValidShaderStringArray("..\\..\\shaders/terrainVertexShader.txt");
        public static string[] terrainFragmentShader = Utils.GetValidShaderStringArray("..\\..\\shaders/terrainFragmentShader.txt");
    }


    public abstract class ShaderProgram
    {
        private uint programID { set; get; }
        private uint vertexShaderID { set; get; }
        private uint fragmentShaderID { set; get; }


        public ShaderProgram(string[] vertexShader, string[] fragmentShader)
        {
            vertexShaderID = loadShader(vertexShader, ShaderType.VertexShader);
            fragmentShaderID = loadShader(fragmentShader, ShaderType.FragmentShader);
            programID = Gl.CreateProgram();
            Gl.AttachShader(programID, vertexShaderID);
            Gl.AttachShader(programID, fragmentShaderID);
            bindAttributes();
            Gl.LinkProgram(programID);
            Gl.ValidateProgram(programID);
            getAllUniformLocations();
        }

        protected abstract void getAllUniformLocations();

        protected int getUinformLocation(string uniformName)
        {
            return Gl.GetUniformLocation(programID, uniformName);
        }

        public void start()
        {
            Gl.UseProgram(programID);
        }

        public void stop()
        {
            Gl.UseProgram(0);
        }


        public void cleanUP()
        {
            stop();
            Gl.DetachShader(programID, vertexShaderID);
            Gl.DetachShader(programID, fragmentShaderID);
            Gl.DeleteShader(vertexShaderID);
            Gl.DeleteShader(fragmentShaderID);
            Gl.DeleteProgram(programID);
        }


        protected abstract void bindAttributes();


        protected void bindAttribute(uint attribute, string variableName)
        {
            Gl.BindAttribLocation(programID, attribute, variableName);
        }

        protected void loadInt(int location, int value)
        {
            Gl.Uniform1i(location, 1, ref value);
        }

        protected void loadFloat(int location, float value)
        {
            Gl.Uniform1f(location, 1, ref value);// count
        }

        protected void loadVector(int location, Vertex3f vector)
        {
            Gl.Uniform3(location, vector.x, vector.y, vector.z);
        }

        protected void loadBool(int location, bool value)
        {
            float toLoad = (value) ? 1f : 0f;

            Gl.Uniform1f(location, 1, ref toLoad); // count
        }

        protected void loadMatrix(int location, Matrix4f matrix)
        {
            Gl.UniformMatrix4(location, false, matrix.Buffer);
        }

        private static uint loadShader(string[] shaderString, ShaderType type)
        {
            uint shaderID = Gl.CreateShader(type);
            Console.WriteLine(shaderID);
            Gl.ShaderSource(shaderID, shaderString);
            Gl.CompileShader(shaderID);
            int status;
            Gl.GetShader(shaderID, ShaderParameterName.CompileStatus, out status);
            if (status == Gl.TRUE)
            {
                Console.WriteLine($"{type} Creation Success.");
                return shaderID;
            }
            else if (status == Gl.FALSE)
            {
                Console.WriteLine($"{type} creation fail.");
                Gl.GetShader(shaderID, ShaderParameterName.InfoLogLength, out int logLength);
                int logMaxLength = 1024;
                StringBuilder infoLog = new StringBuilder(logMaxLength);
                Gl.GetShaderInfoLog(shaderID, 1024, out int infoLogLength, infoLog);
                Console.WriteLine("Errors: \n{0}", infoLog.ToString());

                return 0;
            }

            return 0;
        }

        
    }



    public class StaticShader : ShaderProgram
    {
        public static string[] vertexString = Shaders.vertexShader;
        public static string[] fragmentString = Shaders.fragmentShader;
        public int location_transformationMatrix;
        public int location_projectionMatrix;
        public int location_viewMatrix;
        public int location_lightPosition;
        public int location_lightColour;
        public int location_shineDamper;
        public int location_reflectivity;
        public int location_useFakeLighting;
        public int location_skyColour;

        public StaticShader() : base(vertexString, fragmentString)
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
            location_useFakeLighting = base.getUinformLocation("useFakeLighting");
            location_skyColour = base.getUinformLocation("skyColour");
        }

        public void loadSkyColour(float r, float g, float b)
        {
            base.loadVector(location_skyColour, new Vertex3f(r, b, g));
        }

        public void loadFakeLighting(bool useFake)
        {
            base.loadBool(location_useFakeLighting, useFake);
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
