using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beex.VFirst.Sample.Models
{
    public class TransMessage 
    {
        public TransMessage()
        {

        }

        public TransMessage(Type viewType)
        {
            ViewType = viewType;
        }

        public TransMessage(Type viewType, string content)
        {
            ViewType = viewType;
            Content = content;
        }

        public TransMessage(Type viewType, UserModel model)
        {
            ViewType = viewType;
            UserInfo = model;
        }

        public Type ViewType { get; set; }

        public string Content { get; set; }

        public UserModel UserInfo { get; set; }
}
}
