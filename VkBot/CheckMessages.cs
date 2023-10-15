using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet.AudioBypassService.Extensions;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet;

namespace VkBot
{
    public class CheckMessages
    {
        private List<string> finalMessages = new List<string> {"","",""};
        public void Start(VkApi api)
        {
            while (true)
            {
                var getHistory = api.Messages.GetHistory(new MessagesGetHistoryParams
                {
                    PeerId = -182985865,
                    //UserId = 178549456,
                    Offset = 0,
                    Reversed = false,
                    Count = 3
                });

                var newMessages = new List<string>();

                foreach (var message in getHistory.Messages)
                {
                    newMessages.Add(message.Text);
                }

                if (newMessages[0] != finalMessages[0] && newMessages[1] != finalMessages[1] && newMessages[2] != finalMessages[2])
                {
                    finalMessages[0] = newMessages[0];
                    finalMessages[1] = newMessages[1];
                    finalMessages[2] = newMessages[2];
                    for (int i = newMessages.Count - 1; i==0;i--)
                    {
                        Console.WriteLine("==============================================");
                        Console.WriteLine(finalMessages[i]);
                    }
                    //Console.WriteLine("==============================================");
                    //Console.WriteLine(newMessage);
                }



                System.Threading.Thread.Sleep(3000);
            }
        }


        
    }
}
