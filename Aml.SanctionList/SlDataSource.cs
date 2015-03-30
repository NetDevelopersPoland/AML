using System;
using System.IO;
using System.Net;
using System.Text;

namespace Aml.SanctionList
{
    public class SlDataSource : ISLDataSource, IDisposable
    {
        private HttpWebResponse _httpWebResponse;
        public Stream GetSanctionListDataSource()
        {
            return GetCustomSanctionListDataSource(ApiConfiguration.EuDataSourceUrl);
        }
        public string GetSanctionListFileName()
        {
            return ApiConfiguration.SdnFileName;
        }
        public string GetNamespace()
        {
            return ApiConfiguration.SdnNamespace;
        }
        public Encoding GetEncoding()
        {
            return ApiConfiguration.Etf8Encoding;
        }
        protected Stream GetCustomSanctionListDataSource(string url)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Proxy = WebRequest.DefaultWebProxy;
            httpWebRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;
            httpWebRequest.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            _httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            return _httpWebResponse.GetResponseStream();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (_httpWebResponse != null)
                _httpWebResponse.Dispose();
        }
    }
}