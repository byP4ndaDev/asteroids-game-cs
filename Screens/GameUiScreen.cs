using Asteroids.lib;
using Asteroids.Objects;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace Asteroids.Screens
{
    internal static class GameUiScreen
    {
        public static void Show()
        {
            Text scoreText = new Text("0", Global.font, 50)
            {
                Position = Global.viewTopLeft
            };

           
            Global.currentScore++;
            scoreText.DisplayedString = Global.currentScore.ToString();

            GameFunctions.DrawLifes(new Vector2f(Global.viewTopLeft.X, Global.viewTopLeft.Y + 60));
            Global.gameWindow.Draw(scoreText);
        }
    }
}
