namespace ModelAPI
{
    public class Equipment : Product
    {
        public Equipment(int productId, string productName, decimal productPrice, string productDescription,
            string weights, string bench, string handles)
            : base(productId, productName, productPrice, productDescription)
        {
            Weights = weights;
            Bench = bench;
            Handles = handles;
        }

        public string Weights { get; set; }
        public string Bench { get; set; }
        public string Handles { get; set; }

        public override string ToString()
        {
            return $"Weights: {Weights}, Bench: {Bench}, Handles: {Handles}, {base.ToString()}";
        }
    }
}