using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
namespace Nhom13_Quan_ly_kho_hang.ViewModel
{
    public class ControlBarViewModel: BaseViewModel
    {
        #region commands
        public ICommand CloseWindowCommand { get; set; }

        public ControlBarViewModel()
        {

        }
    }
}
