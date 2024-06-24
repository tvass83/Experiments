using Avalonia;
using Avalonia.Threading;
using DynamicData;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DynamicDataTest
{
    public class ViewModel : ReactiveObject
    {
        private readonly SourceList<Model> _models = new();
        private int _lastModelId = 1;

        public ViewModel()
        {
            AddModelCommand = ReactiveCommand.Create(OnAddModel);
            IncreaseAgeCommand = ReactiveCommand.Create(OnIncreaseAge);
            RemoveModelCommand = ReactiveCommand.Create<Model>(OnRemoveModel);
            ClearAllCommand = ReactiveCommand.Create(OnClearAll);

            ReadOnlyObservableCollection <Model> models;

            var connection = _models.Connect();

            connection.WhenAnyPropertyChanged()
                .Subscribe(_ => Debug.WriteLine($"TV: Model changed: {_.Id}"));

            connection
                .Bind(out models)
                .DisposeMany()
                .Subscribe();

            Models = models;
        }

        public ReactiveCommandBase<Unit, Unit> AddModelCommand { get; }
        public ReactiveCommandBase<Model, Unit> RemoveModelCommand { get; }
        public ReactiveCommandBase<Unit, Unit> ClearAllCommand { get; }
        public ReactiveCommandBase<Unit, Unit> IncreaseAgeCommand { get; }

        public ReadOnlyObservableCollection<Model> Models { get; }

        private void OnAddModel()
        {
            _models.Add(new Model() { Id = _lastModelId++, Person = new Person() { Name = Guid.NewGuid().ToString(), Age = Random.Shared.Next(20, 40) } });
        }

        private void OnRemoveModel(Model model)
        {
            _models.Remove(model);
        }

        private void OnClearAll()
        {
            _models.Clear();
        }

        private void OnIncreaseAge()
        {
            foreach (var model in Models)
            {
                model.Person.Age++;
            }
        }
    }
}
