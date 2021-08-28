using Beex.VFirst.Sample.Models;
using Beex.VFirst.Sample.Views;
using Beex.Xaml.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Beex.VFirst.Sample.ViewModels
{
    public class FirstViewModel : BindableBase
    {
        public FirstViewModel()
        {
            NextCmd = new DelegateCommand(OnNext, CanOnNext)
                .ObservesProperty(()=> User.Name)
                .ObservesProperty(()=>User.Mobile)
                .ObservesProperty(() => User.Email);
            BackCmd = new DelegateCommand(OnBack);
        }

        public ICommand NextCmd { get; set; }

        public ICommand BackCmd { get; set; }

        private UserViewModel _user = new UserViewModel();
        public UserViewModel User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }

        private async void OnNext()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            await Task.Delay(1000);
            Mouse.OverrideCursor = null;
            Messenger.Default.Send(new TransMessage(typeof(SecondView), User.ToModel()));
        }

        private bool CanOnNext()
        {
            if (User == null || string.IsNullOrWhiteSpace(User.Name)
                || string.IsNullOrWhiteSpace(User.Mobile)
                || string.IsNullOrWhiteSpace(User.Email))
                return false;
            return true;
        }

        private void OnBack()
        {
            Messenger.Default.Send(new TransMessage(typeof(DefaultView)));
        }

    }
}
