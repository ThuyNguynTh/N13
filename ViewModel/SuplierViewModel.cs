using Nhom13_Quan_ly_kho_hang.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        public ICommand DeleteCommand { get; set; }
        public SuplierViewModel()
        {
            LoadData();
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
                        return true;

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
                });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                DialogResult result = System.Windows.Forms.MessageBox.Show("Bạn có chắc chắn muốn xóa nhà cung cấp không?\nThao tác này sẽ xóa tất cả vật tư liên quan.", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    var objectList = DataProvider.Ins.DB.Objects.Where(x => x.IdSuplier == SelectedItem.Id);

                    List<InputInfo> inputInfoList = new List<InputInfo>();
                    foreach (var objectt in objectList)
                    {
                        var removeList = DataProvider.Ins.DB.InputInfoes.Where(x => x.IdObject == objectt.Id);
                        inputInfoList.AddRange(removeList);
                    }
                    
                    List<Input> inputList = new List<Input>();
                    foreach (var inputInfo in inputInfoList)
                        inputList.Add(DataProvider.Ins.DB.Inputs.First(x => x.Id == inputInfo.IdInput));

                    Console.WriteLine(inputInfoList.Count());
                    DataProvider.Ins.DB.InputInfoes.RemoveRange(inputInfoList);
                    DataProvider.Ins.DB.Inputs.RemoveRange(inputList);

                    List<OutputInfo> outputInfoList = new List<OutputInfo>();
                    foreach (var objectt in objectList)
                    {
                        var removeList = DataProvider.Ins.DB.OutputInfoes.Where(x => x.IdObject == objectt.Id);
                        outputInfoList.AddRange(removeList);
                    }

                    List<Output> outputList = new List<Output>();
                    foreach (var outputInfo in outputInfoList)
                        outputList.Add(DataProvider.Ins.DB.Outputs.First(x => x.Id == outputInfo.IdOutput));

                    DataProvider.Ins.DB.OutputInfoes.RemoveRange(outputInfoList);
                    DataProvider.Ins.DB.Outputs.RemoveRange(outputList);

                    DataProvider.Ins.DB.Objects.RemoveRange(objectList);
                    DataProvider.Ins.DB.Supliers.RemoveRange(DataProvider.Ins.DB.Supliers.Where(x => x.Id == SelectedItem.Id));


                    DataProvider.Ins.DB.SaveChanges();
                    LoadData();
                }
            });
        }

        void LoadData()
        {
            List = new ObservableCollection<Suplier>(DataProvider.Ins.DB.Supliers);
        }
    }
}
