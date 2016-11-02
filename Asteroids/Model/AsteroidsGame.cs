using System.Collections.Generic;
using System.Timers;
using System;
using System.Diagnostics;

namespace Asteroids.Model
{
    //TODO spawn asteroids
    //TODO remove asteroids out of bounds
    class AsteroidsGame
    {
        private const int speedMs = 5;
        public const int UpdateRate = 1000 / speedMs;
        private SpaceShip player;
        private List<Asteroid> asteroids = new List<Asteroid>(20);
        private Timer timer;
        private Random random = new Random();
        private Stopwatch stopWatch;

        private double width;
        private double height;
        private int fps;
        private int updateInterval;
        private long lastUpdate;
        private bool isStarted = false;
        private bool pendingLeft = false;
        private bool pendingRight = false;
        private double chanceToSpawn;

        public delegate void FrameUpdateHandler(object sender, FrameEventArgs e);
        public event FrameUpdateHandler OnFrameUpdate;

        public AsteroidsGame(double width, double height, int fps)
        {
            this.width = width;
            this.height = height;
            this.fps = fps;
            updateInterval = 1000 / fps;
        }

        public void start()
        {
            if (!isStarted)
            {
                player = new SpaceShip(width, height);
                isStarted = true;
                timer = new Timer(speedMs);
                timer.Elapsed += gameLoop;
                timer.Start();
                stopWatch = new Stopwatch();
                stopWatch.Start();
                lastUpdate = stopWatch.ElapsedMilliseconds;
            }
        }

        private void gameLoop(object sender, ElapsedEventArgs e)
        {
            advanceObjects();
            spawnObjects();
            if (playerCollided())
                gameOver();
            OnFrameUpdate(this, new FrameEventArgs(player.X, player.Y, player.Size));
            //updateView();
        }

        private void updateView()
        {
            long now = stopWatch.ElapsedMilliseconds;
            if (updateInterval < now - lastUpdate)
            {
                lastUpdate = now;
                OnFrameUpdate(this, new FrameEventArgs(player.X, player.Y, player.Size));
            }
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
