using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Nhom13_Quan_ly_kho_hang.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public bool Isloaded = false;
        public MainViewModel()
        {
            if (!Isloaded)
            {
                Isloaded = true;
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
            }
            
        }
    }
}
