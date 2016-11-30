using System;

namespace Asteroids.Model
{
    public abstract class GameObject
    {
        protected double size;
        protected double velocityX;
        protected double velocityY;
        protected double x;
        protected double y;

        public double X { get { return x; } }
        public double Y { get { return y; } }
        public double Size
        {
            get { return size; }
            set { size = value; }
        }
        public double VelocityX
        {
            get { return velocityX; }
            set { velocityX = value; }
        }
        public double VelocityY
        {
            get { return velocityY; }
            set { velocityY = value; }
        }

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
