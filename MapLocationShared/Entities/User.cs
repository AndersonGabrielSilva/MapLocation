using MapLocationShared.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MapLocationShared.Entities
{
    [Table("User")]
    public class User : Entity, IEntityMethods<User>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        public byte[] ProfilePicture { get; set; }

        public Guid AddressId { get; set; }
        public Address Address { get; set; }

        #region Methods
        public User Update(User entity)
        {
            this.Name = entity.Name;
            this.LastName = entity.LastName;

            this.ProfilePicture = entity.ProfilePicture;

            this.AddressId = entity.AddressId;
            return this;
        }

        public void UpdatePassword(string password)
        {
            this.Password = password;
        }
        #endregion  
    }
}
