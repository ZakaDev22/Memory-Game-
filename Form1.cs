using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Memory_Game_V2._0
{
    public partial class Form1 : Form
    {

        List<int> Numbers = new List<int> { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6 };

        string FirstChoise, SecondChoise;
        int Tries;

        List<PictureBox> Pictures = new List<PictureBox>();

        PictureBox picA, picB;

        int TotalTime = 50;
        int CountDownTime;

        bool gameover;

        public Form1()
        {
            InitializeComponent();
            LoedPictures();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            CountDownTime--;

            lbTimeLeft.Text = "Time Left: " + CountDownTime;

            if (CountDownTime < 1)
            {
                GameOver("Times Up, You Lose :-(");

                foreach (PictureBox x in Pictures)
                {
                    if (x.Tag != null)
                    {
                        x.Image = Image.FromFile("Pics/" + (string)x.Tag + ".png");
                    }
                }
            }

        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            RestartGame();
        }

        private void LoedPictures()
        {
            int LeftPos = 20;
            int TopPos = 20;
            int Rows = 0;


            for (int i = 1; i <= 12; i++)
            {
                PictureBox NewPic = new PictureBox();
                NewPic.Height = 75;
                NewPic.Width = 75;
                NewPic.BackColor = Color.LightGray;
                NewPic.SizeMode = PictureBoxSizeMode.StretchImage;
                NewPic.Click += NewPic_Click;
                Pictures.Add(NewPic);

                if (Rows < 3)
                {
                    Rows++;
                    NewPic.Left = LeftPos;
                    NewPic.Top = TopPos;
                    this.Controls.Add(NewPic);
                    LeftPos += 80;
                }

                if (Rows == 3)
                {
                    LeftPos = 20;
                    TopPos += 80;
                    Rows = 0;
                }
            }

            RestartGame();


        }

        private void NewPic_Click(object sender, EventArgs e)
        {
            if (gameover)
            {
                return;
            }

            if (FirstChoise == null)
            {
                picA = sender as PictureBox;
                if (picA.Tag != null && picA.Image == null)
                {
                    picA.Image = Image.FromFile("Pics/" + (string)picA.Tag + ".png");
                    FirstChoise = (string)picA.Tag;
                }
            }
            else if (SecondChoise == null)
            {

                picB = sender as PictureBox;
                if (picB.Tag != null && picB.Image == null)
                {
                    picB.Image = Image.FromFile("Pics/" + (string)picB.Tag + ".png");
                    SecondChoise = (string)picB.Tag;
                }
            }
            else
            {
                CheckPictures(picA, picB);
            }


        }

        private void RestartGame()
        {
            var RandomList = Numbers.OrderBy(x => Guid.NewGuid()).ToList();

            Numbers = RandomList;

            for (int i = 0; i < Pictures.Count; i++)
            {
                Pictures[i].Image = null;
                Pictures[i].Tag = Numbers[i].ToString();
            }

            Tries = 0;
            lbMatchOrMisMatch.Text = "MisMatch " + Tries + " Times.";
            lbTimeLeft.Text = "Time Left : " + TotalTime;
            gameover = false;
            GameTimer.Start();
            CountDownTime = TotalTime;


        }

        private void CheckPictures(PictureBox A, PictureBox B)
        {
            if (FirstChoise == SecondChoise)
            {
                A.Tag = null;
                B.Tag = null;
            }
            else
            {
                Tries++;
                lbMatchOrMisMatch.Text = "MisMatch " + Tries + " Times.";
            }

            FirstChoise = null;
            SecondChoise = null;


            foreach (PictureBox pic in Pictures.ToList())
            {
                if (pic.Tag != null)
                {
                    pic.Image = null;
                }
            }

            if (Pictures.All(o => o.Tag == Pictures[0].Tag))
            {
                GameOver("Nice Work You Win! :-) ");
            }
        }

        private void GameOver(string msg)
        {
            GameTimer.Stop();
            gameover = true;

            MessageBox.Show(msg + Environment.NewLine + "Click Restart Button To Play Again", " Game Over Notify");
        }
    }
}
