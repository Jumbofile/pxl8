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
    public partial class NewPrompt : Form
    {
        private int xNum = 0;
        private int yNum = 0;
        public NewPrompt()
        {
            InitializeComponent();
        }

        private void newAccept_Click(object sender, EventArgs e)
        {
            int x = Int32.Parse(widthBox.Text);
            int y = Int32.Parse(heightBox.Text);
            xNum = x;
            yNum = y;
            Form1 main = new Form1();
            main.makeImage();

            this.Close();
        }

        public int getNumX()
        {
            return xNum;
        }

        public int getNumY()
        {
            return yNum;
        }
    }
}
