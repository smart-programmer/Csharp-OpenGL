using System;
using OpenGL;
using Episode8.entities;
using Episode8.toolbox;
using System.Text;
using System.IO;

namespace Episode8
{
    class Shaders
    {
        public static string[] vertexShader = Utils.GetValidShaderStringArray("..\\..\\shaders/vertexShader.txt");
        public static string[] fragmentShader = Utils.GetValidShaderStringArray("..\\..\\shaders/fragmentShader.txt");
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

        protected void loadFloat(int location, float value)
        {
            Gl.Uniform1f(location, 1, ref value);
        }

        protected void loadVector(int location, Vertex3f vector)
        {
            Gl.Uniform3(location, vector.x, vector.y, vector.z);
        }

        protected void loadBool(int location, bool value)
        {
            float toLoad = (value) ? 1 : 0;

            Gl.Uniform1f(location, 1, ref toLoad);
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
                Console.WriteLine("Vertex Creation Success.");
                return shaderID;
            }
            else if (status == Gl.FALSE)
            {
                Console.WriteLine("Vertex creation fail.");
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

        public StaticShader() : base(vertexString, fragmentString)
        {

        }
        

        protected override void bindAttributes()
        {
            base.bindAttribute(0, "position");
            base.bindAttribute(1, "textureCoords");
        }

        protected override void getAllUniformLocations()
        {
            location_transformationMatrix = base.getUinformLocation("transformationMatrix");
            location_projectionMatrix = base.getUinformLocation("projectionMatrix");
            location_viewMatrix = base.getUinformLocation("viewMatrix");
        }

        public void loadTransformationMatrix(Matrix4f matrix)
        {
            base.loadMatrix(location_transformationMatrix, matrix);
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
