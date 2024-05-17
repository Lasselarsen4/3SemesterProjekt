namespace ModelAPI
{
    public class Customer
    {
        public Customer(string firstName, string lastName, string email, int phone, int customerId, string streetName, int houseNumber, int zipCode)
        {
            CustomerId = customerId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            StreetName = streetName;
            HouseNumber = houseNumber;
            ZipCode = zipCode;
        }
        
        public Customer()
        {
        }
        
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }
        public int ZipCode { get; set; }
        
        
    }
}