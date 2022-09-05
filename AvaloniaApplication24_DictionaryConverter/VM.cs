using System.ComponentModel;
using System.Windows.Input;

namespace AvaloniaApplication24_DictionaryConverter
{
    public class VM : INotifyPropertyChanged
    {
        private SyncState _syncState;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand ChangeSyncStateCommand { get; set; } = new ChangeSyncStateCommand();

        public SyncState SyncState
        {
            get
            {
                return _syncState;
            }
            set
            {
                _syncState = value;
                RaisePropertyChanged(nameof(SyncState));
            }
        }

        private void RaisePropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
