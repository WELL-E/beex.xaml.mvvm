using Beex.VFirst.Sample.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace Beex.VFirst.Sample
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            IoC.Instance.Register<DefaultView>();
            IoC.Instance.Register<FirstView>();
            IoC.Instance.Register<SecondView>().AsSingleton();
        }
    }
}
