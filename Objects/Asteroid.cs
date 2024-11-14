using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids.Objects
{
    internal class Asteroid : SpaceObject
    {
        private RectangleShape asteroid;
        private Texture bigAsteroidTexture = new Texture(@".\images\astroid3.png");
        private Texture mediumAsteroidTexture = new Texture(@".\images\astroid2.png");
        private Texture smallAsteroidTexture = new Texture(@".\images\astroid1.png");
        private int currentLife = 0;
        public int asteroidPoints { get; private set; } = 0;

        public Asteroid(Vector2f spawnPosition, float rotation, int startLife) : base(spawnPosition)
        {
            currentLife = startLife;
            Texture spawnTexture = null;
            Vector2f size = new Vector2f(100, 100);
            switch (startLife)
            {
                case 3:
                    spawnTexture = bigAsteroidTexture;
                    size = new Vector2f(130, 130);
                    asteroidPoints = 45;
                    break;
                case 2:
                    spawnTexture = mediumAsteroidTexture;
                    size = new Vector2f(100, 100);
                    asteroidPoints = 30;
                    break;
                case 1:
                    spawnTexture = smallAsteroidTexture;
                    size = new Vector2f(100, 100);
                    asteroidPoints = 15;
                    break;
                default:
                    break;
            }
            Size = size;

            asteroid = new RectangleShape(Size)
            {
                Position = spawnPosition,
                Texture = spawnTexture,
                Origin = new Vector2f(Size.X / 2, Size.Y / 2),
                Rotation = rotation
            };
            objectToDraw = asteroid;

        }

        public void SetVelocity(Vector2f velocity)
        {
            this.Velocity = velocity;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            asteroid.Position = Position;
        }

        public bool SetLifesAndCheckIfAlive()
        {
            currentLife--;
            if (currentLife <= 0) return true;
            return false;
        }

        public bool IsInAsteroid(Vector2f position)
        {
            Vector2f localPosition = position - asteroid.Position;
            float angleInRadians = -MathF.PI * asteroid.Rotation / 180f;
            float cosTheta = MathF.Cos(angleInRadians);
            float sinTheta = MathF.Sin(angleInRadians);
            float rotatedX = localPosition.X * cosTheta - localPosition.Y * sinTheta;
            float rotatedY = localPosition.X * sinTheta + localPosition.Y * cosTheta;

            float halfWidth = Size.X / 2f;
            float halfHeight = Size.Y / 2f;

            if (MathF.Abs(rotatedX) <= halfWidth && MathF.Abs(rotatedY) <= halfHeight)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
