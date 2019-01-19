using System;
using OpenGL;
using Episode8.toolbox;
using Episode8.Models;
using Episode8.entities;


namespace Episode8
{
    class Renderer
    {
        private const float FOV = 70;
        private const float NEAR_PLANE = 0.1f;
        private const float FAR_PLANE = 1000;
        WinowInfo window { set; get; }

        private Matrix4f projectionMatrix = new Matrix4f();

        public Renderer(StaticShader shader, WinowInfo Window)
        {
            window = Window;
            createProjectionMatrix();
            shader.start();
            shader.loadProjectionMatrix(projectionMatrix);
            shader.stop();
        }
            

        public void prepare()
        {
            Gl.Enable(EnableCap.DepthTest);
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Gl.ClearColor(1, 0, 0, 1);
        }


        public void render(Entity entity, StaticShader shader)
        {
            TexturedModel model = entity.model;
            RawModel rawModel = model.rawModel;
            Gl.BindVertexArray(rawModel.vaoID);
            Gl.EnableVertexAttribArray(0);
            Gl.EnableVertexAttribArray(1);
            Matrix4f transformationMatrix = Maths.createTransformationMatrix(entity.position, entity.rotX, entity.rotY, entity.rotZ, entity.scale);
            shader.loadTransformationMatrix(transformationMatrix);
            Gl.ActiveTexture(TextureUnit.Texture0);
            Gl.BindTexture(TextureTarget.Texture2d, model.modelTexture.textureId);
            Gl.DrawElements(PrimitiveType.Triangles, rawModel.vertexCount, DrawElementsType.UnsignedInt, IntPtr.Zero);
            Gl.DisableVertexAttribArray(0);
            Gl.DisableVertexAttribArray(1);
            Gl.BindVertexArray(0);
        }

        private void createProjectionMatrix()
        {
            float aspectRatio = (float)window.windowWidth / window.windowHeight;
            //float y_scale = (float)((1f / Math.Tan(math.toRadians(FOV / 2f))) * aspectRatio); // wrong formula by ThinMAtrix
            float y_scale = 1f / (float)Math.Tan(math.toRadians(FOV / 2f)); // correct formula
            float x_scale = y_scale / aspectRatio;
            float frustum_length = FAR_PLANE - NEAR_PLANE;
            Console.WriteLine(window.windowWidth);
            Console.WriteLine(window.windowHeight);

            float[] matbuffer = projectionMatrix.Buffer;
     

            matbuffer[0] = x_scale;
            matbuffer[5] = y_scale;
            matbuffer[10] = -((FAR_PLANE + NEAR_PLANE) / frustum_length);
            matbuffer[11] = -1;
            matbuffer[14] = -((2 * NEAR_PLANE * FAR_PLANE) / frustum_length); 
            matbuffer[15] = 0;
          
            this.projectionMatrix = new Matrix4f(matbuffer);
        }

    }
}
