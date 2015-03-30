using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aml.SanctionList
{
    public class FseDataSource : SlDataSource, ISLDataSource, IDisposable
    {
        public virtual Stream GetSanctionListDataSource()
        {
            return GetCustomSanctionListDataSource(ApiConfiguration.FseDataSourceUrl);
        }
        public string GetSanctionListFileName()
        {
            return ApiConfiguration.FseFileName;
        }
    }
}