namespace Asteroids.Model
{
    public interface IGameRules
    {
        int SpeedMs { get; }
        double Width { get; }
        double Height { get; }
        Asteroid getNextAsteroid();
        bool isOutOfBounds(Asteroid asteroid);
        bool canSpawn(long elapsedMs);
    }
}
