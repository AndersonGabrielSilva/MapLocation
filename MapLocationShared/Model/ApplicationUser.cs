using Microsoft.AspNet.Identity.EntityFramework;

namespace MapLocationShared.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public string TypeImage { get; set; }
    }

}
