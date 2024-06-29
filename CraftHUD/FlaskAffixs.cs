using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftHUD
{
    public static class FlaskAffixs
    {
        public static CraftRule[] Rules = new CraftRule[]
        {
            new CraftRule(@"(\d+)\(\d+-\d+\)% chance to gain a Flask Charge when you deal a Critical Strike", 24),
            new CraftRule(@"(\d+)\(\d+-\d+\)% increased Charge Recovery", 40),
            new CraftRule(@"(\d+)\(\d+-\d+\)% increased Duration", 30),
            new CraftRule(@"(\d+)\(\d+-\d+\)% reduced Charges per use",23, false),
            new CraftRule(@"(\d+)\(\d+-\d+\)% reduced Duration\r\n25% increased effect", 27, false),
            new CraftRule(@"Gain (\d+) Charges when you are Hit by an Enemy", 2),
            new CraftRule(@"(\d+)\(\d+-\d+\)% increased Armour during Effect", 45),
            new CraftRule(@"(\d+)\(\d+-\d+\)% increased Attack Speed during Effect", 15),
            new CraftRule(@"(\d+)\(\d+-\d+\)% increased Cast Speed during Effect", 15),
            new CraftRule(@"(\d+)\(\d+-\d+\)% increased Critical Strike Chance during Effect",50),
            new CraftRule(@"(\d+)\(\d+-\d+\)% increased Evasion Rating during Effect", 45),
            new CraftRule(@"(\d+)\(\d+-\d+\)% increased Movement Speed during Effect", 9),
            new CraftRule(@"(\d+)\(\d+-\d+\)% less Duration\r\nImmunity to Bleeding and Corrupted Blood during Effect", 45, false)
        };
    }

    
}
