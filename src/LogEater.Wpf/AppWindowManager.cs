using System.Collections.Generic;
using System.Windows;
using Caliburn.Micro;

namespace LogEater.Wpf {
    public sealed class AppWindowManager : WindowManager {
        protected override Window CreateWindow(object rootModel, bool isDialog, object context, IDictionary<string, object> settings) {
            var window = base.CreateWindow(rootModel, isDialog, context, settings);

            window.WindowStyle = WindowStyle.ToolWindow;

            return window;
        }
    }
}