using Asteroids.Objects;
using Asteroids.Screens;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
namespace Asteroids
{
    internal class GameWindow
    {
        public GameWindow(string title, Color backgroundColor)
        {
            Global.gameWindow = new RenderWindow(new VideoMode(Global.desktopMode.Width, Global.desktopMode.Height), title, Styles.Fullscreen);
            Global.gameWindow.SetView(Global.gameView);
            // Global.gameWindow.SetIcon();
            Global.gameWindow.Closed += (sender, e) => Global.gameWindow.Close();


            bool pauseKeyPressed = false;

            Clock clock = new Clock();

            while (Global.gameWindow.IsOpen)
            {
                Time deltaTime = clock.Restart();

                Global.gameWindow.DispatchEvents();
                Global.gameWindow.Clear(backgroundColor);

                Global.viewSize = Global.gameView.Size;
                Global.viewCenter = Global.gameView.Center;
                Global.viewTopLeft = Global.viewCenter - (Global.viewSize / 2f);

                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape) && (Global.currentGameState == GameState.Game || Global.currentGameState == GameState.Paused))
                {
                    if (!pauseKeyPressed)
                    {
                        pauseKeyPressed = true;
                        if (Global.currentGameState != GameState.Paused)
                        {
                            GameManager.StopTimer();
                            Global.currentGameState = GameState.Paused;
                        }
                        else
                        {
                            GameManager.StartTimer();
                            Global.currentGameState = GameState.Game;
                        }
                    }
                }
                else
                {
                    pauseKeyPressed = false;
                }

                if (Global.currentGameState == GameState.Game || Global.currentGameState == GameState.End)
                {
                    GameScreen.Update(deltaTime.AsSeconds());
                }

                if (Global.currentGameState == GameState.MainMenu)
                {
                    HomeScreen.GetInstance().Show();
                }
                if (Global.currentGameState == GameState.Game || Global.currentGameState == GameState.Paused || Global.currentGameState == GameState.End)
                {
                    GameUiScreen.Show();
                    GameScreen.Show();
                }
                if (Global.currentGameState == GameState.Paused)
                {
                    PauseScreen.GetInstance().Show();
                }
                if (Global.currentGameState == GameState.End)
                {
                    EndScreen.GetInstance().Show();
                }

                Global.gameWindow.Display();
            }
        }
    }
}
