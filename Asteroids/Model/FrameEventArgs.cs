using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids.Model
{
    class FrameEventArgs : EventArgs
    {
        public double PlayerX { get; private set; }
        public double PlayerY { get; private set; }
        public double PlayerSize { get; private set; }

        public FrameEventArgs(double playerX, double playerY, double playerSize)
        {
            PlayerX = playerX;
            PlayerY = playerY;
            PlayerSize = playerSize;
        }
    }
}
