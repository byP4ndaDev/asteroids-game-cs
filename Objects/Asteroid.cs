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

        public Asteroid(Vector2f spawnPosition, float rotation, int startLife)
        {
            currentLife = startLife;
            Texture spawnTexture = null;
            Vector2f size = new Vector2f(100, 100);
            switch (startLife)
            {
                case 3:
                    spawnTexture = bigAsteroidTexture;
                    size = new Vector2f(130, 130);
                    break;
                case 2:
                    spawnTexture = mediumAsteroidTexture;
                    size = new Vector2f(100, 100);
                    break;
                case 1:
                    spawnTexture = smallAsteroidTexture;
                    size = new Vector2f(100, 100);
                    break;
                default:
                    break;
            }
            Size = size;
            Position = spawnPosition;
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
    }
}
