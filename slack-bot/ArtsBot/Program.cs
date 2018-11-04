using System;
using LibGit2Sharp;
using SlackAPI;
namespace ArtsBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var repPath = Environment.CurrentDirectory;
            Console.WriteLine(repPath);
            var commitMsg = "";
            using (var repo = new Repository(repPath))
            {
                var commit = repo.Head.Tip;
                commitMsg = commit.Message;
                Console.WriteLine("Message: {0}", commitMsg);
            }

            var arts = $"arts:";
            if (!commitMsg.StartsWith(arts)) return;
            var authToken = Environment.GetEnvironmentVariable("SLACK_BOT_USER_TOKEN");
            var channelName = Environment.GetEnvironmentVariable("CHANNEL_NAME");
            var postMsg = $"https://github.com/codeyu/follow-haoel-to-gain-level/tree/master/arts-in-action/{DateTime.Now.Year}/{commitMsg.Split(":")[1]}";
            var slack = new SlackInstance(authToken);
            slack.PostTextMessage(channelName, postMsg);

        }
        
    }
}
