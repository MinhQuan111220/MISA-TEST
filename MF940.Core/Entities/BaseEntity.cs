using MF940.Core.Entities.Interfaces.MISAAtrribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF940.Core.Entities
{
    public class BaseEntity
    {
        #region Property

        /// <summary>
        /// Thời gian tạo 
        /// </summary>
        [MISAExport(false)]
        public DateTime? createdDate { get; set; }

        /// <summary>
        /// Người tạo 
        /// </summary>
        [MISAExport(false)]
        public String createdBy { get; set; }

        /// <summary>
        /// Thời gian sửa đổi
        /// </summary>
        [MISAExport(false)]
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa đổi
        /// </summary>
        [MISAExport(false)]
        public String ModifiedBy { get; set; }
        #endregion
    }
}
