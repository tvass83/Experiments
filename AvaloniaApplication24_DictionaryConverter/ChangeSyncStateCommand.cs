using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AvaloniaApplication24_DictionaryConverter
{
    public class ChangeSyncStateCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is VM vm)
            {
                switch (vm.SyncState)
                {
                    case SyncState.Online:
                        vm.SyncState = SyncState.Offline;
                        break;

                    case SyncState.Offline:
                        vm.SyncState = SyncState.Online;
                        break;

                    default:
                        throw new NotSupportedException();
                }
            }
        }
    }
}
