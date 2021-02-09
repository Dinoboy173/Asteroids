using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using Raylib_cs;

namespace Asteroids
{
    class Asteroid
    {
        Program program;

        public Vector2 pos = new Vector2();
        public Vector2 dir = new Vector2();
        public float radius = 40;

        Color color;

        static Color[] colours = new Color[6] { Color.RED, Color.ORANGE, Color.YELLOW, Color.GREEN, Color.BLUE, Color.PURPLE };
        static int nextColor = 0;

        public Asteroid(Program program, Vector2 pos, Vector2 dir)
        {
            this.program = program;
            this.pos = pos;
            this.dir = dir;

            color = colours[nextColor];
            nextColor += 1;
            if (nextColor >= colours.Length)
                nextColor = 0;
        }

        public void Update()
        {
            pos += dir;

            if (pos.X < 0) pos.X = program.windowWidth;
            if (pos.X > program.windowWidth) pos.X = 0;
            if (pos.Y < 0) pos.Y = program.windowHeight;
            if (pos.Y > program.windowHeight) pos.Y = 0;
        }

        public void Draw()
        {
            Raylib.DrawCircleLines((int)pos.X, (int)pos.Y, radius, color);
        }
    }
}
