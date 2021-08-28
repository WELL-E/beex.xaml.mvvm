using Beex.VFirst.Sample.Models;
using Beex.VFirst.Sample.Views;
using Beex.Xaml.Mvvm;
using System.Windows.Input;

namespace Beex.VFirst.Sample.ViewModels
{
    public class DefaultViewModel : BindableBase
    {
        public DefaultViewModel()
        {
            NextCmd = new DelegateCommand(OnNext);
        }

        public ICommand NextCmd { get; set; }

        private void OnNext()
        {
            Messenger.Default.Send(new TransMessage(typeof(FirstView)));
        }
    }
}
