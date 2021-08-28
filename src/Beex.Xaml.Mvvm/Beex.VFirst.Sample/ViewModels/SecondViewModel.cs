using Beex.VFirst.Sample.Models;
using Beex.VFirst.Sample.Services;
using Beex.VFirst.Sample.Views;
using Beex.Xaml.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Beex.VFirst.Sample.ViewModels
{
    public class SecondViewModel : ViewModelBase, IInitializeService
    {
        public SecondViewModel()
        {
            BackCmd = new DelegateCommand(OnBack);
            PrintCmd = new DelegateCommand(OnPrint);
        }

        public ICommand BackCmd { get; set; }

        public ICommand PrintCmd { get; set; }

        public IViewService CustomerViewService
        {
            get { return GetService<IViewService>(); }
        }

        private ObservableCollection<UserViewModel> _users = new ObservableCollection<UserViewModel>();

        public ObservableCollection<UserViewModel> Users
        {
            get { return _users; }
            set { SetProperty(ref _users, value); }
        }

        public void InitializeData(TransMessage message)
        {
            if (message == null || message.UserInfo == null
                || string.IsNullOrWhiteSpace(message.UserInfo.Name))
                return;
            Users.Add(new UserViewModel(message.UserInfo));
        }

        private void OnBack()
        {
            Messenger.Default.Send(new TransMessage(typeof(FirstView)));
        }

        private async void OnPrint()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            await Task.Delay(1000);
            Mouse.OverrideCursor = null;
            CustomerViewService.PrintView();
        }
    }
}
