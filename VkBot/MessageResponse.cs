using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkBot
{
    static public class MessageResponse
    {
        static string response = " ";
        static public string GetNewResponse(string newMessage, Character character)
        {
            
            if (!character.HpIsNormal() && !character.TrophiesIsNormal())  return "В портал";
            if (CheckDangeon(newMessage, character) != null) return CheckDangeon(newMessage, character);
            if (ChecEnemy(newMessage, character) != null) return ChecEnemy(newMessage, character);
            if (ChecTrap(newMessage, character) != null) return ChecTrap(newMessage, character);


            return response;
        }

        

        static private string CheckDangeon(string newMessage, Character character)
        {
            if (newMessage.Contains("Локaция:") && newMessage.Contains("HP:"))
            {
                return "Исследовать уровень";
            }

            return null;
        }
        static private string ChecEnemy(string newMessage, Character character)
        {
            if (newMessage.Contains("Мoжно cразиться c ним, не теpяя вpeмени"))
            {
                return "В бой";
            }

            return null;
        }

        static private string ChecBattle(string newMessage, Character character)
        {
            if (newMessage.Contains("Мoжно cразиться c ним, не теpяя вpeмени"))
            {
                return "В бой";
            }

            return null;
        }

        static private string ChecTrap(string newMessage, Character character)
        {
            if (newMessage.Contains("Вы набрели на капкан") && newMessage.Contains("Вы пережили полученные повреждения, но нужно немного времени, чтобы освободиться..."))
            {
                return "Освободиться";
            }

            return null;
        }
    }
}
