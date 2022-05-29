
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
    public class ObjectViewModel : BaseViewModel
    {
        private ObservableCollection<Model.Object> _List;
        public ObservableCollection<Model.Object> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Unit> _Unit;
        public ObservableCollection<Model.Unit> Unit { get => _Unit; set { _Unit = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Suplier> _Suplier;
        public ObservableCollection<Model.Suplier> Suplier { get => _Suplier; set { _Suplier = value; OnPropertyChanged(); } }


        //binding SelectedItem tu Objectwindow.xaml --> cap nhat o bien (1)
        private Model.Object _SelectedItem;
        public Model.Object SelectedItem //(1)
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value; OnPropertyChanged();
                if (SelectedItem != null)  // neu lua chon khong null
                {
                    DisplayName = SelectedItem.DisplayName; //cap nhat displayname truyen vao bien (2)
                    ORCode = SelectedItem.ORCode;
                    BarCode = SelectedItem.BarCode;
                    SelectedUnit = SelectedItem.Unit;
                    SelectedSuplier = SelectedItem.Suplier;

                }
            }
        }

        private Model.Unit _SelectedUnit;
        public Model.Unit SelectedUnit 
        {
            get => _SelectedUnit;
            set
            {
                _SelectedUnit = value; OnPropertyChanged();
            }
        }

        private Model.Suplier _SelectedSuplier;
        public Model.Suplier SelectedSuplier
        {
            get => _SelectedSuplier;
            set
            {
                _SelectedSuplier = value; OnPropertyChanged();
            }
        }

        private String _DisplayName;
        public string DisplayName //(2) --> cap nhat lại binging displayname ở file Objectwindow.xaml
        {
            get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); }
        }

        private String _ORCode;
        public string ORCode
        {
            get => _ORCode; set { _ORCode = value; OnPropertyChanged(); }
        }

        private String _BarCode;
        public string BarCode
        {
            get => _BarCode; set { _BarCode = value; OnPropertyChanged(); }
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
        public ObjectViewModel()
        {
            List = new ObservableCollection<Model.Object>(DataProvider.Ins.DB.Objects);
            Unit = new ObservableCollection<Model.Unit>(DataProvider.Ins.DB.Units);
            Suplier = new ObservableCollection<Model.Suplier>(DataProvider.Ins.DB.Supliers);


            AddCommand = new RelayCommand<object>
                (
                    (p) =>
                    {
                    if (SelectedSuplier == null || SelectedUnit == null)
                            return false;
                        return true;

                    },
                (p) =>
                {
                    var Object = new Model.Object() { DisplayName = DisplayName, BarCode = BarCode, ORCode = ORCode, IdSuplier = SelectedSuplier.Id, IdUnit = SelectedUnit.Id, Id = Guid.NewGuid().ToString()};
                    DataProvider.Ins.DB.Objects.Add(Object);
                    DataProvider.Ins.DB.SaveChanges();
                    List.Add(Object);
                });

            EditCommand = new RelayCommand<Model.Object>
                (
                    (p) =>
                    {
                        if (SelectedItem == null || SelectedSuplier == null || SelectedUnit == null)
                            return false;
                        var displayList = DataProvider.Ins.DB.Objects.Where(x => x.Id == SelectedItem.Id);
                        if (displayList != null && displayList.Count() != 0)
                        {
                            return true;
                        }
                        else return false;

                    },
                (p) =>
                {
                    var Object = DataProvider.Ins.DB.Objects.Where(x => x.Id == SelectedItem.Id).SingleOrDefault(); // lay Object tuong ung
                    Object.DisplayName = DisplayName;
                    Object.BarCode = BarCode; 
                    Object.ORCode = ORCode;
                    Object.IdSuplier = SelectedSuplier.Id;
                    Object.IdUnit = SelectedUnit.Id;
                    DataProvider.Ins.DB.SaveChanges();
                    SelectedItem.DisplayName = DisplayName; 

                    OnPropertyChanged();
                });
        }


    }
}
