using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aml.SanctionList.Model
{
    public class SdnIdList
    {
        public int uid { get; set; }
        public string idType { get; set; }
        public string idNumber { get; set; }
        public string idCountry { get; set; }
    }
}