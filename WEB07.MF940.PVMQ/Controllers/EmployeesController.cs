using MF940.Core.Entities.Interfaces.MISAAtrribute;
using MF940.Core.Entities.Interfaces.Repository;
using MF940.Core.Entities.Interfaces.Respository;
using MF940.Core.Entities.Interfaces.Services;
using MF940.Core.Entities.ModelEmployee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WEB07.MF940.PVMQ.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : BaseEntityController<Employee>
    {
        #region DECLARE

        IEmployeeService _employeeService;
        IEmployeeRepository _employeeRepository;
        IBaseRepository<Employee> _baseRepository;
        IBaseService<Employee> _baseService;
        #endregion

        #region Constructor
        public EmployeesController(IEmployeeService employeeService, IEmployeeRepository employeeRepository, IBaseRepository<Employee> baseRepository, IBaseService<Employee> baseService) : base(employeeService, baseRepository)
        {
            _employeeService = employeeService;
            _employeeRepository = employeeRepository;
            _baseRepository = baseRepository;
            _baseService = baseService;
        }
        #endregion

        #region Method

        /// <summary>
        /// Phân trang 
        /// </summary>
        /// <param name="EmployeeFilter"></param>
        /// <param name="DepartmentId"></param>
        /// <param name="PostitionId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalPage"></param>
        /// <returns>Mảng chứa thông tin</returns>
        /// creatBy : PVM Quân(19/08/2021)
        [HttpGet("filter")]
        public IActionResult GetEmployeesFilterPaging([FromQuery] string EmployeeFilter, [FromQuery] Int32 pageIndex, [FromQuery] Int32 pageSize)
        {
            try
            {
                var employees = _employeeRepository.GetEmployeesFilterPaging(EmployeeFilter, pageIndex, pageSize,"");

                var response = StatusCode(200, employees);
                return response;
            }
            catch (Exception ex)
            {
                var erroObject = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.Erro500,
                    erorrCode = "misa-001",
                };
                return StatusCode(500, erroObject);
            }
        }
        /// <summary>
        /// Lấy mã mới 
        /// </summary>
        /// <returns>ServiceResult Kết quả xử lý nghiệp vụ</returns> 
        /// CreatBy : PVM.Quan (19/08/2021)
        [HttpGet("NewEmployeeCode")]
        public IActionResult GetNewCode()
        {
            try
            {
                var newCode = _employeeRepository.GetNewCode();
                if (newCode.ToString() != string.Empty)
                {
                    return StatusCode(200, newCode);

                }
                else
                {
                    return BadRequest(newCode);
                }
            }
            catch (Exception ex)
            {
                var erroObject = new
                {
                    devMsg = ex.Message,
                    usermsg = Properties.Resources.Erro500,
                    erorrCode = "misa-001",
                };
                return StatusCode(500, erroObject);
            }
        }
        /// <summary>
        /// Export du lieu
        /// </summary>
        /// <returns></returns>
        [HttpGet("Export")]
  
        public IActionResult Export([FromQuery] string EmployeeFilter, [FromQuery] Int32 pageIndex, [FromQuery] Int32 pageSize)
        {

            // query data from database  
            Task.Yield();
            var employees = new List<Employee>();

            var datas = _employeeRepository.GetEmployeesFilterPaging(EmployeeFilter,pageIndex,pageSize,"export");
            foreach(var data in datas)
            {
            employees.Add(data);
            }
            foreach(var employee in employees)
            {
                employee.GenderName = ConvertGender(employee.Gender);
            }

            var stream = new MemoryStream();

            var properties = typeof(Employee).GetProperties();



            using (var package = new ExcelPackage(stream))
            {

                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells.LoadFromCollection(employees, true);
                var column = 1;

                foreach (var prop in properties)
                {
                    var propMISAExports = prop.GetCustomAttributes(typeof(MISAExport), true);
                    if(propMISAExports.Length> 0)
                    {

                    var isHidden = (propMISAExports[0] as MISAExport).isHidden;
                        workSheet.Cells.AutoFitColumns();

                        // 1. Xét các trường có export hay không 
                        if (isHidden)
                        {
                            workSheet.Column(column).Hidden = true;
                        }

                        // 2. Định dạng ngày tháng năm 
                        if (prop.PropertyType.Name.Contains(typeof(Nullable).Name) && prop.PropertyType.GetGenericArguments()[0] == typeof(DateTime))
                        {
                            workSheet.Column(column).Style.Numberformat.Format = "mm/dd/yyyy";
                        }
                        column++;
                    }


                }

                package.Save();
            }

            stream.Position = 0;
            string excelName = $"DanhSachThongTinNhânViên.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        
        }

        /// <summary>
        /// Chuyển dữ liệu giới tính 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ConvertGender(int? value)
        {
            if (value == 0)
            {
                return "Nam";
            }
            else if (value == 1)
            {
                return "Nữ";
            }
            else
            {
                return "Khác";
            }
        }

        /// <summary>
        /// Lấy bảng theo từng property
        /// </summary>
        /// <returns>ServiceResult Kết quả xử lý nghiệp vụ</returns> 
        /// CreatBy : PVM.Quan (19/08/2021)
        [HttpGet("property/{properTy}")]
        public IActionResult GetDataByProPerTy (string properTy)
        {
            try
            {
                var data = _employeeRepository.getDataByProperty(properTy);
                var response = StatusCode(200, data);
                return response;
            }
            catch (Exception ex)
            {
                var erroObject = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.Erro500,
                    erorrCode = "misa-001",
                };
                return StatusCode(500, erroObject);
            }
        }
        #endregion

    }
}
