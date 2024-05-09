namespace Model
{
    public class Product
    {
        public Product(int productId, string productName, decimal productPrice, string productDescription)
        {
            ProductId = productId;
            ProductName = productName;
            ProductPrice = productPrice;
            ProductDescription = productDescription;
        }

        public Product()
        {
            throw new NotImplementedException();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductDescription { get; set; }

        public override string ToString()
        {
            return $"Id: {ProductId}, Name: {ProductName}, Price: {ProductPrice}, Description: {ProductDescription}";
        }
    }
}
