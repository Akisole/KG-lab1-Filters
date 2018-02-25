using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace Filtres
{
     abstract class Filters
    {
         public int Rm = -1, Gm, Bm;

        protected abstract Color calculateNewPixelColor(Bitmap sourseImage, int x, int y);

        public Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resultImage.Width * 100));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    resultImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
                }
            }
            Rm = -1;
            return resultImage;
        }

        public int Clamp(int value, int min, int max)
        {
            if (value < min)
                return min;
            if (value > max)
                return max;
            return value;
        }
    }
     class InvertFilter:Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
        {
            Color sourseColor = sourseImage.GetPixel(x, y);
            Color resultColor = Color.FromArgb(255 - sourseColor.R, 255 - sourseColor.G, 255 - sourseColor.B);
            return resultColor;

        }  
    }
     class GreyScaleFilter : Filters
     {
         protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
         {
            Color sourseColor = sourseImage.GetPixel(x, y);
            double intensity = 0.36*sourseColor.R + 0.53*sourseColor.G + 0.11*sourseColor.B;
            return Color.FromArgb(
                Clamp((int)intensity, 0, 255),
                Clamp((int)intensity, 0, 255),
                Clamp((int)intensity, 0, 255));
         }
     }
     class SepiaFilter : Filters
     {
         protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
         {
             Color sourseColor = sourseImage.GetPixel(x, y);
             double intensity = 0.36 * sourseColor.R + 0.53 * sourseColor.G + 0.11 * sourseColor.B;
             double k=20;
             double resultR = intensity + 2 * k,
                    resultG = intensity + 0.5 * k,
                    resultB = intensity - 1 * k;

             return Color.FromArgb(
                 Clamp((int)resultR, 0, 255),
                 Clamp((int)resultG, 0, 255),
                 Clamp((int)resultB, 0, 255));
         }
     }
     class BrightFilter : Filters
     {
         protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
         {
             Color sourseColor = sourseImage.GetPixel(x, y);
             double k = 10;
             double resultR = sourseColor.R + k,
                    resultG = sourseColor.G + k,
                    resultB = sourseColor.B + k;

             return Color.FromArgb(
                 Clamp((int)resultR, 0, 255),
                 Clamp((int)resultG, 0, 255),
                 Clamp((int)resultB, 0, 255));
         }
     }
     class PerenosFilter : Filters
     {
         protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
         {
             int resultR = 0, resultG = 0, resultB = 0;

             if (x + 50 < sourseImage.Width)
             {
                 Color resultColor = sourseImage.GetPixel(x+50, y);
                 resultR = resultColor.R;
                 resultG = resultColor.G;
                 resultB = resultColor.B;
             }
             return Color.FromArgb(resultR, resultG, resultB);

         }
     }
     class PovorotFilter : Filters
     {
         protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
         {
             int x0 = sourseImage.Width / 2, y0 = sourseImage.Height / 2, m=20;
             int resultR = 0, resultG = 0, resultB = 0;

             int k = (int)((x - x0) * Math.Cos(m) - (y - y0) * Math.Sin(m) + x0);
             int l = (int)((x - x0) * Math.Sin(m) + (y - y0) * Math.Cos(m) + y0);

             if (k > 0 && k < sourseImage.Width && l > 0 && l < sourseImage.Height)
             {
                 Color resultColor = sourseImage.GetPixel(k, l);
                 resultR = resultColor.R;
                 resultG = resultColor.G;
                 resultB = resultColor.B;
             }
             return Color.FromArgb(resultR, resultG, resultB);
         }


     }

     class MatrixFilter : Filters
    {
        protected float[,] kernel = null;
        protected MatrixFilter() { }
        public MatrixFilter(float[,] kernel)
        {
            this.kernel = kernel;
        }

        protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
        {
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;

            float resultR = 0, resultG = 0, resultB = 0;
            for (int l=-radiusY; l<=radiusY; l++)
                for (int k = -radiusX; k <= radiusX; k++)
                {
                    int idX = Clamp(x + k, 0, sourseImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourseImage.Height - 1);
                    Color neighborColor = sourseImage.GetPixel(idX, idY);
                    resultR += neighborColor.R * kernel[k + radiusX, l + radiusY];
                    resultG += neighborColor.G * kernel[k + radiusX, l + radiusY];
                    resultB += neighborColor.B * kernel[k + radiusX, l + radiusY];
                }

            return Color.FromArgb(
                Clamp((int)resultR, 0, 255),
                Clamp((int)resultG, 0, 255),
                Clamp((int)resultB, 0, 255));
        }

    }
     class BlurFilter : MatrixFilter
    {
        public BlurFilter()
        {
            int sizeX = 3;
            int sizeY = 3;
            kernel = new float[sizeX, sizeY];
            for (int i = 0; i < sizeX; i++)
                for (int j = 0; j < sizeY; j++)
                    kernel[i, j] = 1.0f / (float)(sizeX * sizeY);
        }
    }
     class GaussianFilter : MatrixFilter
    {
        public GaussianFilter() {
            createGaussianKernel(3, 2);
        }

        public void createGaussianKernel(int radius, float sigma)
        {
            int size = 2 * radius + 1;
            kernel = new float[size, size];
            float norm = 0;
            for (int i = -radius; i <= radius; i++)
                for (int j = -radius; j <= radius; j++)
                {
                    kernel[i + radius, j + radius] = (float)(Math.Exp(-(i * i + j * j) / (sigma * sigma)));
                    norm += kernel[i + radius, j + radius];
                }
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    kernel[i, j] /= norm;
        }

        protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
        {
            float resultR = 0, resultG = 0, resultB = 0;
            for (int l = -3; l <= 3; l++)
                for (int k = -3; k <= 3; k++)
                {
                    int idX = Clamp(x + k, 0, sourseImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourseImage.Height - 1);
                    Color neighborColor = sourseImage.GetPixel(idX, idY);
                    resultR += neighborColor.R * kernel[k + 3, l + 3];
                    resultG += neighborColor.G * kernel[k + 3, l + 3];
                    resultB += neighborColor.B * kernel[k + 3, l + 3];
                }

            return Color.FromArgb(
                Clamp((int)resultR, 0, 255),
                Clamp((int)resultG, 0, 255),
                Clamp((int)resultB, 0, 255));
        }

    }

     class SobelYFilter : MatrixFilter
     {
         public SobelYFilter()
         {
             int size = 3;
             kernel = new float[size, size];
             kernel[0, 0] = kernel[0, 2] = -1;
             kernel[2, 0] = kernel[2, 2] = 1;
             kernel[1, 0] = kernel[1, 1] = kernel[1, 2] = 0;
             kernel[0, 1] = -2;
             kernel[2, 1] = 2;
         }
     }
     class SobelXFilter : MatrixFilter
     {
         public SobelXFilter()
         {
             int size = 3;
             kernel = new float[size, size];
             kernel[0, 0] = kernel[2, 0] = -1;
             kernel[0, 2] = kernel[2, 2] = 1;
             kernel[0, 1] = kernel[1, 1] = kernel[2, 1] = 0;
             kernel[1, 0] = -2;
             kernel[1, 2] = 2;
         }
     }

     class BorderYShFilter : MatrixFilter
     {
         public BorderYShFilter()
         {
             int size = 3;
             kernel = new float[size, size];
             kernel[0, 0] = kernel[0, 2] = 3;
             kernel[2, 0] = kernel[2, 2] = -3;
             kernel[1, 0] = kernel[1, 1] = kernel[1, 2] = 0;
             kernel[0, 1] = 10;
             kernel[2, 1] = -10;
         }
     }
     class BorderXShFilter : MatrixFilter
     {
         public BorderXShFilter()
         {
             int size = 3;
             kernel = new float[size, size];
             kernel[0, 0] = kernel[2, 0] = 3;
             kernel[0, 2] = kernel[2, 2] = -3;
             kernel[0, 1] = kernel[1, 1] = kernel[2, 1] = 0;
             kernel[1, 0] = 10;
             kernel[1, 2] = -10;
         }
     }
     class BorderYPFilter : MatrixFilter
     {
         public BorderYPFilter()
         {
             int size = 3;
             kernel = new float[size, size];
             kernel[0, 0] = kernel[0, 1] = kernel[0, 2] = -1;
             kernel[1, 0] = kernel[1, 1] = kernel[1, 2] = 0;
             kernel[2, 0] = kernel[2, 1] = kernel[2, 2] = 1;
         }
     }
     class BorderXPFilter : MatrixFilter
     {
         public BorderXPFilter()
         {
             int size = 3;
             kernel = new float[size, size];
             kernel[0, 0] = kernel[1, 0] = kernel[2, 0] = -1;
             kernel[0, 1] = kernel[1, 1] = kernel[2, 1] = 0;
             kernel[0, 2] = kernel[1, 2] = kernel[2, 2] = 1;
         }
     }

     class SharpnessFilter : MatrixFilter
     {
         public SharpnessFilter()
         {
             int size = 3;
             kernel = new float[size, size];
             kernel[0, 0] = kernel[0, 2] = kernel[2, 0] = kernel[2, 2] = 0;
             kernel[0, 1] = kernel[1, 0] = kernel[1, 2] = kernel[2, 1] = -1;
             kernel[1, 1] = 5;
         }
     }
 //Не робит
     class GreyWorldFilter : Filters
     {
         int Avg;

         void GetZn(Bitmap sourceImage)
         {
             Rm = Bm = Gm = 0;
             int N = 0;
             Color tmpColor;
             for (int i = 0; i < sourceImage.Width; i++)
                 for (int j = 0; j < sourceImage.Height; j++)
                 {
                     tmpColor = sourceImage.GetPixel(i, j);
                     Rm += tmpColor.R;
                     Gm += tmpColor.G;
                     Bm += tmpColor.B;
                     N++;
                 }

             Rm = Rm / N;
             Gm = Gm / N;
             Bm = Bm / N;
             Avg = (Rm + Gm + Bm) / 3;
         }


         protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
         {
//             Color sourseColor = sourseImage.GetPixel(x, y);
//             GetZn(sourseImage);

             if (Rm == -1)
                 GetZn(sourseImage);

             Color sourseColor = sourseImage.GetPixel(x, y);
/*             double intensity = (Avg / Rm) * sourseColor.R + (Avg / Gm) * sourseColor.G + (Avg / Bm) * sourseColor.B;
             return Color.FromArgb(
                 Clamp((int)intensity, 0, 255),
                 Clamp((int)intensity, 0, 255),
                 Clamp((int)intensity, 0, 255));


           */              double resultR = (Avg / Rm) * sourseColor.R,
                                 resultG = (Avg / Gm) * sourseColor.G,
                                 resultB = (Avg / Bm) * sourseColor.B;

                          return Color.FromArgb(
                              Clamp((int)resultR, 0, 255),
                              Clamp((int)resultG, 0, 255),
                              Clamp((int)resultB, 0, 255));
            
         }  
     }
     class IdealityFilter : Filters
     {
         void GetMax (Bitmap sourseImage)
         {
             Rm = Bm = Gm = 0;
             Color C;
             for (int i = 0; i < sourseImage.Width; i++)
                 for (int j = 0; j < sourseImage.Height; j++)
                 {
                     C = sourseImage.GetPixel(i,j);
                     if (Rm < C.R)
                         Rm = C.R;
                     if (Gm < C.G)
                         Gm = C.G;
                     if (Bm < C.B)
                         Bm = C.B;
                 }
         }
         protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
         {
             if (Rm == -1)
                 GetMax(sourseImage);

             Color sourseColor = sourseImage.GetPixel(x, y);
             double intensity = (255 / Rm) * sourseColor.R + (255 / Gm) * sourseColor.G + (255 / Bm) * sourseColor.B;
             return Color.FromArgb(
                 Clamp((int)intensity, 0, 255),
                 Clamp((int)intensity, 0, 255),
                 Clamp((int)intensity, 0, 255));
         }


     }
//Rasshirenie
     class DelatiomFilter : MatrixFilter
     {
  //       private double MaxIntens;
  /*       public DelatiomFilter()
         {
             int size = 3;
             kernel = new float[size, size];
             kernel[0, 0] = kernel[0, 2] = kernel[2, 0] = kernel[2, 2] = 0;
             kernel[0, 1] = kernel[1, 0] = kernel[1, 1] = kernel[1, 2] = kernel[2, 1] = 1;
         }
*/
  /*      void GetMaxIntensive (Bitmap sourseImage, int x, int y) 
         {
             Color sourseColor;
             double mintens = 0, tmpintens;
             for (int _x = x-1; _x<=x+1; _x++)
                 for (int _y = x-1; _y<=y+1; _y++)
                 {
                     sourseColor = sourseImage.GetPixel(_x, _y);
                     tmpintens =  0.36*sourseColor.R + 0.53*sourseColor.G + 0.11*sourseColor.B;
                     if (mintens < tmpintens)
                         mintens = tmpintens;
                 }
             MaxIntens = mintens;
         }
*/

     }
}
