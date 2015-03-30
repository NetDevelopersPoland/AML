using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aml.SanctionList.Model;

namespace Aml.SanctionList
{
    public class SdnSanctionList : SanctionListBase
    {
        public SdnSanctionList()
            : this(new SlDataSource())
        {
        }
        /// <summary>
        /// Creates new Api instance
        /// </summary>
        public SdnSanctionList(ISLDataSource slDataSource)
        {
            _slDataSource = slDataSource;
        }
    }
}