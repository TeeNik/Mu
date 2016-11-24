using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MeeMay
{
    public partial class Form1 : Form
    {

        class Cat : PictureBox
        {

            Random rand = new Random();
            int aim;
            int stepX = 0;
            int stepY = 0;
            Timer timer = new Timer();
            int floor;
            bool isStanding = false;
            bool wasOnAir = false;

            public Cat()
            {              
                create();
                getAim();
                move();      
            }

            void create()
            {
                Location = new Point(200,Screen.PrimaryScreen.Bounds.Height-280);
                Image = Image.FromFile("right.gif");
                MinimumSize = new Size(244, 306);
                Size = new Size(244, 306);

                floor = Location.Y;

                timer.Tick += new EventHandler(tickStep);
                timer.Start();

                MouseClick += rightMouseClick;
                MouseDown += mouseLeftDown;
                MouseMove += mouseLeftMove;
                MouseUp += mouseLeftUp;
                MouseEnter += mouseEnterIdle;
                MouseLeave += mouseExitIdle;
            }

            void getStep()
            {
                if (Location.Y < floor && !wasOnAir)
                    Image = Image.FromFile("fall.gif");
                if (Location.Y < floor)
                {
                    stepX = 0;
                    stepY = 20 ;
                    wasOnAir = true;
                }
                else if (isStanding == true)
                {
                    stepX = 0;
                    stepY = 0;                
                }
                else
                {
                    Location = new Point(Location.X, floor) ;
                    stepY = 0;
                    if (Location.X - aim > 0)
                    {
                        stepX = -4;
                        if (wasOnAir)
                        {
                            Image = Image.FromFile("left.gif");
                            wasOnAir = false;
                        }
                    }
                    else
                    {
                        stepX = 4;
                        if (wasOnAir)
                        {
                            Image = Image.FromFile("right.gif");
                            wasOnAir = false;
                        }
                    }
                }
            }

            void stand()
            {
                Image = Image.FromFile("stand.gif");
                timer.Interval = rand.Next(1500, 5000);
                isStanding = true;
            }

            void getAim()
            {
                aim = rand.Next(20, Screen.PrimaryScreen.Bounds.Size.Width - 100);
                if (Location.X - aim > 0) Image = Image.FromFile("left.gif");
                else Image = Image.FromFile("right.gif");
            }

            private void InitializeComponent()
            {
                ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
                this.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
                this.ResumeLayout(false);
            }

            private void move()
            {
                isStanding = false;
                getAim();
                timer.Interval = 40;
            }

            void fall()
            {
                Image = Image.FromFile("fall.gif");
            }

            void tickStep(object sender, EventArgs e)
            {

                if (isStanding) move();
                getStep();
                
                Location += new Size(stepX, stepY);

                if ((stepX < 0 && Location.X <= aim) || (stepX > 0 && Location.X >= aim))
                {
                    stand();
                }    
            }



            private void mouseLeftDown(object sender, MouseEventArgs e)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    timer.Interval = 40;
                    Image = Image.FromFile("pull.gif");
                    isStanding = true;
                    timer.Stop();
                }
            }

            private void mouseLeftMove(object sender, MouseEventArgs e)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    Left = e.X + Left-138;
                    Top = e.Y + Top -100;
                }
            }

            private void mouseLeftUp(object sender, MouseEventArgs e)
            {
              if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {                    
                    timer.Start();
                    wasOnAir = false;
                    Image = Image.FromFile("fall.gif");
                }
            }

            void rightMouseClick(object sender, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Right)
                {
                    Application.Exit();
                }
            }

            void mouseEnterIdle(object sender, EventArgs e)
            {
                if(Location.Y == floor)
                {
                    isStanding = true;
                    timer.Stop();
                    Image = Image.FromFile("idle.gif");
                }               
            }

            void mouseExitIdle(object sender, EventArgs e)
            {
                isStanding = false;
                wasOnAir = true;
                timer.Start();
                timer.Interval = 40;
                getStep();
            }
        }

        public Form1()
        {
            InitializeComponent();         
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TopMost = true;

            Size = Screen.PrimaryScreen.Bounds.Size + (new Size(20, 20));
            Location = new Point(0, 0);
            FormBorderStyle = FormBorderStyle.None;
            BackColor = Color.Black;
            TransparencyKey = Color.Black;



            Cat cat = new Cat();

            Controls.Add(cat);
        }



    }
}
