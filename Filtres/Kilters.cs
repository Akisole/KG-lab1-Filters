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
         

        protected abstract Color calculateNewPixelColor(Bitmap sourseImage, int x, int y);

        public virtual Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
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
     class GreyWorldFilter : Filters
     {
         double Rm = -1, Gm, Bm, Avg;
         void GetZn(Bitmap sourceImage)
         {
             Rm = Bm = Gm = 0.0;
             int N = sourceImage.Width * sourceImage.Height;
             Color tmpColor;
             for (int i = 0; i < sourceImage.Width; i++)
                 for (int j = 0; j < sourceImage.Height; j++)
                 {
                     tmpColor = sourceImage.GetPixel(i, j);
                     Rm += tmpColor.R;
                     Gm += tmpColor.G;
                     Bm += tmpColor.B;
                 }

             Rm = Rm / N;
             Gm = Gm / N;
             Bm = Bm / N;
             Avg = (Rm + Gm + Bm) / 3;
         }

         protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
         {
             if (Rm == -1)
                 GetZn(sourseImage);
             Color sourseColor = sourseImage.GetPixel(x, y);
             double resultR = (Avg / Rm) * sourseColor.R,
                    resultG = (Avg / Gm) * sourseColor.G,
                    resultB = (Avg / Bm) * sourseColor.B;

             return Color.FromArgb(
                    Clamp((int)resultR, 0, 255),
                    Clamp((int)resultG, 0, 255),
                    Clamp((int)resultB, 0, 255));
            
         }  
     }

     class MorfologyFilter : MatrixFilter
     {
         protected int iter;
         public MorfologyFilter()
         {
             iter = 0;
             int size = 3;
             kernel = new float[size, size];
             kernel[0, 0] = kernel[0, 2] = kernel[2, 0] = kernel[2, 2] = 0;
             kernel[0, 1] = kernel[1, 0] = kernel[1, 1] = kernel[1, 2] = kernel[2, 1] = 1;
         }
         protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
         {
             int radiusX = kernel.GetLength(0) / 2;
             int radiusY = kernel.GetLength(1) / 2;
             double minintens = 300, maxintens = -1, tmpintens;
             float resultR = 0, resultG = 0, resultB = 0;
             for (int l = -radiusY; l <= radiusY; l++)
                 for (int k = -radiusX; k <= radiusX; k++)
                 {
                     if (kernel[k + radiusX, l + radiusY] == 1)
                     {
                         int idX = Clamp(x + k, 0, sourseImage.Width - 1);
                         int idY = Clamp(y + l, 0, sourseImage.Height - 1);
                         Color neighborColor = sourseImage.GetPixel(idX, idY);
                         tmpintens = 0.36 * neighborColor.R + 0.53 * neighborColor.G + 0.11 * neighborColor.B;
                         if (iter == 1)
                         {
                             if (minintens > tmpintens)
                             {
                                 minintens = tmpintens;
                                 resultR = neighborColor.R;
                                 resultG = neighborColor.G;
                                 resultB = neighborColor.B;
                             }
                         }
                         if (iter == 2)
                         {
                             if (maxintens < tmpintens)
                             {
                                 maxintens = tmpintens;
                                 resultR = neighborColor.R;
                                 resultG = neighborColor.G;
                                 resultB = neighborColor.B;
                             }
                         }
                     }
                 }

             return Color.FromArgb(
                 Clamp((int)resultR, 0, 255),
                 Clamp((int)resultG, 0, 255),
                 Clamp((int)resultB, 0, 255));
         }
     }
     class OpeningFilter : MorfologyFilter
     {
         public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
         {
             Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
             Bitmap tmpImage = new Bitmap(sourceImage.Width, sourceImage.Height);
             iter = 1;
             for (int i = 0; i < sourceImage.Width; i++)
             {
                 worker.ReportProgress((int)((float)i / (tmpImage.Width*2) * 100));
                 
                 for (int j = 0; j < sourceImage.Height; j++)
                 {
                     tmpImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
                 }
             }
             iter = 2;
             for (int i = 0; i < tmpImage.Width; i++) 
             {
                worker.ReportProgress((int)((float)(i+tmpImage.Width) / (resultImage.Width*2) * 100));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < tmpImage.Height; j++)
                {
                    resultImage.SetPixel(i, j, calculateNewPixelColor(tmpImage, i, j));
             }
        }

             return resultImage;
         }
     }
     class ClosingFilter : MorfologyFilter
     {
         public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
         {
             Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
             Bitmap tmpImage = new Bitmap(sourceImage.Width, sourceImage.Height);
             iter = 2;
             for (int i = 0; i < sourceImage.Width; i++)
             {
                 worker.ReportProgress((int)((float)i / (tmpImage.Width * 2) * 100));

                 for (int j = 0; j < sourceImage.Height; j++)
                 {
                     tmpImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
                 }
             }
             iter = 1;
             for (int i = 0; i < tmpImage.Width; i++)
             {
                 worker.ReportProgress((int)((float)(i + tmpImage.Width) / (resultImage.Width * 2) * 100));
                 if (worker.CancellationPending)
                     return null;
                 for (int j = 0; j < tmpImage.Height; j++)
                 {
                     resultImage.SetPixel(i, j, calculateNewPixelColor(tmpImage, i, j));
                 }
             }

             return resultImage;
         }
     }
     class DelatiomFilter : MorfologyFilter
     {
         public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
         {
             Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
             iter = 2;
             for (int i = 0; i < sourceImage.Width; i++)
             {
                 worker.ReportProgress((int)((float)i / resultImage.Width * 100));

                 for (int j = 0; j < sourceImage.Height; j++)
                 {
                     resultImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
                 }
             }
             return resultImage;
         }
     }
     class ErosionFilter : MorfologyFilter
     {
         public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
         {
             Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
             iter = 1;
             for (int i = 0; i < sourceImage.Width; i++)
             {
                 worker.ReportProgress((int)((float)i / resultImage.Width * 100));

                 for (int j = 0; j < sourceImage.Height; j++)
                 {
                     resultImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
                 }
             }
             return resultImage;
         }
     }
//Ne robit
     class BlackHatFilter : MorfologyFilter
     {
         public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
         {
             Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
             Bitmap tmp1Image = new Bitmap(sourceImage.Width, sourceImage.Height);
             Bitmap tmp2Image = new Bitmap(sourceImage.Width, sourceImage.Height);
             double resultR, resultG, resultB;
             Color tmp2color, sourcecolor;
             iter = 1;
             for (int i = 0; i < sourceImage.Width; i++)
             {
                 worker.ReportProgress((int)((float)i / (tmp1Image.Width * 3) * 100));

                 for (int j = 0; j < sourceImage.Height; j++)
                 {
                     tmp1Image.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
                 }
             }
             iter = 2;
             for (int i = 0; i < tmp1Image.Width; i++)
             {
                 worker.ReportProgress((int)((float)(i + tmp1Image.Width) / (resultImage.Width * 3) * 100));
                 if (worker.CancellationPending)
                     return null;
                 for (int j = 0; j < tmp1Image.Height; j++)
                 {
                     tmp2Image.SetPixel(i, j, calculateNewPixelColor(tmp1Image, i, j));
                 }
             }
            
             for (int i = 0; i < tmp2Image.Width; i++)
             {
                 worker.ReportProgress((int)((float)(i + tmp1Image.Width*2) / (resultImage.Width * 3) * 100));
                 if (worker.CancellationPending)
                     return null;
                 for (int j = 0; j < tmp2Image.Height; j++)
                 {
                     tmp2color = tmp2Image.GetPixel(i, j);
                     sourcecolor = sourceImage.GetPixel(i, j);
                     resultR = tmp2color.R - sourcecolor.R;
                     resultB = tmp2color.B - sourcecolor.B;
                     resultG = tmp2color.G - sourcecolor.G;
                     resultImage.SetPixel(i, j, Color.FromArgb(Clamp((int)resultR, 0, 255), Clamp((int)resultG, 0, 255), Clamp((int)resultB, 0, 255)));
                 }
             }

             return resultImage;
         }
     }

     class MedianFilter : MatrixFilter
     {

         protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
         {

             double[] mas = new double[9];
             float resultR = 0, resultG = 0, resultB = 0;
             for (int l = -1; l <= 1; l++)
                 for (int k = -1; k <= 1; k++)
                 {
                     int idX = Clamp(x + k, 0, sourseImage.Width - 1);
                     int idY = Clamp(y + l, 0, sourseImage.Height - 1);
                     Color neighborColor = sourseImage.GetPixel(idX, idY);
                     double tmpintens = 0.36 * neighborColor.R + 0.53 * neighborColor.G + 0.11 * neighborColor.B;
                     mas[(l + 1) * 3 + (k + 1)] = tmpintens;
                 }
             Array.Sort(mas);
             double median = mas[4];
             for (int l = -1; l <= 1; l++)
                 for (int k = -1; k <= 1; k++)
                 {
                     int idX = Clamp(x + k, 0, sourseImage.Width - 1);
                     int idY = Clamp(y + l, 0, sourseImage.Height - 1);
                     Color neighborColor = sourseImage.GetPixel(idX, idY);
                     double tmpintens = 0.36 * neighborColor.R + 0.53 * neighborColor.G + 0.11 * neighborColor.B;
                     if (tmpintens == median)
                     {
                         resultR = neighborColor.R;
                         resultG = neighborColor.G;
                         resultB = neighborColor.B;
                     }
                         
                 }
             return Color.FromArgb(
                 Clamp((int)resultR, 0, 255),
                 Clamp((int)resultG, 0, 255),
                 Clamp((int)resultB, 0, 255));
         }

     }
}
         