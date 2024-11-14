using SFML.Graphics;
using SFML.System;

namespace Asteroids.Objects
{
    internal class SpaceShipBullet : SpaceObject
    {
        private RectangleShape bullet;
        private float lifetime = 2f;
        private float elapsedTime = 0f;
        private Texture spaceShipBulletTexture = new Texture(@".\images\spaceship_bullet.png");

        public SpaceShipBullet(Vector2f spawnPosition, float rotation) : base(spawnPosition)
        {
            Size = new Vector2f(150, 150);
            Position = spawnPosition;   
            float rotationRadians = (rotation - 90f) * (float)(Math.PI / 180f);

            Velocity = new Vector2f(
                (float)Math.Cos(rotationRadians) * Global.bulletSpeed,
                (float)Math.Sin(rotationRadians) * Global.bulletSpeed
            );
            bullet = new RectangleShape(Size)
            {
                Position = spawnPosition,
                Texture = spaceShipBulletTexture,
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
