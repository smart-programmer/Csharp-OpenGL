using System;
using OpenGL;
using System.Collections.Generic;


namespace MainProject.toolbox
{
    public class Maths
    {
        public static Matrix4f createTransformationMatrix(Vertex3f translation, float rx, float ry, float rz, float scale)
        {
            Matrix4f matrix = new Matrix4f();
            matrix.SetIdentity();

            Matrix4f.scale(new Vertex3f(scale, scale, scale), matrix, ref matrix);
            Matrix4f.rotate(math.toRadians(rx), new Vertex3f(1, 0, 0), matrix, ref matrix);
            Matrix4f.rotate(math.toRadians(ry), new Vertex3f(0, 1, 0), matrix, ref matrix);
            Matrix4f.rotate(math.toRadians(rz), new Vertex3f(0, 0, 1), matrix, ref matrix);
            Matrix4f.translate(translation, matrix, out matrix);

            return matrix;
        }
    }
}
