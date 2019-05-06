using System;
using OpenGL;
using Episode18.Models;
using System.IO;
using System.Collections.Generic;


namespace Episode18
{
    class OBJLoader
    {
        public static RawModel loadObjModel(String filename, Loader loader)
        {
            StreamReader fr = null;//
            try
            {
                fr = new StreamReader(File.OpenRead("..\\..\\res/" + filename + ".obj"));//
            }catch (FileNotFoundException e)
            {
                Console.WriteLine("Couldn't load file!");
            }
            string line;
            List<Vertex3f> vertices = new List<Vertex3f>();
            List<Vertex2f> textures = new List<Vertex2f>();
            List<Vertex3f> normals = new List<Vertex3f>();
            List<uint> indices = new List<uint>();
            float[] verticesArray = null;
            float[] normalsArray = null;
            float[]textureArray = null;
            uint[] indicesArray = null;

            try
            {
                while (true)
                {
                    line = fr.ReadLine();
                    string[] currentLine = line.Split(' ');
                    if (line.StartsWith("v "))
                    {
                        Vertex3f vertex = new Vertex3f(float.Parse(currentLine[1]), float.Parse(currentLine[2]), float.Parse(currentLine[3]));
                        vertices.Add(vertex);
                    }
                    else if (line.StartsWith("vt "))
                    {
                        Vertex2f texture = new Vertex2f(float.Parse(currentLine[1]), float.Parse(currentLine[2]));
                        textures.Add(texture);
                    }
                    else if (line.StartsWith("vn "))
                    {
                        Vertex3f normal = new Vertex3f(float.Parse(currentLine[1]), float.Parse(currentLine[2]), float.Parse(currentLine[3]));
                        normals.Add(normal);
                    }
                    else if (line.StartsWith("f "))
                    {
                        textureArray = new float[vertices.Count * 2];
                        normalsArray = new float[vertices.Count * 3];
                        break;
                    }
                }

                while (line != null)
                {
                    if (!line.StartsWith("f "))
                    {
                        line = fr.ReadLine();
                        continue;
                    }
                    string[] currentLine = line.Split(' ');
                    string[] vertex1 = currentLine[1].Split('/');
                    string[] vertex2 = currentLine[2].Split('/');
                    string[] vertex3 = currentLine[3].Split('/');

                    processVertex(ref vertex1, ref indices, ref textures, ref normals, ref textureArray, ref normalsArray);
                    processVertex(ref vertex2, ref indices, ref textures, ref normals, ref textureArray, ref normalsArray);
                    processVertex(ref vertex3, ref indices, ref textures, ref normals, ref textureArray, ref normalsArray);
                    line = fr.ReadLine();
                }
                fr.Close();

            }catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            verticesArray = new float[vertices.Count * 3];
            indicesArray = new uint[indices.Count];

            int vertexPointer = 0;
            foreach (Vertex3f vertex in vertices)
            {
                verticesArray[vertexPointer++] = vertex.x;
                verticesArray[vertexPointer++] = vertex.y;
                verticesArray[vertexPointer++] = vertex.z;
            }

            for (int i = 0; i < indices.Count; i++)
            {
                indicesArray[i] = indices[i];
            }
            return loader.LoadToVao(verticesArray, textureArray, normalsArray, indicesArray);
        }

        private static void processVertex(ref string[] vertexData, ref List<uint> indices, ref List<Vertex2f> textures, ref List<Vertex3f> normals, ref float[] textureArray, ref float[] normalsArray)
        {
            uint currentVertexPointer = uint.Parse(vertexData[0]) - 1;//
            indices.Add(currentVertexPointer);
            Vertex2f currentTex = textures[int.Parse(vertexData[1]) - 1]; //
            textureArray[currentVertexPointer * 2] = currentTex.x;
            textureArray[currentVertexPointer * 2 + 1] = 1 - currentTex.y;
            Vertex3f currentNorm = normals[int.Parse(vertexData[2]) - 1];//
            normalsArray[currentVertexPointer * 3] = currentNorm.x;
            normalsArray[currentVertexPointer * 3 + 1] = currentNorm.y;
            normalsArray[currentVertexPointer * 3 + 2] = currentNorm.z;
        }
    }
}
