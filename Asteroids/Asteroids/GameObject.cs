using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Asteroids
{
    class GameObject
    {
        public Vector2 position = new Vector2();
        public Vector2 direction = new Vector2();
        public float size = 0;

        float GetRotation()
        {
            return MathF.Atan2(direction.Y, direction.X);
        }

        void SetRotation(float rot)
        {
            direction = new Vector2(
                MathF.Cos(rot * (MathF.PI / 180 / .0f)),
                MathF.Sin(rot * (MathF.PI / 180 / .0f)));
        }

        void Rotate(float amount)
        {
            float rot = GetRotation() + amount;
            SetRotation(rot);
        }
    }
}
