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
    public partial class WindDialogBox : Form
    {
        public int strengt
        {
            get; private set;
        }

        public Direction direction
        {
            get;
            private set;

        }
        public WindDialogBox()
        {
            InitializeComponent();
            okButton.DialogResult = DialogResult.OK;
            cancelButton.DialogResult = DialogResult.Cancel;
        }

        private void strengtTrackBar_Scroll(object sender, EventArgs e)
        {
            strengtUpDown.Text = strengtTrackBar.Value.ToString();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            switch (directionComboBox.Text)
            {
                case "Left":
                    direction = Direction.Left;
                    break;
                case "Right":
                    direction = Direction.Right;
                    break;
                case "Up":
                    direction = Direction.Up;
                    break;
                case "Down":
                    direction = Direction.Down;
                    break;
            }

            strengt = int.Parse(strengtUpDown.Text);
        }
    }
}
