using Dapper;
using MF940.Core.Entities.Interfaces.Repository;
using MF940.Core.Entities.ModelDepartment;
using MF940.Core.Entities.ModelEmployee;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF940.Infarstructure.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        #region DECLARE

        // 1. Khai báo thông tin kết nối với database
        public readonly string connectionString;

        #endregion
        #region Constructor
        public EmployeeRepository(IConfiguration configuration)
        {

            connectionString = configuration.GetConnectionString("CukCukDataBase");

        }


        #endregion
        #region Method

        /// <summary>
        /// Phân trang 
        /// </summary>
        /// <param name="EmployeeFilter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalPage"></param>
        /// <returns>Mảng chứa thông tin</returns>
        /// creatBy : PVM Quân(19/08/2021)
        public dynamic GetEmployeesFilterPaging(String EmployeeFilter,Int32 pageIndex, Int32 pageSize, string check)
        {
            // 2.Khởi tạo đối tượng kết nối với database
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            // 3. Lấy dữ liệu 
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@EmployeeFilter", EmployeeFilter == null ? string.Empty : EmployeeFilter);
            parameters.Add("@pageIndex", pageIndex);
            parameters.Add("@pageSize", pageSize);
            parameters.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var data = dbConnection.Query<Employee>($"Proc_GetEmployeesFilterPaging", param: parameters, commandType: CommandType.StoredProcedure);

            var totalPage = parameters.Get<Int32>("@TotalPage");
            var totalRecord = parameters.Get<Int32>("@TotalRecord");

                var arr = new List<Array>();
            if(check == "")
            {

                var datas = new
                {
                    totalPage,
                    totalRecord,
                    data
                };

                return datas;
            }
            else
            {
                
                return data.ToList(); 
            }
        }
        /// <summary>
        /// Lấy mã mới 
        /// </summary>
        /// <returns>ServiceResult Kết quả xử lý nghiệp vụ</returns> 
        /// CreatBy : PVM.Quan (19/08/2021)
        public string GetNewCode()
        {
            // 2. Khởi tạo đối tượng kết nối với database
            using (IDbConnection dbConnection = new MySqlConnection(connectionString))
            {

                // 3. Lấy dữ liệu 
                string sqlCommand = $"SELECT EmployeeCode FROM Employee";

                var res = dbConnection.Query<string>(sqlCommand);
                var arr = res.ToList();
                var arrNumber = new List<int>();

                if (arr.Count == 0)
                {
                    return "NV-001";
                }
                else
                {
                    foreach (var code in arr)
                    {

                        arrNumber.Add(Int32.Parse(code.Substring(3)));
                    }
                    arrNumber.Sort((x, y) => y.CompareTo(x));
                    var numberCode = arrNumber[0] + 1;
                    var newCode = "NV-";

                    return newCode + "" + numberCode;
                }
            }
        }
        /// <summary>
        /// Kiểm tra  bị trùng 
        /// </summary>
        /// <param property="property">Tên chứa dữ liệu  </param>
        /// <returns>ServiceResult Kết quả xử lý nghiệp vụ</returns> 
        /// CreatBy : PVM.Quan (26/08/2021)
        public List<string> getDataByProperty(string property)
        {
            // 2. Khởi tạo đối tượng kết nối với database
            using (IDbConnection dbConnection = new MySqlConnection(connectionString))
            {

                // 3. Lấy dữ liệu 
                string sqlCommand = $"SELECT {property} FROM Employee";

                var res = dbConnection.Query<string>(sqlCommand);

                return res.ToList();
            }
        }
        /// <summary>
        /// Lấy Code theo id  
        /// </summary>
        /// <param property="property">Tên chứa dữ liệu  </param>
        /// <returns>ServiceResult Kết quả xử lý nghiệp vụ</returns> 
        /// CreatBy : PVM.Quan (26/08/2021)
        public List<string> getCodeById(Guid? Id)
        {
            // 2. Khởi tạo đối tượng kết nối với database
            using (IDbConnection dbConnection = new MySqlConnection(connectionString))
            {

                // 3. Lấy dữ liệu 
                var res = $"SELECT EmployeeCode FROM Employee WHERE EmployeeId = @EmployeeId ";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@EmployeeId", Id);
                var code = dbConnection.Query<string>(res,param:parameters);
                return code.ToList();
            }
        }
        /// <summary>
        /// Lấy Id đơn vị theo tên đơn vị 
        /// </summary>
        /// <returns>ServiceResult Kết quả xử lý nghiệp vụ</returns> 
        /// CreatBy : PVM.Quan (26/08/2021)
        public Guid? getDepartmentId(string DepartmentName)
        {
            // 2. Khởi tạo đối tượng kết nối với database
            using (IDbConnection dbConnection = new MySqlConnection(connectionString))
            {
                // 3. Lấy dữ liệu 
                string sqlCommand = $"SELECT * FROM Department WHERE DepartmentName = @DepartmentNameParam";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@DepartmentNameParam", DepartmentName);
                var res = dbConnection.QueryFirstOrDefault<Department>(sqlCommand, param: parameters);
                if (res != null)
                {
                    return res.DepartmentId;
                }
                return null;
            }
        }
        
        #endregion
    }
}
