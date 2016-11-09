using System;

namespace Asteroids.Model
{
    class SpaceShip : GameObject
    {
        private const double SIZE_RATIO = 8.0;
        private double leftBoundary;
        private double rightBoundary;
        private double topBoundary;
        private double bottomBoundary;
        private double velocityY;

        public SpaceShip(double fieldWidth, double fieldHeight)
        {
            size = fieldWidth / SIZE_RATIO;
            leftBoundary = topBoundary = 0 - size / 4;
            rightBoundary = fieldWidth - size * (3.0 / 4);
            bottomBoundary = fieldHeight - size * (3.0 / 4);
            x = fieldWidth / 2 - size / 2;
            y = fieldHeight - size - fieldHeight / 30;
        }

        public void thrustLeft()
        {
            thrustX(true);
        }

        public void thrustRight()
        {
            thrustX(false);
        }

        public void thrustUp()
        {
            thrustY(true);
        }

        public void thrustDown()
        {
            thrustY(false);
        }

        private void thrustX(bool left)
        {
            double speed = Math.Abs(velocity);
            if (speed < 7.5)
            {
                double increase = 0.25 > speed ? 0.5 : Math.Sqrt(0.05 * speed);
                velocity += left ? -increase : increase;
            }
        }

        private void thrustY(bool up)
        {
            double speed = Math.Abs(velocityY);
            if (speed < 7.5)
            {
                double increase = 0.25 > speed ? 0.5 : Math.Sqrt(0.05 * speed);
                velocityY += up ? -increase : increase;
            }
        }

        public override void advance()
        {
            bounceBack();
            x += velocity;
            y += velocityY;
            throttle();
            throttleY();
        }

        private void bounceBack()
        {
            if (velocity < 0 && x < leftBoundary || rightBoundary < x && velocity > 0) velocity = 0;
            if (velocityY < 0 && y < topBoundary || bottomBoundary < y && velocityY > 0) velocityY = 0;
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
                velocity -= Math.Sqrt(speed) * 1/24 * Math.Sign(velocity);
            }
        }

        private void throttleY()
        {
            double speed = Math.Abs(velocityY);
            if (speed < 0.5)
            {
                velocityY = 0;
            }
            else if (speed > 0)
            {
                velocityY -= Math.Sqrt(speed) * 1 / 24 * Math.Sign(velocityY);
            }
        }
    }
}
