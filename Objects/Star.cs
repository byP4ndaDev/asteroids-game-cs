using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids.Objects
{
    internal class Star
    {
        public Vector2f Position { get; set; }
        public float Radius { get; set; }
        public float ParallaxFactor { get; set; }

        public Star(Vector2f position, float radius, float parallaxFactor)
        {
            Position = position;
            Radius = radius;
            ParallaxFactor = parallaxFactor;
        }
    }

}
