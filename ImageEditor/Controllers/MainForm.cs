using ImageEditor.Effects;
using ImageEditor.Effects.Binarization;
using System;
using System.Drawing;
using System.Windows.Forms;
using ImageEditor.Controllers;

namespace ImageEditor
{
    public partial class Form1 : Form
    {

        private Bitmap originalBitmap;
        private Bitmap modifiedBitmap;
        private readonly History history = History.Instance;
        private int startX, startY, endX, endY;
        private bool isSelecting;
        Selection selection = Selection.Instance;
        

        public Form1()
        {
            InitializeComponent();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                originalBitmap = new Bitmap(Image.FromFile(openFileDialog1.FileName));
                modifiedBitmap = new Bitmap(Image.FromFile(openFileDialog1.FileName));
                originalImage.Image = originalBitmap;
                modifiedImage.Image = originalBitmap;

                Pen pen = new Pen(Color.Red);
                Graphics g = originalImage.CreateGraphics();
                g.DrawRectangle(pen, 10, 10, 30, 30);

                // Enable menu items
                effectsToolStripMenuItem.Enabled = true;
                editToolStripMenuItem.Enabled = true;

                history.AddElement(originalBitmap, "Load file");
            }
        }
        private void setImage(Bitmap image)
        {
            modifiedImage.Image = image;
            modifiedBitmap = image;
        }

        private void rotateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RotateDialogBox rotateDialogBox = new RotateDialogBox();
            rotateDialogBox.ShowDialog();
            if (rotateDialogBox.DialogResult == DialogResult.OK)
            {
                int angle = int.Parse(rotateDialogBox.angleUpDown.Text) - 360;
                Rotate effect = new Rotate(sourceImage: modifiedBitmap, angle: 90);
                history.AddElement(effect);
                effect.ApplyEffect(setImage);
                rotateDialogBox.Dispose();
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            history.Undo(setImage);
            setImage(history.GetCurrentImage());
        }

        
        private void windToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindDialogBox windDialogBox = new WindDialogBox();
            windDialogBox.ShowDialog();
            if (windDialogBox.DialogResult == DialogResult.OK)
            {
                Wind wind = new Wind(modifiedBitmap,windDialogBox.direction, windDialogBox.strengt);
                wind.ApplyEffect(setImage);
                history.AddElement(wind);
                windDialogBox.Dispose();
            }
            modifiedImage.Image = modifiedBitmap;
        }

        private void grayWorldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GrayWorld grayWorld = new GrayWorld(modifiedBitmap);
            grayWorld.ApplyEffect(setImage);
            history.AddElement(grayWorld);
        }

        private void threadholdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToGrayScale toGrayScale = new ToGrayScale(sourceImage: modifiedBitmap);
            toGrayScale.ApplyEffect(setImage);
            Threadhold effect = new Threadhold(sourceImage: modifiedBitmap, value: 200);
            effect.ApplyEffect(setImage);
            history.AddElement(effect);
        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //HistogramForm rotateDialogBox = new HistogramForm();
            //var host = new Window();
            //host.Content = rotateDialogBox;
            //host.Show();
        }

        private void startSelection(object sender, MouseEventArgs e)
        {
            startX = e.X;
            startY = e.Y;
            isSelecting = true;
        }

        private void originalImage_Paint(object sender, PaintEventArgs e)
        {
            int x, y;
            if (startX < endX)
            {
                x = startX;
            } else
            {
                x = endX;
            }

            if (startY < endY)
            {
                y = startY;
            } else
            {
                y = endY;
            }

            Rectangle rect = new Rectangle(x, y, Math.Abs(endX - startX), Math.Abs(endY - startY));
            Pen whitePen = new Pen(Color.White, 3);
            e.Graphics.DrawRectangle(whitePen, rect);

            rect = new Rectangle(x, y, Math.Abs(endX - startX), Math.Abs(endY - startY));

            whitePen = new Pen(Color.Red, 1);
            e.Graphics.DrawRectangle(whitePen, rect);
        }

        private void viewHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HistoryController historyController = new HistoryController();
            historyController.ShowDialog();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            history.Redo(setImage);
            setImage(history.GetCurrentImage());
        }

        private void originalImage_MouseUp(object sender, MouseEventArgs e)
        {
            isSelecting = false;
        }

        private void originalImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (isSelecting)
            {
                endX = e.X;
                endY = e.Y;
                originalImage.Refresh();
            }
        }

        private void randomJitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RandomJitter randomJitter = new RandomJitter(sourceImage: modifiedBitmap, degree: 1000);
            randomJitter.ApplyEffect(setImage);

        }
    }
}
