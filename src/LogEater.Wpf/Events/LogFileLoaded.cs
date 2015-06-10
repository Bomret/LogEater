namespace LogEater.Wpf.Events {
    public sealed class LogFileLoaded {
        public LogFileLoaded(string logFile) {
            LogFile = logFile;
        }

        public string LogFile { get; private set; }
    }
}