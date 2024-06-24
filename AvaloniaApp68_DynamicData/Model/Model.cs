using ReactiveUI;
using System;
using System.Diagnostics;

namespace DynamicDataTest
{
    public class Model : ReactiveObject, IDisposable
    {
        private int _id;

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _id, value);
            }
        }

        public Person Person { get; set; }

        public void Dispose()
        {
            Debug.WriteLine($"TV: Mode #{Id} was disposed.");
        }
    }
}
