//namespace DollarInfo.Services.Helpers
//{
//    using System;
//    using System.Net.Http;
//    using System.Text;
//    using Microsoft.Extensions.Options;

//    public class Authenticator : IAuthenticator
//    {
//        private readonly PciProxyConfig _pciProxyConfig;

//        public Authenticator(IOptions<PciProxyConfig> pciProxyConfig) => _pciProxyConfig = pciProxyConfig.Value;

//        public void SetBasicAuthHeader(HttpClient client)
//        {
//            var byteArray = Encoding.ASCII.GetBytes($"{_pciProxyConfig.Username}:{_pciProxyConfig.Password}");
//            client.DefaultRequestHeaders.Authorization =
//                new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
//        }
//    }
//}
