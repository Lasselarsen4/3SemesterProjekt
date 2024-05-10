namespace Model
{
    public class Customer
    {
        public Customer(string firstName, string lastName, string email, string address, int phone, int customerId)
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
        public string Address { get; set; }
        public int Phone { get; set; }
        
    }
}