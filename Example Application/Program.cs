using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Example_Application
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This application is intended to demostrate the functionality included in StrawpollNET library. - swiftyspiffy");

            Console.WriteLine("Let's create a poll first. What should the title of this poll be?");
            string title = Console.ReadLine();

            Console.WriteLine("Great! What should the options for this poll be? Submit 'end' to stop submitting options.");
            string input = "";
            List<string> options = new List<string>();
            while (input != "end")
            {
                Console.WriteLine($"What should option #{options.Count + 1} be?");
                input = Console.ReadLine();
                if (input != "end")
                    options.Add(input);
            }
            Console.WriteLine($"Awesome, a total of {options.Count} options will be great!");

            input = "";
            Console.WriteLine("Should voters be allowed to select multiple options, or just a single option? Submit 'single' or 'multiple'.");
            while (input != "single" && input != "multiple")
            {
                input = Console.ReadLine();
                if (input != "single" && input != "multiple")
                    Console.WriteLine("Valid options are: single, multiple");
            }
            bool multi = (input == "single") ? false : true;

            input = "";
            Console.WriteLine("Should voters be presented with a Captcha? Submit 'yes' or 'no'.");
            while (input != "yes" && input != "no")
            {
                input = Console.ReadLine();
                if (input != "yes" && input != "no")
                    Console.WriteLine("Valid options are: yes, no");
            }
            bool captcha = (input == "yes") ? true : false;

            Console.WriteLine("Looks like we're done! I will create your poll now, and list out the details below!");
            Console.WriteLine("-----------------------------");

            var createResp = createPoll(title, options, multi, StrawPollNET.Enums.DupCheck.Normal, captcha).Result;

            Console.WriteLine("Poll created! The details are below:");
            Console.WriteLine("Poll ID: " + createResp.Id);
            Console.WriteLine("Poll Title: " + createResp.Title);
            Console.WriteLine("Poll Options:");
            foreach (string option in createResp.Options)
                Console.WriteLine("-" + option);
            Console.WriteLine("Multiple choice? " + createResp.Multi);
            Console.WriteLine("DupCheck: " + createResp.DupCheck.ToString());
            Console.WriteLine("Captcha? " + createResp.Captcha);
            Console.WriteLine("Poll URL: " + createResp.PollUrl);

            Console.WriteLine("Go vote in the poll, and then hit enter here and I will fetch the details of the poll!");
            Console.ReadLine();

            var getResp = getPoll(createResp.Id).Result;

            Console.WriteLine("Alright, I've fetched the poll details! They will be listed below!");
            Console.WriteLine("Poll ID: " + getResp.Id);
            Console.WriteLine("Poll ID: " + getResp.Title);
            Console.WriteLine("Poll Results:");
            foreach (KeyValuePair<string, int> result in getResp.Results)
                Console.WriteLine($"-{result.Key}: {result.Value} votes");
            Console.WriteLine("Multiple choice? " + getResp.Multi);
            Console.WriteLine("DupCheck: " + getResp.DupCheck.ToString());
            Console.WriteLine("Captcha? " + getResp.Captcha);
            Console.WriteLine("Poll URL: " + getResp.PollUrl);

            Console.WriteLine("And that's everything! Submit to close!");
            Console.ReadLine();
        }

        private async static Task<StrawPollNET.Models.CreatedPoll> createPoll(string title, List<string> options, bool multi, StrawPollNET.Enums.DupCheck dupCheck, bool captcha)
        {
            return await StrawPollNET.API.Create.CreatePollAsync(title, options, multi, dupCheck, captcha);
        }

        private async static Task<StrawPollNET.Models.FetchedPoll> getPoll(int pollId)
        {
            return await StrawPollNET.API.Get.GetPollAsync(pollId);
        }
    }
}
