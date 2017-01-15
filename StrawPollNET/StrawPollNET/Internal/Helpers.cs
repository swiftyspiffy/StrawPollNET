using Newtonsoft.Json.Linq;
using StrawPollNET.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StrawPollNET.Internal
{
    internal static class Helpers
    {
        public static async Task<string> MakeGetRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.Method = "GET";
            try
            {
                using (var responseStream = await request.GetResponseAsync())
                    return await new StreamReader(responseStream.GetResponseStream(), Encoding.Default, true).ReadToEndAsync();
            }
            catch (WebException e) { handleWebException(e); return null; }
        }

        public static async Task<string> MakePostRequest(string url, string requestData = null)
        {
            var data = new UTF8Encoding().GetBytes(requestData ?? "");
            var request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.Method = "POST";
            request.ContentType = "application/json";
            using (var requestStream = await request.GetRequestStreamAsync())
                await requestStream.WriteAsync(data, 0, data.Length);
            try
            {
                using (var responseStream = await request.GetResponseAsync())
                    return await new StreamReader(responseStream.GetResponseStream(), Encoding.Default, true).ReadToEndAsync();
            }
            catch (WebException e) { handleWebException(e); return null; }
        }

        private static void handleWebException(WebException e)
        {
            HttpWebResponse errorResp = e.Response as HttpWebResponse;
            switch (errorResp.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    throw new BadResourceException("The resource you tried to access was not valid.");
                default:
                    throw e;
            }
        }

        public static string BuildPayload(string title, List<string> options, bool multi = false,
            Enums.DupCheck dupCheck = Enums.DupCheck.Normal, bool captcha = false)
        {
            JObject json = new JObject();
            json.Add("title", title);
            json.Add("options", new JArray(options));
            json["multi"] = multi;
            switch(dupCheck)
            {
                case Enums.DupCheck.Disabled:
                    json.Add("dupcheck", "disabled");
                    break;
                case Enums.DupCheck.Normal:
                    json.Add("dupcheck", "normal");
                    break;
                case Enums.DupCheck.Permissive:
                    json.Add("dupcheck", "permissive");
                    break;
            }
            json["captcha"] = captcha;
            return json.ToString();
        }
    }
}
