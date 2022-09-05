using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace WpfApp8_Arrange
{
    public class MainViewModel : ReactiveObject
    {
        public MainViewModel()
        {
            NavigateCommand = ReactiveCommand.Create<string>(commandParameter =>
            {
                switch (commandParameter)
                {
                    case "A":
                        ViewToggle = false;
                        Tracks.Clear();
                        break;

                    case "B":
                        ViewToggle = true;
                        AddTracks();
                        break;

                    default: throw new NotSupportedException();
                }
            });
        }

        public ObservableCollection<string> Tracks { get; } = new ObservableCollection<string>();
        public ICommand NavigateCommand { get; }

        public bool ViewToggle
        {
            get => _viewToggle;
            set => this.RaiseAndSetIfChanged(ref _viewToggle, value);
        }

        private void AddTracks()
        {
            for (int i = 0; i < new Random().Next(1, 10); i++)
            {
                Tracks.Add(Guid.NewGuid().ToString());
            }
        }

        private bool _viewToggle;
    }
}
