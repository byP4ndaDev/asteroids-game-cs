using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids.Objects
{
    internal class SpaceShip: SpaceObject
    {
        private Texture spaceShipFlyingTexture;
        private Texture spaceShipAcceleratingTexture;
        private RectangleShape spaceShip;
        private float currentRotation = 0f;
        private float targetRotation = 0f;
        private Vector2f currentPosition;
        public List<SpaceShipBullet> _spaceShipbullets { get; private set; } = new List<SpaceShipBullet>();
        private List<SpaceShipTrail> _spaceShipTrails = new List<SpaceShipTrail>();
        // ASTEROIDS SOLLEN AUF DAS RAUMSCHIFF ZUFLIEGEN
        public SpaceShip()
        {
            spaceShipFlyingTexture = new Texture(@".\images\spaceship.png");
            spaceShipAcceleratingTexture = new Texture(@".\images\spaceship_flame.png");
            spaceShip = new RectangleShape(new Vector2f(100, 100))
            {
                Texture = spaceShipFlyingTexture,
            };
            Position = Global.gameView.Center;
            spaceShip.Position = Position;
            spaceShip.Origin = new Vector2f(50, 50);
            objectToDraw = spaceShip;
        }

        public void Update(float deltaTime)
        {

            spaceShip.Texture = Global.isAccelerating ? spaceShipAcceleratingTexture : spaceShipFlyingTexture;

            for (int i = _spaceShipbullets.Count - 1; i >= 0; i--)
            {
                _spaceShipbullets[i].Update(deltaTime);

                // Kugel entfernen, wenn sie abgelaufen ist
                if (_spaceShipbullets[i].IsExpired())
                {
                    _spaceShipbullets.RemoveAt(i);
                }
            }
            for (int i = _spaceShipTrails.Count - 1; i >= 0; i--)
            {
                _spaceShipTrails[i].Update(deltaTime);
                if (_spaceShipTrails[i].IsExpired())
                {
                    _spaceShipTrails.RemoveAt(i);
                }
            }


            // Position aktualisieren
            Position += Velocity * deltaTime;
            spaceShip.Position = Position;

            // Reibung anwenden
            float friction = 0.9f;
            Velocity *= (float)Math.Pow(friction, deltaTime);
            if (Velocity.X > Global.maxSpeed) Velocity = new Vector2f(Global.maxSpeed, Velocity.Y);
            if (Velocity.X < -Global.maxSpeed) Velocity = new Vector2f(-Global.maxSpeed, Velocity.Y);
            if (Velocity.Y > Global.maxSpeed) Velocity = new Vector2f(Velocity.X, Global.maxSpeed);
            if (Velocity.Y < -Global.maxSpeed) Velocity = new Vector2f(Velocity.X, -Global.maxSpeed);

            // Sanft zur Zielrotation drehen
            float rotationSpeed = 180f; // Maximale Rotationsgeschwindigkeit in Grad pro Sekunde
            float angleDifference = targetRotation - currentRotation;

            // Winkelunterschied auf [-180°, 180°] normalisieren
            if (angleDifference > 180f)
                angleDifference -= 360f;
            else if (angleDifference < -180f)
                angleDifference += 360f;

            // Maximale Rotation für diesen Frame berechnen
            float maxRotationThisFrame = rotationSpeed * deltaTime;

            // Tatsächliche Rotation für diesen Frame
            float rotationThisFrame = Math.Max(-maxRotationThisFrame, Math.Min(angleDifference, maxRotationThisFrame));

            currentRotation += rotationThisFrame;

            // Rotation setzen
            spaceShip.Rotation = currentRotation;
            currentPosition = spaceShip.Position;

            foreach(var trail in _spaceShipTrails)
            {
                trail.Show();
            }
            foreach(var bullet in _spaceShipbullets)
            {
                bullet.Show();
            }
        }


        public void Accelerate(Vector2f acceleration, float deltaTime)
        {
            Velocity += acceleration * deltaTime;
            Vector2f spawnPosition = GetPosition();
            float rotation = GetRotation();

            SpaceShipTrail trail = new SpaceShipTrail(spawnPosition, rotation);
            _spaceShipTrails.Add(trail);
        }

        public void SetRotation(float angle)
        {
            targetRotation = angle;
        }

        public float GetRotation()
        {
            return currentRotation;
        }

        public Vector2f GetPosition()
        {
            return currentPosition;
        }

        public void ShootBullet()
        {
            Vector2f spawnPosition = GetPosition();
            float rotation = GetRotation();

            SpaceShipBullet bullet = new SpaceShipBullet(spawnPosition, rotation);
            _spaceShipbullets.Add(bullet);
        }

        public void RemoveBullet(SpaceShipBullet bullet)
        {
            _spaceShipbullets.Remove(bullet);
        }
    }


}
