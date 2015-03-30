using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aml.SanctionList.Model
{
    public class EuEntity
    {
        public DateTime ListDate { get; set; }

        public int Id { get; set; }
        public string Type { get; set; }
        public string legal_basis { get; set; }
        public DateTime reg_date { get; set; }
        public string pdf_link { get; set; }
        public string programme { get; set; }
        public string remark { get; set; }

        public List<EuName> Names { get; set; }
        public List<EuBirth> Births { get; set; }
        public List<EuCitizen> Citizens { get; set; }
        public List<UePassport> Passports { get; set; }
    }
}