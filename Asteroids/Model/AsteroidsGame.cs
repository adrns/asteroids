using System.Collections.Generic;
using System.Timers;
using System;

namespace Asteroids.Model
{
    //TODO count seconds
    //TODO spawn asteroids
    //TODO remove asteroids out of bounds
    //TODO send events from game loop
    class AsteroidsGame
    {
        private SpaceShip player;
        private List<Asteroid> asteroids;
        private Timer timer;
        private Random random = new Random();

        private double width;
        private double height;
        private int fps;
        private bool isStarted = false;
        private bool pendingLeft = false;
        private bool pendingRight = false;
        private double chanceToSpawn;

        public AsteroidsGame(double width, double height, int fps)
        {
            this.width = width;
            this.height = height;
            this.fps = fps;
        }

        public void start()
        {
            if (!isStarted)
            {
                player = new SpaceShip(width, height, fps);
                isStarted = true;
                timer = new Timer(1000.0 / fps);
                timer.Elapsed += gameLoop;
                timer.Start();
            }
        }

        private void gameLoop(object sender, ElapsedEventArgs e)
        {
            advanceObjects();
            spawnObjects();
            if (playerCollided())
                gameOver();
        }

        private void advanceObjects()
        {
            acceleratePlayer();
            player.advance();
            foreach (Asteroid asteroid in asteroids)
                asteroid.advance();
        }

        private void spawnObjects()
        {

        }

        private void acceleratePlayer()
        {
            if (pendingLeft)
            {
                player.thrustLeft();
                pendingLeft = false;
            }
            if (pendingRight)
            {
                player.thrustRight();
                pendingRight = false;
            }
        }

        private bool playerCollided()
        {
            foreach (Asteroid asteroid in asteroids)
                if (asteroid.collidesWith(player))
                    return true;

            return false;
        }

        private void gameOver()
        {
            timer.Stop();
        }

        public void moveLeft()
        {
            pendingLeft = true;
        }

        public void moveRight()
        {
            pendingRight = true;
        }
    }
}
