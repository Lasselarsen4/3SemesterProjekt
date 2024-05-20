namespace WebshopApplication.Models
{
    public class CheckoutViewModel
    {
        public int CartIndex { get; set; }
        // Add customer information fields
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}