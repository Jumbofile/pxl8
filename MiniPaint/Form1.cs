using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace MiniPaint
{
    //URL http://csharphelper.com/blog/2014/12/draw-on-a-bitmap-in-c/
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
        bool drawSquare = false;
        bool drawRectangle = false;
        bool drawCircle = false;
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
                Pen p = new Pen(btn_PenColor.BackColor, float.Parse(txt_ShapeSize.Text));
                //Drawing the line.
                //g.DrawRectangle(p, initX ?? e.X, initY ?? e.Y, 1, 1);
                //g.DrawLine(p, new Point(initX ?? e.X, initY ?? e.Y), new Point(e.X, e.Y));
                //initX = e.X;
                //initY = e.Y;
                using (Graphics gr = Graphics.FromImage(bm))
                {
                    if (tool == "marker") { 
                        System.Diagnostics.Debug.WriteLine("Draw");
                        gr.SmoothingMode = SmoothingMode.AntiAlias;

                        Rectangle rect = new Rectangle(pnl_Draw.Location.X, pnl_Draw.Location.Y, pnl_Draw.Width, pnl_Draw.Height);
                        gr.DrawLine(p, new Point(initX ?? e.X, initY ?? e.Y), new Point(e.X, e.Y));
                        initX = e.X;
                        initY = e.Y;
                    }else if (tool == "pencil")
                    {
                        System.Diagnostics.Debug.WriteLine("Draw");
                        //gr.SmoothingMode = SmoothingMode.AntiAlias;

                        Rectangle rect = new Rectangle(pnl_Draw.Location.X, pnl_Draw.Location.Y, pnl_Draw.Width, pnl_Draw.Height);
                        gr.DrawRectangle(p, new Rectangle(initX ?? e.X, initY ?? e.Y, int.Parse(txt_ShapeSize.Text), int.Parse(txt_ShapeSize.Text)));
                        initX = e.X;
                        initY = e.Y;
                    }
                    else if (tool == "pen")
                    {
                        System.Diagnostics.Debug.WriteLine("Draw");
                        gr.SmoothingMode = SmoothingMode.AntiAlias;

                        Rectangle rect = new Rectangle(pnl_Draw.Location.X, pnl_Draw.Location.Y, pnl_Draw.Width, pnl_Draw.Height);
                        gr.DrawEllipse(p, new Rectangle(e.X, e.Y, int.Parse(txt_ShapeSize.Text), int.Parse(txt_ShapeSize.Text)));
                        initX = e.X;
                        initY = e.Y;
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
            Pen p = new Pen(btn_PenColor.BackColor, float.Parse(txt_ShapeSize.Text));
            using (Graphics gr = Graphics.FromImage(bm))
            {
                /**
                 * This method basically allows the user to draw pixel by pixel or place a dot on the canvas
                 * Straight copy of the Drawing method but its on mouse down with a startPaint == false condition
                 **/

                if (!startPaint)
                {
                    if (tool == "marker")
                    {
                        System.Diagnostics.Debug.WriteLine("Draw");
                        gr.SmoothingMode = SmoothingMode.AntiAlias;

                        Rectangle rect = new Rectangle(pnl_Draw.Location.X, pnl_Draw.Location.Y, pnl_Draw.Width, pnl_Draw.Height);
                        gr.DrawLine(p, new Point(initX ?? e.X, initY ?? e.Y), new Point(e.X, e.Y));
                        initX = e.X;
                        initY = e.Y;
                    }
                    else if (tool == "pencil")
                    {
                        System.Diagnostics.Debug.WriteLine("Draw");
                        //gr.SmoothingMode = SmoothingMode.AntiAlias;

                        Rectangle rect = new Rectangle(pnl_Draw.Location.X, pnl_Draw.Location.Y, pnl_Draw.Width, pnl_Draw.Height);
                        gr.DrawRectangle(p, new Rectangle(initX ?? e.X, initY ?? e.Y, int.Parse(txt_ShapeSize.Text), int.Parse(txt_ShapeSize.Text)));
                        initX = e.X;
                        initY = e.Y;
                    }
                    else if (tool == "pen")
                    {
                        System.Diagnostics.Debug.WriteLine("Draw");
                        gr.SmoothingMode = SmoothingMode.AntiAlias;

                        Rectangle rect = new Rectangle(pnl_Draw.Location.X, pnl_Draw.Location.Y, pnl_Draw.Width, pnl_Draw.Height);
                        gr.DrawEllipse(p, new Rectangle(e.X, e.Y, int.Parse(txt_ShapeSize.Text), int.Parse(txt_ShapeSize.Text)));
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
        //New 
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
                pnl_Draw.BackColor = Color.White;
                btn_CanvasColor.BackColor = Color.White;
                //center panel
                centerPanel();
                System.Diagnostics.Debug.WriteLine("start");
                Bitmap bitm = new Bitmap(sizes[0], sizes[1]);
                imageSizelbl.Text = sizes[0] + " x " + sizes[1]+"px";
                bm = bitm;
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
                pnl_Draw.BackColor = c.Color;
                btn_CanvasColor.BackColor = c.Color;
            }
        }

        private void btn_Square_Click(object sender, EventArgs e)
        {
            drawSquare = true;
        }

        private void btn_Rectangle_Click(object sender, EventArgs e)
        {
            drawRectangle = true;
        }

        private void btn_Circle_Click(object sender, EventArgs e)
        {
            drawCircle = true;
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
                pnl_Draw.DrawToBitmap(bmp, new Rectangle(0, 0, width, height));
                switch (dialog.FilterIndex)
                {
                    case 1:
                        bmp.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                        break;

                    case 2:
                        bmp.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 3:
                        bmp.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case 4:
                        bmp.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    case 5:
                        bmp.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Icon);
                        break;
                }
                //bmp.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
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
            dialog.Title = "Save an Image File";
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

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
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(bm.Width, bm.Height);

            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);

            //create the grayscale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(
               new float[][]
               {
                 new float[] {.3f, .3f, .3f, 0, 0},
                 new float[] {.59f, .59f, .59f, 0, 0},
                 new float[] {.11f, .11f, .11f, 0, 0},
                 new float[] {0, 0, 0, 1, 0},
                 new float[] {0, 0, 0, 0, 1}
               });

            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            //draw the original image on the new image
            //using the grayscale color matrix
            g.DrawImage(bm, new Rectangle(0, 0, bm.Width, bm.Height),
               0, 0, bm.Width, bm.Height, GraphicsUnit.Pixel, attributes);

            bm = newBitmap;
            pnl_Draw.Image = bm;
        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            float[][] sepiaValues = {
            new float[]{.393f, .349f, .272f, 0, 0},
            new float[]{.769f, .686f, .534f, 0, 0},
            new float[]{.189f, .168f, .131f, 0, 0},
            new float[]{0, 0, 0, 1, 0},
            new float[]{0, 0, 0, 0, 1}};
            System.Drawing.Imaging.ColorMatrix sepiaMatrix = new System.Drawing.Imaging.ColorMatrix(sepiaValues);
            System.Drawing.Imaging.ImageAttributes IA = new System.Drawing.Imaging.ImageAttributes();
            IA.SetColorMatrix(sepiaMatrix);
            Bitmap sepiaEffect = bm;
            using (Graphics G = Graphics.FromImage(sepiaEffect))
            {
                G.DrawImage(pnl_Draw.Image, new Rectangle(0, 0, sepiaEffect.Width, sepiaEffect.Height), 0, 0, sepiaEffect.Width, sepiaEffect.Height, GraphicsUnit.Pixel, IA);
            }
            bm = sepiaEffect;
            pnl_Draw.Image = bm;
        }
    }
}
