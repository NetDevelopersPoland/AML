using System;
using System.IO;
using System.Text;

namespace Aml.SanctionList
{
	public class ConsolidatedDataSource : SlDataSource, ISLDataSource, IDisposable
	{
		public virtual Stream GetSanctionListDataSource()
		{
			return GetCustomSanctionListDataSource(ApiConfiguration.ConsolidatedDataSourceUrl);
		}
		public string GetSanctionListFileName()
		{
			return ApiConfiguration.ConsolidatedFileName;
		}
		public Encoding GetEncoding()
		{
			return ApiConfiguration.Etf8Encoding;
		}
	}
}

