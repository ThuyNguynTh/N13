using Nhom13_Quan_ly_kho_hang.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Nhom13_Quan_ly_kho_hang.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private ObservableCollection<TonKho> _TonKhoList;
        public ObservableCollection<TonKho> TonKhoList { get => _TonKhoList; set { _TonKhoList = value; OnPropertyChanged(); } }
        public bool Isloaded = false;
        private int _TongNhap;

        public int TongNhap
        {
            get { return _TongNhap; }
            set { _TongNhap = value; OnPropertyChanged(); }
        }
        private int _TongXuat;

        public int TongXuat
        {
            get { return _TongXuat; }
            set { _TongXuat = value; OnPropertyChanged(); }
        }
        private int _TongTon;

        public int TongTon
        {
            get { return _TongTon; }
            set { _TongTon = value; OnPropertyChanged(); }
        }

        public ICommand LoadedWindowCommand { get; set; }
        public ICommand UnitCommand { get; set; }
        public ICommand SuplierCommand { get; set; }
        public ICommand CustomerCommand { get; set; }
        public ICommand ObjectCommand { get; set; }
        public ICommand UserCommand { get; set; }
        public ICommand InputCommand { get; set; }
        public ICommand OutputCommand { get; set; }



        public MainViewModel()
        {
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
             {
                 Isloaded = true;
                 if (p == null)
                     return;
                 p.Hide();
                 LoginWindow loginWindow = new LoginWindow();
                 loginWindow.ShowDialog();

                 if (loginWindow.DataContext == null)
                     return;
                 var loginVM = loginWindow.DataContext as LoginViewModel;

                 if (loginVM.IsLogin)
                 {
                     p.Show();
                     LoadTonKhoData();
                 }
                 else
                     p.Close();
             }
             );

            UnitCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                UnitWindow wd = new UnitWindow();
                wd.ShowDialog();
            }
            );
            SuplierCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                SuplierWindow wd = new SuplierWindow();
                wd.ShowDialog();
            }
            );
            CustomerCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CustomerWindow wd = new CustomerWindow();
                wd.ShowDialog();
            }
            );
            ObjectCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ObjectWindow wd = new ObjectWindow();
                wd.ShowDialog();
            }
            );
            UserCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                UserWindow wd = new UserWindow();
                wd.ShowDialog();
            }
            );
            InputCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                InputWindow wd = new InputWindow();
                wd.ShowDialog();
                LoadTonKhoData();
            }
            );
            OutputCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                OutputWindow wd = new OutputWindow();
                wd.ShowDialog();
                LoadTonKhoData();
            }
           );
        }
        void LoadTonKhoData()
        {
            TonKhoList = new ObservableCollection<TonKho>();

            var objectList = DataProvider.Ins.DB.Objects.OrderBy(item => item.DisplayName);
            int i = 1;
            TongNhap = TongXuat = 0;
            foreach(var item in objectList)
            {
                var inputList  = DataProvider.Ins.DB.InputInfoes.Where( p => p.IdObject == item.Id); //lấy thông tin input infor có idOject = object đang tính
                var outputList = DataProvider.Ins.DB.OutputInfoes.Where(p => p.IdObject == item.Id);

                int sumInput = 0;
                int sumOutput = 0;
                
                if(inputList != null && inputList.Count() > 0)
                {
                    sumInput = (int)inputList.Sum(p => p.Count);
                    TongNhap += sumInput;
                }
                if (outputList != null && outputList.Count() > 0)
                {
                    sumOutput = (int)outputList.Sum(p => p.Count);
                    TongXuat += sumOutput;
                }

                TonKho tonkho = new TonKho();
                tonkho.STT = i;
                tonkho.Count = sumInput - sumOutput;
                tonkho.Object = item;

                TonKhoList.Add(tonkho);

                i++;
            }
            TongTon = TongNhap - TongXuat;
        }
    }
}
