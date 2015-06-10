using System.ComponentModel.Composition;
using Caliburn.Micro;
using LogEater.Wpf.Events;

namespace LogEater.Wpf.ViewModels {
    [Export(typeof (ShellViewModel))]
    public sealed class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IHandle<LogFileLoaded> {
        readonly LoadFileViewModel _loadLog;
        readonly ShowLogViewModel _showLog;

        [ImportingConstructor]
        public ShellViewModel(IEventAggregator pubSub,
            HeaderViewModel header,
            FooterViewModel footer,
            LoadFileViewModel loadLog,
            ShowLogViewModel showLog) {
            pubSub.Subscribe(this);

            Header = header;
            Footer = footer;

            _loadLog = loadLog;
            _showLog = showLog;

            DisplayName = "LogEater";
            ActivateItem(_loadLog);
        }

        public HeaderViewModel Header { get; private set; }
        public FooterViewModel Footer { get; private set; }

        public void ShowLogFile() {
            ActivateItem(_showLog);
        }

        public void ShowLoadScreen() {
            ActivateItem(_loadLog);
        }

        public void Handle(LogFileLoaded message) {
            ActivateItem(_showLog);
        }
    }
}