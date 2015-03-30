using System;
using System.IO;
using System.Text;

namespace Aml.SanctionList
{
    public class PlcDataSource : SlDataSource, ISLDataSource, IDisposable
    {
        public virtual Stream GetSanctionListDataSource()
        {
            return GetCustomSanctionListDataSource(ApiConfiguration.PlcDataSourceUrl);
        }
        public string GetSanctionListFileName()
        {
            return ApiConfiguration.PlcFileName;
        }
        public Encoding GetEncoding()
        {
            return ApiConfiguration.Etf8Encoding;
        }
        public string GetNamespace()
        {
            return ApiConfiguration.PlcNamespace;
        }
    }
}