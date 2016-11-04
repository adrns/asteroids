using System;

namespace Asteroids.Model
{
    abstract class GameObject
    {
        protected double size;
        protected double velocity;
        protected double x;
        protected double y;

        public double X { get { return x; } }
        public double Y { get { return y; } }
        public double Size { get { return size; } }

        abstract public void advance();

        public bool collidesWith(GameObject other)
        {
            double distance = Math.Sqrt(
                Math.Pow((x + size / 2.0) - (other.x + other.size / 2.0), 2) +
                Math.Pow((y + size / 2.0) - (other.y + other.size / 2.0), 2));
            return distance <= size / 2 + other.size / 2;
        }
    }
}
