using Nhom13_Quan_ly_kho_hang.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nhom13_Quan_ly_kho_hang.ViewModel
{
    internal class LoginViewModel : BaseViewModel
    {
       
        public bool IsLogin { get; set; }

        private string _UserName;
        public string UserName { get { return _UserName; } set { _UserName = value; OnPropertyChanged(); } }

        private string _Password;
        public string Password { get { return _Password; } set { _Password = value; OnPropertyChanged(); } }

        public ICommand LoginCommand { get; set; }

        public ICommand CloseCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }

        public LoginViewModel()
        {
            IsLogin = false;
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Login(p);
              
            }
            );
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();

            }
            );
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                Password = p.Password;

            }
            );
            void Login(Window p )
            {
                if (p == null)
                    return;
                var accCount = DataProvider.Ins.DB.Users.Where(x => x.UserName == UserName && x.PassWord == Password).Count();
                if (accCount > 0) 
                {
                    IsLogin = true;
                    p.Close();
                }

                else
                {
                    IsLogin = false;
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Đăng nhập thất bại", MessageBoxButton.OK, MessageBoxImage.Stop);
                }

            }
           
        }
    }
}
