using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aml.SanctionList.Model;
using Aml.LoggingService;

namespace Aml.SanctionList
{
	public class ConsolidatedSanctionList : SanctionListBase
	{
		/// <summary>
		/// Creates new Api instance
		/// </summary>
		public ConsolidatedSanctionList(ISLDataSource slDataSource)
		{
			_slDataSource = slDataSource;
		}
	}
}

