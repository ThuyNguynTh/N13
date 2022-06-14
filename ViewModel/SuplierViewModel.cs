using Nhom13_Quan_ly_kho_hang.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Nhom13_Quan_ly_kho_hang.ViewModel
{
    internal class SuplierViewModel : BaseViewModel
    {
        private ObservableCollection<Suplier> _List;
        public ObservableCollection<Suplier> List { get => _List; set { _List = value; OnPropertyChanged(); } }


        //binding SelectedItem tu Suplierwindow.xaml --> cap nhat o bien (1)
        private Suplier _SelectedItem;
        public Suplier SelectedItem //(1)
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value; OnPropertyChanged();
                if (SelectedItem != null)  // neu lua chon khong null
                {
                    DisplayName = SelectedItem.DisplayName; //cap nhat displayname truyen vao bien (2)
                    Address = SelectedItem.Address;
                    Phone = SelectedItem.Phone;
                    Email = SelectedItem.Email;
                    MoreInfo = SelectedItem.MoreInfo;
                    ContractDate = SelectedItem.ContractDate;

                }
            }
        }

        private String _DisplayName;
        public string DisplayName //(2) --> cap nhat lại binging displayname ở file Suplierwindow.xaml
        {
            get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); }
        }

        private String _Address;
        public string Address
        {
            get => _Address; set { _Address = value; OnPropertyChanged(); }
        }

        private String _Phone;
        public string Phone
        {
            get => _Phone; set { _Phone = value; OnPropertyChanged(); }
        }

        private String _Email;
        public string Email
        {
            get => _Email; set { _Email = value; OnPropertyChanged(); }
        }

        private String _MoreInfo;
        public string MoreInfo
        {
            get => _MoreInfo; set { _MoreInfo = value; OnPropertyChanged(); }
        }

        private DateTime? _ContractDate;
        public DateTime? ContractDate
        {
            get => _ContractDate; set { _ContractDate = value; OnPropertyChanged(); }
        }

        //Thêm
        public ICommand AddCommand { get; set; }

        // Sửa
        public ICommand EditCommand { get; set; }
        public SuplierViewModel()
        {
            List = new ObservableCollection<Suplier>(DataProvider.Ins.DB.Supliers);

            AddCommand = new RelayCommand<object>
                (
                    (p) =>
                    {
                         return true;

                    },
                (p) =>
                {
                    var Suplier = new Suplier() { DisplayName = DisplayName, Address = Address, Phone = Phone, Email = Email, MoreInfo = MoreInfo, ContractDate = ContractDate };
                    DataProvider.Ins.DB.Supliers.Add(Suplier);
                    DataProvider.Ins.DB.SaveChanges();
                    List.Add(Suplier);
                });

            EditCommand = new RelayCommand<object>
                (
                    (p) =>
                    {
                        if (SelectedItem == null)
                            return false;
                        var displayList = DataProvider.Ins.DB.Supliers.Where(x => x.Id == SelectedItem.Id);
                        if (displayList != null && displayList.Count() != 0)
                        {
                            return true;
                        }
                        else return false;

                    },
                (p) =>
                {
                    var Suplier = DataProvider.Ins.DB.Supliers.Where(x => x.Id == SelectedItem.Id).SingleOrDefault(); // lay Suplier tuong ung
                    Suplier.DisplayName = DisplayName;
                    Suplier.Address = Address; 
                    Suplier.Phone = Phone; 
                    Suplier.Email = Email; 
                    Suplier.MoreInfo = MoreInfo; 
                    Suplier.ContractDate = ContractDate;
                    DataProvider.Ins.DB.SaveChanges();
                    SelectedItem.DisplayName = DisplayName;

                    OnPropertyChanged();
                });
        }


    }
}
