using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Air_Hockey
{
    public partial class Form1 : Form
    {
        int paddle1X = 50;
        int paddle1Y = 170;
        int player1Score = 0;

        int paddle2X = 600;
        int paddle2Y = 170;
        int player2Score = 0;

        int paddleWidth = 10;
        int paddleHeight = 30;
        int paddleSpeed = 6;

        int ballX = 320;
        int ballY = 180;
        int ballXSpeed = 8;
        int ballYSpeed = -8;
        int ballWidth = 10;
        int ballHeight = 10;

        int topwidth = 590;
        int toplenth = 5;
        int topx = 30;
        int topy = 60;

        int bottomwidth = 590;
        int bottomlength = 5;
        int bottomx = 30;
        int bottomy = 320;

        int side1width = 5;
        int side1length = 70;
        int side1x = 30;
        int side1y = 60;

        int side2width = 5;
        int side2length = 70;
        int side2x = 620;
        int side2y = 60;

        int side3width = 5;
        int side3length = 70;
        int side3x = 30;
        int side3y = 250;

        int side4width = 5;
        int side4length = 75;
        int side4x = 620;
        int side4y = 250;

        bool wDown = false;
        bool sDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;
        bool rightarrowdown = false;
        bool leftarrowdown = false;
        bool adown = false;
        bool ddown = false;



        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        Font screenFont = new Font("Consolas", 12);

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Keydown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.D:
                    ddown = true;
                    break;
                case Keys.A:
                    adown = true;
                    break;
                case Keys.Left:
                    leftarrowdown = true;
                    break;
                case Keys.Right:
                    rightarrowdown = true;
                    break;
            }
        }
            private void Form1_Keyup(object sender, KeyEventArgs e)
            {
                switch (e.KeyCode)
                {
                    case Keys.W:
                        wDown = false;
                        break;
                    case Keys.S:
                        sDown = false;
                        break;
                    case Keys.Up:
                        upArrowDown = false;
                        break;
                    case Keys.Down:
                        downArrowDown = false;
                        break;
                    case Keys.D:
                        ddown = false;
                        break;
                    case Keys.A:
                        adown = false;
                        break;
                    case Keys.Left:
                        leftarrowdown = false;
                        break;
                    case Keys.Right:
                        rightarrowdown = false;
                        break;
                }
            }

        private void gametimer_Tick_1(object sender, EventArgs e)
        {
            SoundPlayer cheer = new SoundPlayer(Properties.Resources.cheer);
            //move ball
            ballX += ballXSpeed;
            ballY += ballYSpeed;
            //move player 1
            if (wDown == true && paddle1Y > 0)
            {
                paddle1Y -= paddleSpeed;
            }

            if (sDown == true && paddle1Y < this.Height - paddleHeight)
            {
                paddle1Y += paddleSpeed;
            }

            if (ddown == true && paddle1X < this.Width - paddleHeight)
            {
                paddle1X += paddleSpeed;
            }

            if (adown == true && paddle1X < this.Width - paddleHeight)
            {
                paddle1X -= paddleSpeed;
            }
            //move player 2
            if (upArrowDown == true && paddle2Y > 0)
            {
                paddle2Y -= paddleSpeed;
            }

            if (downArrowDown == true && paddle2Y < this.Height - paddleHeight)
            {
                paddle2Y += paddleSpeed;
            }
            if (rightarrowdown == true && paddle2X < this.Width - paddleHeight)
            {
                paddle2X += paddleSpeed;
            }
            if (leftarrowdown == true && paddle2X < this.Width - paddleHeight)
            {
                paddle2X -= paddleSpeed;
            }
            //top and bottom wall collision
            if (ballY < 70 || ballY > 320 - ballHeight)
            {
                ballYSpeed *= -1;  // or: ballYSpeed = -ballYSpeed;
            }
            if (ballX < 40 || ballX > 615 - ballHeight)
            {
                ballYSpeed *= -1;  // or: ballYSpeed = -ballYSpeed;
            }
            //create Rectangles of objects on screen to be used for collision detection
            Rectangle player1Rec = new Rectangle(paddle1X, paddle1Y, paddleWidth, paddleHeight);
            Rectangle player2Rec = new Rectangle(paddle2X, paddle2Y, paddleWidth, paddleHeight);
            Rectangle ballRec = new Rectangle(ballX, ballY, ballWidth, ballHeight);
            Rectangle top = new Rectangle(topx, topy, topwidth, toplenth);
            Rectangle bottom = new Rectangle(bottomx, bottomy, bottomwidth, bottomlength);
            Rectangle side1 = new Rectangle(side1x, side1y, side1width, side1length);
            Rectangle side2 = new Rectangle(side2x, side2y, side2width, side2length);
            Rectangle side3 = new Rectangle(side3x, side3y, side3width, side3length);
            Rectangle side4 = new Rectangle(side4x, side4y, side4width, side4length);
            //check if ball hits either paddle. If it does change the direction
            //and place the ball in front of the paddle hit
            if (player1Rec.IntersectsWith(ballRec))
            {
                ballXSpeed *= -1;

                ballX = paddle1X + paddleWidth + 1;
            }
            else if (player2Rec.IntersectsWith(ballRec))
            {
                ballXSpeed *= -1;
                ballX = paddle2X - ballWidth - 1;
            }
            else if (player2Rec.IntersectsWith(top))
            {

                paddle2Y = topy - paddle2Y + side2y;
            }
            else if (player2Rec.IntersectsWith(bottom))
            {

                paddle2Y = bottomy - paddle2Y + side3y;
            }
            else if (player2Rec.IntersectsWith(side1))
            {

                paddle2X = side1x + paddle2Y - side1y;
            }
            else if (player2Rec.IntersectsWith(side2))
            {

                paddle2X = side2x - paddle2Y + side2y;
            }
            else if (player2Rec.IntersectsWith(side3))
            {

                paddle2X = side3x + paddle2Y - side3y;
            }
            else if (player2Rec.IntersectsWith(side4))
            {

                paddle2X = side4x - paddle2Y + side4y;
            }
            else if (player1Rec.IntersectsWith(top))
            {

                paddle1Y = topy - paddle1Y + side2y;
            }
            else if (player1Rec.IntersectsWith(bottom))
            {

                paddle1Y = bottomy - paddle1Y + side3y;
            }
            else if (player1Rec.IntersectsWith(side1))
            {

                paddle1X = side1x + paddle1Y - side1y;
            }
            else if (player1Rec.IntersectsWith(side2))
            {

                paddle1X = side2x - paddle1Y + side2y;
            }
            else if (player1Rec.IntersectsWith(side3))
            {

                paddle1X = side3x + paddle1Y - side3y;
            }
            else if (player1Rec.IntersectsWith(side4))
            {

                paddle1X = side4x - paddle1Y + side4y;

            }
            if (ballX < 20)
            {
                cheer.Play();
                player2Score++;
                ballX = 320;
                ballY = 180;
                paddle1X = 50;
                paddle2X = 600;
                paddle1Y = 170;
                paddle2Y = 170;
            }
            else if (ballX > 620)
            {
                cheer.Play();
                player1Score++;
                ballX = 320;
                ballY = 180;
                paddle1X = 50;
                paddle1Y = 170;
                paddle2X = 600;
                paddle2Y = 170;
            }
            if (player1Score == 3 || player2Score == 3)
            {
                gametimer.Enabled = false;
            }

            Refresh();
        }    
   

            private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(whiteBrush, ballX, ballY, ballWidth, ballHeight);

            e.Graphics.FillRectangle(redBrush, paddle1X, paddle1Y, paddleWidth, paddleHeight);
            e.Graphics.FillRectangle(redBrush, paddle2X, paddle2Y, paddleWidth, paddleHeight);

            e.Graphics.FillRectangle(blueBrush, topx, topy, topwidth, toplenth);
            e.Graphics.FillRectangle(blueBrush, bottomx, bottomy, bottomwidth, bottomlength);
            e.Graphics.FillRectangle(blueBrush, side1x, side1y, side1width, side1length);
            e.Graphics.FillRectangle(blueBrush, side2x, side2y, side2width, side2length);
            e.Graphics.FillRectangle(blueBrush, side3x, side3y, side3width, side3length);
            e.Graphics.FillRectangle(blueBrush, side4x, side4y, side4width, side4length);


            e.Graphics.DrawString($"{player1Score}", screenFont, whiteBrush, 310, 10);
            e.Graphics.DrawString($"{player2Score}", screenFont, whiteBrush, 350, 10);



        }    
        }
    }
    


