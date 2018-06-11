using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MiniPaint
{
    public partial class Splash : Form
    {
        //Use timer class

        Timer tmr;

        public Splash()
        {
            InitializeComponent();
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            Console.WriteLine("Shown");
            tmr = new Timer();

            //set time interval 3 sec

            tmr.Interval = 3000;

            //starts the timer

            tmr.Start();

            tmr.Tick += tmr_Tick;
        }

        void tmr_Tick(object sender, EventArgs e)

        {
            Console.WriteLine("timer done");
            //after 3 sec stop the timer

            tmr.Stop();

            //display mainform

            Form1 mf = new Form1();

            mf.Show();

            //hide this form

            this.Hide();

        }
    }
}
