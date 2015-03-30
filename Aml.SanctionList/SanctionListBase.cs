using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aml.SanctionList.Model;
using Aml.LoggingService;

namespace Aml.SanctionList
{
    public abstract class SanctionListBase : ISanctionList, IDisposable
    {
        protected ISLDataSource _slDataSource;
        protected ILoggingService _logger;
        ///// <summary>
        ///// Creates new Api instance
        ///// </summary>
        public SanctionListBase()
            : this(new SlDataSource(), new LoggingService.LoggingService())
        {
        }
        /// <summary>
        /// Creates new Api instance
        /// </summary>
        public SanctionListBase(ISLDataSource slDataSource, ILoggingService logger)
        {
            _slDataSource = slDataSource;
            _logger = logger;
        }
        protected MemoryStream GetActualStrem()
        {
            Stream actualExchangeRatesDataSourceStream = _slDataSource.GetSanctionListDataSource();
            if (actualExchangeRatesDataSourceStream.CanSeek)
                actualExchangeRatesDataSourceStream.Seek(0, SeekOrigin.Begin);

            MemoryStream tempStream = new MemoryStream();
            actualExchangeRatesDataSourceStream.CopyTo(tempStream);
            if (tempStream.CanSeek)
                tempStream.Seek(0, SeekOrigin.Begin);

            return tempStream;
        }
        public List<SdnEntity> GetActualSanctionList()
        {
            MemoryStream tempStream = GetActualStrem();
            List<SdnEntity> entityList;
            using (StreamReader streamReader = new StreamReader(tempStream, _slDataSource.GetEncoding()))
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

                var xmlEntities = xmlDocument.Descendants(ns + "sdnEntry").ToList();
                foreach (var xmlEntity in xmlEntities)
                {
                    var entity = GetSdnEntity(xmlEntity, ns, date);
                    GetProgramList(entity, xmlEntity, ns);
                    GetAdressList(entity, xmlEntity, ns);
                    GetAkaList(entity, xmlEntity, ns);
                    GetIdList(entity, xmlEntity, ns);

                    entityList.Add(entity);
                }
            }
            return entityList;
        }
        private SdnEntity GetSdnEntity(XElement xmlEntity, XNamespace ns, DateTime date)
        {
            SdnEntity entity = new SdnEntity()
            {
                uid = Convert.ToInt32(xmlEntity.Element(ns + "uid").Value),
                firstName = GetXElementSafeValue(xmlEntity, ns, "firstName"),
                lastName = GetXElementSafeValue(xmlEntity, ns, "lastName"),
                type = GetXElementSafeValue(xmlEntity, ns, "sdnType"),
                ListDate = date
            };
            return entity;
        }
        protected static void GetProgramList(SdnEntity entity, XElement xmlEntity, XNamespace ns)
        {
            entity.programList = new List<SdnProgramList>();
            var xmlProgramList = xmlEntity.Descendants(ns + "programList").ToList();
            foreach (var xElement in xmlProgramList)
            {
                SdnProgramList list = new SdnProgramList()
                {
                    program = xElement.Element(ns + "program").Value
                };
                entity.programList.Add(list);
            }
        }
        protected void GetIdList(SdnEntity entity, XElement xmlEntity, XNamespace ns)
        {
            entity.idList = new List<SdnIdList>();
            var xmlIdList = xmlEntity.Descendants(ns + "idList").ToList();
            foreach (var xElement in xmlIdList)
            {
                var xmlId = xElement.Descendants(ns + "id").ToList();
                foreach (var xId in xmlId)
                {
                    SdnIdList list = new SdnIdList()
                    {
                        uid = Convert.ToInt32(xId.Element(ns + "uid").Value),
                        idType = GetXElementSafeValue(xId, ns, "idType"),
                        idNumber = GetXElementSafeValue(xId, ns, "idNumber"),
                        idCountry = GetXElementSafeValue(xId, ns, "idCountry")
                    };
                    entity.idList.Add(list);
                }
            }
        }
        protected void GetAkaList(SdnEntity entity, XElement xmlEntity, XNamespace ns)
        {
            entity.akaList = new List<SdnAkaList>();
            var xmlAkaList = xmlEntity.Descendants(ns + "akaList").ToList();
            foreach (var xElement in xmlAkaList)
            {
                var xmlAka = xElement.Descendants(ns + "aka").ToList();
                foreach (var xAka in xmlAka)
                {
                    SdnAkaList list = new SdnAkaList()
                    {
                        uid = Convert.ToInt32(xAka.Element(ns + "uid").Value),
                        type = GetXElementSafeValue(xAka, ns, "type"),
                        category = GetXElementSafeValue(xAka, ns, "category"),
                        firstName = GetXElementSafeValue(xAka, ns, "firstName"),
                        lastName = GetXElementSafeValue(xAka, ns, "lastName")
                    };
                    entity.akaList.Add(list);
                }
            }
        }
        protected void GetAdressList(SdnEntity entity, XElement xmlEntity, XNamespace ns)
        {
            entity.addressList = new List<SdnAddressList>();
            var xmlAddressList = xmlEntity.Descendants(ns + "addressList").ToList();
            foreach (var xElement in xmlAddressList)
            {
                var xmlAddress = xElement.Descendants(ns + "address").ToList();
                foreach (var xAdress in xmlAddress)
                {
                    SdnAddressList list = new SdnAddressList()
                    {
                        uid = Convert.ToInt32(xAdress.Element(ns + "uid").Value),
                        address1 = GetXElementSafeValue(xAdress, ns, "address1"),
                        address2 = GetXElementSafeValue(xAdress, ns, "address2"),
                        address3 = GetXElementSafeValue(xAdress, ns, "address3"),
                        stateOrProvince = GetXElementSafeValue(xAdress, ns, "stateOrProvince"),
                        city = GetXElementSafeValue(xAdress, ns, "city"),
                        country = GetXElementSafeValue(xAdress, ns, "country")
                    };
                    entity.addressList.Add(list);
                }
            }
        }

        protected string GetXElementSafeValue(XElement element, XNamespace ns, string value)
        {
            return element.Element(ns + value) != null ? element.Element(ns + value).Value : null;
        }
        protected string GetXElementSafeValue(XElement element, string value)
        {
            return element.Element(value) != null ? element.Element(value).Value : null;
        }
        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (_slDataSource != null)
                _slDataSource.Dispose();
        }
    }
}