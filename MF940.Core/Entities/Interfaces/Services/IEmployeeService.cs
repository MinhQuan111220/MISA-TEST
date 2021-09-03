using MF940.Core.Entities.ModelEmployee;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF940.Core.Entities.Interfaces.Services
{
    public interface IEmployeeService : IBaseService<Employee>
    {
        #region Method

        /// <summary>
        /// Export dữ liệu nhân viên  
        /// </summary>
        /// <returns>Danh sách nhân viên  dạng excel</returns>
        /// CreatBy : PVM.Quan (26/08/2021)
        ServiceResult Export();
        #endregion
    }
}
