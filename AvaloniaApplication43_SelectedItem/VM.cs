using Avalonia.Threading;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AvaloniaApplication43_SelectedItem
{
    public class VM : ReactiveObject
    {
        public VM()
        {
            Tracks = new ObservableCollection<string>
            {
                "aaa",
                "bbb"
            };

            ResetCommand = ReactiveCommand.Create(() =>
            {
                //Tracks.Clear();
                ChangeTracks();
            });

            Tracks.CollectionChanged += Tracks_CollectionChanged;

            SelectedAngle = "bbb";
        }

        private void Tracks_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            //SelectedAngle = Tracks.FirstOrDefault();
        }

        public void ChangeTracks()
        {
            Tracks = new ObservableCollection<string>(){
                "ccc",
                "ddd"
            };
        }

        private ObservableCollection<string> _tracks;
        public ObservableCollection<string> Tracks
        {
            get => _tracks;
            set => this.RaiseAndSetIfChanged(ref _tracks, value);
        }

        public ICommand ResetCommand { get; set; }

        private string _selectedAngle;
        public string SelectedAngle
        {
            get => _selectedAngle;
            set => this.RaiseAndSetIfChanged(ref _selectedAngle, value);
        }
    }
}
