using System;
using SlackAPI;

namespace ArtsBot
{
    public class SlackInstance : IDisposable
    {
        public SlackInstance(string authToken)
        {
            this.BotClient = this.GetClient(authToken);
        }
        public SlackSocketClient BotClient { get; }
        public void Dispose()
        {
            this.BotClient.CloseSocket();
        }
        private SlackSocketClient GetClient(string authToken)
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
            return client;
        }
        
        public void PostTextMessage(string channelName, string msg)
        {
            // given
            var client = BotClient;
            PostMessageResponse actual = null;

            // when
            using (var sync = new InSync(nameof(SlackClient.PostMessage)))
            {
                client.GetChannelList((clr) => { Console.WriteLine("got channels");  });
                var c = client.Channels.Find(x => x.name == channelName);
                client.PostMessage((mr) => Console.WriteLine($"sent message to {channelName}!"), c.id, msg);
                client.PostMessage(
                    response =>
                    {
                        Console.WriteLine($"sent message to {channelName}!");
                        actual = response;
                        sync.Proceed();
                    },
                    c.id,
                    msg);
            }

            Console.WriteLine(!actual.ok ? "Error while posting message to channel. " : actual.message.text);
        }
    }
}