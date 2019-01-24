using System;
using OpenGL;
using Episode12.Models;


namespace Episode12.entities
{
    class Entity
    {
        public TexturedModel model { set; get; }
        public Vertex3f position { set; get; }
        public float rotX { set; get; }
        public float rotY { set; get; }
        public float rotZ { set; get; }
        public float scale { set; get; }


        public Entity(TexturedModel model, Vertex3f position, float rotX, float rotY, float rotZ, float scale)
        {
            this.model = model;
            this.position = position;
            this.rotX = rotX;
            this.rotY = rotY;
            this.rotZ = rotZ;
            this.scale = scale;
        }

        public void increasePosition(float dx, float dy, float dz)
        {
            this.position = new Vertex3f(position.x + dx, position.y + dy, position.z + dz);
        }

        public void increaseRotation(float dx, float dy, float dz)
        {
            this.rotX += dx;
            this.rotY += dy;
            this.rotZ += dz;

        }
    }
}
