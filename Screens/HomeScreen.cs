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
    internal class HomeScreen : StateScreen
    {
        private static HomeScreen instance;

        // Private Konstruktor, um zu verhindern, dass neue Instanzen von außen erstellt werden
        private HomeScreen() { }

        // Öffentliche Methode, um die einzige Instanz der Klasse zu erhalten
        public static HomeScreen GetInstance()
        {
            if (instance == null)
            {
                instance = new HomeScreen();
            }
            return instance;
        }

        public override void Show()
        {

            // "Paused"-Text erstellen und zentrieren
            Text titleText = new Text("Asteroids", Global.font, 150)
            {
                FillColor = Color.White,
                OutlineColor = Color.White,
                OutlineThickness = 0.5f
            };
            FloatRect textBounds = titleText.GetLocalBounds();
            titleText.Origin = UiFunctions.CenterOrigin(textBounds);
            titleText.Position = Global.viewCenter - new Vector2f(0, 300);

            // Button-Größe und Positionen
            Vector2f buttonSize = new Vector2f(400, 100);
            float buttonYSpacing = 150.0f;
            float firstButtonY = Global.viewCenter.Y + 100f;

            // "Resume"-Button erstellen und zentrieren
            RectangleShape startButtonBackground = new RectangleShape(buttonSize)
            {
                FillColor = Color.Black,
                OutlineColor = Color.White,
                OutlineThickness = 0.3f
            };
            startButtonBackground.Origin = UiFunctions.CenterOrigin(buttonSize);
            startButtonBackground.Position = UiFunctions.CenterPosition();

            Text startButtonText = new Text("Start", Global.font, 70)
            {
                FillColor = Color.White,
                OutlineColor = Color.White,
                OutlineThickness = 0.5f
            };
            FloatRect resumeTextBounds = startButtonText.GetLocalBounds();
            startButtonText.Origin = UiFunctions.CenterOrigin(resumeTextBounds);
            startButtonText.Position = startButtonBackground.Position;

            // "Menu"-Button erstellen und zentrieren
            RectangleShape exitButtonBackground = new RectangleShape(buttonSize)
            {
                FillColor = Color.Black,
                OutlineColor = Color.White,
                OutlineThickness = 0.5f
            };
            exitButtonBackground.Origin = UiFunctions.CenterOrigin(buttonSize);
            exitButtonBackground.Position = UiFunctions.CenterPosition(Global.viewCenter.Y + buttonYSpacing);

            Text exitButtonText = new Text("Exit", Global.font, 70)
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
            bool isHoveringStart = startButtonBackground.GetGlobalBounds().Contains(mousePosInView.X, mousePosInView.Y);
            bool isHoveringExit = exitButtonBackground.GetGlobalBounds().Contains(mousePosInView.X, mousePosInView.Y);

            // Hover-Effekt
            startButtonBackground.OutlineThickness = isHoveringStart ? 1f : 0f;
            exitButtonBackground.OutlineThickness = isHoveringExit ? 1f : 0f;

            // Klickereignisse überprüfen
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                if (!Global.isLeftMouseButtonPressed)
                {
                    Global.isLeftMouseButtonPressed = true;
                    if (isHoveringStart)
                    {
                        Console.WriteLine($"HomeScreen >> Continue Button Clicked");
                        GameManager.StartTimer();
                        Global.currentGameState = GameState.Game;
                        Global.gameWindow.Clear();
                    }
                    if (isHoveringExit)
                    {
                        Console.WriteLine($"HomeScreen >> Home Button Clicked");
                        Global.gameWindow.Close();
                    }
                }
            }
            else
            {
                Global.isLeftMouseButtonPressed = false;
            }
            Global.gameWindow.Draw(titleText);
            Global.gameWindow.Draw(startButtonBackground);
            Global.gameWindow.Draw(startButtonText);
            Global.gameWindow.Draw(exitButtonBackground);
            Global.gameWindow.Draw(exitButtonText);
        }
    }
}
