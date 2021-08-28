using Beex.Xaml.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beex.VFirst.Sample
{
    public class IoC
    {
        private IoC()
        {
        }

        private static readonly Lazy<IServiceContainer> lazy = new Lazy<IServiceContainer>(() => new ServiceContainer());

        public static IServiceContainer Instance
        {
            get
            {
                return lazy.Value;
            }
        }
    }
}
