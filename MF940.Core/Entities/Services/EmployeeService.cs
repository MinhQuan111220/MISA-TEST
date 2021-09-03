using MF940.Core.Entities.Interfaces.Repository;
using MF940.Core.Entities.Interfaces.Respository;
using MF940.Core.Entities.Interfaces.Services;
using MF940.Core.Entities.ModelEmployee;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MF940.Core.Entities.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        #region DECLARE
        IEmployeeRepository _employeeRepository;
        IBaseRepository<Employee> _baseRepository;
        #endregion

        #region Constructor

        public EmployeeService(IBaseRepository<Employee> baseRepository, IConfiguration configuration, IEmployeeRepository employeeRepository) : base(baseRepository, configuration)

        {
            _employeeRepository = employeeRepository;
            _baseRepository = baseRepository;

        }
        #endregion

        #region Validate
        /// <summary>
        /// Kiêm tra dữ liệu 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>0-dữ liệu hợp lệ , !0 dữ liệu không hợp lệ</returns>
        /// creatBy : PVMQuan(26/08/2021)  
        protected override bool ValidateCustom(Employee entity, Guid? id, string code)
        {

            // 1. Kiểm tra đơn vị không được để trống 
            if (entity.DepartmentId == null)
            {
                _serviceResult.Messenger = Properties.Resources.EmtyDepartmentId;
                return false;
            }
            // 2. Kiểm tra ten không được để trống 
            if (entity.FullName == null)
            {
                _serviceResult.Messenger = Properties.Resources.EmtyFullName;
                return false;
            }
            //3. Kiểm tra mã bị trùng 
            var employee = _baseRepository.GetById(id);
            var employeeCode = new List<string>();
             employeeCode = _employeeRepository.getCodeById(id);
            var arrEmployeeCode = _employeeRepository.getDataByProperty("EmployeeCode");
            if(employee != null && employeeCode[0].ToString() == code)
            {
               
            }
            else
            {
                if (Checkduplicates(arrEmployeeCode, code) == false)
                {

                _serviceResult.Messenger = Properties.Resources.DoubleEmployeeCode;
                return false;
                }
            }

            return true;
        }
        #endregion
        #region Validate

        /// <summary>
        /// Export dữ liệu nhân viên  
        /// </summary>
        /// <returns>Danh sách nhân viên  dạng excel</returns>
        public ServiceResult Export()
        {
            //var customers = _baseRepository.GetAll();
            return _serviceResult;

        }

        /// <summary>
        /// Kiểm tra bị trùng  
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool Checkduplicates(List<string> array, string value)
        {
            var index = 0;
            foreach (var item in array)
            {
                if (value == item)
                {
                    index++;

                }
            }
            if (index == 1)
            {
                return false;
            }
            return true;
        }
        

        #endregion
    }
}