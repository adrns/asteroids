using System;

namespace Asteroids.Model
{
    class SpaceShip : GameObject
    {
        private const double SIZE_RATIO = 7.5;
        private double leftBoundary;
        private double rightBoundary;

        public SpaceShip(double fieldWidth, double fieldHeight, double fps)
        {
            size = fieldWidth / SIZE_RATIO;
            leftBoundary = 0 - size / 4;
            rightBoundary = fieldWidth - size * (3.0 / 4);
            x = (fieldWidth + size) / 2;
            y = fieldHeight - size;
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
            velocity += left ? -1.5 : 1.5; //TODO implement acceleration
        }

        public override void advance()
        {
            bounceBack();
            x += velocity;
            throttle();
        }

        private void bounceBack()
        {
            if (velocity < 0 && x < leftBoundary || rightBoundary < x && velocity > 0)
            {
                velocity *= -1;
            }
        }

        private void throttle()
        {
            if (Math.Abs(velocity) > 0.0)
            {
                velocity = velocity - 0.25 * Math.Sign(velocity);
            }
        }
    }
}
