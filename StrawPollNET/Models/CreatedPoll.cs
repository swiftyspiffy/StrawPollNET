using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrawPollNET.Models
{
    public class CreatedPoll
    {
        /// <summary>Auto generated id of created poll.</summary>
        public int Id { get; protected set; }
        /// <summary>Titled assigned by poll creator.</summary>
        public string Title { get; protected set; }
        /// <summary>All options currently in the poll.</summary>
        public List<string> Options { get; protected set; } = new List<string>();
        /// <summary>Boolean representing whether or not poll is multiple choice.</summary>
        public bool Multi { get; protected set; }
        /// <summary>Type of duplication check employed by this poll.</summary>
        public Enums.DupCheck DupCheck { get; protected set; }
        /// <summary>Boolean representing whether or not poll requires captcha.</summary>
        public bool Captcha { get; protected set; }
        /// <summary>URL to the created poll.</summary>
        public string PollUrl { get; protected set; }

        /// <summary>CreatedPoll constructor.</summary>
        public CreatedPoll(JToken json)
        {
            bool isMulti, isCaptcha;

            Id = int.Parse(json.SelectToken("id").ToString());
            Title = json.SelectToken("title")?.ToString();
            foreach (JToken option in json.SelectToken("options"))
                Options.Add(option.ToString());
            if (bool.TryParse(json.SelectToken("multi").ToString(), out isMulti) && isMulti) Multi = true;
            if (bool.TryParse(json.SelectToken("captcha").ToString(), out isCaptcha) && isCaptcha) Captcha = true;
            if (json.SelectToken("dupcheck") != null)
            {
                switch (json.SelectToken("dupcheck").ToString())
                {
                    case "normal":
                        DupCheck = Enums.DupCheck.Normal;
                        break;
                    case "permissive":
                        DupCheck = Enums.DupCheck.Permissive;
                        break;
                    case "disabled":
                        DupCheck = Enums.DupCheck.Disabled;
                        break;
                }
            }
            PollUrl = $"https://strawpoll.me/{Id}";
        }
    }
}
