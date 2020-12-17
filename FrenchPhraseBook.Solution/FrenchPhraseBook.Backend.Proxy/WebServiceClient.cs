using System;
using System.Threading.Tasks;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Web Service

using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

using FrenchPhraseBook.Backend.Proxy.KronosMobileService;

#endregion
namespace FrenchPhraseBook.Backend.Proxy
{
    public class WebServiceClient : TwisterWCFServiceClient
    {
        public WebServiceClient(BasicHttpBinding binding, EndpointAddress address) : base(binding, address) { }
      
    }
}
