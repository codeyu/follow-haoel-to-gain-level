using System;
using SlackAPI;
namespace ArtsBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = GetClient("");
            client.GetChannelList((clr) => { Console.WriteLine("got channels");  });
            var c = client.Channels.Find(x => x.name.Equals("general"));
            client.PostMessage((mr) => Console.WriteLine("sent message to general!"), c.id, "Hello general world! \nfrom codeyu-bot");
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
                throw new Exception("Can't get authToken!");
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
            if(client.IsConnected)
            {
                Console.WriteLine("Doh, still isn't connected");
            }
            return client;
        }
    }
}
