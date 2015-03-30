using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aml.SanctionList.Model;

namespace Aml.SanctionList
{
    public class PlcSanctionList : SanctionListBase
    {
        public PlcSanctionList()
            : this(new SlDataSource())
        {
        }
        /// <summary>
        /// Creates new Api instance
        /// </summary>
        public PlcSanctionList(ISLDataSource slDataSource)
        {
            _slDataSource = slDataSource;
        }
        public List<SdnEntity> GetActualSanctionList()
        {
            MemoryStream tempStream = GetActualStrem();
            List<SdnEntity> entityList;
            using (StreamReader streamReader = new StreamReader(tempStream, _slDataSource.GetEncoding()))// ApiConfiguration.Etf8Encoding))
            {
                XNamespace ns = _slDataSource.GetNamespace();
                XDocument xmlDocument = XDocument.Load(streamReader);

                var publshInformationElementList = xmlDocument.Descendants(ns + "publshInformation").ToList();
                var publshInformationElement = publshInformationElementList[0];
                var dateAttribute = publshInformationElement.Element(ns + "Publish_Date");
                var dateAtr = dateAttribute.Value.Split('/');
                var date = new DateTime(Convert.ToInt32(dateAtr[2]), Convert.ToInt32(dateAtr[0]), Convert.ToInt32(dateAtr[1]));
                var countAttribute = publshInformationElement.Element(ns + "Record_Count");
                int count = Int32.Parse(countAttribute.Value);
                entityList = new List<SdnEntity>(count);

                var xmlEntities = xmlDocument.Descendants(ns + "nspEntry").ToList();
                foreach (var xmlEntity in xmlEntities)
                {
                    SdnEntity entity = new SdnEntity()
                    {
                        uid = Convert.ToInt32(xmlEntity.Element(ns + "uid").Value),
                        firstName = GetXElementSafeValue(xmlEntity, ns, "firstName"),
                        lastName = GetXElementSafeValue(xmlEntity, ns, "lastName"),
                        type = GetXElementSafeValue(xmlEntity, ns, "nspType"),
                        ListDate = date
                    };

                    GetProgramList(entity, xmlEntity, ns);
                    GetAdressList(entity, xmlEntity, ns);
                    GetAkaList(entity, xmlEntity, ns);
                    GetIdList(entity, xmlEntity, ns);
                    entityList.Add(entity);
                }
            }
            return entityList;
        }
    }
}
