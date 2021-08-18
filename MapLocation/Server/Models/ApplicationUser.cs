using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapLocation.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public string TypeImage { get; set; }
    }
}
