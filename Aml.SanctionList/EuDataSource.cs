using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aml.SanctionList
{
    public class EuDataSource : SlDataSource, ISLDataSource, IDisposable
    {
        public virtual Stream GetSanctionListDataSource()
        {
            return GetCustomSanctionListDataSource(ApiConfiguration.EuDataSourceUrl);
        }
        public string GetSanctionListFileName()
        {
            return ApiConfiguration.EuFileName;
        }
        public Encoding GetEncoding()
        {
            return ApiConfiguration.WesternEuropeEncoding;
        }
    }
}