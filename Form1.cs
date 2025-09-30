using OpenCvSharp;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenCvSharp.Extensions;
using WebCamLib;
using System.Transactions;

namespace ImageProcessing
{
    public partial class Form1 : Form
    {
        private VideoCapture capture;
        private Thread cameraThread;
        Bitmap loaded, processed, background;


        private volatile bool cameraRunning = false;
        private System.Windows.Forms.Timer processingTimer;
        //Device[] devices = DeviceManager.GetAllDevices();

        private enum ProcessingMode
        {
            None,
            BasicCopy,
            Greyscale,
            Sepia,
            Subtraction,
            Histogram,
            Inversion
        }

        private ProcessingMode currentMode = ProcessingMode.None;
        public Form1()
        {
            InitializeComponent();
        }

        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cameraRunning)
            {
                cameraRunning = false;

                if (cameraThread != null && cameraThread.IsAlive)
                {
                    cameraThread.Join(500); // wait for thread to stop
                }

                capture?.Release();
                capture?.Dispose();
                capture = null;
                processingTimer?.Stop();
            }

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select an Image";
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    loaded = new Bitmap(openFileDialog.FileName);
                    pictureBox1.Image = loaded;
                }
            }
        }

        private void basicCopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = null;
            if (cameraRunning)
            {
                currentMode = ProcessingMode.BasicCopy;
                return;
            }
            processed = new Bitmap(loaded.Width, loaded.Height);
            for (int y = 0; y < loaded.Height; y++)
            {
                for (int x = 0; x < loaded.Width; x++)
                {
                    Color pixelColor = loaded.GetPixel(x, y);
                    processed.SetPixel(x, y, pixelColor);
                }
            }

            pictureBox2.Image = processed;
        }

        private void greyscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = null;
            if (cameraRunning)
            {
                currentMode = ProcessingMode.Greyscale;
                return;
            }
            processed = new Bitmap(loaded.Width, loaded.Height);
            for (int y = 0; y < loaded.Height; y++)
            {
                for (int x = 0; x < loaded.Width; x++)
                {
                    Color pixelColor = loaded.GetPixel(x, y);
                    int grey = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    Color color = Color.FromArgb(grey, grey, grey);
                    processed.SetPixel(x, y, color);
                }
            }

            pictureBox2.Image = processed;
        }

        private void colorInversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = null;
            if (cameraRunning)
            {
                currentMode = ProcessingMode.Inversion;
                return;
            }
            processed = new Bitmap(loaded.Width, loaded.Height);
            for (int y = 0; y < loaded.Height; y++)
            {
                for (int x = 0; x < loaded.Width; x++)
                {
                    Color pixelColor = loaded.GetPixel(x, y);

                    Color color = Color.FromArgb(255 - pixelColor.R, 255 - pixelColor.G, 255 - pixelColor.B);
                    processed.SetPixel(x, y, color);
                }
            }

            pictureBox2.Image = processed;
        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = null;
            if (cameraRunning)
            {
                currentMode = ProcessingMode.Sepia;
                return;
            }
            processed = new Bitmap(loaded.Width, loaded.Height);
            for (int y = 0; y < loaded.Height; y++)
            {
                for (int x = 0; x < loaded.Width; x++)
                {
                    Color pixelColor = loaded.GetPixel(x, y);

                    int r = pixelColor.R;
                    int g = pixelColor.G;
                    int b = pixelColor.B;

                    int newR = (int)(0.393 * r + 0.769 * g + 0.189 * b);
                    int newG = (int)(0.349 * r + 0.686 * g + 0.168 * b);
                    int newB = (int)(0.272 * r + 0.534 * g + 0.131 * b);

                    newR = Math.Min(255, newR);
                    newG = Math.Min(255, newG);
                    newB = Math.Min(255, newB);

                    Color sepiaColor = Color.FromArgb(newR, newG, newB);
                    processed.SetPixel(x, y, sepiaColor);
                }
            }

            pictureBox2.Image = processed;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cameraRunning)
            {
                currentMode = ProcessingMode.Histogram;
                return;
            }
            Image original = pictureBox1.Image;
            Bitmap greyImage = new Bitmap(original.Width, original.Height);
            for (int x = 0; x < original.Width; x++)
            {
                for (int y = 0; y < original.Height; y++)
                {
                    Color pixelColor = ((Bitmap)original).GetPixel(x, y);
                    int greyValue = (int)(pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    Color greyColor = Color.FromArgb(greyValue, greyValue, greyValue);
                    greyImage.SetPixel(x, y, greyColor);
                }
            }
            pictureBox2.Image = greyImage;

            int[] levels = new int[256];
            for (int i = 0; i < greyImage.Width; i++)
            {
                for (int j = 0; j < greyImage.Height; j++)
                {
                    Color pixelColor = greyImage.GetPixel(i, j);
                    levels[pixelColor.R]++;
                }
            }

            DrawHistogram(levels);
        }

        private void DrawHistogram(int[] levels)
        {
            int histWidth = 256;
            int histHeight = 100;
            Bitmap histImage = new Bitmap(histWidth, histHeight);
            int maxLevel = levels.Max();
            using (Graphics g = Graphics.FromImage(histImage))
            {
                g.Clear(Color.White);
                for (int i = 0; i < levels.Length; i++)
                {
                    int barHeight = (int)((levels[i] / (float)maxLevel) * histHeight);
                    g.DrawLine(Pens.Black, i, histHeight, i, histHeight - barHeight);
                }
            }
            pictureBox3.Image = histImage;
        }

        private void backgroundImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cameraRunning)
            {
                if (currentMode != ProcessingMode.Subtraction)
                    currentMode = ProcessingMode.None;
            }
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select an Image";
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    background = new Bitmap(openFileDialog.FileName);
                    pictureBox2.Image = background;
                }
            }
        }

        private void subtractionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cameraRunning)
            {
                currentMode = ProcessingMode.Subtraction;
                return;
            }
            if (pictureBox1.Image == null || pictureBox2.Image == null) return;
            Bitmap foreground = new Bitmap(pictureBox1.Image);
            Bitmap background = new Bitmap(pictureBox2.Image);
            int width = Math.Min(foreground.Width, background.Width);
            int height = Math.Min(foreground.Height, background.Height);
            Bitmap result = new Bitmap(width, height);


            int greenThreshold = 100;
            int greenDifference = 60;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Color fg = foreground.GetPixel(i, j);
                    if (fg.G > greenThreshold && fg.G - fg.R > greenDifference && fg.G - fg.B > greenDifference)
                    {
                        result.SetPixel(i, j, background.GetPixel(i, j));
                    }
                    else
                    {
                        result.SetPixel(i, j, fg);
                    }
                }
            }
            pictureBox3.Image = result;
        }

        private void webcamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Device[] devices = DeviceManager.GetAllDevices();
            //Device device = devices[0];
            //device.ShowWindow(pictureBox1);
            //device.Sendmessage();


            //IDataObject data;
            //Image bmap;
            //Device d = DeviceManager.GetDevice(0);
            //d.Sendmessage();
            //data = Clipboard.GetDataObject();
            //bmap = (Image)(data.GetData("System.Drawing.Bitmap", true));
            //Bitmap b = new Bitmap(bmap);

            //pictureBox3.Image = b;

            if (!cameraRunning)
            {
                capture = new VideoCapture(0);
                cameraRunning = true;
                cameraThread = new Thread(CaptureCamera);
                cameraThread.IsBackground = true;
                cameraThread.Start();

                StartProcessingTimer();
            }
        }

        private void StartCamera()
        {
            capture = new VideoCapture(0); // 0 = default camera
            if (!capture.IsOpened())
            {
                MessageBox.Show("Webcam not found!");
                return;
            }

            cameraRunning = true;
            cameraThread = new Thread(new ThreadStart(CaptureCamera));
            cameraThread.Start();
        }
        private void StopCamera()
        {
            cameraRunning = false;
            processingTimer?.Stop();
            capture?.Release();
            capture?.Dispose();
        }
        private void CaptureCamera()
        {
            while (cameraRunning)
            {
                using (Mat frame = capture.RetrieveMat())
                {
                    if (!frame.Empty())
                    {
                        Bitmap bitmap = BitmapConverter.ToBitmap(frame);

                        // keep the most recent frame
                        lock (this)
                        {
                            loaded?.Dispose();
                            loaded = (Bitmap)bitmap.Clone();
                        }

                        // show preview
                        pictureBox1.Invoke((MethodInvoker)(() =>
                        {
                            pictureBox1.Image?.Dispose();
                            pictureBox1.Image = (Bitmap)bitmap.Clone();
                        }));
                    }
                }
            }
        }

        private void StartProcessingTimer()
        {
            processingTimer = new System.Windows.Forms.Timer();
            processingTimer.Interval = 33; // ~30 fps
            processingTimer.Tick += (s, e) => ProcessFrame();
            processingTimer.Start();
        }

        private void ProcessFrame()
        {
            if (loaded == null) return;

            Bitmap input;
            lock (this)
            {
                input = (Bitmap)loaded.Clone();
            }

            switch (currentMode)
            {
                case ProcessingMode.BasicCopy:
                    processed = MakeCopy(input);
                    break;

                case ProcessingMode.Greyscale:
                    processed = ApplyGreyscale(input);
                    break;

                case ProcessingMode.Sepia:
                    processed = ApplySepia(input);
                    break;
                case ProcessingMode.Histogram:
                    processed = ApplyGreyscale(input);
                    ApplyHistogram(processed);
                    break;
                case ProcessingMode.Inversion:
                    processed = ApplyInversion(input);
                    break;
                case ProcessingMode.Subtraction:
                    if (background == null)
                    {
                        // no background image loaded
                        processed = null;
                        break;
                    }
                    processed = ApplySubtraction(input, background);
                    break;
                default:
                    break;
            }

            if (processed != null)
            {
                if (currentMode != ProcessingMode.Subtraction)
                {
                    pictureBox2.Invoke((MethodInvoker)(() =>
                    {
                        pictureBox2.Image?.Dispose();
                        pictureBox2.Image = processed;
                    }));
                }
                else
                {
                    pictureBox3.Invoke((MethodInvoker)(() =>
                    {
                        pictureBox3.Image?.Dispose();
                        pictureBox3.Image = processed;
                    }));
                }
            }

            input.Dispose();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            cameraRunning = false;     // stop while loop

            // wait for camera thread to finish
            if (cameraThread != null && cameraThread.IsAlive)
            {
                cameraThread.Join(500); // wait max 500ms
            }

            capture?.Release();
            capture?.Dispose();
        }



        private Bitmap MakeCopy(Bitmap loaded)
        {
            Bitmap processed = new Bitmap(loaded.Width, loaded.Height);

            for (int y = 0; y < loaded.Height; y++)
            {
                for (int x = 0; x < loaded.Width; x++)
                {
                    Color pixelColor = loaded.GetPixel(x, y);
                    processed.SetPixel(x, y, pixelColor);
                }
            }

            return processed;
        }

        private Bitmap ApplyGreyscale(Bitmap loaded)
        {
            // Convert Bitmap to Mat
            Mat src = loaded.ToMat();

            // Destination Mat
            Mat gray = new Mat();

            // Convert to grayscale using OpenCV
            Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);

            // Convert back to Bitmap for PictureBox display
            return gray.ToBitmap();
        }

        private Bitmap ApplySepia(Bitmap loaded)
        {
            Mat src = OpenCvSharp.Extensions.BitmapConverter.ToMat(loaded);

            // Sepia filter kernel (3x3)
            float[,] sepiaKernel =
            {
                { 0.272f, 0.534f, 0.131f },
                { 0.349f, 0.686f, 0.168f },
                { 0.393f, 0.769f, 0.189f }
            };

            // Create a 3x3 Mat for kernel
            Mat kernel = Mat.FromArray(sepiaKernel);
            // Convert source to float for transform
            Mat srcFloat = new Mat();
            src.ConvertTo(srcFloat, MatType.CV_32F);

            // Apply sepia transform
            Mat sepia = new Mat();
            Cv2.Transform(srcFloat, sepia, kernel);

            // Convert back to 8-bit
            sepia.ConvertTo(sepia, MatType.CV_8U);

            // Convert Mat - Bitmap
            return OpenCvSharp.Extensions.BitmapConverter.ToBitmap(sepia);
        }
        private void ApplyHistogram(Bitmap greyed)
        {
            // 1. Convert to grayscale is done already


            // 2. Count pixel levels
            int[] levels = new int[256];
            for (int x = 0; x < greyed.Width; x++)
            {
                for (int y = 0; y < greyed.Height; y++)
                {
                    Color pixelColor = greyed.GetPixel(x, y);
                    levels[pixelColor.R]++;
                }
            }

            // 3. Draw histogram
            DrawHistogram(levels);
        }

        private Bitmap ApplyInversion(Bitmap loaded)
        {
            Bitmap processed = new Bitmap(loaded.Width, loaded.Height);

            for (int y = 0; y < loaded.Height; y++)
            {
                for (int x = 0; x < loaded.Width; x++)
                {
                    Color pixelColor = loaded.GetPixel(x, y);

                    // invert each channel
                    Color inverted = Color.FromArgb(
                        255 - pixelColor.R,
                        255 - pixelColor.G,
                        255 - pixelColor.B);

                    processed.SetPixel(x, y, inverted);
                }
            }

            return processed;
        }

        private Bitmap ApplySubtraction(Bitmap foreground, Bitmap background)
        {
            if (foreground == null || background == null)
                throw new ArgumentException("Foreground or background bitmap is null");

            int width = Math.Min(foreground.Width, background.Width);
            //int width = 200;
            int height = Math.Min(foreground.Height, background.Height);
            //int height = 200;

            if (width <= 0 || height <= 0)
                throw new ArgumentException("One of the images has invalid dimensions");

            Bitmap result = new Bitmap(width, height);

            int greenThreshold = 100;  // how strong green must be
            int greenDifference = 60;  // how much stronger green must be vs red/blue

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color fg = foreground.GetPixel(x, y);

                    if (fg.G > greenThreshold && fg.G - fg.R > greenDifference && fg.G - fg.B > greenDifference)
                    {
                        // replace with background pixel
                        result.SetPixel(x, y, background.GetPixel(x, y));
                    }
                    else
                    {
                        // keep foreground pixel
                        result.SetPixel(x, y, fg);
                    }
                }
            }

            return result;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (processed == null) return;
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Title = "Save Processed Image";
            saveDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Png;

                string ext = System.IO.Path.GetExtension(saveDialog.FileName).ToLower();
                if (ext == ".jpg" || ext == ".jpeg")
                    format = System.Drawing.Imaging.ImageFormat.Jpeg;
                else if (ext == ".bmp")
                    format = System.Drawing.Imaging.ImageFormat.Bmp;

                processed.Save(saveDialog.FileName, format);

                MessageBox.Show("Image saved successfully!");
            }
        }
    }
}
