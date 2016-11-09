using System;

namespace Asteroids.Model
{
    public class Asteroid : GameObject
    {
        private static Random random = new Random();
        public const double MIN_SECONDS_VISIBLE = 3.0;
        public const double MAX_SECONDS_VISIBLE = 15.0;
        private const double MIN_SIZE_RATIO = 15.0;
        private const double MAX_SIZE_RATIO = 7.0;
        
        public Asteroid(double fieldWidth, double fieldHeight)
        {
            size = calculateSize(fieldWidth);
            velocity = calculateVelocity(fieldHeight);
            x = random.NextDouble() * (fieldWidth - size);
            y = -size;
        }

        private double calculateSize(double fieldWidth)
        {
            double minSize = fieldWidth / MIN_SIZE_RATIO;
            double maxSize = fieldWidth / MAX_SIZE_RATIO;

            return random.NextDouble() * maxSize + minSize;
        }

        private double calculateVelocity(double fieldHeight)
        {
            double minVelocity = fieldHeight / MIN_SECONDS_VISIBLE / AsteroidsGame.UpdateRate;
            double maxVelocity = fieldHeight / MAX_SECONDS_VISIBLE / AsteroidsGame.UpdateRate;

            return random.NextDouble() * maxVelocity + minVelocity;
        }

        public override void advance()
        {
            y += velocity;
        }
    }
}
