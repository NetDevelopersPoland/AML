using System;
using System.Collections.Generic;

namespace Aml.SanctionList.Model
{
    public class SdnEntity
    {
        public DateTime ListDate { get; set; }

        public int uid { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string type { get; set; }

        public List<SdnProgramList> programList { get; set; }
        public List<SdnAddressList> addressList { get; set; }
        public List<SdnAkaList> akaList { get; set; }
        public List<SdnIdList> idList { get; set; }
        public List<SdnDateOfBirthList> dateOfBirthList { get; set; }
        public List<SdnProgramList> placeOfBirthList { get; set; }
    }
}