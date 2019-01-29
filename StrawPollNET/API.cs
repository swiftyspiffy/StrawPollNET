using StrawPollNET.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StrawPollNET
{
    /// <summary>
    /// Base API of this Strawpoll library.
    /// </summary>
    public static class API
    {
        /// <summary>
        /// Namespace for functions to get details about existing polls.
        /// </summary>
        public static class Get
        {
            /// <summary>
            /// [SYNC] This call will fetch the various details including results of an existing Strawpoll.
            /// </summary>
            /// <param name="pollId">The unique id assigned to the poll.</param>
            /// <returns>FetchedPoll is returned, which houses all poll data.</returns>
            public static FetchedPoll GetPoll(int pollId) => Task.Run(() => Internal.Calls.GetPoll(pollId)).Result;
            /// <summary>
            /// [ASYNC] This call will fetch the various details including results of an existing Strawpoll.
            /// </summary>
            /// <param name="pollId">The unique id assigned to the poll.</param>
            /// <returns>FetchedPoll is returned, which houses all poll data.</returns>
            public async static Task<FetchedPoll> GetPollAsync(int pollId) => await Internal.Calls.GetPoll(pollId);
        }

        /// <summary>
        /// Namespace for functions to create a new Strawpoll.
        /// </summary>
        public static class Create
        {
            /// <summary>
            /// [SYNC] This call will attempt to create a poll and return an object housing all the details about the poll.
            /// </summary>
            /// <param name="title">The title that is presented to the voter.</param>
            /// <param name="options">List of strings to be included as options in the poll. Max: 30, Min: 2.</param>
            /// <param name="multi">Boolean value dictating whether voters can select multiple choices or not.</param>
            /// <param name="dupCheck">Enum reflecting the kind of duplication checking Strawpoll should do.</param>
            /// <param name="captcha">Boolean value dicating whether or not a captcha should be required.</param>
            /// <returns>CreatedPoll is returned, which houses all poll data.</returns>
            public static CreatedPoll CreatePoll(string title, IList<string> options, bool multi = false, Enums.DupCheck dupCheck = Enums.DupCheck.Normal, bool captcha = false) => Task.Run(() => Internal.Calls.CreatePoll(title, options, multi, dupCheck, captcha)).Result;
            /// <summary>
            /// [SYNC] This call will attempt to create a poll and return an object housing all the details about the poll.
            /// </summary>
            /// <param name="title">The title that is presented to the voter.</param>
            /// <param name="options">List of strings to be included as options in the poll. Max: 30, Min: 2.</param>
            /// <param name="multi">Boolean value dictating whether voters can select multiple choices or not.</param>
            /// <param name="dupCheck">Enum reflecting the kind of duplication checking Strawpoll should do.</param>
            /// <param name="captcha">Boolean value dicating whether or not a captcha should be required.</param>
            /// <returns>CreatedPoll is returned, which houses all poll data.</returns>
            public async static Task<CreatedPoll> CreatePollAsync(string title, IList<string> options, bool multi = false, Enums.DupCheck dupCheck = Enums.DupCheck.Normal, bool captcha = false) => await Internal.Calls.CreatePoll(title, options, multi, dupCheck, captcha);
        }
    }
}
