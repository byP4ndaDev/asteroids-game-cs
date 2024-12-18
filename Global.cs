﻿using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Asteroids
{    
    enum GameState
    {
        MainMenu,
        Game,
        Paused,
        End
    }
    internal static class Global
    {
        private static int _currentLifes = 3;

        // Window
        public static Font font { get; } = new Font("fonts/VectorBattle-e9XO.ttf");
        public static RenderWindow gameWindow { get; set; } = null;
        public static VideoMode desktopMode { get; set; } = VideoMode.DesktopMode;
        public static View gameView { get; set; } = new View(new FloatRect(0, 0, desktopMode.Width, desktopMode.Height));
        public static Vector2f viewSize = gameView.Size;
        public static Vector2f viewCenter = gameView.Center;
        public static Vector2f viewTopLeft = viewCenter - (viewSize / 2f);
        
        // Game
        public static GameState currentGameState { get; set; } = GameState.MainMenu;
        public static bool isLeftMouseButtonPressed { get; set; } = false;
        public static int currentLifes
        {
            get { return _currentLifes; }
            set
            {
                _currentLifes = value;
                if (_currentLifes <= 0)
                {
                    _currentLifes = 0;
                    GameManager.EndGame();
                }
            }
        }
        public static int currentScore { get; set; } = 0;

        // SPACE SHIP
        public static float accelerationAmount { get; } = 100f;
        public static float maxSpeed { get; } = 1000f;
        public static float rotationSpeed { get; } = 180f; // Rotationsgeschwindigkeit in Grad pro Sekunde
        public static float decelerationAmount { get; } = 200f; // Verzögerungsbetrag
        public static float cameraSpeed { get;  } = 5f;
        public static bool isAccelerating { get; set; } = false;

        // SPACE SHIP BULLET
        public static float bulletSpeed { get; } = 1000f;
        public static float bulletSpawnRate { get; } = 50f;
        // ASTEROIDS
        public static float asteroidSpawnRate { get; } = 30f;
        public static float asteroidMinSpeed { get; } = 40f;
        public static float asteroidMaxSpeed { get; } = 90f;
        public static float asteroidSpawnRadius { get; } = 1000f;
        public static float asteroidDespawnRadius { get; } = 1200f;

        // POWERUP
        public static float powerupSpawnChance { get; } = 0.05f;
        public static int maxPowerUps { get; } = 3;

        // STARS
        public static float sectorSize { get; } = 1000f; // Größe eines Sektors
        public static int starsPerSector { get; } = 200; // Anzahl der Sterne pro Sektor
        public static int sectorLoadRadius { get; } = 2; // Wie viele Sektoren um das Raumschiff herum geladen werden sollen

    }
}
