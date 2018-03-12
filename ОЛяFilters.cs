using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace KG
{
    public abstract class Filters
    {
        public float maxbr = 0.0f;       public float minbr = 1.0f;
        public float avg = 1.0f;         public float currbrR = 1.0f;
        public float currbrG = 1.0f;     public float currbrB = 1.0f;
        private void max_min(Bitmap img) //определяеются максимальное и минимальное значение цвета
        {
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    //float pixcolor = img.GetPixel(i, j).GetBrightness();
                    if (maxbr < img.GetPixel(i, j).GetBrightness()) maxbr = img.GetPixel(i, j).GetBrightness();
                    if (minbr > img.GetPixel(i, j).GetBrightness()) minbr = img.GetPixel(i, j).GetBrightness();
                 }
             }
         }
        private void curr_brightness(Bitmap img) //считает средние яркости
        {
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    currbrR += img.GetPixel(i, j).R;
                    currbrG += img.GetPixel(i, j).G;
                    currbrB += img.GetPixel(i, j).B;
                }
            }
            int n = img.Width * img.Height;
            currbrR /= n;
            currbrG /= n;
            currbrB /= n;
            avg = (currbrR + currbrG + currbrB) / 3;
        }
        protected abstract Color calculateNewPixelColor(Bitmap sourceImage, int x, int y);
        virtual public Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
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

    class InvertFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            Color resultColor = Color.FromArgb(255 - sourceColor.R, 255 - sourceColor.G, 255 - sourceColor.B);
            return resultColor;
        }
    }
    class GreyScaleFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y); //получаем пиксель исходного
            int Intensity = (int)(sourceColor.R * 0.36F + sourceColor.G * 0.53F + sourceColor.B * 0.11F); //вычисляем интенсивность
            Color resultColor = Color.FromArgb(Intensity, Intensity, Intensity); //переводим цвет в ч/б
            return resultColor; //выводим картинку
        }
    }
    class Sepia : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y); //получаем пиксель исходного
            float Intensity = (sourceColor.R * 0.36F + sourceColor.G * 0.53F + sourceColor.B * 0.11F); //вычисляем интенсивность
            double k = 50;
            int R = (int)(Intensity + 2 * k);
            int G = (int)(Intensity + 0.5 * k);
            int B = (int)(Intensity - 1 * k);
            Color resultColor = Color.FromArgb(Clamp(R, 0, 255), Clamp(G, 0, 255), Clamp(B, 0, 255)); //переводим цвет в коричневый
            return resultColor; //выводим картинку
        }
    }
    class Brightness : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            int k = 50;
            Color resultColor = Color.FromArgb(Clamp(sourceColor.R + k, 0, 255), Clamp(sourceColor.G + k, 0, 255), Clamp(sourceColor.B + k, 0, 255));
            return resultColor;
        }
    }
    class Waveseffect :Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int k =(int)(x + 20 * Math.Sin(2 * Math.PI * y / 30));
            int l = y;
            return sourceImage.GetPixel(Clamp(k,0,sourceImage.Width - 1),Clamp(l,0, sourceImage.Height - 1));
        }
    }
    class Windoweffect : Filters
    {
        private Random rand;
        public Windoweffect()
        {
            rand = new Random();
        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int k = (int)(x + (rand.NextDouble() - 0.5) * 10);
            int l = (int)(y + (rand.NextDouble() - 0.5) * 10);
            return sourceImage.GetPixel(Clamp(k, 0, sourceImage.Width - 1), Clamp(l, 0, sourceImage.Height - 1));
        }
    }
    class linearstretching:Filters 
    {
         protected override Color calculateNewPixelColor(Bitmap img, int x, int y)
         {
             float br;
             float result;
             br = img.GetPixel(x, y).GetBrightness();
             result = recalculation(br, maxbr, minbr);
             Color resultColor = Color.FromArgb(Clamp((int)result, 0, 255), Clamp((int)result, 0, 255), Clamp((int)result, 0, 255));
             return resultColor;
         }
         private float recalculation(float x, float max, float min) //функция для рассчета оттенка изображения
          //в качестве парамтров передаются оттенок пиксела исходного изображения,
          //максимальное и минимальное значения оттенка
         {
            float y = 0;
            y = (x - min) * 255 / (max - min);
            return y;
         }
           
     }
    class grayworld : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap img, int x, int y)
        {
            Color imCol = img.GetPixel(x,y);
            double R = imCol.R * avg / currbrR;
            double G = imCol.G * avg / currbrG;
            double B = imCol.B * avg / currbrB;
            Color resColor=Color.FromArgb(Clamp((int)R, 0, 255), Clamp((int)G, 0, 255), Clamp((int)B, 0, 255));
            return resColor;
        }
        public override Bitmap processImage(Bitmap img, BackgroundWorker worker)
        {
            Bitmap result = new Bitmap(img.Width, img.Height);
            for (int i = 0; i < img.Width; i++)
            {
                worker.ReportProgress((int)((float)i / result.Width *100));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < img.Height; j++)
                {
                    result.SetPixel(i, j, calculateNewPixelColor(img, i, j)); 
                }
            }
            return result;
        }
    }

    class MatrixFilter : Filters
    {
        protected float[,] kernel = null;
        protected MatrixFilter()
        { }
        public MatrixFilter(float[,] kernel)
        {
            this.kernel = kernel;
        }
        public void arrayhandling(float[,] mas, int sizemas)
        {
            kernel = new float[sizemas, sizemas];
            for (int i = 0; i < sizemas; i++)
                for (int j = 0; j < sizemas; j++)
                    kernel[i, j] = mas[i, j];
        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;
            float resultR = 0;
            float resultG = 0;
            float resultB = 0;
            for (int l = -radiusY; l <= radiusY; l++)
                for (int k = -radiusX; k <= radiusX; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);
                    resultR += neighborColor.R * kernel[k + radiusX, l + radiusY];
                    resultG += neighborColor.G * kernel[k + radiusX, l + radiusY];
                    resultB += neighborColor.B * kernel[k + radiusX, l + radiusY];
                }
            return Color.FromArgb(Clamp((int)resultR, 0, 255), Clamp((int)resultG, 0, 255), Clamp((int)resultB, 0, 255));
        }
        public Color calculateNewPixelColormax(Bitmap sourceImage, int x, int y)
        {
            Color maximum = Color.FromArgb(0, 0, 0);
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;
            for (int j = -radiusY; j <= radiusY; j++)
            {
                for (int i = -radiusX; i <= radiusX; i++)
                {
                    int idX = Clamp(x + i, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + j, 0, sourceImage.Height - 1);
                    Color newCol = sourceImage.GetPixel(idX, idY);
                    if ((kernel[i + radiusX, j + radiusY] != 0) && (Math.Sqrt(newCol.R * newCol.R + newCol.G * newCol.G + newCol.B * newCol.B) > Math.Sqrt(maximum.R * maximum.R + maximum.G * maximum.G + maximum.B * maximum.B)))
                        maximum = newCol;
                }
            }
            return maximum;
        }
        public Color calculateNewPixelColormin(Bitmap sourceImage, int x, int y)
        {
            Color minimum = Color.FromArgb(255, 255, 255);
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;
            for (int j = -radiusY; j <= radiusY; j++)
            {
                for (int i = -radiusX; i <= radiusX; i++)
                {
                    int idX = Clamp(x + i, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + j, 0, sourceImage.Height - 1);
                    Color newCol = sourceImage.GetPixel(idX, idY);
                    if ((kernel[i + radiusX, j + radiusY] != 0) && (Math.Sqrt(newCol.R * newCol.R + newCol.G * newCol.G + newCol.B * newCol.B) < Math.Sqrt(minimum.R * minimum.R + minimum.G * minimum.G + minimum.B * minimum.B)))
                        minimum = newCol;
                }
            }
            return minimum;
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
        public void createGaussianKernel(int radius, float sigma)
        {
            int size = 2 * radius + 1;
            kernel = new float[size, size];
            float norm = 0;
            for (int i = -radius; i < radius; i++)
                for (int j = -radius; j < radius; j++)
                {
                    kernel[i + radius, j + radius] = (float)(Math.Exp(-(i * i + j * j) / (sigma * sigma)));
                    norm += kernel[i + radius, j + radius];
                }
            for (int i = 0; i < size; i++) //нормировка
                for (int j = 0; j < size; j++)
                    kernel[i, j] /= norm;
        }
        public GaussianFilter()
        {
            createGaussianKernel(3, 2);
        }
    }
    class FilterSobely : MatrixFilter
    {
        public FilterSobely() { }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            float rR1 = 0;  float rR2 = 0;
            float rG1 = 0;  float rG2 = 0;
            float rB1 = 0;  float rB2 = 0;
            kernel = new float[3, 3]{{-1,-2,-1},{0,0,0},{1,2,1}};
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;
            for (int i = -radiusY; i <= radiusY; i++)
                for (int j = -radiusX; j <= radiusX; j++)
                {
                    int idX = Clamp(x + j, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + i, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);
                    rR1 += neighborColor.R * kernel[j + radiusX, i + radiusY];
                    rG1 += neighborColor.G * kernel[j + radiusX, i + radiusY];
                    rB1 += neighborColor.B * kernel[j + radiusX, i + radiusY];
                }
            kernel = new float[3, 3]{{-1,0,1},{-2,0,2},{-1,0,1}};
            for (int i = -radiusY; i <= radiusY; i++)
                for (int j = -radiusX; j <= radiusX; j++)
                {
                    int idX = Clamp(x + j, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + i, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);
                    rR2 += neighborColor.R * kernel[j + radiusX, i + radiusY];
                    rG2 += neighborColor.G * kernel[j + radiusX, i + radiusY];
                    rB2 += neighborColor.B * kernel[j + radiusX, i + radiusY];
                }
            int sum = (int)Math.Sqrt(rR1 * rR1 + rR2 * rR2 + rG1 * rG1 + rG2 * rG2 + rB1 * rB1 + rB2 * rB2)/3;
            return Color.FromArgb(Clamp((int)sum, 0, 255), Clamp((int)sum, 0, 255), Clamp((int)sum, 0, 255));
        }
    }
    class Definition : MatrixFilter
    {
        public Definition() { }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            float rR = 0; float rB = 0; float rG = 0;
            kernel = new float[3, 3] { { 0, -1, 0 }, { -1, 5, -1 }, { 0, -1, 0 } };
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;
            for (int i = -radiusY; i <= radiusY; i++)
                for (int j = -radiusX; j <= radiusX; j++)
                {
                    int idX = Clamp(x + j, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + i, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);
                    rR += neighborColor.R * kernel[j + radiusX, i + radiusY];
                    rG += neighborColor.G * kernel[j + radiusX, i + radiusY];
                    rB += neighborColor.B * kernel[j + radiusX, i + radiusY];
                }
            return Color.FromArgb(Clamp((int)rR, 0, 255), Clamp((int)rG, 0, 255), Clamp((int)rB, 0, 255));
        }
    }
    class MotionBlur : MatrixFilter
    {
        public MotionBlur() { }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            float rR = 0; float rB = 0; float rG = 0;
            kernel = new float[5, 5] { { 1, 0, 0, 0, 0 }, { 0, 1, 0, 0, 0 }, { 0, 0, 1, 0, 0 }, { 0, 0, 0, 1, 0 }, { 0, 0, 0, 0, 1 } };
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;
            for (int i = -radiusY; i <= radiusY; i++)
                for (int j = -radiusX; j <= radiusX; j++)
                {
                    int idX = Clamp(x + j, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + i, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);
                    rR += neighborColor.R * kernel[j + radiusX, i + radiusY];
                    rG += neighborColor.G * kernel[j + radiusX, i + radiusY];
                    rB += neighborColor.B * kernel[j + radiusX, i + radiusY];
                }
            return Color.FromArgb(Clamp((int)rR/5, 0, 255), Clamp((int)rG/5, 0, 255), Clamp((int)rB/5, 0, 255));
        }
    }
    class Borderselection : MatrixFilter
    {
        public Borderselection() { }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            float rR1 = 0;  float rR2 = 0;
            float rG1 = 0;  float rG2 = 0;
            float rB1 = 0;  float rB2 = 0;
            kernel = new float[3, 3] { { 3, 10, 3 }, { 0, 0, 0 }, { -3, -10, -3 } };
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;
            for (int i = -radiusY; i <= radiusY; i++)
                for (int j = -radiusX; j <= radiusX; j++)
                {
                    int idX = Clamp(x + j, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + i, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);
                    rR1 += neighborColor.R * kernel[j + radiusX, i + radiusY];
                    rG1 += neighborColor.G * kernel[j + radiusX, i + radiusY];
                    rB1 += neighborColor.B * kernel[j + radiusX, i + radiusY];
                }
            kernel = new float[3, 3] { { 3, 0, -3 }, { 10, 0, -10 }, { 3, 0, -3 } };
            for (int i = -radiusY; i <= radiusY; i++)
                for (int j = -radiusX; j <= radiusX; j++)
                {
                    int idX = Clamp(x + j, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + i, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);
                    rR2 += neighborColor.R * kernel[j + radiusX, i + radiusY];
                    rG2 += neighborColor.G * kernel[j + radiusX, i + radiusY];
                    rB2 += neighborColor.B * kernel[j + radiusX, i + radiusY];
                }
            int sum = (int)Math.Sqrt(rR1 * rR1 + rR2 * rR2 + rG1 * rG1 + rG2 * rG2 + rB1 * rB1 + rB2 * rB2)/3;
            return Color.FromArgb(Clamp((int)sum, 0, 255), Clamp((int)sum, 0, 255), Clamp((int)sum, 0, 255));
        }
    }   
    class Dilation : MatrixFilter
    {
        public Dilation()
        {
            kernel = new float[3, 3] { { 0, 1, 0 }, { 1, 1, 1 }, { 0, 1, 0 } };
        }
        protected override Color calculateNewPixelColor(Bitmap img, int x, int y)
        {
            Color maximum = Color.FromArgb(0, 0, 0);
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;
            for (int j = -radiusY; j <= radiusY; j++)
            {
                for (int i = -radiusX; i <= radiusX; i++)
                {
                    int idX = Clamp(x + i, 0, img.Width-1);
                    int idY = Clamp(y + j, 0, img.Height-1);
                    Color newCol = img.GetPixel(idX, idY);
                    if ((kernel[i+radiusX, j+radiusY]!=0)&&(Math.Sqrt(newCol.R*newCol.R+newCol.G*newCol.G+newCol.B*newCol.B)>Math.Sqrt(maximum.R*maximum.R+maximum.G*maximum.G+maximum.B*maximum.B)))
                        maximum = newCol;
                }
            }
            return maximum;
        }
    }
    class Erosian: MatrixFilter
    {
        public Erosian()
        {
            kernel = new float[3, 3] { { 0, 1, 0 }, { 1, 1, 1 }, { 0, 1, 0 } };
        }
        protected override Color calculateNewPixelColor(Bitmap img, int x, int y)
        {
            Color minimum = Color.FromArgb(255, 255, 255);
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;
            for (int j = -radiusY; j <= radiusY; j++)
            {
                for (int i = -radiusX; i <= radiusX; i++)
                {
                    int idX = Clamp(x + i, 0, img.Width-1);
                    int idY = Clamp(y + j, 0, img.Height-1);
                    Color newCol = img.GetPixel(idX, idY);
                    if ((kernel[i+radiusX, j+radiusY]!=0)&&(Math.Sqrt(newCol.R*newCol.R+newCol.G*newCol.G+newCol.B*newCol.B)<Math.Sqrt(minimum.R*minimum.R+minimum.G*minimum.G+minimum.B*minimum.B)))
                        minimum = newCol;
                }
            }
            return minimum;
        }
    }
    class Opening: MatrixFilter
    {
        public Opening()
        {
            kernel = new float[3, 3] { { 0, 1, 0 }, { 1, 1, 1 }, { 0, 1, 0 } };
        }
        public override Bitmap processImage(Bitmap img, BackgroundWorker worker) 
        {
            Bitmap resultImage = new Bitmap(img.Width, img.Height);
            for (int i = 0; i < img.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resultImage.Width * 50));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < img.Height; j++)
                {
                    resultImage.SetPixel(i, j, calculateNewPixelColormin(img, i, j)); //применяем эрозию
                }
            }
            Bitmap resultImage1 = new Bitmap(img.Width, img.Height); 
            for (int i = 0; i < img.Width; i++)
            {
                worker.ReportProgress((int)(50+(float)i / resultImage.Width * 50));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < img.Height; j++)
                {
                    resultImage1.SetPixel(i, j, calculateNewPixelColormax(resultImage, i, j));  //к эрозии применяем дилатацию
                }
            }
            return resultImage1;
        }
    }
    class Closing: MatrixFilter
    {
        public Closing()
        {
            kernel = new float[3, 3] { { 0, 1, 0 }, { 1, 1, 1 }, { 0, 1, 0 } };
        }
        public override Bitmap processImage(Bitmap img, BackgroundWorker worker)
        {
            Bitmap resultImage = new Bitmap(img.Width, img.Height);
            for (int i = 0; i < img.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resultImage.Width * 50));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < img.Height; j++)
                {
                    resultImage.SetPixel(i, j, calculateNewPixelColormax(img, i, j)); //применяем делатацию
                }
            }
            Bitmap resultImage1 = new Bitmap(img.Width, img.Height);
            for (int i = 0; i < img.Width; i++)
            {
                worker.ReportProgress((int)(50+(float)i / resultImage.Width * 50));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < img.Height; j++)
                {
                    resultImage1.SetPixel(i, j, calculateNewPixelColormin(resultImage, i, j)); //к результату применяем эрозию
                }
            }
            return resultImage1;
        }
    }
    class medianfilter : MatrixFilter
    {
        public medianfilter() 
        {
            int sizeX = 9;
            int sizeY = 9;
            kernel = new float[sizeX, sizeY];
        }
        protected override Color calculateNewPixelColor(Bitmap img, int x, int y)
        {
            int radX = kernel.GetLength(0)/ 2;
            int radY = kernel.GetLength(1)/ 2;
            double[] mas = new double[(kernel.GetLength(0)) * (kernel.GetLength(1))];
            int sh = 0;
            for (int i = -radY; i <= radY; i++) //формируем массив из значений цветов
            {
                for (int j = -radX; j <= radX; j++)
                {
                    int idX = Clamp(x + j, 0, img.Width - 1);
                    int idY = Clamp(y + i, 0, img.Height - 1);
                    mas[sh] = (img.GetPixel(idX, idY)).ToArgb();   //заполняет массив значениями цветов
                    sh++;
                }
            }
            mas=Sort(mas,(kernel.GetLength(0)) * (kernel.GetLength(1))); //сортируем массив
            double madiana = mas[(kernel.GetLength(0)) * (kernel.GetLength(1)) / 2]; // создание медианы
            sh = 0; //обнулили счетсчик
            Color resColor = Color.FromArgb(0, 50, 0);
            for (int i = -radY; i <= radY; i++)
            {
                for (int j = -radX; j <= radX; j++)
                {
                    int idX = Clamp(x + j, 0, img.Width - 1);
                    int idY = Clamp(y + i, 0, img.Height - 1);
                    Color newColor = img.GetPixel(idX, idY);
                    if (madiana == newColor.ToArgb())      //число равно медиане, выкидываем как результат
                        resColor = newColor;
                }
            }
            return resColor;
        }
        double[] Sort(double[] mas, int sizemas)
        {
            double m = 0;
            int pos;
            for (int i = 1; i < sizemas; i++)
            {
                m = mas[i];
                pos = i - 1;
                while (pos >= 0 && mas[pos] > m)
                {
                    mas[pos + 1] = mas[pos];
                    pos = pos - 1;
                }
                mas[pos + 1] = m;
            }
            return mas;
        }
    }
    class grad : MatrixFilter
    {
        //применить сначала эрозию отдельно потом дилатацию, а потом на результатах эрозию
        public grad()
        {
            kernel = new float[3, 3] { { 0, 1, 0 }, { 1, 1, 1 }, { 0, 1, 0 } };
        }
        public override Bitmap processImage(Bitmap img, BackgroundWorker worker)
        {
            Bitmap resErosian = new Bitmap(img.Width, img.Height);
            for (int i = 0; i < img.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resErosian.Width * 33));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < img.Height; j++)
                {
                    resErosian.SetPixel(i, j, calculateNewPixelColormin(img, i, j)); //применяем эрозию
                }
            }
            Bitmap resDelation = new Bitmap(img.Width, img.Height);
            for (int i = 0; i < img.Width; i++)
            {
                worker.ReportProgress((int)(33+(float)i / resDelation.Width * 33));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < img.Height; j++)
                {
                    resDelation.SetPixel(i, j, calculateNewPixelColormax(img, i, j)); //применяем делатация
                }
            }
            for (int i = 0; i < img.Width; i++)
            {
                worker.ReportProgress((int)(66 + (float)i / resDelation.Width * 34));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < img.Height; j++)
                {
                    resDelation.SetPixel(i, j, calculateNewPixelColormin(resErosian, i, j)); //применяем к дeлатации эрозию
                }
            }
            return resDelation;
        }
    }
    class tophat : MatrixFilter
    {
        //делаем закрытие и применяем эрозию
        public tophat()
        {
            kernel = new float[3, 3] { { 0, 1, 0 }, { 1, 1, 1 }, { 0, 1, 0 } };
        }
        public override Bitmap processImage(Bitmap img, BackgroundWorker worker)
        {
            Bitmap resultDel = new Bitmap(img.Width, img.Height);
            for (int i = 0; i < img.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resultDel.Width * 33));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < img.Height; j++)
                {
                    resultDel.SetPixel(i, j, calculateNewPixelColormax(img, i, j)); //применяем делатацию
                }
            }
            Bitmap resultClose = new Bitmap(img.Width, img.Height);
            for (int i = 0; i < img.Width; i++)
            {
                worker.ReportProgress((int)(33 + (float)i / resultDel.Width * 33));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < img.Height; j++)
                {
                    resultClose.SetPixel(i, j, calculateNewPixelColormin(resultDel, i, j)); //к результату применяем эрозию
                }
            }
            Bitmap resulttop = new Bitmap(img.Width, img.Height);
            for (int i = 0; i < img.Width; i++)
            {
                worker.ReportProgress((int)(66 + (float)i / resultDel.Width * 34));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < img.Height; j++)
                {
                    resulttop.SetPixel(i, j, calculateNewPixelColormin(resultClose, i, j));
                }
            }
            return resulttop;
        }
    }
}
