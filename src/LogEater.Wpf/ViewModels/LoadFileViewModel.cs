using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Caliburn.Micro;
using LogEater.Wpf.Events;
using Microsoft.Win32;

namespace LogEater.Wpf.ViewModels {
    [Export(typeof (LoadFileViewModel))]
    public sealed class LoadFileViewModel : Screen {
        readonly IEventAggregator _pubSub;
        string _pattern;

        public string Pattern {
            get { return _pattern; }
            set {
                if (Equals(_pattern,value)) return;

                _pattern = value;
                NotifyOfPropertyChange(()=>Pattern);
                NotifyOfPropertyChange(()=>CanLoadLogFile);
            }
        }

        [ImportingConstructor]
        public LoadFileViewModel(IEventAggregator pubSub) {
            _pubSub = pubSub;
        }

        public async Task LoadLogFile() {
            var fileChooser = new OpenFileDialog {
                DefaultExt = ".log",
                Filter = "Log files|*.log|Text files|*.txt|All files|*.*",
                Multiselect = false
            };

            var hasChosenFile = fileChooser.ShowDialog();
            if (hasChosenFile == true)
                await _pubSub.PublishOnUIThreadAsync(new LogFileLoaded(fileChooser.FileName));
        }

        public bool CanLoadLogFile => !string.IsNullOrWhiteSpace(_pattern);
    }
}