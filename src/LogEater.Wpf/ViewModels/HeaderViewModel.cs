using System.ComponentModel.Composition;
using Caliburn.Micro;
using Humanizer;
using LogEater.Wpf.Events;

namespace LogEater.Wpf.ViewModels {
    [Export(typeof (HeaderViewModel))]
    public sealed class HeaderViewModel : PropertyChangedBase, IHandle<LogFileLoaded> {
        string _logFile;

        [ImportingConstructor]
        public HeaderViewModel(IEventAggregator pubSub) {
            pubSub.Subscribe(this);
        }

        public string LogFile {
            get { return _logFile; }
            private set {
                if (string.Equals(_logFile, value))
                    return;

                _logFile = value;
                NotifyOfPropertyChange(() => LogFile);
            }
        }

        public void Handle(LogFileLoaded message) {
            LogFile = message.LogFile.Truncate(50, Truncator.FixedLength, TruncateFrom.Left);
        }
    }
}