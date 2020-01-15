using System;
using OpenGL;
using System.Text;
using System.IO;


namespace Episode5
{
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

        private uint loadShader(string[] shaderString, ShaderType type)
        {
            uint shaderID = Gl.CreateShader(type);
            Gl.ShaderSource(shaderID, shaderString);
            Gl.CompileShader(shaderID);

            // debuging code 
            int status;
            Gl.GetShader(shaderID, ShaderParameterName.CompileStatus, out status);
            if (status == Gl.TRUE)
            {
                Console.WriteLine(String.Format("shader number {0} creation succeeded", shaderID));
                return shaderID;
            }
            else if (status == Gl.FALSE)
            {
                Console.WriteLine(String.Format("shader number {0} creation failed", shaderID));
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
        static string[] vertexShader = Utils.GetValidShaderStringArray("..\\..\\shaders/vertexShader.txt");
        static string[] fragmentShader = Utils.GetValidShaderStringArray("..\\..\\shaders/fragmentShader.txt");

        public StaticShader() : base(vertexShader, fragmentShader)
        {

        }

        protected override void bindAttributes()
        {
            base.bindAttribute(0, "position");
        }
    }
}
