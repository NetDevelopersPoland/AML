using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aml.SanctionList.Model
{
    public class SdnAddressList
    {
        public int uid { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        public string stateOrProvince { get; set; }
        public string city { get; set; }
        public string country { get; set; }
    }
}