using System;
using System.Collections.Generic;

namespace Asteroids.Model
{
    class FrameEventArgs : EventArgs
    {
        public SpaceShip Player { get; private set; }
        public List<Asteroid> Asteroids { get; private set; }

        public FrameEventArgs(SpaceShip player, List<Asteroid> asteroids)
        {
            Player = player;
            Asteroids = new List<Asteroid>(asteroids);
        }
    }
}
