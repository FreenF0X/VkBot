using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkBot
{
    public class Character
    {
        double maxHP;
        double actualHP;
        public int Trophies { get; set; }

        public void SetHP(string hpString)//hpString должен быть формата "103/124"
        {
            actualHP = int.Parse(hpString.Substring(0,hpString.IndexOf("/")));
            maxHP = int.Parse(hpString.Substring(hpString.IndexOf("/")+1));
        }

        public bool HpIsNormal()
        {
            return actualHP/maxHP>0.3;
        }

        public bool TrophiesIsNormal() 
        { 
            return Trophies < 50; 
        }

        public bool PotionIsNeed()
        {
            return maxHP - actualHP > 60;
        }
    }
}
