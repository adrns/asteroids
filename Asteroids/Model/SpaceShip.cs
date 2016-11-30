using System;

namespace Asteroids.Model
{
    public class SpaceShip : GameObject
    {
        private const double SIZE_RATIO = 8.0;
        private double leftBoundary;
        private double rightBoundary;
        private double topBoundary;
        private double bottomBoundary;

        public SpaceShip(IGameRules gameRules)
        {
            size = gameRules.Width / SIZE_RATIO;
            leftBoundary = topBoundary = 0 - size / 4;
            rightBoundary = gameRules.Width - size * (3.0 / 4);
            bottomBoundary = gameRules.Height - size * (3.0 / 4);
            x = gameRules.Width / 2 - size / 2;
            y = gameRules.Height - size - gameRules.Height / 30;
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
            double speed = Math.Abs(velocityX);
            if (speed < 7.5)
            {
                double increase = 0.25 > speed ? 0.5 : Math.Sqrt(0.05 * speed);
                velocityX += left ? -increase : increase;
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
            x += velocityX;
            y += velocityY;
            throttleX();
            throttleY();
        }

        private void bounceBack()
        {
            if (velocityX < 0 && x < leftBoundary || rightBoundary < x && velocityX > 0) velocityX = 0;
            if (velocityY < 0 && y < topBoundary || bottomBoundary < y && velocityY > 0) velocityY = 0;
        }

        private void throttleX()
        {
            double speed = Math.Abs(velocityX);
            if (speed < 0.5)
            {
                velocityX = 0;
            }
            else if (speed > 0)
            {
                velocityX -= Math.Sqrt(speed) * 1/24 * Math.Sign(velocityX);
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
