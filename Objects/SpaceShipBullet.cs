using SFML.Graphics;
using SFML.System;

namespace Asteroids.Objects
{
    internal class SpaceShipBullet : SpaceObject
    {
        private RectangleShape bullet;
        private float lifetime = 2f;
        private float elapsedTime = 0f;

        public SpaceShipBullet(Vector2f spawnPosition, float rotation)
        {
            Size = new Vector2f(5, 15);
            Position = spawnPosition;
            float rotationRadians = (rotation - 90f) * (float)(Math.PI / 180f);

            Velocity = new Vector2f(
                (float)Math.Cos(rotationRadians) * Global.bulletSpeed,
                (float)Math.Sin(rotationRadians) * Global.bulletSpeed
            );
            bullet = new RectangleShape(Size)
            {
                Position = spawnPosition,
                FillColor = Color.Cyan,
                Origin = new Vector2f(Size.X / 2, Size.Y / 2),
                Rotation = rotation
            };
            objectToDraw = bullet;

        }

        public void Update(float deltaTime)
        {
            Position += Velocity * deltaTime;
            bullet.Position = Position;
            elapsedTime += deltaTime;
            
        }

        public bool IsExpired()
        {
            return elapsedTime > lifetime;
        }
    }

}
