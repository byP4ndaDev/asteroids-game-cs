using Asteroids.lib;
using Asteroids.Objects;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;


namespace Asteroids.Screens
{
    public static class GameScreen
    {
        private static Vector2f deadZoneSize = new Vector2f(400f, 300f);
        

        private static SpaceShip spaceShip = new SpaceShip();


        public static void Update(float deltaTime)
        {
            Vector2f acceleration = new Vector2f(0, 0);
            Keyboard.Key[] accelerationKeys = new Keyboard.Key[]
            {
                Keyboard.Key.W,
                Keyboard.Key.A,
                Keyboard.Key.S,
                Keyboard.Key.D
            };

            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                spaceShip.ShootBullet();
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                acceleration.Y -= Global.accelerationAmount;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                acceleration.Y += Global.accelerationAmount;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                acceleration.X -= Global.accelerationAmount;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                acceleration.X += Global.accelerationAmount;
            }

            if (acceleration != new Vector2f(0, 0))
            {
                // Winkel aus dem Beschleunigungsvektor berechnen
                float angle = (float)(Math.Atan2(acceleration.Y, acceleration.X) * 180 / Math.PI);
                spaceShip.SetRotation(angle + 90f); // +90°, falls nötig
            }

            foreach(Keyboard.Key key in accelerationKeys)
            {
                if (Keyboard.IsKeyPressed(key))
                {
                    Global.isAccelerating = true;
                    Console.WriteLine($"{key} is pressed, {Global.isAccelerating}");
                }
                else Global.isAccelerating = false;
            }
            spaceShip.Accelerate(acceleration, deltaTime);
            spaceShip.Update(deltaTime);
            UpdateCameraPosition(deltaTime);
        }


        public static void Show()
        {
            GameFunctions.DrawStarBackground();
            spaceShip.Show();
        }
        public static Vector2f GetSpaceShipPosition()
        {
            return spaceShip.Position;
        }
        private static void UpdateCameraPosition(float deltaTime)
        {
            Vector2f shipPosition = GetSpaceShipPosition();
            Vector2f cameraPosition = Global.gameView.Center;

            // Berechnen Sie die Differenz zwischen Raumschiff und Kamera
            Vector2f deltaPosition = shipPosition - cameraPosition;

            // Dead Zone Grenzen
            float leftBoundary = -deadZoneSize.X / 2;
            float rightBoundary = deadZoneSize.X / 2;
            float topBoundary = -deadZoneSize.Y / 2;
            float bottomBoundary = deadZoneSize.Y / 2;

            // Initialisieren Sie die Zielposition der Kamera
            Vector2f targetPosition = cameraPosition;

            // Überprüfen Sie, ob das Raumschiff die Dead Zone horizontal verlassen hat
            if (deltaPosition.X < leftBoundary)
            {
                targetPosition.X = shipPosition.X - leftBoundary;
            }
            else if (deltaPosition.X > rightBoundary)
            {
                targetPosition.X = shipPosition.X - rightBoundary;
            }

            // Überprüfen Sie vertikal
            if (deltaPosition.Y < topBoundary)
            {
                targetPosition.Y = shipPosition.Y - topBoundary;
            }
            else if (deltaPosition.Y > bottomBoundary)
            {
                targetPosition.Y = shipPosition.Y - bottomBoundary;
            }

            // Kamera sanft zur Zielposition bewegen
            Vector2f direction = targetPosition - cameraPosition;

            if (direction != new Vector2f(0, 0))
            {
                cameraPosition += direction * Global.cameraSpeed * deltaTime;
                Global.gameView.Center = cameraPosition;
                Global.gameWindow.SetView(Global.gameView);
            }
        }

        public static void ResetScreen()
        {
            spaceShip.Reset();
        }

    }


}
