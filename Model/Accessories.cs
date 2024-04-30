namespace Model
{
    public class Accessories : Product
    {
        public Accessories(int productId, string productName, decimal productPrice, string productDescription,
            string straps, string clothes, string gloves)
            : base(productId, productName, productPrice, productDescription)
        {
            Straps = straps;
            Clothes = clothes;
            Gloves = gloves;
        }

        public string Straps { get; set; }
        public string Clothes { get; set; }
        public string Gloves { get; set; }

        public override string ToString()
        {
            return $"Straps: {Straps}, Clothes: {Clothes}, Gloves: {Gloves}, {base.ToString()}";
        }
    }
}