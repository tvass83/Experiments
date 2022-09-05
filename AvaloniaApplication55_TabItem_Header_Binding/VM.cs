using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace AvaloniaApplication55_TabItem_Header_Binding
{
    public class VM : ReactiveObject
    {
        public VM()
        {
            Command1 = ReactiveCommand.Create(() => SetHeaderText("test1"));
            Command2 = ReactiveCommand.Create(() => TomEx.Tom = null);
        }

        public IReactiveCommand Command1 { get; }
        public IReactiveCommand Command2 { get; }


        [Reactive]
        public TomEx TomEx { get; set; }

        private void SetHeaderText(string text)
        {
            TomEx = new TomEx() { Tom = new Tom() { HeaderText = text } };
        }
    }

    public class TomEx : ReactiveObject
    {
        [Reactive]
        public Tom Tom { get; set; }
    }

    public class Tom
    {
        public string HeaderText { get; set; }
    }
}
