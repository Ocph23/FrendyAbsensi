using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;

namespace WebApi
{
    public class ErrorResponse: HttpResponseMessage
    {
        public ErrorResponse (HttpResponseMessage request,string message):base(request.StatusCode)
        {
           
            var content = new { Message = message};
            ReasonPhrase = message;
            RequestMessage = request.RequestMessage;
            Content = new ObjectContent(content.GetType(), content, new JsonMediaTypeFormatter());
        }
        
    }
}