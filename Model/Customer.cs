namespace Model
{
    public class Customer
    {
        public Customer(string name, string email, string address, int phone)
        {
            Name = name;
            Email = email;
            Address = address;
            Phone = phone;
        }
        
        public Customer()
        {
           
        }
        
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
    }
}