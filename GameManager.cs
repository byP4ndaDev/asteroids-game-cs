using Asteroids.lib;
using Asteroids.Screens;
using SFML.Graphics;
using System.Timers;

namespace Asteroids
{
    internal static class GameManager
    {
        private static System.Timers.Timer timer = new System.Timers.Timer();
        public static bool isTimerPaused { get; private set; } = false;
        public static int ticks { get; private set; } = 0;
        public static void ResetGame()
        {
            ResetTimer();
            GameScreen.ResetScreen();
            GameFunctions.ResetStarBackground();
            Global.currentGameState = GameState.Game;
            Global.currentLifes = 3;
            Global.currentScore = 0;
            StartTimer();
        }

        public static void EndGame()
        {
            StopTimer();
            Console.WriteLine($"GameManager >> Game end... Score: {Global.currentScore}");
            Global.currentGameState = GameState.End;
        }

        // ZWISCHEN DURCH KOMMT EIN ANGEKÜNDIGTER LASER DURCH DEN BILDSCHIRM

        public static void StartTimer()
        {
            if(isTimerPaused)
            {
                isTimerPaused = false;
                timer.Enabled = true;
                Console.WriteLine("GameManager >> Game continues...");
            }

        }
        public static void StopTimer()
        {
            if(!isTimerPaused)
            {
                isTimerPaused = true;
                timer.Enabled = false;
                Console.WriteLine("GameManager >> Game stopped...");
            }
        }

        public static void ResetTimer()
        {
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = false;
            isTimerPaused = true;
        }
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if(!isTimerPaused)
            {
                Console.WriteLine("Tick: {0:HH:mm:ss.fff}", e.SignalTime);
                Console.WriteLine($"Tick: {e.SignalTime}");
                Console.WriteLine($"Tick: {ticks}");
            }
        }
    }
}
