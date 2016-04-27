using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageEditor.Controllers
{
    public partial class HistoryController : Form
    {
        private readonly History history;
        public HistoryController()
        {
            InitializeComponent();
            history = History.Instance;
            ApplyHistoryToController();
        }

        private void ApplyHistoryToController()
        {
            var items = new string[2];
            foreach (var element in history.history)
            {
                items[0] = element.name;
                items[1] = element.time;
                historyListController.Items.Add(new ListViewItem(items));
            }
        }


        private void Clean_Click(object sender, EventArgs e)
        {
            historyListController.Items.Clear();
            history.Clear();
        }
    }
}
