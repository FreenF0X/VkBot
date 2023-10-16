using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkBot
{
    public class Character
    {
        float maxHP;
        float actualHP;
        public int Trophies { get; set; }

        public void SetHP(string hpString)//hpString должен быть формата "103/124"
        {
            actualHP = int.Parse(hpString.Substring(0,hpString.IndexOf("/")));
            maxHP = int.Parse(hpString.Substring(hpString.IndexOf("/")+1));
        }

        public bool HPisNormal()
        {
            return actualHP/maxHP>0.3;
        }
    }
}
