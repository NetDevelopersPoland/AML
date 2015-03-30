using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aml.SanctionList
{
    internal static class ApiConfiguration
    {
        /// <summary>
        /// Default encoding
        /// </summary>
        public static Encoding DefaultEncoding
        {
            get { return Encoding.GetEncoding("iso-8859-2"); }
        }
        public static Encoding WesternEuropeEncoding
        {
            get { return Encoding.GetEncoding("iso-8859-1"); }
        }
        public static Encoding Etf8Encoding
        {
            get { return Encoding.UTF8; }
        }
        /// <summary>
        /// URL to actual EU list xml (•	Consolidated list of persons, groups and entities subject to EU financial sanctions )
        /// </summary>
        public static string EuDataSourceUrl
        {
            get { return @"http://ec.europa.eu/external_relations/cfsp/sanctions/list/version4/global/global.xml"; }
        }
        /// <summary>
        /// URL to actual SDN list xml (•	Specially Designated Nationals List (SDN) )
        /// </summary>
        public static string SdnDataSourceUrl
        {
            get { return @"http://www.treasury.gov/ofac/downloads/sdn.xml"; }
        }
        /// <summary>
        /// URL to actual FSE list xml (•	Foreign Sanctions Evaders (FSE) )
        /// </summary>
        public static string FseDataSourceUrl
        {
            get { return @"http://www.treasury.gov/ofac/downloads/fse/fse.xml"; }
        }
        /// <summary>
        /// URL to actual PLC list xml (•	Palestinian Legislative Council (PLC) List  )
        /// </summary>
        public static string PlcDataSourceUrl
        {
            get { return @"http://www.treasury.gov/resource-center/sanctions/Terrorism-Proliferation-Narcotics/Documents/ns_plc.xml"; }
        }

        public static string SdnNamespace = "http://tempuri.org/sdnList.xsd";
        public static string PlcNamespace = "http://tempuri.org/nsplc.xsd";

        public static string EuFileName = "Eu";
        public static string FseFileName = "Fse";
        public static string SdnFileName = "Sdn";
        public static string PlcFileName = "Plc";
    }
}