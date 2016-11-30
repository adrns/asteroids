namespace Asteroids.Model
{
    public class Asteroid : GameObject
    {

        public Asteroid(double size, double velocityY, double x, double y)
        {
            this.size = size;
            this.velocityY = velocityY;
            this.x = x;
            this.y = y;
        }

        public override void advance()
        {
            y += velocityY;
        }
    }
}
