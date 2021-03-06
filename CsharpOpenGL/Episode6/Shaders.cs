﻿using System;
using OpenGL;
using System.Text;
using System.IO;

namespace MainProject
{
    class Shaders
    {
        //H:\CsharpOpenGL\OpenGLDotNet\FirstOpenGLProject\shaders
        // ..\\..\\shaders/
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

        public StaticShader() : base(vertexString, fragmentString)
        {

        }
        

        protected override void bindAttributes()
        {
            // bind variables to the correct AttributeList in the activated vao
            base.bindAttribute(0, "position");
            base.bindAttribute(1, "textureCoords");
        }
    }
}
