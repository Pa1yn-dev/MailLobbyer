using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;

namespace Mail_Lobbyer.ViewModels
{
    public class EmailFieldViewModel : ViewModelBase
    {
        private string _name;
        private string _subject;
        private string _content;
        private string _selectedGroup;
        private ObservableCollection<string> _groups;

        public EmailFieldViewModel()
        {
            _groups = new ObservableCollection<string>
            {
                "A",
                "B",
                "C",
                "D"
            };
           
            SelectedGroup = _groups[0];

            OnSubmitClicked = ReactiveCommand.Create(SubmitForm);
        }

        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        public string Subject
        {
            get => _subject;
            set => this.RaiseAndSetIfChanged(ref _subject, value);
        }

        public string Content
        {
            get => _content;
            set => this.RaiseAndSetIfChanged(ref _content, value);
        }

        public string SelectedGroup
        {
            get => _selectedGroup;
            set => this.RaiseAndSetIfChanged(ref _selectedGroup, value);
        }

        public ObservableCollection<string> Groups
        {
            get => _groups;
            set => this.RaiseAndSetIfChanged(ref _groups, value);
        }

        public ReactiveCommand<Unit, Unit> OnSubmitClicked { get; }

        private void SubmitForm()
        {
            // Call smtp method here etc
        }
    }
}
