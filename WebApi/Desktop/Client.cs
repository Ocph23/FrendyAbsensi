using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Desktop
{
   public class Client: IDisposable
    {
        public HttpClient ClientContext { get; set; }

        public Client()
        {
            ClientContext = new HttpClient();
            ClientContext.BaseAddress = new Uri("http://localhost:12006/");
            //   ClientContext.BaseAddress = new Uri("http://www.trireksacargo.com/");
            ClientContext.DefaultRequestHeaders.Accept.Clear();

            if (ResourcesBase.Token != null)
            {
                ClientContext.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                ClientContext.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(ResourcesBase.Token.token_type, ResourcesBase.Token.access_token);
            }
        }

        public void Dispose()
        {
            ClientContext.Dispose();
        }
    }
}

