using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using LogEater.Wpf.ViewModels;

namespace LogEater.Wpf {
    public sealed class AppBootstrapper : BootstrapperBase {
        CompositionContainer _container;

        public AppBootstrapper() {
            Initialize();
        }

        protected override void Configure() {
            _container =
                new CompositionContainer(
                    new AggregateCatalog(AssemblySource.Instance.Select(x => new AssemblyCatalog(x))));

            var batch = new CompositionBatch();

            batch.AddExportedValue<IWindowManager>(new AppWindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue(_container);

            _container.Compose(batch);
        }

        protected override object GetInstance(Type serviceType, string key) {
            var contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
            var exports = _container.GetExportedValues<object>(contract).ToList();

            if (exports.Count > 0) return exports.First();

            throw new Exception($"Could not locate any instances of contract {contract}.");
        }

        protected override void OnStartup(object sender, StartupEventArgs e) {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}