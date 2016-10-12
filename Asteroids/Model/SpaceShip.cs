namespace Asteroids.Model
{
    class SpaceShip : GameObject
    {
        private const double SIZE_RATIO = 7.5;

        public SpaceShip(double fieldWidth, double fieldHeight, double fps)
        {
            size = fieldWidth / SIZE_RATIO;
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
            //TODO implement acceleration
        }

        public override void advance()
        {
            x += velocity;
        }
    }
}
