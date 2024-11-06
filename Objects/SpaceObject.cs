using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids.Objects
{
    internal abstract class SpaceObject
    {
        public Vector2f Position { get; protected set; }
        public Vector2f Velocity { get; protected set; } = new Vector2f(0, 0);
        public Drawable objectToDraw { get; protected set; }

        public Vector2f Size { get; protected set; }

        public virtual void Show()
        {
            //IEnumerable<Type> alleTypen;
            //alleTypen = Assembly.GetExecutingAssembly().GetTypes();
            //IEnumerable<Type> areaKinder;
            //areaKinder = alleTypen.Where(kids => kids.IsAbstract);
            if (objectToDraw != null) Global.gameWindow.Draw(objectToDraw);
            else Console.WriteLine("Error: Object is not To call the Show Method from SpaceObject you need to set objectToDraw");
        }
        public virtual void Update(float deltaTime)
        {
            Position += Velocity * deltaTime;

            if (objectToDraw is Transformable transformable)
            {
                transformable.Position = Position;
            }
        }

        public void Reset()
        {
            Velocity = new Vector2f(0, 0);
            Position = new Vector2f(0, 0);
        }
    }
}
