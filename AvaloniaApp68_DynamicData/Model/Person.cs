using ReactiveUI;

namespace DynamicDataTest
{
    public class Person : ReactiveObject
    {
        private int _age;

        public string Name { get; set; }

        public int Age
        {
            get
            {
                return _age;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _age, value);
            }
        }
    }
}
