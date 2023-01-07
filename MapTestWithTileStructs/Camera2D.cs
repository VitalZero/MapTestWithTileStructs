using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapTestWithTileStructs
{
    public static class Camera2D
    {
        public static Vector2 Position;
        public static float Zoom;
        public static float Rotation;
        public static Vector2 Origin;
        public static Rectangle Viewport { get; set; }
        public static Rectangle WorldRect { get; set; }
        public static Matrix Transform = Matrix.Identity;
        public static float CameraSpeed { get; set; }
        private static bool UpdateMatrix;

        public static void Init()
        {
            CameraSpeed = 4.0f;
            Rotation = 0.0f;
            Zoom = 1.0f;
            Position = Vector2.Zero;// new Vector2(Viewport.Width / 2, Viewport.Height / 2);
            Origin = new Vector2(Viewport.Width / 2, Viewport.Height / 2);
        }

        public static void ZoomBy(float amount)
        {
            Zoom += amount;
            UpdateMatrix = true;
        }

        public static void Rotate(float amount)
        {
            Rotation += MathHelper.ToRadians(amount);
            UpdateMatrix = true;
        }

        public static void Move(float x, float y)
        {
            Position.X += x;
            Position.Y += y;
        }

        public static void Follow(Vector2 target)
        {
            Position = target;
            UpdateMatrix = true;
        }

        public static Matrix TransformMatrix()
        {
            if (UpdateMatrix)
            {
                Transform = Matrix.CreateTranslation(new Vector3(-Position, 0))
                    * Matrix.CreateRotationZ(Rotation)
                    * Matrix.CreateScale(new Vector3(Zoom, Zoom, 1))
                    * Matrix.CreateTranslation(new Vector3(Origin, 0));

                UpdateMatrix = false;
            }

            return Transform;
        }
    }
}
