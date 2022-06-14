using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Nhom13_Quan_ly_kho_hang.Model;

namespace Nhom13_Quan_ly_kho_hang.ViewModel
{
    public class UnitViewModel : BaseViewModel
    {
        private ObservableCollection<Unit> _List;
        public ObservableCollection<Unit> List { get => _List; set { _List = value; OnPropertyChanged(); } }


        //binding SelectedItem tu unitwindow.xaml --> cap nhat o bien (1)
        private Unit _SelectedItem;
        public Unit SelectedItem //(1)
        {
            get => _SelectedItem;
            set 
            { 
                _SelectedItem = value; OnPropertyChanged(); 
                if (SelectedItem != null)  // neu lua chon khong null
                {
                    DisplayName = SelectedItem.DisplayName; //cap nhat displayname truyen vao bien (2)
                } 
            }
        }

        private String _DisplayName;
        public string DisplayName //(2) --> cap nhat lại binging displayname ở file unitwindow.xaml
        { 
            get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } 
        }

        //Thêm
        public ICommand AddCommand { get; set; }

        // Sửa
        public ICommand EditCommand { get; set; }
        public UnitViewModel()
        {
            List = new ObservableCollection<Unit>(DataProvider.Ins.DB.Units);

            AddCommand = new RelayCommand<object>
                (
                    (p) => 
                    {
                        if (string.IsNullOrEmpty(_DisplayName))
                            return false;
                        var displayList = DataProvider.Ins.DB.Units.Where(x => x.DisplayName == _DisplayName);
                        if (displayList == null || displayList.Count() != 0)
                        {
                            return false;
                        }
                        else return true;

                    }, 
                (p) => 
                {
                    var unit = new Unit() { DisplayName = DisplayName };
                    DataProvider.Ins.DB.Units.Add(unit);
                    DataProvider.Ins.DB.SaveChanges();
                    List.Add(unit);
                });

            EditCommand = new RelayCommand<object>
                (
                    (p) =>
                    {
                        if (string.IsNullOrEmpty(_DisplayName) || SelectedItem == null)
                            return false;
                        var displayList = DataProvider.Ins.DB.Units.Where(x => x.DisplayName == _DisplayName);
                        if (displayList == null || displayList.Count() != 0)
                        {
                            return false;
                        }
                        else return true;

                    },
                (p) =>
                {
                    var unit = DataProvider.Ins.DB.Units.Where(x => x.Id == SelectedItem.Id).SingleOrDefault(); // lay unit tuong ung
                    unit.DisplayName = DisplayName;
                    DataProvider.Ins.DB.SaveChanges();
                    SelectedItem.DisplayName = DisplayName;
                });
        }

        
    }
}
