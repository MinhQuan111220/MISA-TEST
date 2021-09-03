using MF940.Core.Entities.ModelEmployee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF940.Core.Entities.Interfaces.Repository
{
    public interface IEmployeeRepository
    {
        #region Method 

        /// <summary>
        /// Phân trang 
        /// </summary>
        /// <param name="EmployeeFilter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalPage"></param>
        /// <returns>Mảng chứa thông tin</returns>
        /// creatBy : PVM Quân(26/08/2021)
        dynamic GetEmployeesFilterPaging(string EmployeeFilter, int pageIndex, int pageSize,string check);

        /// <summary>
        /// Lấy mã mới 
        /// </summary>
        /// <returns>ServiceResult Kết quả xử lý nghiệp vụ</returns> 
        /// CreatBy : PVM.Quan (26/08/2021)
        string GetNewCode();

        

        /// <summary>
        /// Lấy 1 mảng  
        /// </summary>
        /// <param property="property">Tên chứa dữ liệu  </param>
        /// <returns>ServiceResult Kết quả xử lý nghiệp vụ</returns> 
        /// CreatBy : PVM.Quan (26/08/2021)
        public List<string> getDataByProperty(string property);

        /// <summary>
        /// Lấy Code theo id  
        /// </summary>
        /// <returns>ServiceResult Kết quả xử lý nghiệp vụ</returns> 
        /// CreatBy : PVM.Quan (26/08/2021)
        public List<string> getCodeById(Guid? Id);

        /// <summary>
        /// Lấy Id đơn vị theo tên đơn vị 
        /// </summary>
        /// <returns>ServiceResult Kết quả xử lý nghiệp vụ</returns> 
        /// CreatBy : PVM.Quan (26/08/2021)
        public Guid? getDepartmentId (string DepartmentName);
        #endregion
    }
}
