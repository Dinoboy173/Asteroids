using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Raylib_cs;

namespace Asteroids
{
    class Bullet : GameObject
    {
        Program program;

        public float speed = 10;

        Color color;

        static Color[] colours = new Color[6] { Color.RED, Color.ORANGE, Color.YELLOW, Color.GREEN, Color.BLUE, Color.PURPLE };
        static int nextColor = 0;

        public Bullet(Program program, Vector2 position, Vector2 direction)
        {
            this.program = program;
            this.position = position;
            this.direction = direction;

            color = colours[nextColor];
            nextColor += 1;
            if (nextColor >= colours.Length)
                nextColor = 0;
        }

        public void Update()
        {
            position += direction * speed;

            if (position.X < 0) position.X = program.windowWidth;
            if (position.X > program.windowWidth) position.X = 0;
            if (position.Y < 0) position.Y = program.windowHeight;
            if (position.Y > program.windowHeight) position.Y = 0;
        }
        public void Draw()
        {
            Program prog = new Program();
            Raylib.DrawLine((int)position.X, (int)position.Y, (int)(position.X + direction.X * speed), (int)(position.Y - direction.Y * speed), color);
        }
    }
}
