using System;
using System.Numerics;
using Raylib_cs;

namespace Asteroids
{
    class Program
    {
        public int windowWidth = 1500;
        public int windowHeight = 900;
        public string windowTitle = "Asteroids";

        Player player;
        Bullet[] bullets = new Bullet[200];
        Asteroid[] asteroids = new Asteroid[100];

        float asteroidSpawnCooldown = 4.0f;
        float asteroidSpawnCooldownReset = 4.0f;

        static void Main(string[] args)
        {
            Program p = new Program();
            p.RunGame();
        }

        void RunGame()
        {
            Raylib.InitWindow(windowWidth, windowHeight, windowTitle);
            Raylib.SetTargetFPS(60);

            LoadGame();

            while (!Raylib.WindowShouldClose())
            {
                Update();
                Draw();
            }

            Raylib.CloseWindow();
        }

        void LoadGame()
        {
            Assets.LoadAssets();

            player = new Player(
                this,
                new Vector2(windowWidth / 2, windowHeight / 2),
                new Vector2(64, 64));

            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i] = null;
            }

            for (int i = 0; i < asteroids.Length; i++)
            {
                asteroids[i] = null;
            }
        }

        void Update()
        {
            asteroidSpawnCooldown -= Raylib.GetFrameTime();
            if (asteroidSpawnCooldown < 0.0f)
            {
                SpawnNewAsteroid();
                asteroidSpawnCooldown = asteroidSpawnCooldownReset;
            }

            player.Update();

            for (int i = 0; i < bullets.Length; i++)
            {
                if (bullets[i] != null)
                {
                    bullets[i].Update();
                }
            }

            for (int i = 0; i < asteroids.Length; i++)
            {
                if (asteroids[i] != null)
                {
                    asteroids[i].Update();
                }
            }

            foreach (var bullet in bullets)
            {
                foreach (var asteroid in asteroids)
                {
                    DoBulletAsteroidCollision(bullet, asteroid);
                }
            }

            //foreach (var asteoid in asteroids)
            //{
            //    DoplayerAsteroidCollision(player, asteoid);
            //}
        }

        void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.BLACK);

            player.Draw();

            for (int i = 0; i < bullets.Length; i++)
            {
                if (bullets[i] != null)
                {
                    bullets[i].Draw();
                }
            }

            for (int i = 0; i < asteroids.Length; i++)
            {
                if (asteroids[i] != null)
                {
                    asteroids[i].Draw();
                }
            }

            Raylib.EndDrawing();
        }

        public void SpawnBullet(Vector2 pos, Vector2 dir)
        {
            Bullet bullet = new Bullet(this, pos, dir);
            for (int i = 0; i < bullets.Length; i++)
            {
                if (bullets[i] == null)
                {
                    bullets[i] = bullet;
                    break;
                }
            }
        }

        void SpawnNewAsteroid()
        {
            Random rand = new Random();
            int side = rand.Next(0, 4);

            float rot = (float)rand.NextDouble() * MathF.PI * 2.0f;

            Vector2 dir = new Vector2(MathF.Cos(rot), MathF.Sin(rot));

            float radius = 40;

            // left spawn
            if (side == 0) SpawnAsteroid(new Vector2(0, rand.Next(0, windowHeight)), dir, radius);
            // top spawn
            if (side == 1) SpawnAsteroid(new Vector2(rand.Next(0, windowWidth), 0), dir, radius);
            // right spawn
            if (side == 2) SpawnAsteroid(new Vector2(windowWidth, rand.Next(0, windowHeight)), dir, radius);
            // botom spawn
            if (side == 3) SpawnAsteroid(new Vector2(rand.Next(0, windowWidth), windowHeight), dir, radius);
        }

        void SpawnAsteroid(Vector2 pos, Vector2 dir, float radius)
        {
            Asteroid asteroid = new Asteroid(this, pos, dir);
            asteroid.radius = radius;
            for (int i = 0; i < asteroids.Length; i++)
            {
                if (asteroids[i] == null)
                {
                    asteroids[i] = asteroid;
                    break;
                }
            }
        }

        //void DoplayerAsteroidCollision(Player playerCol, Asteroid asteroid)
        //{
        //    if (playerCol == null || asteroid == null)
        //        return;
        //
        //    float distance = (playerCol.position - asteroid.position).Length();
        //    if (distance < asteroid.radius + player.size.X / 2)
        //    {
        //        if (asteroid.radius > 10)
        //        {
        //            SpawnAsteroid(asteroid.position, new Vector2(-asteroid.dir.X, asteroid.dir.Y), asteroid.radius / 4);
        //            SpawnAsteroid(asteroid.position, new Vector2(-asteroid.dir.X,-asteroid.dir.Y), asteroid.radius / 4);
        //            SpawnAsteroid(asteroid.position, new Vector2( asteroid.dir.X,-asteroid.dir.Y), asteroid.radius / 4);
        //            SpawnAsteroid(asteroid.position, new Vector2( asteroid.dir.X, asteroid.dir.Y), asteroid.radius / 4);
        //        }
        //    }
        //}

        void DoBulletAsteroidCollision(Bullet bullet, Asteroid asteroid)
        {
            if (bullet == null || asteroid == null)
                return;

            float distance = (bullet.position - asteroid.position).Length();
            if (distance < asteroid.radius)
            {
                if (asteroid.radius > 10)
                {
                    SpawnAsteroid(asteroid.position, asteroid.direction, asteroid.radius / 2);
                    SpawnAsteroid(asteroid.position, -asteroid.direction, asteroid.radius / 2);
                }

                for (int i = 0; i < bullets.Length; i++)
                {
                    if (bullets[i] == bullet)
                    {
                        bullets[i] = null;
                        break;
                    }
                }

                for (int i = 0; i < asteroids.Length; i++)
                {
                    if (asteroids[i] == asteroid)
                    {
                        asteroids[i] = null;
                        break;
                    }
                }
            }
        }
    }
}

