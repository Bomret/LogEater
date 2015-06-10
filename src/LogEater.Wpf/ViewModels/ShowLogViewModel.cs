using System;
using System.ComponentModel.Composition;
using System.Data;
using Caliburn.Micro;
using Humanizer;

namespace LogEater.Wpf.ViewModels {
    [Export(typeof (ShowLogViewModel))]
    public sealed class ShowLogViewModel : Screen {
        BindableCollection<object> _logEntries;

        [ImportingConstructor]
        public ShowLogViewModel(IEventAggregator pubSub) {
            pubSub.Subscribe(this);

            LogEntries = new BindableCollection<object>(new[] {
                new {Date = DateTimeOffset.Now.Humanize(), Level="INFO", Message ="Hello!"},
                new {Date = DateTimeOffset.Now.AddSeconds(2).Humanize(), Level="DEBUG", Message ="Hello again!"}
            });
        }

        public BindableCollection<object> LogEntries {
            get { return _logEntries; }
            private set {
                if (Equals(_logEntries, value))
                    return;

                _logEntries = value;
                NotifyOfPropertyChange(() => LogEntries);
            }
        }
    }
}