using VkNet.AudioBypassService.Extensions;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet;
using Microsoft.Extensions.DependencyInjection;
using VkNet.Enums;

namespace VkBot
{
    internal class Program
    {

        static readonly HttpClient client = new HttpClient();

        

        static async Task Main()
        {

            var user = new UserData();

            var api = new VkApi();

            api.Authorize(new ApiAuthParams
            {
                //id приложения kate mobile
                ApplicationId = 2685278,
                Login = user.Login,
                Password = user.Password,
                Settings = Settings.All

            });
            Console.WriteLine(api.Token);
            //var res = api.Groups.Get(new GroupsGetParams());

            //Console.WriteLine(res.TotalCount);

            //var longPoll = api.Messages.GetLongPollServer(needPts: true);
            //var serv = api.Messages.GetLongPollHistory(new MessagesGetLongPollHistoryParams() { Pts = longPoll.Pts, Ts = longPoll.Ts });

            //Console.WriteLine(serv.Messages);

            var getHistory = api.Messages.GetHistory(new MessagesGetHistoryParams
            {
                PeerId = -182985865,
                Offset = 1
            });

            Console.WriteLine(getHistory);

            //api.Messages.Send(new VkNet.Model.MessagesSendParams
            //{
            //    RandomId = 1232345235, // уникальный
            //    PeerId = -182985865,
            //    Message = "!?"
            //});


            Console.ReadLine();
        }
    }
}