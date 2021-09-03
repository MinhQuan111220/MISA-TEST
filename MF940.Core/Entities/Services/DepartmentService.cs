using MF940.Core.Entities.Interfaces.Respository;
using MF940.Core.Entities.Interfaces.Services;
using MF940.Core.Entities.ModelDepartment;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF940.Core.Entities.Services
{
    public class DepartmentService : BaseService<Department>, IDepartmentService
    {
        #region DECLARE


        #endregion

        #region Constructor

        public DepartmentService(IBaseRepository<Department> baseRepository, IConfiguration configuration) : base(baseRepository, configuration)

        {

        }
        #endregion

    }
}
