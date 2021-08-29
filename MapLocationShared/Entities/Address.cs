using MapLocationShared.Interfaces;

namespace MapLocationShared.Entities
{
    public class Address : Entity , IEntityMethods<Address>
    {
        #region Data
        public string Street { get; set; }
        public string Neighborhood { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        #endregion

        #region Methods
        public Address Update(Address model)
        {
            this.Street = model.Street;
            this.Neighborhood = model.Neighborhood;
            this.ZipCode = model.ZipCode;
            this.City = model.City;

            return this;
        }
        #endregion
    }
}
