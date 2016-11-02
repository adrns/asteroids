using System;
using System.Collections.Generic;

namespace Asteroids.Model
{
    class FrameEventArgs : EventArgs
    {
        public double PlayerX { get; private set; }
        public double PlayerY { get; private set; }
        public double PlayerSize { get; private set; }
        public List<Asteroid> Asteroids { get; private set; }

        public FrameEventArgs(double playerX, double playerY, double playerSize, List<Asteroid> asteroids)
        {
            PlayerX = playerX;
            PlayerY = playerY;
            PlayerSize = playerSize;
            Asteroids = new List<Asteroid>(asteroids);
        }
    }
}
