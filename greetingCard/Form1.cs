using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Media;
using System.IO;

namespace greetingCard
{

    //Alistair Krieck
    //2023-11-16
    //Brief animation for a greeting card


    public partial class greetingCard : Form
    {
        //declaring utensils

        Font font = new Font("Bauer Badoni", 50, FontStyle.Bold);
        Font font2 = new Font("Century Gothic", 25, FontStyle.Bold);

        Pen redPen = new Pen(Color.Red, 5);
        Pen whitePen = new Pen(Color.White, 5);
        Pen blackPen = new Pen(Color.Black, 6);
        Pen bluePen = new Pen(Color.Blue, 5);
        Pen cyanPen = new Pen(Color.Cyan, 5);
        Pen peachPen = new Pen(Color.PeachPuff, 5);

        SolidBrush blackBrush = new SolidBrush(Color.Black);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush blueBrush = new SolidBrush(Color.Blue);
        SolidBrush cyanBrush = new SolidBrush(Color.Cyan);
        SolidBrush yellowBrush = new SolidBrush(Color.Yellow);
        SolidBrush peachBrush = new SolidBrush(Color.PeachPuff);

        int counter = 0;
        bool mouthOpen = true;

        //TRYING to set up sounds
        System.Windows.Media.MediaPlayer pacMunch = new System.Windows.Media.MediaPlayer();
        System.Windows.Media.MediaPlayer ghostDie = new System.Windows.Media.MediaPlayer();
        System.Windows.Media.MediaPlayer startUp = new System.Windows.Media.MediaPlayer();
        System.Windows.Media.MediaPlayer ghostMove = new System.Windows.Media.MediaPlayer();



        public greetingCard()
        {
            InitializeComponent();
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            Graphics loadArt = this.CreateGraphics();

            loadArt.DrawString("Welcome :D", font, blackBrush, 100, 100);

            pacMunch.Open(new Uri(Application.StartupPath + "/Resources/waka.wav"));
            //ghostDie.Open(new Uri(Application.StartupPath + "/Resources/pacman_eatghost.wav"));
            //ghostMove.Open(new Uri(Application.StartupPath + "/Resources/ghost_move.wav"));
            //startUp.Open(new Uri(Application.StartupPath + "/Resources/pacman_beginning.wav"));
        }


        private void Form1_Click(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();

            g.Clear(Color.Black);
            g.DrawLine(bluePen, 0, 150, 10000, 150);

           

            for (int loop = 0; loop < 2; loop++)
            {
                for (int balls = 0; balls < 600; balls += 50)
                {
                    g.FillEllipse(whiteBrush, balls, 65, 15, 15);
                }

                for (int pacMove = -10; pacMove < 650; pacMove += 5)
                {
                    PacManMoveRight(pacMove, g);
                    pacMunch.MediaEnded += new EventHandler(pacMunch_MediaEnded);
                    Thread.Sleep(20);
                    g.FillPie(blackBrush, pacMove, 50, 50, 50, 30, 360);
                }
            }

            for (int inkyMove = -15; inkyMove < 700; inkyMove += 5)
            {
                g.Clear(Color.Black);
                g.DrawLine(bluePen, 0, 150, 10000, 150);
                PacManMoveRight(inkyMove + 65, g);
                InkyMoveRight(inkyMove, g);
                if (inkyMove + 65 < 500)
                {
                    g.FillEllipse(whiteBrush, 550, 50, 30, 30);
                }
                Thread.Sleep(20);
            }

            int move = 700;

            for (int inkyMove = 550; inkyMove > 250; inkyMove -= 5)
            {
                move -= 7;
                g.Clear(Color.Black);
                g.DrawLine(bluePen, 0, 150, 10000, 150);
                InkyMoveLeft(inkyMove, g);
                PacManMoveLeft(move, g);
                Thread.Sleep(20);
            }

            for (int inkyMove = 220; inkyMove > -1000; inkyMove -= 10)
            {
                move -= 5;
                g.Clear(Color.Black);
                g.DrawLine(bluePen, 0, 150, 10000, 150);
                DeadGhostMove(inkyMove, g);
                if (move < 350)
                {
                    g.DrawString("G", font2, whiteBrush, move + 45, 50);
                }

                if (move < 300)
                {
                    g.DrawString("O", font2, whiteBrush, move + 70, 50);
                }

                if (move < 250)
                {
                    g.DrawString(" H", font2, whiteBrush, move + 95, 50);
                }

                if (move < 200)
                {
                    g.DrawString("O", font2, whiteBrush, move + 125, 50);
                }

                if (move < 150)
                {
                    g.DrawString("M", font2, whiteBrush, move + 155, 50);
                }

                if (move < 100)
                {
                    g.DrawString("E", font2, whiteBrush, move + 185, 50);
                }

                PacManMoveLeft(move, g);
                Thread.Sleep(20);
            }


        }

        public void PacManMoveRight(int pacMove, Graphics g)
        {
            pacMunch.Play();

            if (mouthOpen == true)
            {
                g.FillPie(yellowBrush, pacMove, 50, 50, 50, 30, 300);

                if (counter % 100 == 5)
                {
                    mouthOpen = false;
                    counter = 0;

                }
                else
                {
                    counter++;
                }
            }

            else if (mouthOpen == false)
            {
                g.FillPie(yellowBrush, pacMove, 50, 50, 50, 30, 360);

                if (counter % 100 == 5)
                {
                    mouthOpen = true;
                    counter = 0;
                }
                else
                {
                    counter++;
                }
            }
        }

        public void PacManMoveLeft(int pacMove, Graphics g)
        {
            pacMunch.Play();

            if (mouthOpen == true)
            {
                g.FillPie(yellowBrush, pacMove, 50, 50, 50, 210, 300);

                if (counter % 100 == 5)
                {
                    mouthOpen = false;
                    counter = 0;
                }
                else
                {
                    counter++;
                }
            }

            else if (mouthOpen == false)
            {
                g.FillPie(yellowBrush, pacMove, 50, 50, 50, 0, 360);

                if (counter % 100 == 5)
                {
                    mouthOpen = true;
                    counter = 0;
                }
                else
                {
                    counter++;
                }
            }
        }

        public void InkyMoveRight(int inkyMove, Graphics g)
        {
            g.FillPie(cyanBrush, inkyMove, 50, 50, 50, 80, -340);
            g.FillEllipse(whiteBrush, inkyMove + 10, 65, 10, 10);
            g.FillEllipse(whiteBrush, inkyMove + 30, 65, 10, 10);
            g.DrawLine(whitePen, inkyMove + 15, 80, inkyMove + 35, 80);
        }

        public void InkyMoveLeft(int inkyMove, Graphics g)
        {
            g.FillPie(blueBrush, inkyMove, 50, 50, 50, 80, -340);
            g.FillEllipse(peachBrush, inkyMove + 10, 65, 10, 10);
            g.FillEllipse(peachBrush, inkyMove + 30, 65, 10, 10);
            g.DrawLine(peachPen, inkyMove + 15, 80, inkyMove + 35, 80);
        }

        public void DeadGhostMove(int deadMove, Graphics g)
        {
            g.FillEllipse(whiteBrush, deadMove + 10, 65, 10, 10);
            g.FillEllipse(whiteBrush, deadMove + 30, 65, 10, 10);
        }

        public void pacMunch_MediaEnded(object sender, EventArgs e)
        {
            pacMunch.Stop();
            pacMunch.Play();
        }
    }
}
