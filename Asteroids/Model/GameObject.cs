namespace Asteroids.Model
{
    abstract class GameObject
    {
        protected double size;
        protected double velocity;
        protected double x;
        protected double y;

        abstract public void advance();

        public bool collidesWith(GameObject other)
        {
            bool overlapX = overlaps(x, x + size, other.x, other.x + size);
            bool overlapY = overlaps(y, y + size, other.y, other.y + size);

            return overlapX && overlapY;
        }

        protected bool overlaps(double aStart, double aEnd, double bStart, double bEnd)
        {
            return isBetween(bStart, aStart, aEnd) || isBetween(bEnd, aStart, aEnd);
        }

        protected bool isBetween(double point, double start, double end)
        {
            return start <= point && point <= end;
        }
    }
}
