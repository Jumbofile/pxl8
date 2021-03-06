﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace MiniPaint
{
    //URL http://csharphelper.com/blog/2014/12/draw-on-a-bitmap-in-c/
    //https://www.codeproject.com/script/Articles/MemberArticles.aspx?amid=6556
    public partial class Form1 : Form
    {
        public Form1()
        {
            Console.WriteLine("Hello World!");
            InitializeComponent();
            g = pnl_Draw.CreateGraphics();
            panel9.Size = new Size(this.Width, this.Height - 24);
            panel9.Location = new Point(0, 24);
            pnl_Draw.Size = new Size(0, 0);
            imageSizelbl.Text = "0 x 0px";
            imageSizelbl.Location = new Point(this.Width - 210, 4);
            
        }
        bool startPaint = false;
        Graphics g;
        //nullable int for storing Null value
        int? initX = null;
        int? initY = null;
        string tool = "pencil";
        Bitmap bm;

        //New image prompt init
        NewPrompt np = new NewPrompt();

        //Event fired when the mouse pointer is moved over the Panel(pnl_Draw).
        private void pnl_Draw_MouseMove(object sender, MouseEventArgs e)
        {
            if (startPaint)
            {
                System.Diagnostics.Debug.WriteLine("start");
                //Setting the Pen BackColor and line Width
                
                //Drawing the line.
                //g.DrawRectangle(p, initX ?? e.X, initY ?? e.Y, 1, 1);
                //g.DrawLine(p, new Point(initX ?? e.X, initY ?? e.Y), new Point(e.X, e.Y));
                //initX = e.X;
                //initY = e.Y;
                using (Graphics gr = Graphics.FromImage(bm))
                {
                    if (tool == "marker") {
                        Pen p = new Pen(btn_PenColor.BackColor, float.Parse(txt_ShapeSize.Text));
                        p.StartCap = LineCap.Flat;
                        p.EndCap = LineCap.Flat;
                        p.LineJoin = LineJoin.Miter;
                        System.Diagnostics.Debug.WriteLine("Draw");
                        gr.SmoothingMode = SmoothingMode.AntiAlias;
                        gr.CompositingQuality = CompositingQuality.HighQuality;
                        Rectangle rect = new Rectangle(pnl_Draw.Location.X, pnl_Draw.Location.Y, pnl_Draw.Width, pnl_Draw.Height);
                        gr.DrawLine(p, new Point(initX ?? e.X, initY ?? e.Y), new Point(e.X, e.Y));
                        initX = e.X;
                        initY = e.Y;
                    } else if (tool == "pencil")
                    {
                        Pen pencil = new Pen(btn_PenColor.BackColor, int.Parse(txt_ShapeSize.Text));
                        System.Diagnostics.Debug.WriteLine("Draw");
                        //gr.SmoothingMode = SmoothingMode.AntiAlias;
                        pencil.StartCap = LineCap.Square;
                        pencil.EndCap = LineCap.Square;
                        Rectangle rect = new Rectangle(pnl_Draw.Location.X, pnl_Draw.Location.Y, pnl_Draw.Width, pnl_Draw.Height);
                        gr.DrawLine(pencil, new Point(initX ?? e.X, initY ?? e.Y), new Point(e.X, e.Y));
                        initX = e.X;
                        initY = e.Y;
                    }
                    else if (tool == "pen")
                    {
                        Pen pen = new Pen(btn_PenColor.BackColor, float.Parse(txt_ShapeSize.Text));
                        System.Diagnostics.Debug.WriteLine("Draw");
                        gr.SmoothingMode = SmoothingMode.HighQuality;
                        gr.CompositingQuality = CompositingQuality.HighQuality;
                        gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        pen.StartCap = LineCap.Round;

                        pen.EndCap = LineCap.Round;

                        //Set of two part link style

                        pen.LineJoin = LineJoin.Round;

                        Rectangle rect = new Rectangle(pnl_Draw.Location.X, pnl_Draw.Location.Y, pnl_Draw.Width, pnl_Draw.Height);
                        gr.DrawLine(pen, new Point(initX ?? e.X, initY ?? e.Y), new Point(e.X, e.Y));
                        initX = e.X;
                        initY = e.Y;
                    }
                    else if (tool == "eraser")
                    {
                        
                        for (int i = 0; i < int.Parse(txt_ShapeSize.Text); i++) {
                            for (int j = 0; j < int.Parse(txt_ShapeSize.Text); j++)
                            {
                                int mX = e.X - (int.Parse(txt_ShapeSize.Text));
                                int mY = e.Y - (int.Parse(txt_ShapeSize.Text));
                                //if (e.X - int.Parse(txt_ShapeSize.Text) > 0 && e.Y - int.Parse(txt_ShapeSize.Text) > 0 &&
                                //  e.X + int.Parse(txt_ShapeSize.Text) < (pnl_Draw.Width + pnl_Draw.Location.X) - 1 && e.Y + int.Parse(txt_ShapeSize.Text) < (pnl_Draw.Height + pnl_Draw.Location.Y) -1) { 
                                // bm.SetPixel(mX + i, mY + j, Color.Transparent);
                                //}  
                                try {
                                    bm.SetPixel(mX + i, mY + j, Color.Transparent);
                                }
                                catch
                                {
                                    Console.WriteLine("LOL");
                                    //write something to fuck the loops
                                    i = int.Parse(txt_ShapeSize.Text);
                                    j = int.Parse(txt_ShapeSize.Text);
                                }  
                                
                            }
                         }
                        /*
                        Pen er = new Pen(Color.White, float.Parse(txt_ShapeSize.Text));
                        
                        System.Diagnostics.Debug.WriteLine("Draw");
                        //gr.SmoothingMode = SmoothingMode.AntiAlias;
                        //gr.CompositingQuality = CompositingQuality.HighQuality;

                        er.StartCap = LineCap.Round;

                        er.EndCap = LineCap.Round;

                        //Set of two part link style

                        er.LineJoin = LineJoin.Round;

                        Rectangle rect = new Rectangle(pnl_Draw.Location.X, pnl_Draw.Location.Y, pnl_Draw.Width, pnl_Draw.Height);
                        gr.DrawLine(er, new Point(initX ?? e.X, initY ?? e.Y), new Point(e.X, e.Y));
                        initX = e.X;
                        initY = e.Y;*/
                    }
                    //gr.FillEllipse(Brushes.LightGreen, rect);
                    // using (Pen thick_pen = new Pen(Color.Blue, 5))
                    // {
                    //gr.DrawEllipse(thick_pen, rect);
                    //}
                }
               

            }
            pnl_Draw.Image = bm;
        }
        //Event Fired when the mouse pointer is over Panel and a mouse button is pressed
        private void pnl_Draw_MouseDown(object sender, MouseEventArgs e)
        {
            //Setting the Pen BackColor and line Width
            //Pen p = new Pen(btn_PenColor.BackColor, float.Parse(txt_ShapeSize.Text));
            using (Graphics gr = Graphics.FromImage(bm))
            {
                /**
                 * This method basically allows the user to draw pixel by pixel or place a dot on the canvas
                 * Straight copy of the Drawing method but its on mouse down with a startPaint == false condition
                 * and uses brushs instead of pens to fill in the static shape
                 **/

                if (!startPaint)
                {
                    if (tool == "marker")
                    {
                        //Pen pencil = new Pen(btn_PenColor.BackColor, 0);
                        SolidBrush marker = new SolidBrush(btn_PenColor.BackColor);
                        System.Diagnostics.Debug.WriteLine("Draw");
                        //gr.SmoothingMode = SmoothingMode.AntiAlias;

                        Rectangle rect = new Rectangle(pnl_Draw.Location.X, pnl_Draw.Location.Y, pnl_Draw.Width, pnl_Draw.Height);
                        gr.FillRectangle(marker, e.X - (int.Parse(txt_ShapeSize.Text) / 2), e.Y - (int.Parse(txt_ShapeSize.Text) / 2), int.Parse(txt_ShapeSize.Text) / 2, int.Parse(txt_ShapeSize.Text));
                        initX = e.X;
                        initY = e.Y;
                    }
                    else if (tool == "pencil")
                    {
                        //Pen pencil = new Pen(btn_PenColor.BackColor, 0);
                        SolidBrush pencil = new SolidBrush(btn_PenColor.BackColor);
                        System.Diagnostics.Debug.WriteLine("Draw");
                        //gr.SmoothingMode = SmoothingMode.AntiAlias;

                        Rectangle rect = new Rectangle(pnl_Draw.Location.X, pnl_Draw.Location.Y, pnl_Draw.Width, pnl_Draw.Height);
                        gr.FillRectangle(pencil, e.X - (int.Parse(txt_ShapeSize.Text) / 2), e.Y - (int.Parse(txt_ShapeSize.Text) / 2), int.Parse(txt_ShapeSize.Text), int.Parse(txt_ShapeSize.Text));
                        initX = e.X;
                        initY = e.Y;
                    }
                    else if (tool == "pen")
                    {
                        //Pen pencil = new Pen(btn_PenColor.BackColor, 0);
                        SolidBrush pen = new SolidBrush(btn_PenColor.BackColor);
                        System.Diagnostics.Debug.WriteLine("Draw");
                        gr.SmoothingMode = SmoothingMode.AntiAlias;
                        gr.PixelOffsetMode = PixelOffsetMode.HighQuality;

                        Rectangle rect = new Rectangle(pnl_Draw.Location.X, pnl_Draw.Location.Y, pnl_Draw.Width, pnl_Draw.Height);
                        gr.FillEllipse(pen, e.X - (int.Parse(txt_ShapeSize.Text) / 2), e.Y - (int.Parse(txt_ShapeSize.Text) / 2), int.Parse(txt_ShapeSize.Text), int.Parse(txt_ShapeSize.Text));
                        initX = e.X;
                        initY = e.Y;
                    }
                   
                }
                //Prints to the bitmap
                pnl_Draw.Image = bm;
            }

            //This has to become true for the drawing lines method to work above, in other words, dont touch this
            startPaint = true;
        }
        //Fired when the mouse pointer is over the pnl_Draw and a mouse button is released.
        private void pnl_Draw_MouseUp(object sender, MouseEventArgs e)
        {
            startPaint = false;
            initX = null;
            initY = null;
        }
        //Button for Setting pen Color
        private void button1_Click(object sender, EventArgs e)
        {
            //Open Color Dialog and Set BackColor of btn_PenColor if user click on OK
            ColorDialog c = new ColorDialog();
            if (c.ShowDialog() == DialogResult.OK)
            {
                btn_PenColor.BackColor = c.Color;
            }
        }

        /**
         * MAKING A NEW IMAGE
         * I CAN NEVER FIND THIS SO THIS IS MY SOLUTION
         * YAYAYAYAYAYAYAYAY!!!1!
         **/
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int x = -1;
            int y = -1;

            if (x == -1 || y == -1)
            {
                np.ShowDialog();
                int[] sizes = makeImage();
                //Clearing the graphics from the Panel(pnl_Draw)
                g.Clear(pnl_Draw.BackColor);

                //size
                pnl_Draw.Size = new Size(sizes[0], sizes[1]);
                //Setting the BackColor of pnl_draw and btn_CanvasColor to White on Clicking New under File Menu
                pnl_Draw.BackColor = Color.Transparent;
                //btn_CanvasColor.BackColor = Color.White;
                //center panel
                centerPanel();
                System.Diagnostics.Debug.WriteLine("start");
                Bitmap bitm = new Bitmap(sizes[0], sizes[1]);
                imageSizelbl.Text = sizes[0] + " x " + sizes[1]+"px";
                bm = bitm;
               // bitm.Dispose();
            }
            else
            {

            }
           

        }

        //Make new image
        public int[] makeImage()
        {
            int x = np.getNumX();
            int y = np.getNumY();
            int[] size = new int[] { x, y };
            return size;
        }

        //Setting the Canvas Color
        private void btn_CanvasColor_Click_1(object sender, EventArgs e)
        {
            ColorDialog c = new ColorDialog();
            if (c.ShowDialog() == DialogResult.OK)
            {
                //pnl_Draw.BackColor = c.Color;
                //btn_CanvasColor.BackColor = c.Color;
            }
        }

        private void btn_Square_Click(object sender, EventArgs e)
        {
            //drawSquare = true;
        }

        private void btn_Rectangle_Click(object sender, EventArgs e)
        {
            //drawRectangle = true;
        }

        private void btn_Circle_Click(object sender, EventArgs e)
        {
            //drawCircle = true;
        }
        //Exit under File Menu
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }


        //About under Help Menu
        private void aboutMiniPaintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About a = new About();
            a.ShowDialog();
        }

        private void pnl_Draw_Paint(object sender, PaintEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            panel9.Size = new Size(this.Width, this.Height - 24);
            panel9.Location = new Point(0, 24);
            //center panel
            centerPanel();
            /*
            //Dont cut off the canvas
            while (posY <= (34))
            {
                posY++;
            }

            while (posX <= (0 + 10))
            {
                posX++;
            }*/

            //pnl_Draw.Location = new Point(posX, posY);

            //Put canvas size label in right location
            imageSizelbl.Location = new Point(this.Width - 210, 4);
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //Saving image!
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "PNG Image|*.png|JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|Icon FIle|*.ico";
            dialog.Title = "Save an Image File";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                int width = Convert.ToInt32(pnl_Draw.Width);
                int height = Convert.ToInt32(pnl_Draw.Height);
                Bitmap bmp = new Bitmap(width, height);

                switch (dialog.FilterIndex)
                {
                    case 1:
                        bm.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                        break;

                    case 2:
                        //Need a white back ground, fuck
                       
                        for (int i = 0; i < bm.Width; i++)
                        {
                            for (int j = 0; j < bm.Height; j++)
                            {
                                Color compare = bm.GetPixel(i, j);
                                if (compare.Equals(Color.Transparent))
                                {
                                    bm.SetPixel(i, j, Color.White);
                                }
                            }
                        }
                        bm.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 3:
                        bm.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case 4:
                        bm.MakeTransparent(Color.Black);
                        bm.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    case 5:
                        bm.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Icon);
                        break;
                }
                //bmp.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                //bmp.Dispose();
            }
            
        }

        private void pnl_Draw_Click(object sender, EventArgs e)
        {
            
            
        }

        //Loading a file
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "PNG Image|*.png|JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|Icon FIle|*.ico";
            dialog.Title = "Open an Image File";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap bmp = (Bitmap)Image.FromFile(dialog.FileName);
                panel9.Location = new Point(0, 24);
                pnl_Draw.Size = new Size(bmp.Width, bmp.Height);
                
                //center panel
                centerPanel();

                imageSizelbl.Text = bmp.Width + " x " + bmp.Height + "px";
                bm = bmp;
                pnl_Draw.Image = bm;
               // bmp.Dispose();
            }
        }

        private void pencilButton_Click(object sender, EventArgs e)
        {
            tool = "pencil";
        }

        private void markerButton_Click(object sender, EventArgs e)
        {
            tool = "marker";
        }

        private void penButton_Click(object sender, EventArgs e)
        {
            tool = "pen";
        }

        private void eraserButton_Click(object sender, EventArgs e)
        {
            tool = "eraser";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        //This centers the image
        private void centerPanel()
        {
            //Center the canvas
            int posX = ((this.Width - pnl_Draw.Width) / 2);
            int posY = ((this.Height - pnl_Draw.Height) / 2);
            /* if (posY < 9 || posX < 9)
             {
                 posX = 10;
                 posY = 10 + 24;
             }*/

            
            //Dont cut off the canvas
            while (posY <= (34))
            {
                posY++;
            }

            while (posX <= (0 + 10))
            {
                posX++;
            }

            pnl_Draw.Location = new Point(posX, posY);
        }

        private void blackAndWhiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pxl8.Filters fl = new pxl8.Filters();

            bm = fl.applyFilters("bw", bm);
            pnl_Draw.Image = bm;
        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pxl8.Filters fl = new pxl8.Filters();

            bm = fl.applyFilters("sp", bm);

            pnl_Draw.Image = bm;
        }

        //TEST
        private void zoomIn()
        {

        }
    }
}
