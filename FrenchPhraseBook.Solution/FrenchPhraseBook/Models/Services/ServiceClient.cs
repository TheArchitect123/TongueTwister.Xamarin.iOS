using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using System.ServiceModel;
using System.ServiceModel.Channels;

namespace FrenchPhraseBook.Models.Services
{
    public class ServiceClient 
    {
        public static BasicHttpBinding GetBinding()
        {

            TimeSpan DefaultTimeout = new TimeSpan(0, 5, 0);
            //Configure Service Client 
            BasicHttpBinding clientBinding = new BasicHttpBinding(BasicHttpSecurityMode.None);

            clientBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
            clientBinding.Name = "basicHttpBinding";
            clientBinding.CloseTimeout = DefaultTimeout;
            clientBinding.OpenTimeout = DefaultTimeout;
            clientBinding.ReceiveTimeout = DefaultTimeout;
            clientBinding.SendTimeout = DefaultTimeout;
            clientBinding.MaxBufferSize = int.MaxValue;
            clientBinding.MaxReceivedMessageSize = long.MaxValue;
            clientBinding.MaxBufferPoolSize = long.MaxValue;
            clientBinding.TransferMode = TransferMode.Streamed;
            clientBinding.AllowCookies = true;

            
            return clientBinding;
        }

    }
}