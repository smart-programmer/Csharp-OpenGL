using System;
using OpenGL;
using MainProject.entities;
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

        public static Matrix4f createViewMatrix(Camera camera)
        {
            Matrix4f viewMatrix = new Matrix4f();
            viewMatrix.SetIdentity();
            Matrix4f.rotate(math.toRadians(camera.pitch), new Vertex3f(1, 0, 0), viewMatrix, ref viewMatrix);
            Matrix4f.rotate(math.toRadians(camera.yaw), new Vertex3f(0, 1, 0), viewMatrix, ref viewMatrix);
            Vertex3f cameraPos = camera.positin;
            Vertex3f negativeCameraPos = new Vertex3f(-cameraPos.x, -cameraPos.y, -cameraPos.z);
            Matrix4f.translate(negativeCameraPos, viewMatrix, out viewMatrix);

            return viewMatrix;
        }
    }
}
