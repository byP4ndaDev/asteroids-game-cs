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
    internal class PauseScreen : StateScreen
    {
        private static PauseScreen instance;

        // Private Konstruktor, um zu verhindern, dass neue Instanzen von außen erstellt werden
        private PauseScreen() { }

        // Öffentliche Methode, um die einzige Instanz der Klasse zu erhalten
        public static PauseScreen GetInstance()
        {
            if (instance == null)
            {
                instance = new PauseScreen();
            }
            return instance;
        }

        public override void Show()
        {

            RectangleShape pauseOverlay = new RectangleShape(Global.viewSize)
            {
                FillColor = new Color(0, 0, 0, 200),
                Position = Global.viewTopLeft
            };

            // "Paused"-Text erstellen und zentrieren
            Text pauseText = new Text("Paused", Global.font, 150)
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
            RectangleShape resumeButtonBackground = new RectangleShape(buttonSize)
            {
                FillColor = Color.Black,
                OutlineColor = Color.White,
                OutlineThickness = 0.3f
            };
            resumeButtonBackground.Origin = UiFunctions.CenterOrigin(buttonSize);
            resumeButtonBackground.Position = UiFunctions.CenterPosition();

            Text resumeButtonText = new Text("Resume", Global.font, 70)
            {
                FillColor = Color.White,
                OutlineColor = Color.White,
                OutlineThickness = 0.5f
            };
            FloatRect resumeTextBounds = resumeButtonText.GetLocalBounds();
            resumeButtonText.Origin = UiFunctions.CenterOrigin(resumeTextBounds);
            resumeButtonText.Position = resumeButtonBackground.Position;

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
            bool isHoveringResume = resumeButtonBackground.GetGlobalBounds().Contains(mousePosInView.X, mousePosInView.Y);
            bool isHoveringExit = exitButtonBackground.GetGlobalBounds().Contains(mousePosInView.X, mousePosInView.Y);

            // Hover-Effekt
            resumeButtonBackground.OutlineThickness = isHoveringResume ? 1f : 0f;
            exitButtonBackground.OutlineThickness = isHoveringExit ? 1f : 0f;

            // Klickereignisse überprüfen
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                if (!Global.isLeftMouseButtonPressed)
                {
                    Global.isLeftMouseButtonPressed = true;
                    if (isHoveringResume)
                    {
                        Console.WriteLine($"Pause >> Continue Button Clicked");
                        GameManager.StartTimer();
                        Global.currentGameState = GameState.Game;
                        Global.gameWindow.Clear();
                    }
                    if (isHoveringExit)
                    {
                        Console.WriteLine($"Pause >> Home Button Clicked");
                        Global.currentGameState = GameState.MainMenu;
                        //GameManager.ResetGame();
                        Global.gameWindow.Clear();
                        GameScreen.ResetScreen();
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
            Global.gameWindow.Draw(resumeButtonBackground);
            Global.gameWindow.Draw(resumeButtonText);
            Global.gameWindow.Draw(exitButtonBackground);
            Global.gameWindow.Draw(exitButtonText);
        }

    }
}
