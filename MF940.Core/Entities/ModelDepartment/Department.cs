using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF940.Core.Entities.ModelDepartment
{
    public class Department : BaseEntity
    {
        #region Property 
        /// <summary>
        /// Khóa Chính
        /// </summary>
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// Mã phòng 
        /// </summary>

        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên phòng  
        /// </summary>
        public string DepartmentName { get; set; }
        #endregion
    }
}
