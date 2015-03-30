using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aml.SanctionList
{
    public interface ISLDataSource : IDisposable
    {
        Stream GetSanctionListDataSource();
        string GetSanctionListFileName();
        Encoding GetEncoding();
        string GetNamespace();
    }
}