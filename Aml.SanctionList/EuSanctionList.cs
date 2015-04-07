using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aml.SanctionList.Model;

namespace Aml.SanctionList
{
    public class EuSanctionList : SanctionListBase
    {
        /// <summary>
        /// Creates new Api instance
        /// </summary>
        public EuSanctionList(ISLDataSource slDataSource)
        {
            _slDataSource = slDataSource;
        }

        public List<EuEntity> GetActualSanctionList()
        {
            MemoryStream tempStream = GetActualStrem();
            List<EuEntity> entityList = new List<EuEntity>(2487);
            using (StreamReader streamReader = new StreamReader(tempStream, _slDataSource.GetEncoding()))
            {
                XDocument xmlDocument = XDocument.Load(streamReader);
                XAttribute dateAttribute = xmlDocument.Root.Attribute("Date");
                var dateAtr = dateAttribute.Value.Split('/');
                var date = new DateTime(Convert.ToInt32(dateAtr[2]), Convert.ToInt32(dateAtr[1]), Convert.ToInt32(dateAtr[0]));
                var xmlEntities = xmlDocument.Descendants("ENTITY").ToList();

                foreach (var xmlEntity in xmlEntities)
                {
                    var entity = GetEuEntity(xmlEntity, date);
                    GetNameList(entity, xmlEntity);
                    GetPassportList(entity, xmlEntity);
                    GetBirthList(entity, xmlEntity);
                    entityList.Add(entity);
                }
                return entityList;
            }
        }
        private static EuEntity GetEuEntity(XElement xmlEntity, DateTime date)
        {
            EuEntity entity = new EuEntity()
            {
                Id = Convert.ToInt32(xmlEntity.Attribute("Id").Value),
                legal_basis = xmlEntity.Attribute("legal_basis").Value,
                pdf_link = xmlEntity.Attribute("pdf_link").Value,
                programme = xmlEntity.Attribute("programme").Value,
                reg_date = DateTime.Parse(xmlEntity.Attribute("reg_date").Value),
                remark = xmlEntity.Attribute("remark").Value,
                Type = xmlEntity.Attribute("Type").Value,
                ListDate = date
            };
            return entity;
        }
        private void GetNameList(EuEntity entity, XElement xmlEntity)
        {
            entity.Names = new List<EuName>();
            var xmlNames = xmlEntity.Descendants("NAME").ToList();
            foreach (var xmlName in xmlNames)
            {
                EuName name = new EuName()
                {
                    Id = Convert.ToInt32(xmlName.Attribute("Id").Value),
                    Entity_id = Convert.ToInt32(xmlName.Attribute("Entity_id").Value),
                    FIRSTNAME = GetXElementSafeValue(xmlName, "FIRSTNAME"),
                    LASTNAME = GetXElementSafeValue(xmlName, "LASTNAME"),
                    FUNCTION = GetXElementSafeValue(xmlName, "FUNCTION"),
                    GENDER = GetXElementSafeValue(xmlName, "GENDER"),
                    LANGUAGE = GetXElementSafeValue(xmlName, "LANGUAGE"),
                    MIDDLENAME = GetXElementSafeValue(xmlName, "MIDDLENAME"),
                    TITLE = GetXElementSafeValue(xmlName, "TITLE"),
                    WHOLENAME = GetXElementSafeValue(xmlName, "WHOLENAME")
                };
                entity.Names.Add(name);
            }
        }
        private void GetBirthList(EuEntity entity, XElement xmlEntity)
        {
            entity.Births = new List<EuBirth>();
            var xmlBirths = xmlEntity.Descendants("BIRTH").ToList();
            foreach (var xmlBirth in xmlBirths)
            {
                EuBirth birth = new EuBirth()
                {
                    Date = GetXElementSafeValue(xmlBirth, "DATE"),
                    Place = GetXElementSafeValue(xmlBirth, "PLACE"),
                    Country = GetXElementSafeValue(xmlBirth, "COUNTRY")
                };
                entity.Births.Add(birth);
            }
        }
        private void GetPassportList(EuEntity entity, XElement xmlEntity)
        {
            entity.Passports = new List<UePassport>();
            var xmlPassports = xmlEntity.Descendants("PASSPORT").ToList();
            foreach (var xmlPassport in xmlPassports)
            {
                UePassport passport = new UePassport()
                {
                    Number = GetXElementSafeValue(xmlPassport, "NUMBER"),
                    Country = GetXElementSafeValue(xmlPassport, "COUNTRY")
                };
                entity.Passports.Add(passport);
            }
        }
    }
}