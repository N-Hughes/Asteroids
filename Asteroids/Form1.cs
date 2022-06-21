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

        float shipY = 250;
        float shipX = 250;

        int shipSpeed = 4;
        double xSpeed = 1;
        double ySpeed = 1;

        int shipAngle = 10;
        float thetaAngle = 0;

        List<Rectangle> bullet = new List<Rectangle>();
        List<int> bulletSpeedX = new List<int>();
        List<int> bulletSpeedY = new List<int>();
        double bXSpeed = 1;
        double bYSpeed = 1;
        float bThetaAngle = 0;
        

        List<Rectangle> rocks = new List<Rectangle>();
        List<int> rockSpeedX = new List<int>();
        List<int> rockSpeedY = new List<int>();


        bool leftDown = false;
        bool rightDown = false;
        bool upDown = false;
        bool spaceBar = false;

        int score = 0;
        int time = 1500;
        int lives = 3;

        int turn = 0;

        SolidBrush whiteBrush = new SolidBrush(Color.White);

        Random randGen = new Random();
        int randValue = 0;
        int randomL = 0;
        int randomW = 0;

        Image playerImage = Properties.Resources.funnyShip;
        Image rocksImage = Properties.Resources.meteor;

        //Start and End Screen
        string gameState = "waiting";

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
                case Keys.Space:
                    spaceBar = true;
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
                case Keys.Space:
                    spaceBar = false;
                    break;
            }
        }
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            ///Movement
            if (rightDown)
            {
                shipAngle += 5;
            }
            if (leftDown)
            {
                shipAngle -= 5;
            }

            //theta measure for angle of fire, (float uses less memory)
            thetaAngle = (90 - shipAngle);

            // determine the end point for each hand (result must be a double)
            xSpeed = Math.Cos(thetaAngle * Math.PI / 180.0);
            ySpeed = Math.Sin(thetaAngle * Math.PI / 180.0);

            if (upDown)
            {
                shipX += (float)xSpeed * shipSpeed;
                shipY -= (float)ySpeed * shipSpeed;
            }

            if (turn < 0)
            {
                turn = 359;
            }

            ///Making rocks
            randValue = randGen.Next(1, 101);

            for (int i = 0; i < rocks.Count; i++)
            {
                int x = rocks[i].X + rockSpeedX[i];
                rocks[i] = new Rectangle(x, rocks[i].Y, 30, 28);

                int y = rocks[i].Y + rockSpeedY[i];
                y = rocks[i].Y + rockSpeedY[i];
                rocks[i] = new Rectangle(rocks[i].X, y, 30, 28);
            }

            ///Moving Rocks 
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

            //Adding Bullets 
            for (int i = 0; i < bullet.Count; i++)
            {
                int x = bullet[i].X + bulletSpeedX[i];
                int y = bullet[i].Y + bulletSpeedY[i];
                bullet[i] = new Rectangle(x, y, 5, 12);

            }

            Rectangle ship = new Rectangle((int)shipX, (int)shipY, 40, 40);

            if (spaceBar)
            {
                bullet.Add(new Rectangle((int)shipX, (int)shipY, 5, 12));
                bulletSpeedX.Add(3 + (int)shipX);
                bulletSpeedY.Add(3 - (int)shipY);
                
            }

            

            ///Interacts and Removing rocks
            for (int i = 0; i < rocks.Count; i++)
            {
                if (ship.IntersectsWith(rocks[i]))
                {
                    rocks.RemoveAt(i);
                    shipX = 250;
                    shipY = 250;
                    lives--;
                    score -= 50;
                }
                
                
                for (int t = 0, j = bullet.Count; t < j; t++)
                {
                    if (bullet[t].IntersectsWith(rocks[i]))
                    {
                        bullet.RemoveAt(t);
                        rocks.RemoveAt(i);
                        score += 100;
                    }
                }

            } 

            

            //Lives score
            if (lives <= 0)
            {
                gameTimer.Stop();
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
            e.Graphics.TranslateTransform(shipX - 30 / 2, shipY - 40 / 2);
            e.Graphics.RotateTransform(shipAngle);
            e.Graphics.DrawImage(playerImage, 0, 0, 40, 40);
            e.Graphics.ResetTransform();

            //Draw Rocks
            for (int i = 0; i < rocks.Count; i++)
            {
                //e.Graphics.FillRectangle(whiteBrush, rocks[i]);
                e.Graphics.DrawImage(rocksImage, rocks[i]);




            }

            for (int i = 0; i < bullet.Count; i++)
            {
                e.Graphics.FillRectangle(whiteBrush, bullet[i]);
                //e.Graphics.DrawImage(rocksImage, rocks[i]);




            }

        }
    }
}
