namespace Model
{
    public class Equipment : Product
    {
        public Equipment(int productId, string productName, decimal productPrice, string productDescription,
            string weights, string benches, string handles)
            : base(productId, productName, productPrice, productDescription)
        {
            Weights = weights;
            Benches = benches;
            Handles = handles;
        }

        public string Weights { get; set; }
        public string Benches { get; set; }
        public string Handles { get; set; }

        public override string ToString()
        {
            return $"Weights: {Weights}, Benches: {Benches}, Handles: {Handles}, {base.ToString()}";
        }
    }
}