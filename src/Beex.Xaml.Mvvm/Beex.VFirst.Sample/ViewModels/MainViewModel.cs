using Beex.VFirst.Sample.Models;
using Beex.VFirst.Sample.Services;
using Beex.VFirst.Sample.Views;
using Beex.Xaml.Mvvm;
using System.Windows.Controls;

namespace Beex.VFirst.Sample.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private ContentControl _currentView;

        public MainViewModel()
        {
            CurrentView = IoC.Instance.Resolve<DefaultView>();
            Messenger.Default.Register<TransMessage>(this, OnMessage);
        }

        public ContentControl CurrentView
        {
            get { return _currentView; }
            set { SetProperty(ref _currentView, value); }
        }

        private void OnMessage(TransMessage model)
        {
            CurrentView =  IoC.Instance.Resolve(model.ViewType) as ContentControl;
            var vm = CurrentView.DataContext as IInitializeService;
            if (vm != null) vm.InitializeData(model);
        }

    }
}
