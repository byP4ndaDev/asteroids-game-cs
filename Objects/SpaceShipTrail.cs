using SFML.Graphics;
using SFML.System;

namespace Asteroids.Objects
{
    internal class SpaceShipTrail: SpaceObject
    {
        private RectangleShape bullet;
        private float lifetime = 1f;
        private float elapsedTime = 0f;

        public SpaceShipTrail(Vector2f spawnPosition, float rotation)
        {
            Size = new Vector2f(5, 15);
            Position = spawnPosition;
            bullet = new RectangleShape(Size)
            {
                Position = spawnPosition,
                FillColor = new Color(20, 20, 20),
                Origin = new Vector2f(Size.X / 2, Size.Y / 2),
                Rotation = rotation
            };
            objectToDraw = bullet;

        }

        public void Update(float deltaTime)
        {
            elapsedTime += deltaTime;
        }

        public bool IsExpired()
        {
            return elapsedTime > lifetime;
        }
    }
}
