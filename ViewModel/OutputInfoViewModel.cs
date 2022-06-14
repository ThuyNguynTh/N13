using Nhom13_Quan_ly_kho_hang.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace Nhom13_Quan_ly_kho_hang.ViewModel
{
    public class OutputInfoViewModel : BaseViewModel
    {
        private ObservableCollection<OutputInfo> _List;

        public ObservableCollection<OutputInfo> List
        {
            get { return _List; }
            set { _List = value; OnPropertyChanged(); }
        }
        private ObservableCollection<Model.Object> _ObjectList;

        public ObservableCollection<Model.Object> ObjectList
        {
            get { return _ObjectList; }
            set { _ObjectList = value; OnPropertyChanged(); }
        }
        private string _Id;

        public string Id
        {
            get { return _Id; }
            set { _Id = value; OnPropertyChanged(); }
        }

        private DateTime _DateOutput;

        public DateTime DateOutput
        {
            get { return _DateOutput; }
            set { _DateOutput = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Customer> _Customer;

        public ObservableCollection<Customer> Customer
        {
            get { return _Customer; }
            set { _Customer = value; OnPropertyChanged(); }
        }

        private int _Count;

        public int Count
        {
            get { return _Count; }
            set { _Count = value; OnPropertyChanged(); }
        }

        private Model.Object _SelectedObject;

        public Model.Object SelectedObject
        {
            get { return _SelectedObject; }
            set { _SelectedObject = value; OnPropertyChanged(); }
        }

        private Customer _SelectedCustomer;

        public Customer SelectedCustomer
        {
            get { return _SelectedCustomer; }
            set { _SelectedCustomer = value; OnPropertyChanged(); }
        }
        private double _PriceOutput;

        public double PriceOutput
        {
            get { return _PriceOutput; }
            set { _PriceOutput = value; OnPropertyChanged(); }
        }

        private string _Status;

        public string Status
        {
            get { return _Status; }
            set { _Status = value; OnPropertyChanged(); }
        }

        private OutputInfo _SelectedItem;

        public OutputInfo SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    Id = SelectedItem.Id;
                    DateOutput = SelectedItem.Output.DateOutput.Value;
                    Count = SelectedItem.Count.Value;
                    PriceOutput = SelectedItem.OutputPrice.Value;
                    Status = SelectedItem.Status;
                    SelectedObject = SelectedItem.Object;
                    SelectedCustomer = SelectedItem.Customer;
                }
                else
                {
                    Id = null;
                    DateOutput = DateTime.Today;
                    Count = 0;
                    PriceOutput = 0;
                    Status = "";
                }
            }
        }


        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public OutputInfoViewModel()
        {
            LoadData();

            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                var outp = DataProvider.Ins.DB.Outputs.Where(x => x.DateOutput == DateOutput);
                if (!(outp == null || outp.Count() != 0))
                {
                    var output = new Model.Output() { DateOutput = DateOutput, Id = Guid.NewGuid().ToString() };
                    DataProvider.Ins.DB.Outputs.Add(output);
                    DataProvider.Ins.DB.SaveChanges();
                }
                var outpu = DataProvider.Ins.DB.Outputs.Where(x => x.DateOutput == DateOutput).SingleOrDefault();
                var outputinfo = new OutputInfo()
                {
                    IdObject = SelectedObject.Id,
                    IdOutput = outpu.Id,
                    Count = Count,
                    IdCustomer = SelectedCustomer.Id,
                    OutputPrice = PriceOutput,
                    Status = Status,
                    Id = Guid.NewGuid().ToString()
                };
                DataProvider.Ins.DB.OutputInfoes.Add(outputinfo);
                DataProvider.Ins.DB.SaveChanges();
                List.Add(outputinfo);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                var outp = DataProvider.Ins.DB.Outputs.Where(x => x.DateOutput == DateOutput);
                if (!(outp == null || outp.Count() != 0))
                {
                    var output = new Model.Output() { DateOutput = DateOutput, Id = Guid.NewGuid().ToString() };
                    DataProvider.Ins.DB.Outputs.Add(output);
                    DataProvider.Ins.DB.SaveChanges();
                }
                var outpu = DataProvider.Ins.DB.Outputs.Where(x => x.DateOutput == DateOutput).SingleOrDefault();
                var outputInfo = DataProvider.Ins.DB.OutputInfoes.Where(x => x.Id == Id).SingleOrDefault();
                outputInfo.IdObject = SelectedObject.Id;
                outputInfo.IdOutput = outpu.Id;
                outputInfo.Count = Count;
                outputInfo.IdCustomer = SelectedCustomer.Id;
                outputInfo.OutputPrice = PriceOutput;
                outputInfo.Status = Status;
                DataProvider.Ins.DB.SaveChanges();
                SelectedItem = outputInfo;
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                DialogResult result = System.Windows.Forms.MessageBox.Show("Bạn có chắc chắn muốn xóa đơn xuất không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    DataProvider.Ins.DB.OutputInfoes.Remove(DataProvider.Ins.DB.OutputInfoes.First(x => x.Id == SelectedItem.Id));
                    DataProvider.Ins.DB.SaveChanges();
                    LoadData();
                }
            });

        }

        void LoadData()
        {
            List = new ObservableCollection<OutputInfo>(DataProvider.Ins.DB.OutputInfoes.OrderBy(item => item.Output.DateOutput));
            ObjectList = new ObservableCollection<Model.Object>(DataProvider.Ins.DB.Objects.OrderBy(item => item.DisplayName));
            Customer = new ObservableCollection<Customer>(DataProvider.Ins.DB.Customers.OrderBy(item => item.DisplayName));
            SelectedCustomer = Customer[0];
            SelectedObject = ObjectList[0];
        }
    }
}
