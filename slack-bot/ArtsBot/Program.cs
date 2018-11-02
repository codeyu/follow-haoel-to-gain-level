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
                Commit commit = repo.Head.Tip;
                commitMsg = commit.Message;
                Console.WriteLine("Message: {0}", commitMsg);
            }

            var arts = $"ARTS:";
            if (commitMsg.StartsWith(arts))
            {
                var channelName = "general";
                var postMsg = "test";
                    //$"https://github.com/codeyu/follow-haoel-to-gain-level/tree/master/arts-in-action/{DateTime.Now.Year}/{commitMsg.Split(":")[1]}";
                var authToken = Environment.GetEnvironmentVariable("SLACK_BOT_USER_TOKEN");
                var client = GetClient(authToken);
                client.GetChannelList((clr) => { Console.WriteLine("got channels");  });
                var c = client.Channels.Find(x => x.name == channelName);
                client.PostMessage((mr) => Console.WriteLine($"sent message to {channelName}!"), c.id, $"{postMsg}");
            }
            
            
        }
        private static AccessTokenResponse GetAccessToken(string clientId, string clientSecret, string redirectUri, string authCode)
        {
            AccessTokenResponse accessTokenResponse = null;

            using (var sync = new InSync(nameof(SlackClient.GetAccessToken)))
            {
                SlackClient.GetAccessToken(response =>
                {
                    accessTokenResponse = response;
                    sync.Proceed();

                }, clientId, clientSecret, redirectUri, authCode);
            }

            if (accessTokenResponse != null)
            {
                return accessTokenResponse;
            }
            else
            {
                throw new Exception("Can't got authToken!");
            }
            
        }
        private static SlackSocketClient GetClient(string authToken)
        {
            SlackSocketClient client;

            using (var syncClient = new InSync($"{nameof(SlackClient.Connect)} - Connected callback"))
            using (var syncClientSocket = new InSync($"{nameof(SlackClient.Connect)} - SocketConnected callback"))
            {
                client = new SlackSocketClient(authToken);
                client.Connect(x =>
                {
                    Console.WriteLine("Connected");
                    syncClient.Proceed();
                }, () =>
                {
                    Console.WriteLine("Socket Connected");
                    syncClientSocket.Proceed();
                });
            }

            if (client.IsConnected) return client;
            throw new Exception("Doh, still isn't connected");
        }
    }
}
