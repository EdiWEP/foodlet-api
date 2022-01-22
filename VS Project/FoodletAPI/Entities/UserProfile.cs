using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Entities
{
    public class UserProfile
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public virtual User User { get; set; }
    }
}
