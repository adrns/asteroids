using System;
using System.Collections.Generic;

namespace Asteroids.Model
{
    public class FrameEventArgs : EventArgs
    {
        public SpaceShip Player { get; private set; }
        public List<Asteroid> Asteroids { get; private set; }
        public long Seconds { get; private set; }

        public FrameEventArgs(SpaceShip player, List<Asteroid> asteroids, long seconds)
        {
            Player = player;
            Asteroids = new List<Asteroid>(asteroids);
            Seconds = seconds;
        }
    }
}
