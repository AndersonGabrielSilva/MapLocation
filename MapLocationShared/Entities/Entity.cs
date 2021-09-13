using Flunt.Notifications;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MapLocationShared.Entities
{
    public abstract class Entity : Notifiable
    {
        #region Constructor
        public Entity()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Data
        public Guid Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IncrementId { get; set; }

        public bool Inactive { get; set; }
        #endregion

        #region Methods
        public virtual bool Inactivate()
        {
            return Inactive = true;
        }

        public virtual bool Activate()
        {
            Inactive = false;

            return true;
        }
        #endregion

    }
}
