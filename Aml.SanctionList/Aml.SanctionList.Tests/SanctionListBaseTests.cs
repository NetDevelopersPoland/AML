using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Xunit;
using System.Xml.Linq;
using Aml.SanctionList.Model;

namespace Aml.SanctionList.Tests
{
    public class SanctionListBaseTests : SanctionListBase
    {
        SdnEntity _entity;
        XNamespace _ns;

        public SanctionListBaseTests()
        {
            _entity = new SdnEntity();
            _ns = "http://tempuri.org/sdnList.xsd";
        }
        [Fact]
        public void GetAdressList_AdressUid16_Ok()
        {
            XElement root = new XElement(_ns + "Root",
                new XElement(_ns + "addressList", new XElement(_ns + "address",
                    new XElement(_ns + "uid", "16"),
                    new XElement(_ns + "address1", "171 Old Bakery Street"),
                    new XElement(_ns + "city", "Valletta"),
                    new XElement(_ns + "country", "Malta")))
            );
            Console.Write(root.ToString());
            base.GetAdressList(_entity, root, _ns);

            Assert.NotNull(_entity.addressList);
            Assert.Equal(1, _entity.addressList.Count);
            var adress = _entity.addressList[0];
            Assert.Equal("16", adress.uid.ToString());
            Assert.Equal("171 Old Bakery Street", adress.address1);
            Assert.Equal("Valletta", adress.city);
            Assert.Equal("Malta", adress.country);
        }
        [Fact]
        public void GetAdressList_AdressUid14856_Ok()
        {
            XElement root = new XElement(_ns + "Root",
                new XElement(_ns + "addressList", new XElement(_ns + "address",
                    new XElement(_ns + "uid", "14856"),
                    new XElement(_ns + "address1", "Calle Mariano Escobedo Ote No. 467-4, Col. Centro"),
                    new XElement(_ns + "city", "Culiacan"),
                    new XElement(_ns + "stateOrProvince", "Sinaloa"),
                    new XElement(_ns + "country", "Mexico")))
            );
            Console.Write(root.ToString());
            base.GetAdressList(_entity, root, _ns);

            Assert.NotNull(_entity.addressList);
            Assert.Equal(1, _entity.addressList.Count);
            var adress = _entity.addressList[0];
            Assert.Equal("14856", adress.uid.ToString());
            Assert.Equal("Calle Mariano Escobedo Ote No. 467-4, Col. Centro", adress.address1);
            Assert.Equal("Culiacan", adress.city);
            Assert.Equal("Sinaloa", adress.stateOrProvince);
            Assert.Equal("Mexico", adress.country);
        }
        [Fact]
        public void GetIdList_IdUid1242_Ok()
        {
            XElement root = new XElement(_ns + "Root", new XElement(_ns + "idList", 
                new XElement(_ns + "id",
                new XElement(_ns + "uid", "1242"),
                new XElement(_ns + "idType", "Cedula No."),
                new XElement(_ns + "idNumber", "2879530"),
                new XElement(_ns + "idCountry", "Colombia")
                )));
            Console.Write(root.ToString());
            base.GetIdList(_entity, root, _ns);

            Assert.NotNull(_entity.idList);
            Assert.Equal(1, _entity.idList.Count);
            var id = _entity.idList[0];
            Assert.Equal("1242", id.uid.ToString());
            Assert.Equal("Cedula No.", id.idType);
            Assert.Equal("2879530", id.idNumber);
            Assert.Equal("Colombia", id.idCountry);
        }
    }
}
