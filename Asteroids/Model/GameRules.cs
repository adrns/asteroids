using System;

namespace Asteroids.Model
{
    class GameRules : IGameRules
    {
        private static Random random = new Random();
        private const int speedMs = 5;
        private const int updateRate = 1000 / speedMs;
        private const double ASTEROID_MIN_SECONDS_VISIBLE = 3.0;
        private const double ASTEROID_MAX_SECONDS_VISIBLE = 15.0;
        private const double ASTEROID_MIN_SIZE_RATIO = 15.0;
        private const double ASTEROID_MAX_SIZE_RATIO = 7.0;
        private double width;
        private double height;

        public int SpeedMs { get { return speedMs; } }
        public double Width { get { return width; } }
        public double Height { get { return height; } }

        public GameRules(double width, double height)
        {
            this.width = width;
            this.height = height;
        }

        public Asteroid getNextAsteroid()
        {
            double size = calculateAsteroidSize();
            return new Asteroid(size,
                calculateAsteroidVelocity(),
                random.NextDouble() * (width - size),
                -size);
        }

        public bool isOutOfBounds(Asteroid asteroid)
        {
            return asteroid.Y > height;
        }

        public bool canSpawn(long elapsedMs)
        {
            return random.NextDouble() < (0.25 * elapsedMs / 6250 + 1.25) / updateRate;
        }

        private double calculateAsteroidSize()
        {
            double minSize = width / ASTEROID_MIN_SIZE_RATIO;
            double maxSize = width / ASTEROID_MAX_SIZE_RATIO;

            return random.NextDouble() * maxSize + minSize;
        }

        private double calculateAsteroidVelocity()
        {
            double minVelocity = height / ASTEROID_MIN_SECONDS_VISIBLE / updateRate;
            double maxVelocity = height / ASTEROID_MAX_SECONDS_VISIBLE / updateRate;

            return random.NextDouble() * maxVelocity + minVelocity;
        }
    }
}
