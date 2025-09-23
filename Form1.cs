namespace ImageProcessing
{
    public partial class Form1 : Form
    {

        Bitmap loaded, processed;
        public Form1()
        {
            InitializeComponent();
        }

        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
    }
}
