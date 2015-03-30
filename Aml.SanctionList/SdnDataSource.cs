using System;
using System.IO;
using System.Text;

namespace Aml.SanctionList
{
    public class SdnDataSource : SlDataSource, ISLDataSource, IDisposable
    {
        public virtual Stream GetSanctionListDataSource()
        {
            return GetCustomSanctionListDataSource(ApiConfiguration.SdnDataSourceUrl);
        }
        public string GetSanctionListFileName()
        {
            return ApiConfiguration.SdnFileName;
        }
        public Encoding GetEncoding()
        {
            return ApiConfiguration.Etf8Encoding;
        }
    }
}