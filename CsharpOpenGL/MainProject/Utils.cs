using System;
using System.IO;

namespace FirstOpenGLProject
{
    class Utils
    {
        public static string[] GetValidShaderStringArray(string fileName)
        {
            string text = File.ReadAllText(fileName);

            return new string[] { text };
        }
    }
}
