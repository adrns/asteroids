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
        private bool movingLeft = false;
        private bool movingRight = false;
        private double chanceToSpawn = 1.0 / UpdateRate * 1.5;

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

        public void resume()
        {
            if (isStarted) timer.Stop();
        }

        public void pause()
        {
            if (isStarted) timer.Start();
        }

        public bool isPaused()
        {
            return timer.Enabled;
        }

        private void gameLoop(object sender, ElapsedEventArgs e)
        {
            advanceObjects();
            spawnObjects();
            if (playerCollided())
                gameOver();
            OnFrameUpdate(this, new FrameEventArgs(player, asteroids));
            //updateView();
        }

        private void updateView()
        {
            long now = stopWatch.ElapsedMilliseconds;
            if (updateInterval < now - lastUpdate)
            {
                lastUpdate = now;
                OnFrameUpdate(this, new FrameEventArgs(player, asteroids));
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
            if (random.NextDouble() < chanceToSpawn)
            {
                Console.WriteLine("Spawned an asteroid");
                asteroids.Add(new Asteroid(width, height));
            }
        }

        private void acceleratePlayer()
        {
            if (movingLeft) player.thrustLeft();
            if (movingRight) player.thrustRight();
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
            Console.WriteLine("Game over");
            timer.Stop();
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
    }
}
