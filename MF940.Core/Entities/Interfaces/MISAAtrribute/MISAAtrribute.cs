using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF940.Core.Entities.Interfaces.MISAAtrribute
{
    #region Validate 

    /// <summary>
    /// Kiểm tra dữ liệu không để trống  
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MISARequired : Attribute
    {
        public string FieldName = string.Empty;

        public MISARequired(string fieldName)
        {
            FieldName = fieldName;
        }
    }

    /// <summary>
    /// Kiểm tra dữ liệu nào được export 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MISAExport : Attribute
    {
        public readonly bool isHidden;
        public MISAExport(bool hidden)
        {
            this.isHidden = !hidden;
        }
    }
    #endregion
}
