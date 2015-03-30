using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aml.SanctionList.Model
{
    public class EuName
    {
        public int Id { get; set; }
        public int Entity_id { get; set; }
        public string LASTNAME { get; set; }
        public string FIRSTNAME { get; set; }
        public string MIDDLENAME { get; set; }
        public string WHOLENAME { get; set; }
        public string GENDER { get; set; }
        public string TITLE { get; set; }
        public string FUNCTION { get; set; }
        public string LANGUAGE { get; set; }
    }
}