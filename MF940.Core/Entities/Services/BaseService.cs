using MF940.Core.Entities.Interfaces.MISAAtrribute;
using MF940.Core.Entities.Interfaces.Respository;
using MF940.Core.Entities.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF940.Core.Entities.Services
{
    public class BaseService<MISAEntity> : IBaseService<MISAEntity>
    {
        #region DECLARE

        IBaseRepository<MISAEntity> _baseRepository;
        protected ServiceResult _serviceResult;

        // 1. Khai báo thông tin kết nối với database
        public readonly string connectionString;
        #endregion

        #region Constructor
        public BaseService(IBaseRepository<MISAEntity> baseRepository, IConfiguration configuration)
        {
            _baseRepository = baseRepository;
            _serviceResult = new ServiceResult();
            connectionString = configuration.GetConnectionString("CukCukDataBase");
        }

        #endregion

        #region Method

        /// <summary>
        /// Thêm mới 
        /// </summary>
        /// <param name="entity">Thông tin </param>
        /// <returns>ServiceResult Kết quả xử lý nghiệp vụ</returns>
        /// CreatBy : PVM.Quan (26/08/201)
        public ServiceResult Add(MISAEntity entity)
        {
            // Validate dữ liệu 
            var check = ValidateData(entity);

            var isValid = true;
            if (check != 0)
            {
                isValid = false;
                // 1 . Không được để trống 
                if (check == 1)
                {
                    var erroObject = new
                    {

                        userMsg = _serviceResult.Messenger,
                        erorrCode = "misa-001",
                    };
                    _serviceResult.Data = erroObject;
                    _serviceResult.isValid = isValid;
                }
            }
            else
            {
                // Validate dữ liệu từ con 
                isValid = ValidateCustom(entity, null,_serviceResult.Messenger);
                if (isValid == false)
                {
                    var erroObject = new
                    {

                        userMsg = _serviceResult.Messenger,
                        erorrCode = "misa-001",
                    };
                    _serviceResult.Data = erroObject;
                    _serviceResult.isValid = isValid;
                }
                else if (isValid == true)
                {
                    _serviceResult.Data = _baseRepository.Add(entity);
                    _serviceResult.isValid = isValid;

                }


            }

            return _serviceResult;

        }

        /// <summary>
        /// Cập nhật thông tin theo Id
        /// </summary>
        /// <param name="entity">Thông tin </param>
        /// <param name="entityId">Id </param>
        /// <returns>ServiceResult Kết quả xử lý nghiệp vụ</returns> 
        /// CreatBy : PVM.Quan (26/08/201)
        public ServiceResult Update(MISAEntity entity, Guid? entityId)
        {
            // Validate dữ liệu 
            var check = ValidateData(entity);
            var isValid = true;
            if (check == 1)
            {
                isValid = false;
                // 1 . Không được để trống 
                if (check == 1)
                {
                    var erroObject = new
                    {

                        userMsg = _serviceResult.Messenger,
                        erorrCode = "misa-001",
                    };
                    _serviceResult.Data = erroObject;
                    _serviceResult.isValid = isValid;
                }

            }
            else
            {
                isValid = ValidateCustom(entity, entityId,_serviceResult.Messenger);
                if (isValid == false)
                {
                    var erroObject = new
                    {

                        userMsg = _serviceResult.Messenger,
                        erorrCode = "misa-001",
                    };
                    _serviceResult.Data = erroObject;
                    _serviceResult.isValid = isValid;
                }
                else if (isValid == true)
                { 
                _serviceResult.Data = _baseRepository.Update(entity, entityId);
                _serviceResult.isValid = true;
                }


            }

            return _serviceResult;
        }
        #endregion

        #region Validate

        /// <summary>
        /// Kiêm tra dữ liệu chung
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>0-dữ liệu hợp lệ , !0 dữ liệu không hợp lệ</returns> 
        /// creatBy : PVMQuan(28/08/2021)
        private int ValidateData(MISAEntity entity)
        {
            // Lấy tên của class
            var className = typeof(MISAEntity).Name;
            // Đọc từng property của object
            var properties = entity.GetType().GetProperties();
            var check = 0;

            // Duyệt từng property
            foreach (var prop in properties)
            {
                // Lấy tên của prop :
                var propName = prop.Name;
                // Lấy ra value của prop :
                var propValue = prop.GetValue(entity);

                if (propName == $"{className}Id" && propValue == null)
                {
                    propValue = Guid.NewGuid();
                }

                // 1. Kiểm tra xem property hiện tại có bắt buộc nhập hay không 
                var propMISARequireds = prop.GetCustomAttributes(typeof(MISARequired), true);

                if (propMISARequireds.Length > 0)
                {
                    var fieldName = (propMISARequireds[0] as MISARequired).FieldName;
                    if ((prop.PropertyType == typeof(string) || prop.PropertyType == typeof(Guid)) && (propValue == null || propValue.ToString() == string.Empty))
                    {
                        check = 1;
                        _serviceResult.Messenger = fieldName + " " + Properties.Resources.Emty;
                        break;
                    }
                    
                    if (propName == $"{className}Code")
                    {
                        _serviceResult.Messenger = propValue.ToString();
                    }
                }
                
            }

            return check;
        }

        protected virtual bool ValidateCustom(MISAEntity entity, Guid? id , string code)
        {
            return true;
        }
        #endregion
    }
}
