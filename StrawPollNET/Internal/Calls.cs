using Newtonsoft.Json.Linq;
using StrawPollNET.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StrawPollNET.Internal
{
    internal static class Calls
    {
        private static string apiEndpoint = "http://www.strawpoll.me/api/v2/polls";

        public static async Task<FetchedPoll> GetPoll(int pollId)
        {
            var resp = await Helpers.MakeGetRequest($"{apiEndpoint}/{pollId}");
            return new FetchedPoll(JObject.Parse(resp));
        }

        public static async Task<CreatedPoll> CreatePoll(string title, List<string> options, bool multi = false,
            Enums.DupCheck dupCheck = Enums.DupCheck.Normal, bool captcha = false)
        {
            if (options.Count < 2 || options.Count > 30)
                throw new Exceptions.BadParameterException("Invalid number of options provided. You must provide at least 2 options, but no more than 30.");
            string payload = Helpers.BuildPayload(title, options, multi, dupCheck, captcha);
            var resp = await Helpers.MakePostRequest(apiEndpoint, payload);
            return new CreatedPoll(JObject.Parse(resp));
        }
    }
}
