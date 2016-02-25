using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageEditor
{
    public partial class Form1 : Form
    {

        private Bitmap originalBitmap;
        private Bitmap modifiedBitmap;

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
                System.IO.StreamReader sr = new
                   System.IO.StreamReader(openFileDialog1.FileName);
                originalBitmap = new Bitmap(Image.FromFile(openFileDialog1.FileName));
                modifiedBitmap = new Bitmap(Image.FromFile(openFileDialog1.FileName));
                originalImage.Image = originalBitmap;
                modifiedImage.Image = originalBitmap;
                sr.Close();
            }
        }

        private void rotateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RotateDialogBox rotateDialogBox = new RotateDialogBox();
            rotateDialogBox.ShowDialog();
            if (rotateDialogBox.DialogResult == DialogResult.OK)
            {
                modifiedBitmap = Effect.rotate(originalBitmap, int.Parse(rotateDialogBox.angleUpDown.Text));
                modifiedImage.Image = modifiedBitmap;
                rotateDialogBox.Dispose();
            }
        }
    }
}
