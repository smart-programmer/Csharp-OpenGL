using System;
using System.IO;
using System.Collections.Generic;

namespace Episode5
{
    class Utils
    {
        // use this method to load a shader string
        public static string[] GetValidShaderStringArray(string fileName)
        {
            string text = File.ReadAllText(fileName);

            return new string[] { text };
        }
    }
}
