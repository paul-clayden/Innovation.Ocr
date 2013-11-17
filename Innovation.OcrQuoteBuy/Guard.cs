using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace Innovation.OcrQuoteBuy
{
    public static class Guard
    {
        public static void IsNotAGuid(string value)
        {
            Guid expected;
            if (!Guid.TryParse(value, out expected))
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
        }

        public static void IsNotNull(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
        }
    }
}