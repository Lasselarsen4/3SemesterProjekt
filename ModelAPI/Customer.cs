namespace ModelAPI
{
    public class Customer
    {
        public Customer(string firstName, string lastName, string email, Address address, int phone, int customerId)
        {
            CustomerId = customerId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Address = address;
            Phone = phone;
        }
        
        public Customer()
        {
        }
        
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
        public int Phone { get; set; }
    }
}