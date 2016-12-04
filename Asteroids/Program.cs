using Asteroids.Model;
using Asteroids.View;
using System;
using System.Windows.Forms;

namespace Asteroids
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            int width = 432;
            int height = 720;
            int fps = 60;
            GameRules rules = new GameRules(width, height);
            AsteroidsGame game = new AsteroidsGame(rules, fps);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GameForm(game));
        }
    }
}
