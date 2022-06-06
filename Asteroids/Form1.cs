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
        int shipSpeed = 4;

        List<Rectangle> rocks = new List<Rectangle>();
        List<int> rockSpeeds = new List<int>();

        List<Rectangle> bullets = new List<Rectangle>();
        List<int> bulletSpeeds = new List<int>();


        bool ADown = false;
        bool DDown = false;

        int score = 0;
        int time = 750;

        SolidBrush whiteBrush = new SolidBrush(Color.White);

        Random randGen = new Random();
        int randValue = 0;
        int randomL = 0;
        int randomW = 0;

        Image playerImage = Properties.Resources.shipTri;

        public Form1()
        {
            InitializeComponent();
        }
      

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    ADown = true;
                    break;
                case Keys.D:
                    DDown = true;
                    break;
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    ADown = false;
                    break;
                case Keys.D:
                    DDown = false;
                    break; //poo 
            }
        }
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //Movement
            if (ADown == true && ship.X > 0)
            {
                ship.X -= shipSpeed;
            }
            if (DDown == true && ship.X < this.Width - ship.Width)
            {
                ship.X += shipSpeed;
            }

            //Making rocks
            randValue = randGen.Next(1, 101);

            for (int i = 0; i < rocks.Count; i++)
            {
                int x = rocks[i].X + rockSpeeds[i];
                rocks[i] = new Rectangle(x, rocks[i].Y, 30, 28);

                int y = rocks[i].Y + rockSpeeds[i];
                y = rocks[i].Y + rockSpeeds[i];
                rocks[i] = new Rectangle(x, rocks[i].Y, 30, 28);
            }

            if (randValue < 2)
            {
                randomL = randGen.Next(20, 550);
                rocks.Add(new Rectangle(-14, randomL, 30, 28));
                rockSpeeds.Add(randGen.Next(2, 5));
            }
            else if (randValue > 98)
            {
                randomW = randGen.Next(20, 550);
                rocks.Add(new Rectangle(this.Height, randomW, 30, 28));
                rockSpeeds.Add(randGen.Next(2, 5) * -1);
            }



            Refresh();
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            //e.Graphics.FillRectangle(whiteBrush, ship);
            e.Graphics.DrawImage(playerImage, ship);

            for (int i = 0; i < rocks.Count; i++)
            {
                e.Graphics.FillRectangle(whiteBrush, rocks[i]);
            }
            
        }
    }
}
