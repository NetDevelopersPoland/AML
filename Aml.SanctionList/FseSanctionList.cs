using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aml.SanctionList.Model;

namespace Aml.SanctionList
{
    public class FseSanctionList : SanctionListBase
    {
        /// <summary>
        /// Creates new Api instance
        /// </summary>
        public FseSanctionList(ISLDataSource slDataSource)
        {
            _slDataSource = slDataSource;
        }

        public List<SdnEntity> GetActualSanctionList()
        {
            MemoryStream tempStream = GetActualStrem();
            List<SdnEntity> entityList =new List<SdnEntity>();
            using (StreamReader streamReader = new StreamReader(tempStream, _slDataSource.GetEncoding()))
            {
                XNamespace ns = ApiConfiguration.SdnNamespace;
                XDocument xmlDocument = XDocument.Load(streamReader);
            }

            return entityList;
        }
    }
}
