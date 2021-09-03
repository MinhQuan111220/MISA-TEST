using Dapper;
using MF940.Core.Entities.Interfaces.Respository;
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
    public class BaseRepository<MISAEntity> : IBaseRepository<MISAEntity>
    {
        #region DECLARE

        // 1. Khai báo thông tin kết nối với database
        public readonly string connectionString;
        string className;
        #endregion
        #region Constructor
        public BaseRepository(IConfiguration configuration)
        {
            // Lấy tên của class
            className = typeof(MISAEntity).Name;

            connectionString = configuration.GetConnectionString("CukCukDataBase");

        }
        #endregion

        #region Method

        /// <summary>
        /// Thêm mới 
        /// </summary>
        /// <param name="entity">Thông tin </param>
        /// <returns>ServiceResult Kết quả xử lý nghiệp vụ</returns>
        /// CreatBy : PVM.Quan (26/08/2021)
        public Int32 Add(MISAEntity entity)
        {
            using (IDbConnection dbConnection = new MySqlConnection(connectionString))
            {
                var dynamicParam = new DynamicParameters();

                ////3. Them du lieu vao db:

                ////Doc tung prop cua obj:
                var properties = entity.GetType().GetProperties();

                ////Duyet tung prop:
                foreach (var prop in properties)
                {
                    //lay ten cua prop
                    var propName = prop.Name;

                    //lay val cu prop
                    var propValue = prop.GetValue(entity);

                    if (propName == $"{className}Id" && prop.PropertyType == typeof(Guid))
                    {
                        propValue = Guid.NewGuid();
                    }
                    if (propName == "createdDate")
                    {
                        propValue = DateTime.Now;
                    }

                    //Them param tuong ung voi moi prop
                    dynamicParam.Add($"@{propName}", propValue);
                }
                var rowEffects = dbConnection.Execute($"Proc_Insert{className}", param: dynamicParam, commandType: CommandType.StoredProcedure);

                return rowEffects;
            }


        }


        /// <summary>
        /// Xóa  theo Id
        /// </summary>
        /// <param name="entityId">Id khách hàng</param>
        /// <returns>ServiceResult Kết quả xử lý nghiệp vụ</returns>
        /// CreatBy : PVM.Quan (26/08/2021)
        public Int32 Delete(Guid? entityId)
        {

            var classNameId = $"{className}Id";
            // Truy cập vào database 


            // 2. Khởi tạo đối tượng kết nối với database
            using (IDbConnection dbConnection = new MySqlConnection(connectionString))
            {
                // 4. Xoá dữ liệu theo id
                string sqlCommandDelete = $"DELETE FROM {className} WHERE {classNameId} = @CustomerIdParam";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CustomerIdParam", entityId);
                var rowEffects = dbConnection.Execute(sqlCommandDelete, param: parameters);
                return rowEffects;
            }
        }

        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns>ServiceResult Kết quả xử lý nghiệp vụ</returns>
        /// CreatBy : PVM.Quan (26/08/2021)
        public List<MISAEntity> GetAll()
        {

            // Truy cập vào database 

            // 2. Khởi tạo đối tượng kết nối với database
            using (IDbConnection dbConnection = new MySqlConnection(connectionString))
            {
                // 3. Lấy dữ liệu
                var entities = dbConnection.Query<MISAEntity>($"Proc_Get{className}s", commandType: CommandType.StoredProcedure);
                return (List<MISAEntity>)entities;
            }
        }

        /// <summary>
        /// Lấy dữ liệu khách  Id
        /// </summary>
        /// <param name="entityId">Id khách hàng</param>
        /// <returns>ServiceResult Kết quả xử lý nghiệp vụ</returns> 
        /// CreatBy : PVM.Quan (26/08/2021)
        public Object GetById(Guid? entityId)
        {
            // Truy cập vào database 

            // 2. Khởi tạo đối tượng kết nối với database
            using (IDbConnection dbConnection = new MySqlConnection(connectionString))
            {
                // 3. Lấy dữ liệu 
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add($"@{className}Id", entityId);
                var entity = dbConnection.QueryFirstOrDefault<MISAEntity>($"Proc_Get{className}ById ", param: parameters, commandType: CommandType.StoredProcedure);
                return entity;
            }

        }

        /// <summary>
        /// Cập nhật thông tin theo Id
        /// </summary>
        /// <param name="entity">Thông tin </param>
        /// <param name="entityId">Id </param>
        /// <returns>ServiceResult Kết quả xử lý nghiệp vụ</returns>
        /// CreatBy : PVM.Quan (26/08/2021)
        public Int32 Update(MISAEntity entity, Guid? entityId)
        {

            var classNameId = $"{className}Id";
            // Truy cập vào database 


            // 2. Khởi tạo đối tượng kết nối với database
            using (IDbConnection dbConnection = new MySqlConnection(connectionString))
            {

                var dynamicParam = new DynamicParameters();

                ////3. Them du lieu vao db:

                ////Doc tung prop cua obj:
                var properties = entity.GetType().GetProperties();

                ////Duyet tung prop:
                foreach (var prop in properties)
                {
                    //lay ten cua prop
                    var propName = prop.Name;

                    //lay val cu prop
                    var propValue = prop.GetValue(entity);

                    //Them param tuong ung voi moi prop

                    dynamicParam.Add($"@{propName}", propValue);

                }

                var rowsEffect = dbConnection.Execute($"Proc_Update{className}", param: dynamicParam, commandType: CommandType.StoredProcedure);

                return rowsEffect;
            }
        }
        #endregion

        #region validate
        /// <summary>
        /// Kiểm tra mã bị trùng 
        /// </summary>
        /// <param name="entityCode">Id khách hàng</param>
        /// <returns>ServiceResult Kết quả xử lý nghiệp vụ</returns> 
        /// CreatBy : PVM.Quan (26/08/2021)
        public bool checkDuplicateCode(String entityCode)
        {
            var propName = $"{className}Code";
            // 2. Khởi tạo đối tượng kết nối với database
            using (IDbConnection dbConnection = new MySqlConnection(connectionString))
            {
                // 3. Lấy dữ liệu 
                string sqlCommand = $"SELECT * FROM {className} WHERE {propName} = @entityCodeParam";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@EntityCodeParam", entityCode);
                var res = dbConnection.QueryFirstOrDefault(sqlCommand, param: parameters);
                if (res != null)
                {
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// Xóa nhieu theo Id
        /// </summary>
        /// <param name="entityId">Id </param>
        /// <returns>ServiceResult Kết quả xử lý nghiệp vụ</returns>
        /// CreatBy : PVM.Quan (26/08/2021)
        public Int32 DeleteListId(List<Guid> entityIds)
        {
            // Truy cập vào database 
            var rowEffects = 0;
            // 2. Khởi tạo đối tượng kết nối với database
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            dbConnection.Open();
            var transaction = dbConnection.BeginTransaction();
            // Khai báo dynamicParam
            var dynamicParam = new DynamicParameters();
            foreach (var id in entityIds)
            {
                dynamicParam.Add($"@m_{className}Id", id.ToString());
                rowEffects += dbConnection.Execute($"Proc_Delete{className}ById", param: dynamicParam, transaction: transaction, commandType: CommandType.StoredProcedure);
            }
            transaction.Commit();
            if (rowEffects == entityIds.Count)
            {
                return rowEffects;
            }
            else
            {
                return 0;
            }
        }
        #endregion
    }
}
