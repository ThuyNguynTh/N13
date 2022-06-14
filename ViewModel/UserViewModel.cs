using Nhom13_Quan_ly_kho_hang.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Forms;
using System.Windows;

namespace Nhom13_Quan_ly_kho_hang.ViewModel
{
    public class UserViewModel : BaseViewModel
    {
        public bool IsLogout { get; set; }
        private string _UserName;

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        private ObservableCollection<UserRole> _Role;

        public ObservableCollection<UserRole> Role
        {
            get { return _Role; }
            set { _Role = value; }
        }

        private UserRole _SelectedRole;

        public UserRole SelectedRole
        {
            get { return _SelectedRole; }
            set 
            { 
                _SelectedRole = value;
            }
        }

        private string _DisplayName;

        public string DisplayName
        {
            get { return _DisplayName; }
            set { _DisplayName = value; }
        }

        private ObservableCollection<Model.User> _List;

        public ObservableCollection<Model.User> List
        {
            get { return _List; }
            set { _List = value; }
        }

        private Model.User _SelectedItem;

        public Model.User SelectedItem
        {
            get { return _SelectedItem; }
            set 
            { 
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    DisplayName = SelectedItem.DisplayName;
                    SelectedRole = SelectedItem.UserRole;
                    UserName = SelectedItem.UserName;
                }
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand LogoutCommand { get; set; }

        public UserViewModel()
        {
            LoadData();
            IsLogout = false;
            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                var hasUser = DataProvider.Ins.DB.Users.Where(x => x.UserName.Equals(UserName));
                if (hasUser.Count() != 0)
                    System.Windows.Forms.MessageBox.Show("Username không thể trùng nhau", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                else
                {
                    var userr = new User()
                    {
                        UserName = UserName,
                        DisplayName = DisplayName,
                        IdRole = SelectedRole.Id,
                        PassWord = "demo",
                        Id = (new Random()).Next(1, 1000)
                    };
                    DataProvider.Ins.DB.Users.Add(userr);
                    DataProvider.Ins.DB.SaveChanges();
                    List.Add(userr);
                }    
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                return true;
            }, (p) =>
            {
                var hasUser = DataProvider.Ins.DB.Users.Where(x => x.UserName.Equals(UserName));
                if (hasUser.Count() != 0 && UserName != SelectedItem.UserName)
                    System.Windows.Forms.MessageBox.Show("Username không thể trùng nhau", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                else
                {
                    var userr = DataProvider.Ins.DB.Users.First(x => x.Id == SelectedItem.Id);
                    userr.UserName = UserName;
                    userr.DisplayName = DisplayName;
                    userr.IdRole = SelectedRole.Id;
                    DataProvider.Ins.DB.SaveChanges();
                }    
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                return true;
            }, (p) =>
            {
                DialogResult result = System.Windows.Forms.MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản này không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    DataProvider.Ins.DB.Users.Remove(DataProvider.Ins.DB.Users.First(x => x.Id == SelectedItem.Id));
                    DataProvider.Ins.DB.SaveChanges();
                    LoadData();
                }    
            });

            LogoutCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                Logout(p);
            });
        }

        void LoadData()
        {
            Role = new ObservableCollection<UserRole>(DataProvider.Ins.DB.UserRoles);
            List = new ObservableCollection<User>(DataProvider.Ins.DB.Users);
            if (Role.Count() != 0)
                SelectedRole = Role[0];
        }

        void Logout(Window p)
        {
            if (p == null)
                return;
            else
            {
                IsLogout = true;
                p.Close();
            }    
        }
    }
}
