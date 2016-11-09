using Asteroids.Model;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Asteroids
{
    public partial class GameForm : Form
    {
        private const string PAUSE = "Paused";
        private AsteroidsGame game;
        private Brush transparentBrush;
        private Brush pausedFontBrush;
        private Font pausedFont;
        private Image background;
        private Image spaceShip;
        private Image asteroidImage;
        private Rectangle cropRectangle;
        private Rectangle displayRectangle;
        private List<Asteroid> asteroids;
        private SpaceShip player;

        private int FPS = 0;

        public GameForm()
        {
            InitializeComponent();
            LoadResources();
            SetupDrawObjects();
            InitializeAndStartGame();
        }

        private void LoadResources()
        {
            background = Properties.Resources.space;
            spaceShip = Properties.Resources.spaceship;
            asteroidImage = Properties.Resources.asteroid;
        }

        private void SetupDrawObjects()
        {
            transparentBrush = new SolidBrush(Color.FromArgb(128, 0, 0, 32));
            pausedFont = new Font("Segoe UI", 32, FontStyle.Bold);
            pausedFontBrush = new SolidBrush(Color.FromArgb(160, 255, 255, 255));
            displayRectangle = new Rectangle(0, 0, Width, Height);
            cropRectangle = new Rectangle(0, 0, Width, Height);
        }

        private void InitializeAndStartGame()
        {
            game = new AsteroidsGame(Width, Height, FPS);
            game.OnFrameUpdate += new AsteroidsGame.FrameUpdateHandler(handler);
            game.start();
            KeyDown += GameForm_KeyDown;
            KeyUp += GameForm_KeyUp;
            Paint += Canvas_Paint;
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A: case Keys.Left: game.leftPressed(); break;
                case Keys.D: case Keys.Right: game.rightPressed(); break;
                case Keys.W: case Keys.Up: game.upPressed(); break;
                case Keys.S: case Keys.Down: game.downPressed(); break;
            }
        }

        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape: Close(); break;
                case Keys.A: case Keys.Left: game.leftReleased(); break;
                case Keys.D: case Keys.Right: game.rightReleased(); break;
                case Keys.W: case Keys.Up: game.upReleased(); break;
                case Keys.S: case Keys.Down: game.downReleased(); break;
                case Keys.Space: if (game.isPaused()) game.resume(); else game.pause(); Invalidate(); break;
            }
        }

        private void handler(object sender, FrameEventArgs e)
        {
            player = e.Player;
            asteroids = e.Asteroids;
            Invalidate();
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;
            graphics.InterpolationMode = InterpolationMode.Low;
            graphics.CompositingQuality = CompositingQuality.HighSpeed;
            graphics.SmoothingMode = SmoothingMode.HighSpeed;
            graphics.TextRenderingHint = TextRenderingHint.SystemDefault;
            graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;

            if (null != background)
                graphics.DrawImage(background, displayRectangle, cropRectangle, GraphicsUnit.Pixel);

            if (null != asteroids && null != asteroidImage)
                foreach (Asteroid asteroid in asteroids)
                    graphics.DrawImage(asteroidImage, new Rectangle((int) asteroid.X, (int) asteroid.Y, (int) asteroid.Size, (int) asteroid.Size));
                    //graphics.FillRectangle(Brushes.Blue, (float)asteroid.X, (float)asteroid.Y, (float)asteroid.Size, (float)asteroid.Size);

            //graphics.DrawString("counter " + counter, font, Brushes.Black, playerX, playerY);
            if (null != spaceShip && null != player)
            {
                graphics.DrawImage(spaceShip, new Rectangle((int)player.X, (int)player.Y, (int)player.Size, (int)player.Size));
            }

            if (game.isPaused())
            {
                SizeF fontSize = graphics.MeasureString(PAUSE, pausedFont);
                graphics.FillRectangle(transparentBrush, 0, 0, Width, Height);
                graphics.DrawString(PAUSE, pausedFont, pausedFontBrush, Width / 2 - fontSize.Width / 2, Height / 2 - fontSize.Height / 2);
            }
            //graphics.FillRectangle(Brushes.Red, playerX, playerY, playerSize, playerSize);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //disable
        }
    }
}
