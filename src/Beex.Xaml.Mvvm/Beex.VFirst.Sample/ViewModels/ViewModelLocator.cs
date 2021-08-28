using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beex.VFirst.Sample.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            IoC.Instance.Register<MainViewModel>();
            IoC.Instance.Register<DefaultViewModel>();
            IoC.Instance.Register<FirstViewModel>();
            IoC.Instance.Register<SecondViewModel>();
        }

        public MainViewModel MianVm
        {
            get { return IoC.Instance.Resolve<MainViewModel>(); }
        }


        public DefaultViewModel DefaultVm
        {
            get { return IoC.Instance.Resolve<DefaultViewModel>(); }
        }

        public FirstViewModel FirstVm => IoC.Instance.Resolve<FirstViewModel>();

        public SecondViewModel SecondVm => IoC.Instance.Resolve<SecondViewModel>();
    }
}
