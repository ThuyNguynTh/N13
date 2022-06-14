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
    public class CustomerViewModel : BaseViewModel
    {
        private ObservableCollection<Customer> _List;
        public ObservableCollection<Customer> List { get => _List; set { _List = value; OnPropertyChanged(); } }


        //binding SelectedItem tu Customerwindow.xaml --> cap nhat o bien (1)
        private Customer _SelectedItem;
        public Customer SelectedItem //(1)
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
        public string DisplayName //(2) --> cap nhat lại binging displayname ở file Customerwindow.xaml
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
        public CustomerViewModel()
        {
            List = new ObservableCollection<Customer>(DataProvider.Ins.DB.Customers);

            AddCommand = new RelayCommand<object>
                (
                    (p) =>
                    {
                        return true;

                    },
                (p) =>
                {
                    var Customer = new Customer() { DisplayName = DisplayName, Address = Address, Phone = Phone, Email = Email, MoreInfo = MoreInfo, ContractDate = ContractDate };
                    DataProvider.Ins.DB.Customers.Add(Customer);
                    DataProvider.Ins.DB.SaveChanges();
                    List.Add(Customer);
                });

            EditCommand = new RelayCommand<object>
                (
                    (p) =>
                    {
                        if (SelectedItem == null)
                            return false;
                        var displayList = DataProvider.Ins.DB.Customers.Where(x => x.Id == SelectedItem.Id);
                        if (displayList != null && displayList.Count() != 0)
                        {
                            return true;
                        }
                        else return false;

                    },
                (p) =>
                {
                    var Customer = DataProvider.Ins.DB.Customers.Where(x => x.Id == SelectedItem.Id).SingleOrDefault(); // lay Customer tuong ung
                    Customer.DisplayName = DisplayName;
                    Customer.Address = Address;
                    Customer.Phone = Phone;
                    Customer.Email = Email;
                    Customer.MoreInfo = MoreInfo;
                    Customer.ContractDate = ContractDate;
                    DataProvider.Ins.DB.SaveChanges();
                    SelectedItem.DisplayName = DisplayName;

                    OnPropertyChanged();
                });
        }


    }
}
