using VkNet.AudioBypassService.Extensions;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet;
using Microsoft.Extensions.DependencyInjection;
using VkNet.Enums;
using System.IO;
using VkNet.Exception;

namespace VkBot
{
    internal class Program
    {

        static async Task Main()
        {
            

            var user = new UserData();

            var api = new VkApi();

            try
            {
                StreamReader sr = new StreamReader("token.txt");
                string token = sr.ReadToEnd();
                sr.Close();

                api.Authorize(new ApiAuthParams
                {
                    AccessToken = token
                });
                var res = api.Groups.Get(new GroupsGetParams());
                Console.WriteLine(res.TotalCount);
            }
            catch (UserAuthorizationFailException)
            {
                api.Authorize(new ApiAuthParams
                {
                    //id приложения kate mobile
                    ApplicationId = 2685278,
                    Login = user.Login,
                    Password = user.Password,
                    Settings = Settings.All

                });
                StreamWriter sw = new StreamWriter("token.txt");
                sw.WriteLine(api.Token);
                sw.Close();
            }

            var checkMess = new CheckMessages();
            checkMess.Start(api);

            //var getLongPollServer = api.Messages.GetLongPollServer(true);

            //var pts = getLongPollServer.Pts;
            //var ts = getLongPollServer.Ts;

            //var getLongPollHistory = api.Messages.GetLongPollHistory(new MessagesGetLongPollHistoryParams
            //{
            //    Ts = ts,
            //    Pts = pts,
            //    PreviewLength = 0,
            //    //Onlines = true, // возвращать сообщения от пользователей которые онлайн
            //    //Fields = UsersFields.Nickname,
            //    //GroupId = -182985865,
            //    EventsLimit = 1000,
            //    MsgsLimit = 200

            //});

            //foreach (var message in getLongPollHistory.Messages)
            //{
            //    Console.WriteLine(string.Format("Дата и время: {0}, Сообщение: {1}\r\n", message.Date, message.Body));

            //}



            //var getHistory = api.Messages.GetHistory(new MessagesGetHistoryParams
            //{
            //    PeerId = -182985865,
            //    //UserId = ,
            //    Offset = 0,
            //    Reversed = false,
            //    Count = 3
            //});

            //var messages = new Stack<string>();

            //foreach (var message in getHistory.Messages)
            //{
            //    messages.Push(message.Text);
            //}
            //while (messages.Count > 0)
            //{
            //    Console.WriteLine("Сообщение: " + messages.Pop());
            //}

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