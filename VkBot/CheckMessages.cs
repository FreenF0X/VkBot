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
        private string lastMessage = "";
        private string finalMessage = "";
        Character character = new Character();
        
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
                    Count = 2
                });

                var newMessages = new List<string>();

                foreach (var message in getHistory.Messages)
                {
                    newMessages.Add(message.Text); 
                }

                if (newMessages[0] != lastMessage)
                {
                    lastMessage = newMessages[0];
                    Console.WriteLine("==============================================");
                    finalMessage = "";
                    for (int i = newMessages.Count - 1; i>=0; i--)
                    {
                        finalMessage += newMessages[i]+"\n";
                    }
                    Console.WriteLine(finalMessage);
                }

                if (finalMessage.Contains("HP:"))
                {
                    string hpString="1/1";
                    int trophies=0;
                    string[] subs = finalMessage.Split('\n');
                    foreach(string sub in subs)
                    {
                        if (sub.Contains("HP:"))
                        {
                            hpString = sub.Substring(sub.IndexOf("HP: ") + 4);
                        }
                        if (sub.Contains("Трoфеев:"))
                        {
                            trophies = int.Parse(sub.Substring(sub.IndexOf("Трoфеев: ") + 9));
                        }
                    }
                    character.SetHP(hpString);
                    character.Trophies = trophies;
                }

                System.Threading.Thread.Sleep(5000);
            }
        }


        
    }
}
