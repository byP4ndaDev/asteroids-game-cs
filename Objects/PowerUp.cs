using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids.Objects
{
    internal class PowerUp : SpaceObject
    {
        private RectangleShape powerUp;
        private Texture powerUpTexture = new Texture(@".\images\PowerUp.png");

        public PowerUp(Vector2f spawnPosition)
        {
            Position = spawnPosition;
            powerUp = new RectangleShape(new Vector2f(70, 70))
            {
                Position = spawnPosition,
                Origin = new Vector2f(powerUpTexture.Size.X / 2, powerUpTexture.Size.Y / 2),
                Texture = powerUpTexture
            };
            objectToDraw = powerUp;
        }

        public bool IsInPowerUp(Vector2f position)
        {
            return powerUp.GetGlobalBounds().Contains(position.X, position.Y);
        }
    }
}
