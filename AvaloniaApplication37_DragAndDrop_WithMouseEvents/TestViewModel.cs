using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication37_DragAndDrop
{
    public class TestViewModel : ReactiveObject
    {
        public TestViewModel()
        {
            Tracks = new ObservableCollection<VM>();

            Tracks.Add(new VM()
            {
                Angle = "CF",
                IsLeft = true
            });

            Tracks.Add(new VM()
            {
                Angle = "Low Home",
                IsLeft = false
            });

            Tracks.Add(new VM()
            {
                Angle = "High Home",
                IsLeft = false
            });
        }

        public ObservableCollection<VM> Tracks { get; set; }
    }

    public class VM : ReactiveObject
    {
        private string _angle;
        public string Angle
        {
            get => _angle;
            set => this.RaiseAndSetIfChanged(ref _angle, value);
        }

        private bool _isLeft;
        public bool IsLeft
        {
            get => _isLeft;
            set => this.RaiseAndSetIfChanged(ref _isLeft, value);
        }
    }
}
