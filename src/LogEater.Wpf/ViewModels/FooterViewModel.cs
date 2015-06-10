using System.ComponentModel.Composition;
using System.Globalization;
using System.IO;
using Caliburn.Micro;
using Humanizer;
using LogEater.Wpf.Events;

namespace LogEater.Wpf.ViewModels {
    [Export(typeof (FooterViewModel))]
    public sealed class FooterViewModel : PropertyChangedBase, IHandle<LogFileLoaded> {
        string _fileSize;

        [ImportingConstructor]
        public FooterViewModel(IEventAggregator pubSub) {
            pubSub.Subscribe(this);
        }

        public string FileSize {
            get { return _fileSize; }
            private set {
                if (Equals(_fileSize,value)) return;

                _fileSize = value;
                NotifyOfPropertyChange(() => FileSize);
            }
        }

        public void Handle(LogFileLoaded message) {
            var size = new FileInfo(message.LogFile).Length;

            FileSize = size.Bytes().Humanize("#.##");
        }
    }
}