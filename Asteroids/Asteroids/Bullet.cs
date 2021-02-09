using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Raylib_cs;

namespace Asteroids
{
    class Bullet
    {
        Program program;
        public Vector2 pos = new Vector2();
        public Vector2 dir = new Vector2();
        public float speed = 10;

        Color color;

        static Color[] colours = new Color[6] { Color.RED, Color.ORANGE, Color.YELLOW, Color.GREEN, Color.BLUE, Color.PURPLE };
        static int nextColor = 0;

        public Bullet(Program program, Vector2 pos, Vector2 dir)
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
            pos += dir * speed;

            if (pos.X < 0) pos.X = program.windowWidth;
            if (pos.X > program.windowWidth) pos.X = 0;
            if (pos.Y < 0) pos.Y = program.windowHeight;
            if (pos.Y > program.windowHeight) pos.Y = 0;
        }
        public void Draw()
        {
            Program prog = new Program();
            Raylib.DrawLine((int)pos.X, (int)pos.Y, (int)(pos.X + dir.X * speed), (int)(pos.Y - dir.Y * speed), color);
        }
    }
}
