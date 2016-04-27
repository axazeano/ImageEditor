using System;
using System.Collections.Generic;
using System.Drawing;
using ImageEditor.Effects;

namespace ImageEditor
{

    struct HistoryElement
    {
        public HistoryElement(BaseEffect effect)
        {
            image = effect.resultImage;
            name = effect.name;
            time = DateTime.Now.ToString("HH:mm:ss tt");
        }

        public HistoryElement(Bitmap image, string name)
        {
            this.image = image;
            this.name = name;
            time = DateTime.Now.ToString("HH:mm:ss tt");
        }

        public Bitmap image { get; }
        public String name { get; }
        public string time { get; }
    }

    class History
    {
        public List<HistoryElement> history { get; }
        private int _currentElement;

        public delegate void Complete(Bitmap image);

        private static History _instance;

        private History()
        {
            this._currentElement = -1;
            history = new List<HistoryElement>();
        }

        /// <summary>
        /// Singleton of history
        /// </summary>
        public static History Instance => _instance ?? (_instance = new History());



        public void AddElement(BaseEffect effect)
        {
            history.Add(new HistoryElement(effect));
            ++_currentElement;
        }

        public void AddElement(Bitmap image, string name)
        {
            history.Add(new HistoryElement(image, name));
            ++_currentElement;
        }

        public void RemoveElement(int index)
        {
            history.RemoveAt(index);
        }

        public Bitmap GetCurrentImage()
        {
            return history[_currentElement].image;
        }


        public void Undo(Complete complete)
        {
            if (_currentElement > 0)
            {
                _currentElement -= 1;
                complete(GetCurrentImage());
            }
        }

        public void Redo(Complete complete)
        {
            if (_currentElement <= history.Count)
            {
                ++_currentElement;
                complete(GetCurrentImage());
            }
        }

        public void Clear()
        {
            history.Clear();
        }

    }
}
