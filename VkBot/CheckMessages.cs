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
        Character character = new Character();
        
        public void Start(VkApi api)
        {
            while (true)
            {
                string newMessage = GetNewMessage(api);

                if (newMessage != null)
                {
                    SetCharacterData(newMessage, character);
                    string responseMessageText = MessageResponse.GetNewResponse(newMessage, character);
                    SendMessageResponse(api, responseMessageText);
                    if (responseMessageText == "В бой" || (newMessage.Contains("Путь преграждает ") && newMessage.Contains("Персонаж:")) || newMessage.Contains("Противник нанес"))
                    {
                        Battle(api,newMessage);
                    }
                }

                System.Threading.Thread.Sleep(10000);
            }
        }

        private string GetNewMessage(VkApi api)
        {
            var getHistory = api.Messages.GetHistory(new MessagesGetHistoryParams
            {
                PeerId = -182985865,
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
                string finalMessage = "";
                lastMessage = newMessages[0];
                for (int i = newMessages.Count - 1; i >= 0; i--)
                {
                    finalMessage += newMessages[i] + "\n";
                }
                return finalMessage;
            }
            else
            {
                return null;
            }
        }

        private void SendMessageResponse(VkApi api, string messageText)
        {
            if (messageText != null && messageText != " " && messageText != "") 
            {
                api.Messages.Send(new VkNet.Model.MessagesSendParams
                {
                    RandomId = 0,
                    PeerId = -182985865,
                    Message = messageText
                });
            }
        }

        static private void SetCharacterData(string finalMessage, Character character)
        {
            if (finalMessage.Contains("HP:"))
            {
                string hpString = "1/1";
                int trophies = 0;
                string[] subs = finalMessage.Split('\n');
                foreach (string sub in subs)
                {
                    if (sub.Contains("HP:"))
                    {
                        string[] sububs = sub.Split(' ');
                        foreach (string subsub in sububs)
                        {
                            if (subsub.Contains("/"))
                            {
                                hpString = subsub;
                            }
                        }
                    }
                    if (sub.Contains("Трoфеев:"))
                    {
                        trophies = int.Parse(sub.Substring(sub.IndexOf("Трoфеев: ") + 9));
                    }
                }
                character.SetHP(hpString);
                character.Trophies = trophies;
            }
        }

        private void Battle(VkApi api, string newMessage)
        {
            int raund = 0;
            System.Threading.Thread.Sleep(20000);
            while (true)
            {
                
                if (newMessage != null )
                {
                    SetCharacterData(newMessage, character);
                    if (character.PotionIsNeed())
                    {
                        SendMessageResponse(api, "Простое зелье");
                        System.Threading.Thread.Sleep(5000);
                    }
                    if (raund == 2)
                        SendMessageResponse(api, "|Слабое исцеление|");
                    else
                        SendMessageResponse(api, "|Сила теней|");

                    System.Threading.Thread.Sleep(10000);
                    raund += 1;

                    if (newMessage.Contains("Бой завершен.") || newMessage.Contains("Локaция:"))
                        break;
                }
                newMessage = GetNewMessage(api);

            }
        }


    }
}
