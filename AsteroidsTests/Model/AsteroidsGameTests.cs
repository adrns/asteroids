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
    public class AsteroidsGameTests
    {
        [TestMethod()]
        public void startTest()
        {
            AsteroidsGame game = new AsteroidsGame(100, 100, 0);
            game.start();

            Assert.IsTrue(game.Started);
            Assert.IsFalse(game.GameOver);
            Assert.IsFalse(game.Paused);
        }

        [TestMethod()]
        public void resumeTest()
        {
            AsteroidsGame game = new AsteroidsGame(100, 100, 0);
            game.start();
            game.pause();
            game.resume();
            Assert.IsFalse(game.Paused);
        }

        [TestMethod()]
        public void pauseTest()
        {
            AsteroidsGame game = new AsteroidsGame(100, 100, 0);
            game.start();
            game.pause();
            Assert.IsTrue(game.Paused);
        }
    }
}