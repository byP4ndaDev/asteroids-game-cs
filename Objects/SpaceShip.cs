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
        public SpaceShip(Vector2f position) : base(position)
        {
            spaceShipFlyingTexture = new Texture(@".\images\spaceship.png");
            spaceShipAcceleratingTexture = new Texture(@".\images\spaceship_flame.png");
            spaceShip = new RectangleShape(new Vector2f(100, 100))
            {
                Texture = spaceShipFlyingTexture,
            };
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

            // Geschwindigkeit begrenzen
            float speed = (float)Math.Sqrt(Velocity.X * Velocity.X + Velocity.Y * Velocity.Y);
            if (speed > Global.maxSpeed)
            {
                Velocity = (Velocity / speed) * Global.maxSpeed;
            }

            // Aktualisiere Geschoss- und Trail-Objekte
            for (int i = _spaceShipbullets.Count - 1; i >= 0; i--)
            {
                _spaceShipbullets[i].Update(deltaTime);

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

            // Grafiken anzeigen
            foreach (var trail in _spaceShipTrails)
            {
                trail.Show();
            }
            foreach (var bullet in _spaceShipbullets)
            {
                bullet.Show();
            }

            currentPosition = spaceShip.Position;
        }

        public void Rotate(float angle)
        {
            currentRotation += angle;
            spaceShip.Rotation = currentRotation;
        }
        public Vector2f GetForwardVector()
        {
            // Konvertiere die Rotation in Radianten
            float rotationRadians = (currentRotation - 90f) * (float)(Math.PI / 180f);

            // Berechne den Vorwärtsvektor basierend auf der aktuellen Rotation
            return new Vector2f((float)Math.Cos(rotationRadians), (float)Math.Sin(rotationRadians));
        }


        public void Accelerate(Vector2f acceleration, float deltaTime)
        {
            Velocity += acceleration * deltaTime;

            if (Global.isAccelerating)
            {
                Vector2f spawnPosition = GetPosition();
                float rotation = GetRotation();

                SpaceShipTrail trail = new SpaceShipTrail(spawnPosition, rotation);
                _spaceShipTrails.Add(trail);
            }
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
