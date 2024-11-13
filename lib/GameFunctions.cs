using Asteroids.Objects;
using Asteroids.Screens;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids.lib
{
    internal static class GameFunctions
    {
        private static Dictionary<Vector2i, Sector> loadedSectors = new Dictionary<Vector2i, Sector>();
        public static void DrawLifes(Vector2f lifePosition)
        {
            RectangleShape life = new RectangleShape(new Vector2f(20, 20))
            {
                FillColor = Color.White,
                Texture = new Texture(@".\images\heart.png")
        };

            for (int i = 0; i < Global.currentLifes; i++)
            {
                life.Position = new Vector2f(lifePosition.X + i * 25, lifePosition.Y);
                Global.gameWindow.Draw(life);
            }
        }

        public static void DrawStarBackground()
        {
            Vector2f shipPosition = GameScreen.GetSpaceShipPosition();

            // Aktuelle Sektorposition des Raumschiffs berechnen
            Vector2i currentSectorPosition = new Vector2i(
                (int)Math.Floor(shipPosition.X / Global.sectorSize),
                (int)Math.Floor(shipPosition.Y / Global.sectorSize)
            );

            // Sektoren innerhalb des Ladebereichs laden
            LoadSectorsAround(currentSectorPosition);

            // Sektoren außerhalb des Bereichs entladen
            UnloadFarSectors(currentSectorPosition);

            // Sterne zeichnen
            foreach (var sector in loadedSectors.Values)
            {
                foreach (Star star in sector.Stars)
                {
                    // Parallaxenverschiebung berechnen
                    Vector2f parallaxPosition = star.Position - (1 - star.ParallaxFactor) * (shipPosition - Global.gameView.Center);

                    // Prüfen, ob der Stern innerhalb der View liegt
                    FloatRect viewRect = new FloatRect(Global.gameView.Center - Global.gameView.Size / 2f, Global.gameView.Size);

                    if (viewRect.Contains(parallaxPosition.X, parallaxPosition.Y))
                    {
                        CircleShape starShape = new CircleShape(star.Radius);
                        starShape.Position = parallaxPosition;
                        starShape.FillColor = Color.White;
                        starShape.Origin = new Vector2f(star.Radius, star.Radius);
                        Global.gameWindow.Draw(starShape);
                    }
                }
            }
        }

        private static void LoadSectorsAround(Vector2i centerSector)
        {
            for (int x = -Global.sectorLoadRadius; x <= Global.sectorLoadRadius; x++)
            {
                for (int y = -Global.sectorLoadRadius; y <= Global.sectorLoadRadius; y++)
                {
                    Vector2i sectorPosition = new Vector2i(centerSector.X + x, centerSector.Y + y);
                    if (!loadedSectors.ContainsKey(sectorPosition))
                    {
                        Sector newSector = new Sector(sectorPosition, Global.starsPerSector, Global.sectorSize);
                        loadedSectors.Add(sectorPosition, newSector);
                    }
                }
            }
        }

        private static void UnloadFarSectors(Vector2i centerSector)
        {
            List<Vector2i> sectorsToUnload = new List<Vector2i>();

            foreach (var sector in loadedSectors)
            {
                int distanceX = Math.Abs(sector.Key.X - centerSector.X);
                int distanceY = Math.Abs(sector.Key.Y - centerSector.Y);

                if (distanceX > Global.sectorLoadRadius || distanceY > Global.sectorLoadRadius)
                {
                    sectorsToUnload.Add(sector.Key);
                }
            }

            foreach (var sectorPosition in sectorsToUnload)
            {
                loadedSectors.Remove(sectorPosition);
            }
        }

        public static void ResetStarBackground()
        {
            loadedSectors.Clear();
        }


        public static float GetDistance(Vector2f a, Vector2f b)
        {
            return (float)Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }
    }
}
