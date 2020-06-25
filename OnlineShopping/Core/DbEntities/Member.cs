using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace OnlineShopping.Core.DbEntities
{
    public class Member : IdentityUser
    {
        public ICollection<Order> Orders { get; set; }
        public ICollection<MemberProductFavourite> MemberProductFavourites { get; set; }
    }
}
