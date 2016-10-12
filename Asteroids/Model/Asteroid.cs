using System;

namespace Asteroids.Model
{
    class Asteroid
    {
        private static Random random = new Random();
        private const double MIN_SECONDS_VISIBLE = 3.0;
        private const double MAX_SECONDS_VISIBLE = 7.0;
        private const double MIN_SIZE_RATIO = 10.0;
        private const double MAX_SIZE_RATIO = 5.0;
        private double size;
        private double velocity;
        private double x;
        private double y;
        
        public Asteroid(double fieldWidth, double fieldHeight, double maxY, double fps)
        {
            size = calculateSize(fieldWidth);
            velocity = calculateVelocity(fieldHeight, fps);
            x = random.NextDouble() * (fieldWidth - size);
            y = random.NextDouble() * maxY + size;
        }

        private double calculateSize(double fieldWidth)
        {
            double minSize = fieldWidth / MIN_SIZE_RATIO;
            double maxSize = fieldWidth / MAX_SIZE_RATIO;

            return random.NextDouble() * maxSize + minSize;
        }

        private double calculateVelocity(double fieldHeight, double fps)
        {
            double minVelocity = fieldHeight / fps / MIN_SECONDS_VISIBLE;
            double maxVelocity = fieldHeight / fps / MAX_SECONDS_VISIBLE;

            return random.NextDouble() * maxVelocity + minVelocity;
        }
    }
}
