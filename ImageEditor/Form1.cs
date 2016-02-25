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
        private History history = new History(10);

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
                history.add(modifiedBitmap, string.Format("Image opened"));
                // Enable menu items
                effectsToolStripMenuItem.Enabled = true;
                editToolStripMenuItem.Enabled = true;
            }
        }

        private void rotateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RotateDialogBox rotateDialogBox = new RotateDialogBox();
            rotateDialogBox.ShowDialog();
            if (rotateDialogBox.DialogResult == DialogResult.OK)
            {
                int angle = int.Parse(rotateDialogBox.angleUpDown.Text);
                history.add(modifiedBitmap, String.Format("Rotate on {0}. ULCorner: {1}, BUCorner: {2}", angle, 0, 0));
                modifiedBitmap = Effect.rotate(originalBitmap, int.Parse(rotateDialogBox.angleUpDown.Text));
                modifiedImage.Image = modifiedBitmap;
                rotateDialogBox.Dispose();
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap modifiedBitmap = history.undo();
            modifiedImage.Image = modifiedBitmap;
        }
    }
}
