using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageEditor
{
    class History
    {
        struct HistoryItem
        {
            public Bitmap imageState;
            public string description;

            public HistoryItem(Bitmap image, string description)
            {
                imageState = image;
                this.description = description;
            }
        }

        private List<HistoryItem> historyStack;
        private int historySize;
        private int head;

        public History(int size)
        {
            historySize = size;
            historyStack = new List<HistoryItem>(size);
            head = -1;
        }

        public bool isEmpty()
        {
            if (head == -1)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public bool isFull()
        {
            if (head == (historySize - 1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void historyShift()
        {
            for (int i = 1; i < historyStack.Capacity; i++)
            {
                historyStack.RemoveAt(historySize - 1);
            }
        }

        public void add(Bitmap image, string description)
        {
            HistoryItem newHistoryItem = new HistoryItem(image, description);
            if (isFull())
            {
                historyShift();
            }
            historyStack.Add(newHistoryItem);
        }

    }
}
