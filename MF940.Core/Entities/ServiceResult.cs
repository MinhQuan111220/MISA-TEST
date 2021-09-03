using MF940.Core.Entities.ModelEmployee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF940.Core.Entities
{
    public class ServiceResult
    {
        #region Validate 

        /// <summary>
        /// Kiểm tra đúng sai sai roi :)) e de no o cai list thoi
        /// </summary>
        public bool isValid { get; set; } = true;

        /// <summary>
        /// Lưu object thông báo
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Mảng chứa dữ liệu import customer 
        /// </summary>
        public List<Employee> DataEmployee { get; set; }

        /// <summary>
        /// Thông báo 
        /// </summary>
        public string Messenger { get; set; }

        /// <summary>
        /// Trả về số lượng
        /// </summary>
        public int? RowAffect { get; set; } = null;

        /// <summary>
        /// Trả về các trạng thái 
        /// </summary>
        public string StatusCode { get; set; }
        #endregion
    }
}
