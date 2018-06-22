using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pxl8
{
    public class Filters
    {
        public Bitmap applyFilters(String name, Bitmap image)
        {
            Console.WriteLine("Filter start");

            //Black and white
            if (name.Equals("bw"))
            {
                Console.WriteLine("bw");
                //create a blank bitmap the same size as original
                Bitmap newBitmap = new Bitmap(image.Width, image.Height);

                //get a graphics object from the new image
                Graphics g = Graphics.FromImage(image);

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
                g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height),
                   0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
            }

            //Sepia
            if (name.Equals("sp"))
            {
                Console.WriteLine("sp");

                //Values for sepia filter color matrix
                float[][] sepiaValues = {
                new float[]{.393f, .349f, .272f, 0, 0},
                new float[]{.769f, .686f, .534f, 0, 0},
                new float[]{.189f, .168f, .131f, 0, 0},
                new float[]{0, 0, 0, 1, 0},
                new float[]{0, 0, 0, 0, 1}};

                //Make and populate color matrix
                ColorMatrix sepiaMatrix = new ColorMatrix(sepiaValues);
                ImageAttributes IA = new ImageAttributes();

                //Setting image attributes
                IA.SetColorMatrix(sepiaMatrix);
                Bitmap sepiaEffect = image;

                //Draw image with sepia
                using (Graphics G = Graphics.FromImage(sepiaEffect))
                {
                    G.DrawImage(sepiaEffect, new Rectangle(0, 0, sepiaEffect.Width, sepiaEffect.Height), 0, 0, sepiaEffect.Width, sepiaEffect.Height, GraphicsUnit.Pixel, IA);
                }
                image = sepiaEffect;
                
            }

            return image;
        }
    }
}
