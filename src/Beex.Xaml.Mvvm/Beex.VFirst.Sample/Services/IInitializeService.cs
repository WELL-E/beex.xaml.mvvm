using Beex.VFirst.Sample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beex.VFirst.Sample.Services
{
    public interface IInitializeService
    {
        void InitializeData(TransMessage message);
    }
}
