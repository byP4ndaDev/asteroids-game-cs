using System;
using System.Collections.Generic;
using SFML.System;

namespace Asteroids.Objects
{
    internal class Sector
    {
        public Vector2i SectorPosition { get; private set; }
        public List<Star> Stars { get; private set; }

        public Sector(Vector2i sectorPosition, int starsPerSector, float sectorSize)
        {
            SectorPosition = sectorPosition;
            Stars = GenerateStars(sectorPosition, starsPerSector, sectorSize);
        }

        private List<Star> GenerateStars(Vector2i sectorPosition, int starsPerSector, float sectorSize)
        {
            List<Star> stars = new List<Star>();

            // Seed basierend auf der Sektorposition generieren
            int seed = sectorPosition.X * 73856093 ^ sectorPosition.Y * 19349663;
            Random random = new Random(seed);

            for (int i = 0; i < starsPerSector; i++)
            {
                float x = (float)(random.NextDouble() * sectorSize + sectorPosition.X * sectorSize);
                float y = (float)(random.NextDouble() * sectorSize + sectorPosition.Y * sectorSize);

                float starRadius = (float)(random.NextDouble() * 2f) + 0.5f; // Radius zwischen 0.5 und 2.5

                // ParallaxFactor zwischen 0.5 und 1.0
                float parallaxFactor = (float)(random.NextDouble() * 0.5) + 0.5f;

                Star newStar = new Star(new Vector2f(x, y), starRadius, parallaxFactor);
                stars.Add(newStar);
            }

            return stars;
        }
    }
}
