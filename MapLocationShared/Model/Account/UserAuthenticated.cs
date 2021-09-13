using System;

namespace MapLocationShared.Model.Account
{
    public class UserAuthenticated
    {
        public string Token { get; set; }

        public DateTime Expire { get; set; }
        public string Message { get; set; }
    }
}
