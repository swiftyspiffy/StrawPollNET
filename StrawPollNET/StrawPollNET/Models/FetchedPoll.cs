using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrawPollNET.Models
{
    public class FetchedPoll
    {
        /// <summary>Auto generated id of created poll.</summary>
        public int Id { get; protected set; }
        /// <summary>Titled assigned by poll creator.</summary>
        public string Title { get; protected set; }
        /// <summary>Boolean representing whether or not poll is multiple choice.</summary>
        public bool Multi { get; protected set; }
        /// <summary>Type of duplication check employed by this poll.</summary>
        public Enums.DupCheck DupCheck { get; protected set; }
        /// <summary>Boolean representing whether or not poll requires captcha.</summary>
        public bool Captcha { get; protected set; }
        /// <summary>URL to the created poll.</summary>
        public string PollUrl { get; protected set; }
        /// <summary>A pair where the key is the option, and the value is the number of votes that key option has.</summary>
        public List<KeyValuePair<string, int>> Results = new List<KeyValuePair<string, int>>();

        /// <summary>FetchedPoll constructor.</summary>
        public FetchedPoll(JToken json)
        {
            bool isMulti, isCaptcha;

            Id = int.Parse(json.SelectToken("id").ToString());
            Title = json.SelectToken("title")?.ToString();
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
            for(int i = 0; i < json.SelectToken("options").Count(); i++)
                Results.Add(new KeyValuePair<string, int>(json.SelectToken("options")[i].ToString(), int.Parse(json.SelectToken("votes")[i].ToString())));
            PollUrl = $"https://strawpoll.me/{Id}";
        }
    }
}
