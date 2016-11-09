using Asteroids.Model;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Asteroids.View
{
    public partial class GameForm : Form
    {
        private const string TITLE = "Asteroids";
        private const string INSTRUCTIONS =
            "Enter - Start game" + "\n"
            + "Space - Pause/Resume game" + "\n"
            + "WASD - Control spaceship" + "\n"
            + "Arrow keys - Control spaceship" + "\n"
            + "Esc - Exit game" + "\n";
        private const string PAUSE = "Paused";
        private const string GAME_OVER = "Game Over";
        private AsteroidsGame game;
        private Brush transparentBrush;
        private Brush pausedFontBrush;
        private Brush timeFontBrush;
        private Font pausedFont;
        private Font timeFont;
        private Image background;
        private Image spaceShip;
        private Image asteroidImage;
        private Rectangle cropRectangle;
        private Rectangle displayRectangle;
        private List<Asteroid> asteroids;
        private SpaceShip player;

        private bool finished = true;
        private long elapsedSeconds;
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
            timeFont = new Font("Segoe UI", 96, FontStyle.Bold);
            timeFontBrush = new SolidBrush(Color.FromArgb(128, 255, 255, 255));
            displayRectangle = new Rectangle(0, 0, Width, Height);
            cropRectangle = new Rectangle(0, 0, Width, Height);
        }

        private void InitializeAndStartGame()
        {
            game = new AsteroidsGame(Width, Height, FPS);
            game.OnFrameUpdate += new AsteroidsGame.FrameUpdateHandler(handler);
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
                case Keys.Space: if (game.Paused) game.resume(); else game.pause(); Invalidate(); break;
                case Keys.Enter: if (finished) { finished = false; game.start(); } break;
            }

            if (game.GameOver && !finished)
            {
                finished = true;
                Invalidate();
            }
        }

        private void handler(object sender, FrameEventArgs e)
        {
            player = e.Player;
            asteroids = e.Asteroids;
            elapsedSeconds = e.Seconds;
            Invalidate();
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;
            setGraphicsMode(graphics);

            drawBackground(graphics);

            if (game.Started && !finished)
            {
                drawTime(graphics);
                if (!game.GameOver)
                {
                    drawObjects(graphics);
                    if (game.Paused)
                        drawPauseOverlay(graphics);
                }
                else
                {
                    drawGameOver(graphics);
                }
            } else
            {
                drawIntro(graphics);
            }
        }

        private void drawGameOver(Graphics graphics)
        {
            Font font = new Font("Segoe UI", 56, FontStyle.Bold);
            SizeF fontSize = graphics.MeasureString(GAME_OVER, font);
            graphics.DrawString(GAME_OVER, font, pausedFontBrush, Width / 2 - fontSize.Width / 2, Height / 2 - fontSize.Height / 2);
        }

        private void drawIntro(Graphics graphics)
        {
            Font font = new Font("Segoe UI", 56, FontStyle.Bold);
            int top = 24;
            int offset = 1;
            SizeF fontSize = graphics.MeasureString(TITLE, font);

            int width = spaceShip.Size.Width;
            int height = spaceShip.Size.Height;
            graphics.DrawImage(spaceShip, new Rectangle(Width / 2 - width / 2, Height / 2 - height / 2 - 70, width, height));

            fontSize = graphics.MeasureString(TITLE, font);
            Brush brush = new LinearGradientBrush(
                new Point(0, top),
                new Point(0, top + (int) fontSize.Height),
                Color.FromArgb(255, 132, 22, 13),
                Color.FromArgb(255, 255, 105, 94));
            graphics.DrawString(TITLE, font, Brushes.Black, Width / 2 - fontSize.Width / 2, top);
            graphics.DrawString(TITLE, font, Brushes.Black, Width / 2 - fontSize.Width / 2 + offset * 2, top + offset * 2);
            graphics.DrawString(TITLE, font, brush, Width / 2 - fontSize.Width / 2 + offset, top + offset);

            top = Height - (int)fontSize.Height * 2 + 24;
            offset = 1;
            font = new Font("Segoe UI", 18, FontStyle.Bold);
            fontSize = graphics.MeasureString(INSTRUCTIONS, font);
            brush = new SolidBrush(Color.FromArgb(255, 22, 155, 193));
            graphics.DrawString(INSTRUCTIONS, font, Brushes.Black, Width / 2 - fontSize.Width / 2, top);
            graphics.DrawString(INSTRUCTIONS, font, Brushes.Black, Width / 2 - fontSize.Width / 2 + offset * 2, top + offset * 2);
            graphics.DrawString(INSTRUCTIONS, font, Brushes.White, Width / 2 - fontSize.Width / 2 + offset, top + offset);
        }

        private void drawPauseOverlay(Graphics graphics)
        {
            SizeF fontSize = graphics.MeasureString(PAUSE, pausedFont);
            graphics.FillRectangle(transparentBrush, 0, 0, Width, Height);
            graphics.DrawString(PAUSE, pausedFont, pausedFontBrush, Width / 2 - fontSize.Width / 2, Height / 2 - fontSize.Height / 2);
        }

        private void drawObjects(Graphics graphics)
        {
            if (null != asteroids && null != asteroidImage)
                foreach (Asteroid asteroid in asteroids)
                    graphics.DrawImage(asteroidImage, new Rectangle((int)asteroid.X, (int)asteroid.Y, (int)asteroid.Size, (int)asteroid.Size));


            if (null != spaceShip && null != player)
                graphics.DrawImage(spaceShip, new Rectangle((int)player.X, (int)player.Y, (int)player.Size, (int)player.Size));
        }

        private void drawTime(Graphics graphics)
        {
            string elapsed = formattedTime();
            SizeF timeFontSize = graphics.MeasureString(elapsed, timeFont);
            graphics.DrawString(elapsed, timeFont, timeFontBrush, Width / 2 - timeFontSize.Width / 2, Height / 5 - timeFontSize.Height / 2);
        }

        private void setGraphicsMode(Graphics graphics)
        {
            graphics.InterpolationMode = InterpolationMode.Low;
            graphics.CompositingQuality = CompositingQuality.HighSpeed;
            graphics.SmoothingMode = SmoothingMode.HighSpeed;
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;
        }

        private void drawBackground(Graphics graphics)
        {
            if (null != background)
                graphics.DrawImage(background, displayRectangle, cropRectangle, GraphicsUnit.Pixel);
        }

        private string formattedTime()
        {
            long minutes = elapsedSeconds / 60;
            long seconds = elapsedSeconds % 60;
            return minutes.ToString().PadLeft(2, '0') + ":" + seconds.ToString().PadLeft(2, '0');
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //disable
        }
    }
}