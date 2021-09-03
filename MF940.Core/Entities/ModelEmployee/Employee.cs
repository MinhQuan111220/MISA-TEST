using MF940.Core.Entities.Interfaces.MISAAtrribute;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF940.Core.Entities.ModelEmployee
{
    public class Employee : BaseEntity
    {
        #region Property
        /// <summary>
        /// Khóa Chính
        /// </summary>
        [MISAExport(false)]
        public Guid? EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên 
        /// </summary>
        [Description("Mã Nhân Viên ")]
        [MISAExport(true)]
        [MISARequired("Mã Nhân Viên")]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Họ và tên
        /// </summary>
        [Description("Tên Nhân Viên ")]
        [MISAExport(true)]
        public string FullName { get; set; }


        /// <summary>
        /// Giới tính
        /// </summary>
        [MISAExport(false)]
        public int? Gender { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        [Description("Giới tính ")]
        [MISAExport(true)]
        public string GenderName { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>

        [MISAExport(false)]
        public string Address { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        [Description("Ngày Sinh")]
        [MISAExport(true)]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [MISAExport(false)]
        public string Email { get; set; }

        /// <summary>
        /// Số điện thoại di động 
        /// </summary>
        [MISAExport(false)]
        public string MobiPhoneNumber { get; set; }

        /// <summary>
        /// Số điện thoại cố định 
        /// </summary>
        [MISAExport(false)]
        public string TelePhoneNumber { get; set; }

        /// <summary>
        /// Số chứng minh thư
        /// </summary>
        [MISAExport(false)]
        public string IdentityNumber { get; set; }

        /// <summary>
        /// Ngày tạo chứng minh thư
        /// </summary>
        [MISAExport(false)]
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Nơi tạo cmt
        /// </summary>
        [MISAExport(false)]
        public string IdentityPlace { get; set; }

        /// <summary>
        /// Tài khoản ngân hàng 
        /// </summary>
        [Description("Số tài khoản ")]
        [MISAExport(true)]
        public string  BankAccount { get; set; }

        /// <summary>
        /// Tên ngân hàng 
        /// </summary>
        [Description("Ngân hàng ")]
        [MISAExport(true)]
        public string BankName { get; set; }

        /// <summary>
        /// ID Đơn vị 
        /// </summary>
        [MISAExport(false)]
        public Guid? DepartmentId { get; set; }

        // <summary>
        /// Chức danh  
        /// </summary>
        [Description("Chức danh ")]
        [MISAExport(true)]
        public string PositionName { get; set; }

        /// <summary>
        /// Đơn vị 
        /// </summary>
        [Description("Tên đơn vị ")]
        [MISAExport(true)]
        public string DepartmentName { get; set; }

        /// <summary>
        /// Chi nhánh  
        /// </summary>
        [MISAExport(false)]
        public string Branch  { get; set; }

        /// <summary>
        /// Mảng chứa những lỗi sai khi import từ file 
        /// </summary>
        [MISAExport(false)]
        public List<string> ImportError { get; set; } = new List<string>();
        #endregion
    }
}
