using Microsoft.VisualStudio.TestTools.UnitTesting;
using Asteroids.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids.Model.Tests
{
    [TestClass()]
    public class AsteroidTests
    {
        [TestMethod()]
        public void advanceTest()
        {
            int screenWidth;
            int screenHeight;
            screenWidth = screenHeight = 1000;

            Asteroid asteroid;
            for (int test = 0; test < 1000; test++)
            {
                asteroid = new Asteroid(screenWidth, screenHeight);
                double start = asteroid.Y;

                for (int i = 0; i < AsteroidsGame.UpdateRate * Asteroid.MAX_SECONDS_VISIBLE + 1; i++)
                    asteroid.advance();

                Assert.IsTrue(asteroid.Y > screenHeight);
                Assert.AreNotEqual(start, asteroid.Y);
            }
        }
    }
}