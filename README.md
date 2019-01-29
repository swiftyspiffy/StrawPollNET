# StrawpollNET - .NET Strawpoll Library for Creating and Accessing Strawpolls
[![NuGet Pre Release](https://img.shields.io/nuget/vpre/StrawPollNET.svg)](https://www.nuget.org/packages/StrawPollNET)

### Overview
This is a simple library designed to allow for the creation and accessing of Strawpolls in a really simple fashion. Synchronous and asynchrnous calls are available and all poll details (including vote counts) are returned when creating a new poll or fetching an existing poll.

### Sample Implementations
Create Strawpoll
```
// Establish the poll settins
string pollTitle = "Which of these are great movies? You can vote on multiple!";
List<string> allOptions = new List<string>() { "Shrek", "Ants", "A Bug's Life", "Lion King" };
bool multipleChoice = true;
StrawPollNET.Enums.DupCheck dupCheck = StrawPollNET.Enums.DupCheck.Normal;
bool requireCaptcha = true;

// Create the poll
StrawPollNET.Models.CreatedPoll newPoll = StrawPollNET.API.Create.CreatePoll(pollTitle, allOptions, multipleChoice, dupCheck, requireCaptcha);

// Show poll link
Console.WriteLine($"Go vote at my new poll, available here: {newPoll.PollUrl}");

```
Output:
```
Go vote at my new poll, available here: https://strawpoll.me/12112663
```
Fetch a Strawpoll
```
// Get the very first Strawpoll ever made
int pollId = 1;
StrawPollNET.Models.FetchedPoll fetchedPoll = StrawPollNET.API.Get.GetPoll(pollId);

// Show results
Console.WriteLine("The current results are:");
foreach(KeyValuePair<string, int> result in fetchedPoll.Results)
    Console.WriteLine($"-{result.Key}: {result.Value} votes");
```
Output:
```
The current results are:
-Shrek: 1 votes
-Ants: 0 votes
-A Bug's Life: 0 votes
-Lion King: 0 votes
```


### Availability
Available via Nuget: `Install-Package StrawPollNET`

### Examples and Implementations
- StrawpollNET Example Application - This project is included in this repo as a master example project.

### Libraries Utilized
- Newtonsoft.Json - JSON parsing class.  Used to parse responses from Strawpoll API

### Contributors
 * Cole ([@swiftyspiffy](http://twitter.com/swiftyspiffy))
 
### License
MIT License. &copy; 2016 Cole
