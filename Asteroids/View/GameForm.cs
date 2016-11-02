using Asteroids.Model;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Asteroids
{
    public partial class GameForm : Form
    {
        private Brush brush;
        private Font font;
        private AsteroidsGame game;
        private int FPS = 120;
        private long counter = 0;

        private float playerX;
        private float playerY;
        private float playerSize;

        public GameForm()
        {
            InitializeComponent();
            brush = new SolidBrush(Color.AliceBlue);
            font = new Font("Courier New", 18);
            game = new AsteroidsGame(Width, Height, FPS);
            game.OnFrameUpdate += new AsteroidsGame.FrameUpdateHandler(handler);
            game.start();
            KeyDown += GameForm_KeyDown;
            Paint += Canvas_Paint;
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape: Close(); break;
                case Keys.Left: game.moveLeft(); break;
                case Keys.Right: game.moveRight(); break;
            }
        }

        private void handler(object sender, FrameEventArgs e)
        {
            playerX = (float) e.PlayerX;
            playerY = (float) e.PlayerY;
            playerSize = (float) e.PlayerSize;
            counter++;
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

            //graphics.DrawString("counter " + counter, font, Brushes.Black, playerX, playerY);
            graphics.FillRectangle(Brushes.Red, playerX, playerY, playerSize, playerSize);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //disable
        }

    }
}