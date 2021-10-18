using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bunga
{
    public class SeedData
    {        
    }

    public class RoleNames
    {
        public const string Moderator = "Модератор";
        public const string Buyer = "Покупатель";
        public const string Deliver = "Сдающий";
        public const string EditorsOfBungalo = "Модератор,Сдающий";

        public static IEnumerable<string> AllRoles
        {
            get
            {
                yield return Moderator;
                yield return Buyer;
                yield return Deliver;
            }           
        }
    }
}
