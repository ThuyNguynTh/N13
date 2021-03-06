//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Nhom13_Quan_ly_kho_hang.Model
{
    using Nhom13_Quan_ly_kho_hang.ViewModel;
    using System;
    using System.Collections.Generic;
    
    public partial class Object : BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Object()
        {
            this.OutputInfoes = new HashSet<OutputInfo>();
            this.InputInfoes = new HashSet<InputInfo>();
        }

        private string _Id;

        public string Id
        {
            get { return _Id; }
            set { _Id = value; OnPropertyChanged(); }
        }
        private string _DisplayName;

        public string DisplayName
        {
            get { return _DisplayName; }
            set { _DisplayName = value; OnPropertyChanged(); }
        }
        private int _IdUnit;

        public int IdUnit
        {
            get { return _IdUnit; }
            set { _IdUnit = value; }
        }
        private int _IdSuplier;

        public int IdSuplier
        {
            get { return _IdSuplier; }
            set { _IdSuplier = value; }
        }

        private string _ORCode;

        public string ORCode
        {
            get { return _ORCode; }
            set { _ORCode = value; }
        }
        private string _BarCode;

        public string BarCode
        {
            get { return _BarCode; }
            set { _BarCode = value; }
        }

        private Suplier _Suplier;

        public virtual Suplier Suplier
        {
            get { return _Suplier; }
            set { _Suplier = value; }
        }

        public virtual Unit Unit { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OutputInfo> OutputInfoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InputInfo> InputInfoes { get; set; }
    }
}
