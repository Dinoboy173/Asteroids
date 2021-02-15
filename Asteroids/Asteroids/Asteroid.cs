using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using Raylib_cs;

namespace Asteroids
{
    class Asteroid : GameObject
    {
        Program program;

        public float radius = 40;

        Color color;

        static Color[] colours = new Color[6] { Color.RED, Color.ORANGE, Color.YELLOW, Color.GREEN, Color.BLUE, Color.PURPLE };
        static int nextColor = 0;

        public Asteroid(Program program, Vector2 position, Vector2 direction)
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
            position += direction;

            if (position.X < 0) position.X = program.windowWidth;
            if (position.X > program.windowWidth) position.X = 0;
            if (position.Y < 0) position.Y = program.windowHeight;
            if (position.Y > program.windowHeight) position.Y = 0;
        }

        public void Draw()
        {
            Raylib.DrawCircleLines((int)position.X, (int)position.Y, radius, color);
        }
    }
}
