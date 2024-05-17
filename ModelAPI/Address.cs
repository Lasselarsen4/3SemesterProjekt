namespace ModelAPI
{
    public class Address
    {
        public Address(string streetName, int houseNumber, int zipcode, int customerId, int addressId)
        {
            AddressId = addressId;
            StreetName = streetName;
            HouseNumber = houseNumber;
            Zipcode = zipcode;
            CustomerId = customerId;
        }

        public Address()
        {
        }

        public int AddressId { get; set; }
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }
        public int Zipcode { get; set; }
        public int CustomerId { get; set; }
    }
}