using Beex.VFirst.Sample.Models;
using Beex.Xaml.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beex.VFirst.Sample.ViewModels
{
    public class UserViewModel : BindableBase
    {
        public UserViewModel()
        {
            _name = "yuhuang";
            _mobile = "18616760801";
            _email = "hb_yh@163.com";
        }

        public UserViewModel(UserModel model)
        {
            Name = model.Name;
            Mobile = model.Mobile;
            Email = model.Email;
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        private string _mobile;
        public string Mobile
        {
            get { return _mobile; }
            set { SetProperty(ref _mobile, value); }
        }

        public UserModel ToModel()
        {
            var model = new UserModel();
            model.Name = Name;
            model.Email = Email;
            model.Mobile = Mobile;
            return model;
        }
    }
}
