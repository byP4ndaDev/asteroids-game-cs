using Asteroids.Objects;
using Asteroids.Screens;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids.lib
{
    internal static class Events
    {
        private static Random rnd = new Random();
        public static Asteroid SpawnAsteroids(Vector2f playerPosition)
        {
            // Zufälliger Winkel in Radianten
            float angle = (float)(rnd.NextDouble() * 2 * Math.PI);

            // Offset berechnen
            float xOffset = Global.asteroidSpawnRadius * (float)Math.Cos(angle);
            float yOffset = Global.asteroidSpawnRadius * (float)Math.Sin(angle);

            // Spawn-Position festlegen
            Vector2f spawnPosition = new Vector2f(playerPosition.X + xOffset, playerPosition.Y + yOffset);

            // Weitere Eigenschaften des Asteroiden
            float spawnRotation = rnd.Next(0, 361);
            int spawnLifes = rnd.Next(1, 4);

            // Richtung zum Spieler berechnen
            Vector2f direction = playerPosition - spawnPosition;

            // Richtung normalisieren
            float length = (float)Math.Sqrt(direction.X * direction.X + direction.Y * direction.Y);
            Vector2f normalizedDirection = new Vector2f(direction.X / length, direction.Y / length);

            // Zufällige Geschwindigkeit festlegen
            float speed = Global.asteroidMinSpeed + (float)rnd.NextDouble() * (Global.asteroidMaxSpeed - Global.asteroidMinSpeed);

            // Geschwindigkeit setzen
            Vector2f velocity = normalizedDirection * speed;

            // Asteroiden erstellen
            Asteroid asteroid = new Asteroid(spawnPosition, spawnRotation, spawnLifes);
            asteroid.SetVelocity(velocity);

            return asteroid;
        }

        public static void SpawnPowerUp(Vector2f position)
        {
            if(GameScreen._powerUps.Count < Global.maxPowerUps)
            {
                float chance = (float)rnd.NextDouble();
                if (chance <= Global.powerupSpawnChance)
                {
                    PowerUp powerUp = new PowerUp(position);
                    GameScreen._powerUps.Add(powerUp);
                }
            }
        }
        public static void LaserBeamEvent()
        {

        }
    }
}
