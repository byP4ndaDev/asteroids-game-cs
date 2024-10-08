using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids.lib
{
    internal static class UiFunctions
    {
        public static Vector2f CenterOrigin(FloatRect textBounds)
        {
            return new Vector2f(textBounds.Left + textBounds.Width / 2.0f, textBounds.Top + textBounds.Height / 2.0f);
        }
        public static Vector2f CenterOrigin(Vector2f item)
        {
            return new Vector2f(item.X / 2.0f, item.Y / 2.0f);
        }
        public static Vector2f CenterPosition()
        {
            return new Vector2f(Global.viewCenter.X, Global.viewCenter.Y);
        }
        public static Vector2f CenterPosition(float YPosition)
        {
            return new Vector2f(Global.viewCenter.X, YPosition);
        }
    }
}
