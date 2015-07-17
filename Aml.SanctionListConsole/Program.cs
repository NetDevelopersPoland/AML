using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aml.SanctionList;
using Aml.LoggingService;

namespace Aml.SanctionListConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ILoggingService _logger = new LoggingService.LoggingService();
            try
            {
                _logger.LogInfo("Eu Sanction List start");
                EuSanctionList euBase = new EuSanctionList(new EuDataSource());
                var euList = euBase.GetActualSanctionList();
                _logger.LogInfo("Eu Sanction List finish");

                _logger.LogInfo("Sdn Sanction List start");
                SanctionListBase sdnBase = new SdnSanctionList(new SdnDataSource());
                var sdnList = sdnBase.GetActualSanctionList();
                _logger.LogInfo("Sdn Sanction List finish");

				/*FSE and PLC oryginal lists temporary unavailable (fse and plc records are in consolidated list)
                _logger.LogInfo("Fse Sanction List start");
                SanctionListBase fseBase = new SdnSanctionList(new FseDataSource());
                var fseList = fseBase.GetActualSanctionList();
                _logger.LogInfo("Fse Sanction List finish");

                _logger.LogInfo("Plc Sanction List start");
                PlcSanctionList plcBase = new PlcSanctionList(new PlcDataSource());
                var plcList = plcBase.GetActualSanctionList();
				*/
				_logger.LogInfo("Consolidated Sanction List start");
				SanctionListBase slBase = new ConsolidatedSanctionList(new ConsolidatedDataSource());
				var consolidatedList = slBase.GetActualSanctionList();
				_logger.LogInfo("Fse Sanction List finish");

                _logger.LogInfo("Plc Sanction List finish");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex);
            }
            Console.ReadKey();
        }
    }
}
