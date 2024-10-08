using Asteroids;
using Asteroids.lib;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids.Screens
{
    internal static class EndScreen
    {
        public static void Show()
        {

            RectangleShape pauseOverlay = new RectangleShape(Global.viewSize)
            {
                FillColor = new Color(0, 0, 0, 200),
                Position = Global.viewTopLeft
            };

            // "Paused"-Text erstellen und zentrieren
            Text pauseText = new Text("Game Over", Global.font, 150)
            {
                FillColor = Color.White,
                OutlineColor = Color.White,
                OutlineThickness = 0.5f
            };
            FloatRect textBounds = pauseText.GetLocalBounds();
            pauseText.Origin = UiFunctions.CenterOrigin(textBounds);
            pauseText.Position = Global.viewCenter - new Vector2f(0, 300);

            // Button-Größe und Positionen
            Vector2f buttonSize = new Vector2f(400, 100);
            float buttonYSpacing = 150.0f;
            float firstButtonY = Global.viewCenter.Y + 100f;

            // "Resume"-Button erstellen und zentrieren
            RectangleShape restartButtonBackground = new RectangleShape(buttonSize)
            {
                FillColor = Color.Black,
                OutlineColor = Color.White,
                OutlineThickness = 0.3f
            };
            restartButtonBackground.Origin = UiFunctions.CenterOrigin(buttonSize);
            restartButtonBackground.Position = UiFunctions.CenterPosition();

            Text restartButtonText = new Text("Restart", Global.font, 70)
            {
                FillColor = Color.White,
                OutlineColor = Color.White,
                OutlineThickness = 0.5f
            };
            FloatRect resumeTextBounds = restartButtonText.GetLocalBounds();
            restartButtonText.Origin = UiFunctions.CenterOrigin(resumeTextBounds);
            restartButtonText.Position = restartButtonBackground.Position;

            // "Menu"-Button erstellen und zentrieren
            RectangleShape exitButtonBackground = new RectangleShape(buttonSize)
            {
                FillColor = Color.Black,
                OutlineColor = Color.White,
                OutlineThickness = 0.5f
            };
            exitButtonBackground.Origin = UiFunctions.CenterOrigin(buttonSize);
            exitButtonBackground.Position = UiFunctions.CenterPosition(Global.viewCenter.Y + buttonYSpacing);

            Text exitButtonText = new Text("Menu", Global.font, 70)
            {
                FillColor = Color.White,
                OutlineColor = Color.White,
                OutlineThickness = 0.5f
            };
            FloatRect exitTextBounds = exitButtonText.GetLocalBounds();
            exitButtonText.Origin = UiFunctions.CenterOrigin(exitTextBounds);
            exitButtonText.Position = exitButtonBackground.Position;

            // Mausposition erhalten und umrechnen
            Vector2i mousePos = Mouse.GetPosition(Global.gameWindow);
            Vector2f mousePosInView = Global.gameWindow.MapPixelToCoords(mousePos);

            // Überprüfen, ob die Maus über einem Button ist
            bool isHoveringRestart = restartButtonBackground.GetGlobalBounds().Contains(mousePosInView.X, mousePosInView.Y);
            bool isHoveringExit = exitButtonBackground.GetGlobalBounds().Contains(mousePosInView.X, mousePosInView.Y);

            // Hover-Effekt
            restartButtonBackground.OutlineThickness = isHoveringRestart ? 1f : 0f;
            exitButtonBackground.OutlineThickness = isHoveringExit ? 1f : 0f;

            // Klickereignisse überprüfen
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                if (!Global.isLeftMouseButtonPressed)
                {
                    Global.isLeftMouseButtonPressed = true;
                    if (isHoveringRestart)
                    {
                        Console.WriteLine($"EndScreen >> Restart Button Clicked");
                        GameManager.ResetGame();
                        Global.gameWindow.Clear();
                    }
                    if (isHoveringExit)
                    {
                        Console.WriteLine($"EndScreen >> Home Button Clicked");
                        Global.currentGameState = GameState.MainMenu;
                        Global.gameWindow.Clear();
                    }
                }
            }
            else
            {
                Global.isLeftMouseButtonPressed = false;
            }

            // Elemente zeichnen
            Global.gameWindow.Draw(pauseOverlay);
            Global.gameWindow.Draw(pauseText);
            Global.gameWindow.Draw(restartButtonBackground);
            Global.gameWindow.Draw(restartButtonText);
            Global.gameWindow.Draw(exitButtonBackground);
            Global.gameWindow.Draw(exitButtonText);
        }
    }
}
