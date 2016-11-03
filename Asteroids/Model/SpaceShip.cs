using System;

namespace Asteroids.Model
{
    class SpaceShip : GameObject
    {
        private const double SIZE_RATIO = 6.0;
        private double leftBoundary;
        private double rightBoundary;

        public SpaceShip(double fieldWidth, double fieldHeight)
        {
            size = fieldWidth / SIZE_RATIO;
            leftBoundary = 0 - size / 4;
            rightBoundary = fieldWidth - size * (3.0 / 4);
            x = (fieldWidth + size) / 2;
            y = fieldHeight - size - fieldHeight / 30;
        }

        public void thrustLeft()
        {
            thrust(true);
        }

        public void thrustRight()
        {
            thrust(false);
        }

        private void thrust(bool left)
        {
            double speed = Math.Abs(velocity);
            if (speed < 10.0)
            {
                double increase = 0.25 > speed ? 1.5 : Math.Sqrt(0.125 * speed);
                velocity += left ? -increase : increase;
            }
        }

        public override void advance()
        {
            bounceBack();
            x += velocity;
            throttle();
        }

        private void bounceBack()
        {
            if (velocity < 0 && x < leftBoundary || rightBoundary < x && velocity > 0) velocity = 0;
        }

        private void throttle()
        {
            double speed = Math.Abs(velocity);
            if (speed < 0.5)
            {
                velocity = 0;
            }
            else if (speed > 0)
            {
                velocity -= Math.Sqrt(speed) * 1/16 * Math.Sign(velocity);
            }
        }
    }
}
