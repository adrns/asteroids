using System.Collections.Generic;
using System.Timers;
using System;
using System.Diagnostics;

namespace Asteroids.Model
{
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
        private bool isOver = false;
        private bool movingLeft = false;
        private bool movingRight = false;
        private bool movingUp = false;
        private bool movingDown = false;
        private double chanceToSpawn;

        public delegate void FrameUpdateHandler(object sender, FrameEventArgs e);
        public event FrameUpdateHandler OnFrameUpdate;

        public AsteroidsGame(double width, double height, int fps)
        {
            this.width = width;
            this.height = height;
            this.fps = fps;
            updateInterval = 0 == fps ? 0 : 1000 / fps;
            timer = new Timer(speedMs);
            timer.Elapsed += gameLoop;
            stopWatch = new Stopwatch();
        }

        public void start()
        {
            if (!isStarted || isOver)
            {
                isOver = false;
                isStarted = true;
                asteroids.Clear();
                player = new SpaceShip(width, height);
                timer.Start();
                stopWatch.Restart();
                lastUpdate = stopWatch.ElapsedMilliseconds;
            }
        }

        public void resume()
        {
            if (isStarted && !isOver)
            {
                timer.Start();
                stopWatch.Start();
            }
        }

        public void pause()
        {
            if (isStarted && !isOver)
            {
                timer.Stop();
                stopWatch.Stop();
            }
        }

        public bool Paused { get { return !timer.Enabled; } }

        private void gameLoop(object sender, ElapsedEventArgs e)
        {
            despawnObjects();
            advanceObjects();
            spawnObjects();
            if (playerCollided())
                gameOver();
            updateView();
        }

        private void updateView()
        {
            long now = stopWatch.ElapsedMilliseconds;
            if (updateInterval < now - lastUpdate)
            {
                lastUpdate = now;
                OnFrameUpdate(this, new FrameEventArgs(player, asteroids, stopWatch.ElapsedMilliseconds / 1000L));
            }
        }

        private void despawnObjects()
        {
            asteroids.RemoveAll(asteroid => asteroid.Y > height);
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
            chanceToSpawn = (0.25 * stopWatch.ElapsedMilliseconds / 6250 + 1.25) / UpdateRate;
            if (random.NextDouble() < chanceToSpawn)
                asteroids.Add(new Asteroid(width, height));
        }

        private void acceleratePlayer()
        {
            if (movingLeft) player.thrustLeft();
            if (movingRight) player.thrustRight();
            if (movingUp) player.thrustUp();
            if (movingDown) player.thrustDown();
        }

        private bool playerCollided()
        {
            foreach (Asteroid asteroid in asteroids)
                if (asteroid.collidesWith(player))
                    return true;

            return false;
        }

        public bool Started { get { return isStarted; } }

        public bool GameOver { get { return isOver; } }

        private void gameOver()
        {
            timer.Stop();
            stopWatch.Stop();
            isOver = true;
        }

        public void leftPressed()
        {
            movingLeft = true;
        }

        public void rightPressed()
        {
            movingRight = true;
        }

        public void leftReleased()
        {
            movingLeft = false;
        }

        public void rightReleased()
        {
            movingRight = false;
        }

        public void upPressed()
        {
            movingUp = true;
        }

        public void downPressed()
        {
            movingDown = true;
        }

        public void upReleased()
        {
            movingUp = false;
        }

        public void downReleased()
        {
            movingDown = false;
        }
    }
}
