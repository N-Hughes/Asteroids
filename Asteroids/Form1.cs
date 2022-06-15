using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace Asteroids
{
    public partial class Form1 : Form
    {
        Rectangle ship = new Rectangle(250, 250, 30, 30);
        int shipSpeed = 10;
        int shipXSpeed = 0;
        int shipYSpeed = 0;

        List<Rectangle> bullet = new List<Rectangle>();
        List<Rectangle> bulletSpeedX = new List<Rectangle>();
        List<Rectangle> bulletSppedY = new List<Rectangle>();

        List<Rectangle> rocks = new List<Rectangle>();
        List<int> rockSpeedX = new List<int>();
        List<int> rockSpeedY = new List<int>();

        List<Rectangle> bullets = new List<Rectangle>();
        List<int> bulletSpeeds = new List<int>();


        bool leftDown = false;
        bool rightDown = false;
        bool upDown = false;

        int score = 0;
        int time = 750;
        int lives = 3;

        int turn = 0;

        SolidBrush whiteBrush = new SolidBrush(Color.White);

        Random randGen = new Random();
        int randValue = 0;
        int randomL = 0;
        int randomW = 0;

        Image playerImage = Properties.Resources.funnyShip;
        Image rocksImage = Properties.Resources.meteor;

        public Form1()
        {
            InitializeComponent();
        }
      

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftDown = true;
                    break;
                case Keys.Right:
                    rightDown = true;
                    break;
                case Keys.Up:
                    upDown = true;
                    break;
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftDown = false;
                    break;
                case Keys.Right:
                    rightDown = false;
                    break; //poo 
                case Keys.Up:
                    upDown = false;
                    break; 
            }
        }
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //Movement
            if (leftDown == true)
            {
                turn -= 2;
            }
            if (rightDown == true )
            {
                turn += 2;

            }

            if (turn <= 90)
            {
                shipYSpeed = -Convert.ToInt16(shipSpeed * Math.Cos(Convert.ToDouble(turn)));
                shipXSpeed = -Convert.ToInt16(shipSpeed * Math.Sin(Convert.ToDouble(turn)));
            }
            else if (turn <= 180)
            {
                shipYSpeed = Convert.ToInt16(shipSpeed * Math.Sin(Convert.ToDouble(turn)));
                shipXSpeed = Convert.ToInt16(shipSpeed * Math.Cos(Convert.ToDouble(turn)));
            }
            else if (turn <= 270)
            {
                shipYSpeed = -Convert.ToInt16(shipSpeed * Math.Sin(Convert.ToDouble(turn)));
                shipXSpeed = -Convert.ToInt16(shipSpeed * Math.Cos(Convert.ToDouble(turn)));
            }
            else if (turn < 360)
            {
                shipYSpeed = Convert.ToInt16(shipSpeed * Math.Cos(Convert.ToDouble(turn)));
                shipXSpeed = Convert.ToInt16(shipSpeed * Math.Sin(Convert.ToDouble(turn)));
            }

            if (upDown == true && ship.Y > 15)
            {
                ship.X += shipXSpeed;
                ship.Y += shipYSpeed;
            }

            if (turn < 0)
            {
                turn = 359;
            }

            //Making rocks
            randValue = randGen.Next(1, 101);

            for (int i = 0; i < rocks.Count; i++)
            {
                int x = rocks[i].X + rockSpeedX[i];
                rocks[i] = new Rectangle(x, rocks[i].Y, 30, 28);

                int y = rocks[i].Y + rockSpeedY[i];
                y = rocks[i].Y + rockSpeedY[i];
                rocks[i] = new Rectangle(x, rocks[i].Y, 30, 28);
            }

            //Moving Rocks 
            if (randValue < 2)
            {
                randomL = randGen.Next(20, 550);
                rocks.Add(new Rectangle(-14, randomL, 30, 28));
                rockSpeedX.Add(randGen.Next(2, 5));
                rockSpeedY.Add(randGen.Next(2, 5) * -1);
            }
            else if (randValue > 98)
            {
                randomW = randGen.Next(20, 550);
                rocks.Add(new Rectangle(this.Height, randomW, 30, 28)); //poo
                rockSpeedY.Add(randGen.Next(2, 5) * -1);
                rockSpeedX.Add(randGen.Next(2, 5));
            }

            //Interacts and Removing rocks
            for (int i = 0; i < rocks.Count; i++)
            {

                if (ship.IntersectsWith(rocks[i]))
                {
                    rocks.RemoveAt(i);
                    ship.X = 250;
                    ship.Y = 250;
                    lives--;
                    score -= 50;
                }

            }

            //Game Timer
            time--;
            if (time < 0)
            {
                gameTimer.Stop();
            }

            Refresh();
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Rotate and Spawn ship image 
            e.Graphics.TranslateTransform(ship.X - ship.Width / 2, ship.Y - ship.Height / 2);
            e.Graphics.RotateTransform(turn);
            e.Graphics.DrawImage(playerImage, 0,0 , ship.Width, ship.Height);
            e.Graphics.ResetTransform();

            //Draw Rocks
            for (int i = 0; i < rocks.Count; i++)
            {
                e.Graphics.FillRectangle(whiteBrush, rocks[i]);
                //e.Graphics.DrawImage

                

            }

        }
    }
}
